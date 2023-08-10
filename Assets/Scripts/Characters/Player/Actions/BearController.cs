using UnityEngine;
using UnityEngine.AI;

public class BearController : MonoBehaviour
{
    [SerializeField]
    private Transform _detector;
    [SerializeField]
    private LayerMask _layerMask;
    [SerializeField, Range(0, 100)]
    private float _distance;
    [SerializeField]
    private SwordAttack _sword;

    private Animator _animator;
    private NavMeshAgent _agent;

    public Collider[] _targets = new Collider[10];

    private float _stoppingDistance = 5f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();

    }
    private void FixedUpdate()
    {
        Detect();
    }

    public void Detect()
    {
        int numColliders = Physics.OverlapSphereNonAlloc(_detector.position, _distance, _targets, _layerMask);
        if (numColliders == 0) return;
        float distance;
        for (int i = 0; i < numColliders; i++)
        {
            distance = Vector3.Distance(_agent.transform.position, _targets[i].ClosestPoint(transform.position));

            if (_targets[i].GetComponent<Enemy>()!=null && _targets[i].GetComponent<Enemy>().IsAlive)
            {
                _agent.SetDestination(_targets[i].transform.position);
            }
            if (distance <= _stoppingDistance)
            {
                _animator.SetBool("isAttacking", true);
            }
            else
            {
                _animator.SetBool("isAttacking", false);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_detector.position, _distance);
    }
}
