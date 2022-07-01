using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outletHighscore : MonoBehaviour
{
    public inBetweenValues ibv;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            ibv.highScore++;
            Destroy(this.gameObject);
            ibv.outletCaught = true;
        }
    }
}
