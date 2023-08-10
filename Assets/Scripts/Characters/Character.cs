using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    protected Transform _hitBox; 
    [SerializeField]
    protected Stats _health;
    [SerializeField]
    protected float _initHealth; 
    [SerializeField]
    private string _typeStr;
    [SerializeField]
    private int _level;
    private Animator _animator;
    [SerializeField]
    private Chest _chestPrefab;

    private Vector3 _offset = new Vector3(2,3,0);
    private EnemyController _enemyController;
    private Player _player;

    protected bool _isAttacking = false;

    public bool IsAlive
    {
        get
        {
            return _health.MyCurrentValue > 0;
        }
    }
    public string Type => _typeStr;
    public Transform HitBox => _hitBox;
    public Stats MyHealth
    {
        get => _health;
        set => _health = value;
    }
    public int MyLevel
    {
        get
        {
            return _level;
        }
        set
        {
            _level = value;
        }
    }

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemyController = GetComponent<EnemyController>();
        _player = GetComponent<Player>();
    }
    protected virtual void Update()
    {
    }

    public virtual void TakeDamage(float damage)
    {
        _health.MyCurrentValue -= damage;
        if(this as Enemy)
        CombatTextManager.Instance.CreateText(transform.position+_offset, damage.ToString(), SCtype.Damage);

        if (this is Enemy && IsAlive)
        {
            _animator.SetTrigger("hit");
            SoundManager.Instance.PlaySound(_enemyController.HitSound);
            transform.GetComponent<NavMeshAgent>().SetDestination(Player.MyInstance.transform.position);
        }
        if (this is Player)
        {
            SoundManager.Instance.PlaySound(_player.HitSound);
        }

        if (_health.MyCurrentValue <= 0)
        {
            _animator.SetBool("isDead", true);
            transform.GetComponent<Collider>().enabled = false;
            if(this is Enemy)
            {
                if (!IsAlive)
                {
                    Player.MyInstance.MyTarget = null;
                    Player.MyInstance.GainXP(XpManager.CalculateExp(this as Enemy));
                    StartCoroutine(Death());
                }
            }
        }
    }

    public void GetHealth(int health)
    {
        _health.MyCurrentValue += health;
        if(_health.MyCurrentValue!=_health.MyMaxValue)
        CombatTextManager.Instance.CreateText(transform.position+_offset, health.ToString(),SCtype.Heal);
    }
    public IEnumerator Death()
    {
        var go = Instantiate(_chestPrefab);
        SoundManager.Instance.PlaySound(_enemyController.LootSound);
        SoundManager.Instance.PlaySound(_enemyController.DeathSound);
        go.transform.position = transform.position;
        _animator.GetComponent<Enemy>().OnCharacterRemoved();
        KillManager.Instance.OnKillConfirmed(this);
        yield return null;
    }
}
