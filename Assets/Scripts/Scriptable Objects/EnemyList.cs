using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class EnemyList : ScriptableObject
{
    [SerializeField]
    private List<EnemyConfiguration> _enemyList;
    public IReadOnlyList<EnemyConfiguration> EnemyListCollection => _enemyList.AsReadOnly();
}
