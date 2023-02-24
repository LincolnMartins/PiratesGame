using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CannonBall : MonoBehaviour
{
    public float shootSpeed;
    public float shootRange;
    
    [HideInInspector] public string shootDirection;

    Vector3 initialPos;
    bool hit = false;
    [HideInInspector] public Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.gameObject.tag.Equals("Enemy"))
        {
            transform.position += moveDirection * shootSpeed * Time.smoothDeltaTime;
            if (Vector2.Distance(initialPos, transform.position) >= shootRange && !hit) gameObject.SetActive(false);
        }
        else
        {
            if (Vector2.Distance(initialPos, transform.position) < shootRange && !hit)
            {
                switch (shootDirection)
                {
                    case "Front": transform.position -= transform.up * shootSpeed * Time.smoothDeltaTime; break;
                    case "Right": transform.position -= transform.right * shootSpeed * Time.smoothDeltaTime; break;
                    case "Left": transform.position += transform.right * shootSpeed * Time.smoothDeltaTime; break;
                }
            }
            else if (!hit) gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.parent.gameObject != collision.gameObject)
        {
            if (!transform.parent.gameObject.tag.Equals(collision.gameObject.tag))
            {
                if (collision.gameObject.tag.Equals("Player") && collision.gameObject.GetComponent<PlayerController>().health > 0)
                {
                    collision.gameObject.GetComponent<PlayerController>().health -= transform.parent.gameObject.GetComponent<EnemyController>().ship.shootDamage;
                    GetComponent<Animator>().SetTrigger("Hit");
                    StartCoroutine(DestroyBall());
                }
                else if (collision.gameObject.tag.Equals("Enemy") && collision.gameObject.GetComponent<EnemyController>().health > 0)
                {
                    collision.gameObject.GetComponent<EnemyController>().health -= transform.parent.gameObject.GetComponent<PlayerController>().playerDamage;

                    GetComponent<Animator>().SetTrigger("Hit");
                    StartCoroutine(DestroyBall());
                }
            }
        }
    }

    IEnumerator DestroyBall()
    {
        hit = true;
        yield return new WaitForSeconds(0.15f);
        hit = false;
        gameObject.SetActive(false);
    }
}
