using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAi : MonoBehaviour
{
    private int dirNumber;
    private bool wait = false;
    private Rigidbody2D rb;
    [SerializeField]
    private float speed = .7f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(7, 8, true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (wait == false)
        {
            StartCoroutine(enemyChoice());
        }
    }

    IEnumerator enemyChoice()
    {
        wait = true;
        dirNumber = Random.Range(1, 8);

        switch (dirNumber)
        {
            case 1:
                rb.velocity = new Vector3(speed, 0, 0);
                break;
            case 2:
                break;
            case 3:
                rb.velocity = new Vector3(-speed, 0, 0);
                break;
            case 4:
                rb.velocity = new Vector3(0, -speed, 0);
                break;
            case 5:
                rb.velocity = new Vector3(speed, speed, 0);
                break;
            case 6:
                rb.velocity = new Vector3(-speed, -speed, 0);
                break;
            case 7:
                rb.velocity = new Vector3(-speed, speed, 0);
                break;
            default:
                rb.velocity = new Vector3(speed, -speed, 0);
                break;

        }

        yield return new WaitForSeconds(1.0f);
        wait = false;

    }
}
