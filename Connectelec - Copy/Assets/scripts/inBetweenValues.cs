using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "inBetweenValues", menuName = "player values")]
public class inBetweenValues : ScriptableObject
{
    public float highScore;
    public int lives;
    public bool outletCaught;
    public bool hit;
}