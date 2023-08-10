using System.Collections;
using UnityEngine;

public class MeleeAttackComponent : MonoBehaviour // компонент для двух видов атак
{
    [SerializeField]
    private LayerMask _isEnemy;
    [SerializeField]
    private MeleeAttack _meleeAttack;
    [SerializeField]
    private SwordAttack _sword;

    private UIBarManager _spellManager;
    private Player _player;

    private float _distance;
    private bool _isCd;
    private bool isStarted;

    private void Awake()
    {
        _sword.gameObject.GetComponent<Collider>().enabled = false;
        _spellManager = FindObjectOfType<UIBarManager>();
        _meleeAttack = GetComponent<MeleeAttack>();
        _player = GetComponent<Player>();

    }
    public void Attacking()
    {
        if(Input.GetMouseButtonDown(0)&&!_isCd) // ближний бой
        {
            Ray agentRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(agentRay, out hitInfo, 100, _isEnemy))
            {
                if (!isStarted)
                {
                    StartCoroutine(MeleeAttackCooldown());
                }
            }
        }
    }

    public IEnumerator MeleeAttackCooldown() // кд атаки
    {
        isStarted = true;
            while (_player.MyTarget != null)
            {
            _distance = Vector3.Distance(transform.position, Player.MyInstance.MyTarget.position);

                if (_distance <= _meleeAttack.AttackRange)
                {
                    _player.GetAgent.speed = 0;
                    _isCd = true;
                    transform.LookAt(_player.MyTarget);
                    _sword.gameObject.GetComponent<Collider>().enabled = true;
                    transform.GetComponent<Animator>().Play("MeleeAttack_OneHanded");
                    yield return new WaitForSeconds(0.15f);
                    _sword.gameObject.GetComponent<Collider>().enabled = false;
                StartCoroutine(_spellManager.Progress(0, _meleeAttack.SwordCooldown)); // для отрисовки кд на ui
                yield return new WaitForSeconds(_meleeAttack.SwordCooldown);
                }
            _player.GetAgent.speed = 8;
            _isCd = false;
            yield return null;
            }
        isStarted = false;
        yield return null;
    }
}