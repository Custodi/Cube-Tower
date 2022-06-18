using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRadiusEffect : TowerEffect, IEffectableTower
{
    [SerializeField]
    private float _radiusBonus;

    private float _initialRadius;

    public override void Start()
    {
        effectType = IEffectableTower.TowerEffect.Radius;
    }
    public override void Init(ShootingTower tower)
    {
        base.Init(tower);
    }

    public override void ApplyEffect()
    {
        tower.AddEffect(this);
        _initialRadius = tower.towerRadius;
        tower.towerRadius = tower.towerRadius * (1 + _radiusBonus);
    }

    public override void CancelEffect()
    {
        tower.RemoveEffect(this);
        tower.fireRate = _initialRadius;
    }
}

