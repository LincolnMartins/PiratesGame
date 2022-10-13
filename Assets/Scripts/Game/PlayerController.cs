using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Sprite[] shipStates;

    public float reloadTime;
    [HideInInspector] public float frontReloadTime;
    [HideInInspector] public float rightReloadTime;
    [HideInInspector] public float leftReloadTime;

    public float maxHealth;
    [HideInInspector] public float health;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    [HideInInspector] public List<GameObject> cannonBalls;
    public float playerDamage;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        frontReloadTime = 0;
        rightReloadTime = 0;
        leftReloadTime = 0;

        foreach (Transform child in transform) cannonBalls.Add(child.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (health > 0)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) transform.Rotate(new Vector3(0f, 0f, rotationSpeed * Time.smoothDeltaTime));
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) transform.Rotate(new Vector3(0f, 0f, -rotationSpeed * Time.smoothDeltaTime));

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) transform.position -= transform.up * moveSpeed * Time.smoothDeltaTime;

            if (Input.GetKey(KeyCode.Space) && frontReloadTime < 1)
            {
                foreach (var ball in cannonBalls)
                {
                    if (!ball.activeSelf)
                    {
                        ball.SetActive(true);
                        ball.transform.position = transform.position - (transform.up / 2.5f);
                        ball.transform.rotation = transform.rotation;
                        ball.GetComponent<CannonBall>().shootDirection = "Front";
                        frontReloadTime = reloadTime;
                        break;
                    }
                }
            }
            else if (frontReloadTime > 0) frontReloadTime -= Time.deltaTime;

            if (Input.GetKey(KeyCode.Q) && leftReloadTime < 1)
            {
                int ballsNumber = 0;
                GameObject firstBall = null;
                foreach (var ball in cannonBalls)
                {                    
                    if (!ball.activeSelf)
                    {
                        ball.SetActive(true);
                        if (ballsNumber < 1)
                        {
                            ball.transform.position = transform.position + (transform.right / 2);
                            firstBall = ball;
                        }
                        else if (ballsNumber < 2) ball.transform.position = firstBall.transform.position - (firstBall.transform.up / 2.5f);
                        else ball.transform.position = firstBall.transform.position + (ball.transform.up / 2.5f);
                        ball.transform.rotation = transform.rotation;
                        ball.GetComponent<CannonBall>().shootDirection = "Left";
                        ballsNumber++;
                        if (ballsNumber >= 3)
                        {
                            leftReloadTime = reloadTime;
                            break;
                        }
                        else continue;
                    }
                }
            }
            else if (leftReloadTime > 0) leftReloadTime -= Time.deltaTime;

            if (Input.GetKey(KeyCode.E) && rightReloadTime < 1)
            {
                int ballsNumber = 0;
                GameObject firstBall = null;
                foreach (var ball in cannonBalls)
                {
                    if (!ball.activeSelf)
                    {
                        ball.SetActive(true);
                        if (ballsNumber < 1)
                        {
                            ball.transform.position = transform.position - (transform.right / 2);
                            firstBall = ball;
                        }
                        else if (ballsNumber < 2) ball.transform.position = firstBall.transform.position - (ball.transform.up / 2.5f);
                        else ball.transform.position = firstBall.transform.position + (ball.transform.up / 2.5f);
                        ball.transform.rotation = transform.rotation;
                        ball.GetComponent<CannonBall>().shootDirection = "Right";
                        ballsNumber++;
                        if (ballsNumber >= 3)
                        {
                            rightReloadTime = reloadTime;
                            break;
                        }
                        else continue;
                    }
                }
            }
            else if (rightReloadTime > 0) rightReloadTime -= Time.deltaTime;
        }

        if (health >= maxHealth && GetComponent<SpriteRenderer>().sprite != shipStates[0]) GetComponent<SpriteRenderer>().sprite = shipStates[0];
        else if ((health <= maxHealth / 2 && health > maxHealth / 4) && GetComponent<SpriteRenderer>().sprite != shipStates[1]) GetComponent<SpriteRenderer>().sprite = shipStates[1];
        else if ((health <= maxHealth / 4 && health > 0) && GetComponent<SpriteRenderer>().sprite != shipStates[2]) GetComponent<SpriteRenderer>().sprite = shipStates[2];
        else if (health <= 0 && GetComponent<SpriteRenderer>().sprite != shipStates[3]) GetComponent<SpriteRenderer>().sprite = shipStates[3];

        GetComponentInChildren<HealthBar>().SetSize(health / maxHealth);
    }
}
