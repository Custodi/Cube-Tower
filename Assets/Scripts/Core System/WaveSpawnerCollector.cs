using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawnerCollector : MonoBehaviour
{
    [SerializeField]
    private EnemySpawner[] _enemySpawners;

    public void OnOfferClicked()
    {
        foreach(var item in _enemySpawners)
        {
            Debug.Log(_enemySpawners.Length);
            item.OnOfferClicked();
        }
    }
}
