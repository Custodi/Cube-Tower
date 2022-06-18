using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : Waypoint
{
    public delegate void EnemyHitEvent(DamageDealer damageDealer);
    public EnemyHitEvent OnEnemyHit;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Enemy component))
        {
            OnEnemyHit?.Invoke(component.GetComponent<DamageDealer>());
            Destroy(other.gameObject);
        }
    }

}
