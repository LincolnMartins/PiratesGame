using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [HideInInspector] public Ship ship;

    public GameObject deathAnim;
    public float reloadTime;
    float reloadTimer;

    Vector3 PrevLocation;
    Vector3 Difference;

    [HideInInspector] public float health;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<NavMeshAgent>().updateRotation = false;
        GetComponent<NavMeshAgent>().updateUpAxis = false;
        health = ship.maxHealth;
        reloadTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Difference = transform.position - PrevLocation;
        PrevLocation = transform.position;

        transform.up = -Difference;

        GetComponent<NavMeshAgent>().SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);

        if (reloadTimer > 0) reloadTimer -= Time.deltaTime;

        if (ship.shipType == "Shooter")
        {
            if (GetComponent<NavMeshAgent>().remainingDistance <= 3)
            {
                if (reloadTimer < 1)
                {
                    reloadTimer = 0;
                    Shoot();
                }
            }
        }

        if (health >= ship.maxHealth && GetComponent<SpriteRenderer>().sprite != ship.shipStates[0]) GetComponent<SpriteRenderer>().sprite = ship.shipStates[0];
        else if ((health <= ship.maxHealth / 2 && health > ship.maxHealth / 4) && GetComponent<SpriteRenderer>().sprite != ship.shipStates[1]) GetComponent<SpriteRenderer>().sprite = ship.shipStates[1];
        else if ((health <= ship.maxHealth / 4 && health > 0) && GetComponent<SpriteRenderer>().sprite != ship.shipStates[2]) GetComponent<SpriteRenderer>().sprite = ship.shipStates[2];
        else if (health <= 0 && GetComponent<SpriteRenderer>().sprite != ship.shipStates[3])
        {
            GetComponent<SpriteRenderer>().sprite = ship.shipStates[3];
            StartCoroutine(RemoveShip());
        }

        GetComponentInChildren<HealthBar>().SetSize(health / ship.maxHealth);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ship.shipType == "Chaser" && collision.gameObject.tag != gameObject.tag && collision.gameObject.tag != "Untagged" && health > 0)
        {
            deathAnim.SetActive(true);
            deathAnim.GetComponent<Animator>().SetTrigger("Death");
            StartCoroutine(DestroyShip());

            collision.gameObject.GetComponent<PlayerController>().health -= ship.ramDamage;
        }
    }

    IEnumerator DestroyShip()
    {
        yield return new WaitForSeconds(0.20f);
        deathAnim.SetActive(false);
        GetComponent<NavMeshAgent>().isStopped = true;
        health = 0;
    }

    IEnumerator RemoveShip()
    {
        GameController.gameManager.playerScore++;
        yield return new WaitForSeconds(GameController.gameManager.enemySpawnTime);
        GetComponent<NavMeshAgent>().isStopped = false;
        GameController.gameManager.enemyShips.Remove(gameObject);
        Destroy(gameObject);
    }

    void Shoot()
    {
        GameObject ball;
        foreach (Transform child in transform)
        {
            if (child.gameObject.name == "cannonBall")
            {
                ball = child.gameObject;
                ball.SetActive(true);
                Vector3 moveDir = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized;
                ball.GetComponent<CannonBall>().moveDirection = moveDir;
                ball.transform.position = transform.position + (moveDir / 2.5f);
                ball.transform.rotation = transform.rotation;
                reloadTimer = reloadTime;
            }
        }
    }
}
