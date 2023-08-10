using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private Transform _detector;
    [SerializeField]
    private LayerMask _layerMask;
    [SerializeField, Range(0, 100)]
    private float _distance;

    [SerializeField]
    private SwordAttack _sword;

    [SerializeField]
    private AudioClip _attackSound;
    [SerializeField]
    private AudioClip _hitSound;
    [SerializeField]
    private AudioClip _deathSound;
    [SerializeField]
    private AudioClip _lootSound;

    public AudioClip AttackSound => _attackSound;
    public AudioClip HitSound => _hitSound;
    public AudioClip DeathSound => _deathSound;
    public AudioClip LootSound => _lootSound;

    public Collider[] _targets = new Collider[10];
    public List<Transform> _points = new List<Transform>();

    private Animator _animator;
    private NavMeshAgent _agent;
    [SerializeField]
    private float _stoppingDistance = 2.1f;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        if(_sword != null)
        _sword.gameObject.GetComponent<Collider>().enabled = false;

    }
    private void FixedUpdate()
    {
        if (_detector != null)
        {
            Detect();
            SwordEnabled();
        }
    }

    public void Detect()
    {
        int numColliders = Physics.OverlapSphereNonAlloc(_detector.position, _distance, _targets, _layerMask);
        if (numColliders == 0) return;
        float distance;
        for (int i = 0; i < numColliders; i++)
        {
            distance = Vector3.Distance(_agent.transform.position, _targets[i].ClosestPoint(transform.position));
            _agent.SetDestination(Player.MyInstance.transform.position);
            if (distance <= _stoppingDistance)
            {
                _animator.SetBool("isAttacking", true);
                _animator.SetBool("isPatrolling", false);
            }
            else
            {
                _animator.SetBool("isPatrolling", true);
                _animator.SetBool("isAttacking", false);
            }
            if (distance > _distance)
            {
                _distance = 9f;
            }
            else
            {
                _distance = 20f;
            }
        }
    }
    private void SwordEnabled()
    {
        if (_animator.GetBool("isAttacking")&&!_animator.GetBool("isPatrolling"))
        {
            transform.LookAt(Player.MyInstance.transform);
            _sword.gameObject.GetComponent<Collider>().enabled = true;
        }
        else
        {
            _sword.gameObject.GetComponent<Collider>().enabled = false;
        }
    }
}
