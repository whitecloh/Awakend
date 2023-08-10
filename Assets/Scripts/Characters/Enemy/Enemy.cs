using UnityEngine;


public delegate void CharacterRemoved();

public class Enemy : Character
{

    [SerializeField]
    private CanvasGroup _heathGroup;

    public event CharacterRemoved _characterRemoved;

    protected override void Awake()
    {
        _health.Initialize(_initHealth, _initHealth);
        base.Awake();
    }
    public Transform Select() // подсветка хп элемента
    {
        _heathGroup.alpha = 1;
        return _hitBox;
    }
    public void DeSelect() //затухание хп элемента
    {
        _heathGroup.alpha = 0;
    }

    public void OnCharacterRemoved()
    {
        if (_characterRemoved != null)
        {
            _characterRemoved();
        }
        Destroy(gameObject);
    }
}
