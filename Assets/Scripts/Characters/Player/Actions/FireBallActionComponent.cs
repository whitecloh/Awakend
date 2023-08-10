using System.Collections;
using UnityEngine;

public class FireBallActionComponent : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private FireBall _fireBall;
    [SerializeField]
    private UIBarManager _spellManager;
    [SerializeField]
    private Transform _spellPlace;
    [SerializeField]
    private Player _player;
    private float _speed;
    [SerializeField]
    private AudioClip fireBallSound;

    private void Start()
    {
        _speed = _player.GetAgent.speed;
    }
    private bool _spellCD = false;
    private void Update()
    {
       FireBallAction();
    }
    private void FireBallAction()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && !_spellCD && _player.MyTarget!=null && Player.MyInstance.MyMana.MyCurrentValue >= _fireBall.ManaCost)
        {
            if (Vector3.Distance(transform.position, _player.MyTarget.position) <= 20)
            {
                _player.GetAgent.speed = 0;
                _animator.SetBool("isSpellCast", true);
                _animator.Play("SpellCast");
                SoundManager.Instance.PlaySound(fireBallSound);
                _fireBall.transform.position = _spellPlace.transform.position;
                Player.MyInstance.MyMana.MyCurrentValue -= _fireBall.ManaCost;
                StartCoroutine(SpellAttackCooldown());
            }
        }
    }

    public IEnumerator SpellAttackCooldown() // кд спелла
    {
        _spellCD = true;
        transform.LookAt(Player.MyInstance.MyTarget);
        Instantiate(_fireBall);
        StartCoroutine(_spellManager.Progress(1, _fireBall.FireCooldown)); // для отрисовки кд на ui
        _player.GetAgent.speed = _speed;
        _animator.SetBool("isSpellCast", false);
        yield return new WaitForSeconds(_fireBall.FireCooldown);
        _spellCD = false;
    }
}
