using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EnemyConfiguration
{
    public string name;
    public int health;
    public float speed;
    public GameObject enemyPrefab;
    public int reward;
    public int damage;
}
