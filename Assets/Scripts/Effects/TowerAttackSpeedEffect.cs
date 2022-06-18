using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttackSpeedEffect : TowerEffect, IEffectableTower
{
    [Tooltip("Max value doubles attack speed")]
    [Range(0f, 1f)]
    [SerializeField]
    private float _attackSpeedBonus;

    private float _initialAttackSpeed;

    public override void Start()
    {
        effectType = IEffectableTower.TowerEffect.Speed;
    }
    public override void Init(ShootingTower tower)
    {
        base.Init(tower);
    }

    public override void ApplyEffect()
    {
        //Debug.Log("Effect was called");
        //tower.AddEffect(this);
        _initialAttackSpeed = tower.fireRate;
        tower.fireRate = tower.fireRate * (1 + _attackSpeedBonus);
    }

    public override void CancelEffect()
    {
        tower.RemoveEffect(this);
        tower.fireRate = _initialAttackSpeed;
    }
}
