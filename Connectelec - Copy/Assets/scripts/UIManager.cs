using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject startBarrier;

    public inBetweenValues ibv;

    public TMP_Text lives;
    public TMP_Text highScore;

    // Update is called once per frame
    void Start()
    {
        startBarrier.SetActive(true);
    }

    void Update()
    {
        if(ibv.lives < 0)
            ibv.lives = 0;

        if(ibv.lives <= 0)
            startBarrier.SetActive(true);

        lives.text = ibv.lives + "";
        highScore.text = ibv.highScore + "";
    }

    public void OnClick()
    {
        startBarrier.SetActive(false);

        Debug.Log("It should be disabled");
    }
}
