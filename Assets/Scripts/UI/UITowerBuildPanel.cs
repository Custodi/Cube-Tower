using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITowerBuildPanel : MonoBehaviour
{
    [SerializeField] private BuildSystem _buildSystem;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClicked(int tower_id)
    {
        _buildSystem.SelectTower(tower_id);
    }
}
