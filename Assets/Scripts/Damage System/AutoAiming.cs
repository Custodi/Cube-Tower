using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAiming : MonoBehaviour
{
    [SerializeField]
    private ShootingTower _shootingTowerScript;

    private GameObject _target;

    public GameObject target { get => _target; set => _target = value; }
    // Update is called once per frame
    void Update()
    {
        if(_shootingTowerScript?.enemiesInRange.Count > 0)
        {
            _target = _shootingTowerScript.enemiesInRange.First?.Value;
            transform.LookAt(_target?.transform, Vector3.up);
        }
        
    }
}
