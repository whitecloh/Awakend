using UnityEngine;

public class MeleeAttack : MonoBehaviour // характеристики ближней атаки(можно наследовать мечи)
{

    [SerializeField]
    private Character character;
    [SerializeField]
    private float _damage;
    [SerializeField]
    private float _cooldown;
    [SerializeField]
    private float _attackRange;

    public Character Character
    {
        get
        {
            return character;
        }
        set
        {
            character = value;
        }
    }
    public float MyDamage
    {
        get
        {
            return _damage;
        }
        set
        {
            _damage = value;
        }
    }
    public float SwordCooldown => _cooldown;
    public float AttackRange => _attackRange;



}
