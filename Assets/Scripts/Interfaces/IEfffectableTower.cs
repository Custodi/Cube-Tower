using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public interface IEffectableTower : IEffectable
{
    public enum TowerEffect { Speed = 0, Attack = 1, Radius = 2 };
    public TowerEffect GetEffect();
    public void Init(ShootingTower tower);
}
