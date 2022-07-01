using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public AudioSource audioSource;//maybe for buzz when you get hit
    public AudioClip clip;
    public float volume = 0.5f;
    //audioSource.PlayOneShot(clip, volume);

    Rigidbody2D body;

    float horizontal;
    float vertical;

    public string playerSelect;

    public float runSpeed = 10.0f;

    public inBetweenValues ibv;

    Vector3 position;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        Physics2D.IgnoreLayerCollision(3, 6, true);
    }

    void Update()
    {
        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down

        if (ibv.lives <= 0)
            Destroy(this.gameObject);
    }

    void FixedUpdate()
    {
        if(playerSelect == "p1")
        {
            if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
            {
                if (horizontal > 0)
                    transform.localScale = new Vector3(1, 1, 1);
                else if (horizontal < 0)
                    transform.localScale = new Vector3(-1, 1, 1);
            }
            else
                body.velocity = Vector3.zero;

            if (Input.GetKey("w"))
            {
                transform.Translate(Vector3.up * 1 * runSpeed * Time.deltaTime);
            }

            if (Input.GetKey("a"))
            {
                transform.Translate(Vector3.right * -1 * runSpeed * Time.deltaTime);
            }

            if (Input.GetKey("s"))
            {
                transform.Translate(Vector3.up * -1 * runSpeed * Time.deltaTime);
            }

            if (Input.GetKey("d"))
            {
                transform.Translate(Vector3.right * 1 * runSpeed * Time.deltaTime);
            }

        }
        else if(playerSelect == "p2")
        {
            if (Input.GetKey("up") || Input.GetKey("left") || Input.GetKey("down") || Input.GetKey("right"))
            {
                if (horizontal > 0)
                    transform.localScale = new Vector3(1, 1, 1);
                else if (horizontal < 0)
                    transform.localScale = new Vector3(-1, 1, 1);
            }
            else 
                body.velocity = Vector3.zero;

            if (Input.GetKey("up"))
            {
                transform.Translate(Vector3.up * 1 * runSpeed * Time.deltaTime);
            }

            if (Input.GetKey("left"))
            {
                transform.Translate(Vector3.right * -1 * runSpeed * Time.deltaTime);
            }

            if (Input.GetKey("down"))
            {
                transform.Translate(Vector3.up * -1 * runSpeed * Time.deltaTime);
            }

            if (Input.GetKey("right"))
            {
                transform.Translate(Vector3.right * 1 * runSpeed * Time.deltaTime);
            }

        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "enemy")
        {
            ibv.lives--;
            StartCoroutine(tempDisable(coll.gameObject));
        }
    }

    IEnumerator tempDisable(GameObject coll)
    {
        coll.SetActive(false);

        yield return new WaitForSeconds(2f);

        coll.SetActive(true);
    }
}
