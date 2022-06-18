using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public GameObject target { private set; get; }
    public int Damage { get; private set; }
    public void Instantiate(int damage)
    {
        Damage = damage;
    }

    public void Instantiate(GameObject target, int damage)
    {
        this.target = target;
        Damage = damage;
    }
}
