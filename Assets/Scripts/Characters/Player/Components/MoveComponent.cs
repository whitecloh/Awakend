using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class MoveComponent : MonoBehaviour
{
    [SerializeField]
    private LayerMask _isGround;
    [SerializeField]
    private LayerMask _isEnemy;
    [SerializeField]
    private AudioClip movement;

    private Transform _targetPositionEnemy;
    private Vector3 _targetPositionGround;

    private Player _player;
    private Animator _animator;

    private float _distance;
    private int _touchCount;
    private float _timer;

    private bool _isMoving = false;
    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            _animator.SetBool("isMoving", value);
        }
    }

    private void Awake()
    {
        _timer = 0;
        _distance = 0f;
        _touchCount = 0;
        _player = GetComponent<Player>();
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        DistanceDetection();
        _timer += Time.deltaTime;
    }

    private void DistanceDetection()
    {
        if (!_player.GetAgent.enabled) return;
        if (_targetPositionEnemy != null)
        {
            _distance = Vector3.Distance(_targetPositionEnemy.position , _player.transform.position);
            _player.GetAgent.SetDestination(_targetPositionEnemy.position + Vector3.forward * 1);
            _targetPositionGround = transform.position;
        }
        else if(_targetPositionGround!=null)
        {
            _player.GetAgent.SetDestination(_targetPositionGround);
            _distance = Vector3.Distance(_targetPositionGround, _player.transform.position);
        }

        if (_distance <= 0.5f || _touchCount == 0)
        {
            IsMoving = false;
        }
        else
        {
            IsMoving = true;
            if (_timer>=0.3f)
            {
                SoundManager.Instance.PlaySound(movement);
                _timer = 0;
            }   
        }
    }

    public void Moving()
    {
        if (Input.GetMouseButtonDown(0)&&!EventSystem.current.IsPointerOverGameObject())
        {
            _touchCount++;
            Ray agentRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(agentRay, out hitInfo, 100, _isEnemy))
            {
                _targetPositionEnemy = hitInfo.transform;
                return;
            }
                if (Physics.Raycast(agentRay, out hitInfo, 100, _isGround))
            {
                _player.GetAgent.SetDestination(hitInfo.point);
                _targetPositionGround = hitInfo.point;
                _targetPositionEnemy = null;

            }
        }
    }
}
