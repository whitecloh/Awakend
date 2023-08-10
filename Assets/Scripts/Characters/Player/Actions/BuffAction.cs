using System.Collections;
using UnityEngine;

public class BuffAction : MonoBehaviour
{

    [SerializeField]
    private Player _player;
    [SerializeField]
    private float _coolDown;
    [SerializeField]
    private float _buffTime;
    [SerializeField]
    private UIBarManager _actionManager;
    [SerializeField]
    private MeleeAttack _sword;
    [SerializeField]
    private GameObject _buff;
    [SerializeField]
    private int _manaCost;
    [SerializeField]
    private float _damage = 50;

    [SerializeField]
    private AudioClip buffSound;

    private bool _isCD;

    private void Awake()
    {
        _buff.SetActive(false);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha3) && !_isCD && Player.MyInstance.MyMana.MyCurrentValue>=_manaCost)
        {
            SoundManager.Instance.PlaySound(buffSound);
            Player.MyInstance.MyMana.MyCurrentValue -= _manaCost;
            StartCoroutine(BuffActionCorroutine());
        }
    }

    private IEnumerator BuffActionCorroutine()
    {
        _isCD = true;
        _sword.MyDamage += _damage;
        _buff.SetActive(true);
        StartCoroutine(_actionManager.Progress(3, _coolDown));
        yield return new WaitForSeconds(_buffTime);
        _buff.SetActive(false);
        _sword.MyDamage -= _damage;
        yield return new WaitForSeconds(_coolDown - _buffTime);
        _isCD = false;
    }
}
