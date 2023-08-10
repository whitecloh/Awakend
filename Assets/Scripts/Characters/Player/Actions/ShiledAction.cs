using System.Collections;
using UnityEngine;

public class ShiledAction : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    [SerializeField]
    private float _immortalTime;
    [SerializeField]
    private float _coolDown;
    [SerializeField]
    private GameObject _bubble;
    [SerializeField]
    private UIBarManager _cooldownManager;
    [SerializeField]
    private int _manaCost;

    [SerializeField]
    private AudioClip shieldSound;

    private bool _isCD;

    private void Awake()
    {
        _bubble.SetActive(false);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha2) && !_isCD && Player.MyInstance.MyMana.MyCurrentValue >= _manaCost)
        {
            SoundManager.Instance.PlaySound(shieldSound);
            Player.MyInstance.MyMana.MyCurrentValue -= _manaCost;
            StartCoroutine(ShieldCooldown());
        }
    }

    public IEnumerator ShieldCooldown()
    {
        _isCD = true;
        _player.GetComponent<Collider>().enabled = false;
        _bubble.SetActive(true);
        StartCoroutine(_cooldownManager.Progress(2, _coolDown));
        yield return new WaitForSeconds(_immortalTime);
        _bubble.SetActive(false);
        _player.GetComponent<Collider>().enabled = true;
        yield return new WaitForSeconds(_coolDown-_immortalTime);
        _isCD = false;
    }
}
