using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class obstacleManagementScript : MonoBehaviour
{
    public GameObject[] barriersVertical;
    public GameObject[] barriersHorizontal;

    public Transform[] squareSpaces;
    public GameObject goal;

    public GameObject enemy;
    public Transform[] enemySpots;

    public GameObject player1;
    public GameObject player2;
    public Transform playerSpot1;
    public Transform playerSpot2;

    GameObject spawnedPlayer1;
    GameObject spawnedPlayer2;
    GameObject spawnedGoal;
    GameObject spawnedEnemy1;
    GameObject spawnedEnemy2;
    GameObject spawnedEnemy3;
    GameObject spawnedEnemy4;

    float playerDistance;
    public float speed;

    public inBetweenValues ibv;

    public GameObject[] spawns;

    int counter;

    bool coolDown;

    public LineRenderer lineRend;

    void Start()
    {
        ibv.outletCaught = false;
        ibv.highScore = 0;
        ibv.lives = 3;
        ibv.hit = false;

        coolDown = false;

        for(int i = 0; i < barriersVertical.Length; i++)
        {
            barriersVertical[i].SetActive(false);
        }
        for (int i = 0; i < barriersHorizontal.Length; i++)
        {
            barriersHorizontal[i].SetActive(false);
        }
    }

    public void OnStartRound(bool button)
    {
        if(button)
        {
            ibv.outletCaught = false;
            ibv.highScore = 0;
            ibv.lives = 3;
        }

        for (int i = 0; i < barriersVertical.Length; i++)
        {
            barriersVertical[i].SetActive(false);
        }
        for (int i = 0; i < barriersHorizontal.Length; i++)
        {
            barriersHorizontal[i].SetActive(false);
        }

        Destroy(spawnedGoal);
        Destroy(spawnedEnemy1);
        Destroy(spawnedEnemy2);
        Destroy(spawnedEnemy3);
        Destroy(spawnedEnemy4);
        if(ibv.hit == false)
        {
            Destroy(spawnedPlayer1);
            Destroy(spawnedPlayer2);
        }

        spawnedPlayer1 = Instantiate(player1, playerSpot1);
        spawnedPlayer2 = Instantiate(player2, playerSpot2);

        var rnd = new System.Random();
        var squareSpawnNumbers = Enumerable.Range(0, 57).OrderBy(x => rnd.Next()).Take(1).ToList();

        spawnedGoal = Instantiate(goal, squareSpaces[(int)squareSpawnNumbers[0]]); 


        spawnedEnemy1 = Instantiate(enemy, enemySpots[0]);
        spawnedEnemy2 = Instantiate(enemy, enemySpots[1]);
        spawnedEnemy3 = Instantiate(enemy, enemySpots[2]);
        spawnedEnemy4 = Instantiate(enemy, enemySpots[3]);

        barrierSpawns();
    }

    void Update()
    {
        Vector3 p2Direction = spawnedPlayer2.transform.position - spawnedPlayer1.transform.position;
        p2Direction.Normalize();
        Vector3 p1Direction = spawnedPlayer1.transform.position - spawnedPlayer2.transform.position;
        p1Direction.Normalize();

        playerDistance = Vector3.Distance(spawnedPlayer1.transform.position, spawnedPlayer2.transform.position);

        if(playerDistance > 4)
        {
            spawnedPlayer1.transform.position += p2Direction * speed * Time.deltaTime;
            spawnedPlayer2.transform.position += p1Direction * speed * Time.deltaTime;
        }

        Transform first = spawnedPlayer1.transform.GetChild(0);
        Transform second = spawnedPlayer2.transform.GetChild(0);

        //DrawLineBetweenObjects(first, second);
        lineRend.SetPosition(0, first.position);
        lineRend.SetPosition(1, second.position);
        lineRend.sortingOrder = 1;
        lineRend.material = new Material(Shader.Find("Sprites/Default"));
        lineRend.material.color = Color.red;
        //

        if (coolDown == false)
            StartCoroutine(staggerBarriers());

        if (ibv.outletCaught)
        {
            OnStartRound(false);
            ibv.outletCaught = false;
        }

        if (ibv.hit)
        {
            OnStartRound(false);
            ibv.hit = false;
        }
    }

    void barrierSpawns()
    {
        for (int i = 0; i < barriersVertical.Length; i++)
        {
            barriersVertical[i].SetActive(false);
        }
        for (int i = 0; i < barriersHorizontal.Length; i++)
        {
            barriersHorizontal[i].SetActive(false);
        }

        var rnd = new System.Random();
        var barrierSpawnNumbersHorizontal = Enumerable.Range(0, 55).OrderBy(x => rnd.Next()).Take(10).ToList();
        var barrierSpawnNumbersVertical = Enumerable.Range(0, 55).OrderBy(x => rnd.Next()).Take(10).ToList();

        foreach (var number in barrierSpawnNumbersHorizontal)
        {
            barriersVertical[number].SetActive(true);
        }

        foreach (var number2 in barrierSpawnNumbersVertical)
        {
            barriersHorizontal[number2].SetActive(true);
        }
    }

    IEnumerator staggerBarriers()
    {
        coolDown = true;
 
        yield return new WaitForSeconds(2f);
        barrierSpawns();
        
        coolDown = false;
    }

    void DrawLineBetweenObjects(Transform firstT, Transform secondT)
    {
        // Set the positions of the LineRenderer
        lineRend.SetPosition(0, firstT.position);
        lineRend.SetPosition(1, secondT.position);
        lineRend.sortingOrder = 1;
        lineRend.material = new Material(Shader.Find("Sprites/Default"));
        lineRend.material.color = Color.blue;
    }
}
