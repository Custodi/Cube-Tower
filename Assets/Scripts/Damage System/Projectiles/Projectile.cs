using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamageDealer))]
public class Projectile : MonoBehaviour
{
    private List<EnemyEffect> _effectsList;
    public List<EnemyEffect> effectList
    {
        get
        {
            return _effectsList;
        }
        private set
        {
            _effectsList = value;
        }
    }

    [SerializeField]
    protected int damage;

    [SerializeField]
    protected Vector3 fixedVector;

    public int Damage { get => damage; }

    protected float damageMultiplier;
    public float DamageMultiplier { get => damageMultiplier; }

    protected GameObject hitGoalEnemy;
    public GameObject targetEnemy { get => hitGoalEnemy; }

    private DamageDealer _damageDealer;
    public DamageDealer damageDealer { get => _damageDealer; protected set => _damageDealer = value; }

    protected float speed;
    protected Vector3 hitGoalPosition;

    private void Awake()
    {
        damageDealer = GetComponent<DamageDealer>();
        _effectsList = new List<EnemyEffect>();
        //_effectsList.AddRange(GetComponents<EnemyEffect>()); // Уже добавляет себя в самих эффектах
    }

    public virtual void Instantiate(float damageMultiplier, float speed, GameObject hitGoalEnemy)
    {
        this.speed = speed;
        this.hitGoalEnemy = hitGoalEnemy;
        this.damageMultiplier = damageMultiplier;
        damageDealer.Instantiate(this.hitGoalEnemy, Mathf.RoundToInt(damageMultiplier * damage)); // Пофиксить
    }

    private void Update()
    {
        if(hitGoalEnemy)
        {
            //Debug.Log(hitGoalEnemy.transform.position);
            hitGoalPosition = hitGoalEnemy.transform.position + fixedVector;
            transform.Translate((hitGoalPosition - transform.position).normalized * speed);
            //Debug.Log(hitGoalPosition + "________________------------------------____________________________--");
        }
        else
        {
            //Debug.Log("Destroyed cuz hitGoalEnemy");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Enemy _))
        {
            //Debug.Log("Destroyed cuz EnemyHit");
            Destroy(gameObject);
            //Debug.Log("Die cuz hit");
        }
    }
}
