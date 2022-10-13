using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class GameController : MonoBehaviour
{
    public static GameController gameManager;

    public int gameSessionTime;
    public int enemySpawnTime;

    public GameObject shipPrefab;
    public GameObject[] spawnPoints;    
    public Ship[] enemies;
    [HideInInspector] public List<GameObject> enemyShips;

    [HideInInspector] public int playerScore;
    [HideInInspector] public bool victory;
    [HideInInspector] public int minutes;
    [HideInInspector] public float seconds;
    [HideInInspector] public float spawnTimer;

    private TextMeshProUGUI timer;
    private TextMeshProUGUI score;

    private void Awake()
    {
        if (gameManager != null)
        {
            if (gameManager != this)
            {
                Destroy(gameManager.gameObject);
                gameManager = this;
            }
        }
        else gameManager = this;

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerScore = 0;
        spawnTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer != null)
        {
            timer.text = $"{minutes.ToString("00")}:{Mathf.FloorToInt(seconds).ToString("00")}";

            if (minutes <= 0 && seconds < 1) 
            {
                victory = true;
            }
            else if (seconds > 0)
            {
                seconds -= Time.deltaTime;
            }
            else if (minutes > 0)
            {
                minutes--;
                seconds = 60;
            }

            if (spawnTimer < 1)
            {
                foreach (var enemy in enemyShips)
                {
                    if (!enemy.activeSelf)
                    {
                        enemy.SetActive(true);
                        var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                        enemy.transform.position = spawnPoint.transform.position;
                        enemy.transform.rotation = spawnPoint.transform.rotation;
                        spawnTimer = enemySpawnTime;
                        break;
                    }
                }
            }
            else spawnTimer -= Time.deltaTime;
        }

        if (score != null) score.text = playerScore.ToString();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            InstantiateEnemyShips();

            playerScore = 0;
            minutes = gameSessionTime;
            seconds = 0;
            timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<TextMeshProUGUI>();
            timer.text = $"{minutes.ToString("00")}:{Mathf.FloorToInt(seconds).ToString("00")}";
            score = GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>();
            spawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
            spawnTimer = 0;
        }
    }

    void OnSceneUnloaded(Scene current)
    {
        if (current.name == "Game")
        {
            timer = null;
            score = null;
            spawnPoints = null;
            foreach (var enemy in enemyShips) Destroy(enemy);
            enemyShips.Clear();
        }
    }

    public void InstantiateEnemyShips()
    {
        for (int i = 0; i < 12; i++)
        {
            var ship = Instantiate(shipPrefab, Vector2.zero, Quaternion.identity);
            ship.GetComponent<EnemyController>().ship = enemies[Random.Range(0, enemies.Length)];
            if (ship.GetComponent<EnemyController>().ship.shipType == "Shooter") ship.GetComponent<NavMeshAgent>().stoppingDistance = 3;
            ship.SetActive(false);
            enemyShips.Add(ship);
        }
    }
}
