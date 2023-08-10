using System.Collections;
using UnityEngine;

public class BearAction : MonoBehaviour
{
    [SerializeField]
    private GameObject _bear;
    [SerializeField]
    private float _coolDown;
    [SerializeField]
    private float _lifeTime;
    [SerializeField]
    private UIBarManager _uiBarManager;
    [SerializeField]
    private Transform _bearPlace;
    [SerializeField]
    private int _manaCost;
    [SerializeField]
    private AudioClip bearSound;

    private bool _isCD;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha4)&&!_isCD&& Player.MyInstance.MyMana.MyCurrentValue>=_manaCost)
        {
            SoundManager.Instance.PlaySound(bearSound);
            Player.MyInstance.MyMana.MyCurrentValue -= _manaCost;
            StartCoroutine(BearSpawn());
        }
    }

    private IEnumerator BearSpawn()
    {
        _isCD = true;
        var go = Instantiate(_bear,_bearPlace);
        go.GetComponent<MeleeAttack>().Character = GetComponent<Player>();
        go.transform.SetParent(transform.parent);
        StartCoroutine(_uiBarManager.Progress(4, _coolDown));
        yield return new WaitForSeconds(_lifeTime);
        Destroy(go);
        yield return new WaitForSeconds(_coolDown - _lifeTime);
        _isCD = false;
    }
}
