using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public interface IEnemyEffectable : IEffectable
{
    public enum EnemyEffect { Frost, Poison, Artillery };
    public EnemyEffect GetEffect();
    public void Init(Enemy enemy);
    public float UpdateEffect(float time);
    public void ResetDuration();
}
