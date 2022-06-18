using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Projectile))]
public class FrostEffect : EnemyEffect
{
    [Range(0f, 1f)]
    [SerializeField]
    private float _freezePower;

    [SerializeField]
    private float _duration;

    private float _initialSpeed;

    public override void Start()
    {
        GetComponent<Projectile>().effectList.Add(this);
        effectType = IEnemyEffectable.EnemyEffect.Frost;
    }
    public override void Init(Enemy enemy)
    {
        inititalDuration = _duration;
        base.Init(enemy);
        _freezePower = 1 - _freezePower;
        //Debug.Log("Freeze power: " + _freezePower);
    }

    public override void ApplyEffect()
    {
        //Debug.Log("Before frezzeing speed: " + enemy.speed);
        _initialSpeed = enemy.speed;
        enemy.speed *=  _freezePower;
        //Debug.Log("After frezzeing speed: " + enemy.speed);
        //enemy.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public override void CancelEffect()
    {
        //Debug.Log(2);
        enemy.speed = _initialSpeed;
        //enemy.gameObject.transform.localScale = new Vector3(2f, 2f, 2f);
    }

    public override void ResetDuration()
    {
        //Debug.Log("reset duration was called " + currentDuration + " | " + inititalDuration);
        currentDuration = inititalDuration;
    }
}
