using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Player : Character // игрок
    {
    [SerializeField]
    private GameObject[] _spellPrefabs; // для возможности замены файрболла
    [SerializeField]
    private Stats _mana;
    [SerializeField]
    private Stats _xpStat;
    [SerializeField]
    private Text _levelText;
    [SerializeField]
    private float _initMana;

    [SerializeField]
    private MoveComponent _moveComponent;
    [SerializeField]
    private MeleeAttackComponent _attackComponent;
    [SerializeField]
    private MeleeAttack _meleeAttack;
    [SerializeField]
    private NavMeshAgent _agent;

    private static Player instance;
    public static Player MyInstance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<Player>();
            return instance;
        }
    }

    [SerializeField]
    private AudioClip healSound;
    [SerializeField]
    private AudioClip hitSound;
    [SerializeField]
    private AudioClip evasion;
    [SerializeField]
    private AudioClip catMeow;

    public NavMeshAgent GetAgent => _agent;
    public AudioClip HealSound => healSound;
    public AudioClip HitSound => hitSound;
    public AudioClip Evasion => evasion;
    public AudioClip CatMeow => catMeow;
    public Transform MyTarget { get; set; }
    public Stats MyXp { get => _xpStat; set => _xpStat = value; }
    public Stats MyMana { get => _mana; set => _mana = value; }

    private int _strength=20;
    private int _stamina=100;
    private int _intellect=20;

    private int _intellectMultiplier = 5;
    private int _strengthMultiplier = 3;

    protected override void Awake()
    {
        ResetStats();
        StartCoroutine(Regen());
        base.Awake();
    }
    protected override void Update()
    {
        _moveComponent.Moving();
        _attackComponent.Attacking();
        base.Update();
    }

    public void SetDefaultsValues()
    {       
        _xpStat.Initialize(0, Mathf.Floor(100 * MyLevel * Mathf.Pow(MyLevel, 0.5f)));
        _levelText.text = MyLevel.ToString();
        
        _stamina = 100;
        _strength = 20;
        _intellect = 20;
        ResetStats();
        UIBarManager.MyInstance.UpdateStatsText(_strength, _stamina, _intellect);
    }
    public void ResetStats()
    {
        _health.Initialize(_stamina*StaminaMultiplier(), _stamina*StaminaMultiplier());
        _mana.Initialize(_intellect* _intellectMultiplier, _intellect* _intellectMultiplier);
        _meleeAttack.MyDamage = _strength*_strengthMultiplier;
        UIBarManager.MyInstance.UpdateStatsText(_strength, _stamina, _intellect);
    }
    private void UpdateMaxStats()
    {
        MyHealth.SetMaxValue(_stamina * StaminaMultiplier());
        MyMana.SetMaxValue(_intellect* _intellectMultiplier);
        _meleeAttack.MyDamage = _strength * _strengthMultiplier;
    }
    public void GainXP(int xp)
    {
        _xpStat.MyCurrentValue += xp;
        CombatTextManager.Instance.CreateText(this.transform.position,xp.ToString(),SCtype.XP);

        if (_xpStat.MyCurrentValue >= _xpStat.MyMaxValue)
        {
            StartCoroutine(Ding());
        }
    }
    private IEnumerator Ding()
    {
        while(!_xpStat.IsFull)
        {
            yield return null;
        }

        MyLevel++;
        _levelText.text = MyLevel.ToString();
        _xpStat.MyMaxValue = Mathf.Floor(100 * MyLevel * Mathf.Pow(MyLevel, 0.5f));
        _xpStat.MyCurrentValue = _xpStat.Overflow;
        _xpStat.Reset();
        _stamina += IncresedBaseStat();
        _intellect += IncresedBaseStat();
        _strength += IncresedBaseStat();
        ResetStats();
        if (_xpStat.MyCurrentValue >= _xpStat.MyMaxValue)
        {
            StartCoroutine(Ding());
        }
    }
    public void UpdateLevel()
    {
        _levelText.text = MyLevel.ToString(); 
    }

    public void EquipGear(Armor armor)
    {
        _stamina += armor.Stamina;
        _intellect += armor.Intellect;
        _strength += armor.Strength;
        UpdateMaxStats();
        UIBarManager.MyInstance.UpdateStatsText(_strength, _stamina, _intellect);
    }
    public void DequipGear(Armor armor)
    {
        _stamina -= armor.Stamina;
        _intellect -= armor.Intellect;
        _strength -= armor.Strength;
        UpdateMaxStats();
        UIBarManager.MyInstance.UpdateStatsText(_strength, _stamina, _intellect);
    }

    private int StaminaMultiplier()
    {
        if (MyLevel < 10)
        {
            return 2;
        }
        else if (MyLevel > 10)
        {
            return 3;
        }
        return 4;
    }

    private int IncresedBaseStat()
    {
        if (MyLevel < 10)
        {
            return 5;
        }
        return 2;
    }

    private IEnumerator Regen()
    {
        yield return new WaitForSeconds(0.1f);
        GetAgent.enabled = true;
        while (true)
        {
                if (_health.MyCurrentValue < _health.MyMaxValue)
                {
                    int value = Mathf.FloorToInt(_health.MyMaxValue * 0.05f);
                    _health.MyCurrentValue += value;
                }

                yield return new WaitForSeconds(0.5f);

                if (_mana.MyCurrentValue < _mana.MyMaxValue)
                {
                    int value = Mathf.FloorToInt(_mana.MyMaxValue * 0.05f);
                    _mana.MyCurrentValue += value;
                }
            yield return new WaitForSeconds(1.5f);
        }
    }
}
