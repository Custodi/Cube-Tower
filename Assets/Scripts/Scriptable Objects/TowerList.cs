using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class TowerList : ScriptableObject
{
    [SerializeField]
    private List<GameObject> _towerList;
    public IReadOnlyList<GameObject> TowerListCollection => _towerList.AsReadOnly();
}