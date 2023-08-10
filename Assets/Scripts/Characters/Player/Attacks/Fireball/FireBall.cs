using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FireBall : MonoBehaviour //скрипт для префаба
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _lifeTime=1f;
    [SerializeField]
    private int _damage;
    [SerializeField]
    private int _manaCost;
    [SerializeField]
    private float _cooldownSpell;
    [SerializeField]
    private Image _castingBar;

    public int ManaCost => _manaCost;
    public float FireCooldown => _cooldownSpell;
    public int FireDamage => (int)(_damage*Player.MyInstance.MyMana.MyMaxValue/10);

    private Transform _target;

    private void Awake()
    {
        _target = Player.MyInstance.MyTarget;
        transform.LookAt(Player.MyInstance.MyTarget);
        StartCoroutine(DestroyFireBall());
    }

    private void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other) // коллизии с разными видами обьектов(но в целом только для енеми т.к. рейкаст с layer Enemy)
    {
        var enemy = other.GetComponent<Enemy>();
        if (enemy == null)
        {
            Destroy(gameObject);
            return;
        }
        enemy.TakeDamage(FireDamage);
        Destroy(gameObject);
    }

    private IEnumerator DestroyFireBall() // уничтожение обьекта через время
    {
        yield return new WaitForSeconds(_lifeTime);
        Destroy(gameObject);
    }
}
