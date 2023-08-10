using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAction : MonoBehaviour
{
    [SerializeField]
    private Item _healthPotion;
    [SerializeField]
    private float _healCooldown=0.5f;

    public IUseable MyUseable { get; set; }

    private Stack<IUseable> _useables;

    private AudioClip _healSound;

    private bool _isCd = false;

    private void Start()
    {
        _healSound = Player.MyInstance.HealSound;
    }

    private void Update()
    {
        UsePotion();
    }
    public void UsePotion()
    {
        if (Input.GetKeyDown(KeyCode.R)&&!_isCd)
        {
                GetPotions(_healthPotion as IUseable);
                if (HandScript.Instance.MyMoveable == null)
                {
                    if (_useables != null && _useables.Count > 0)
                    {
                        StartCoroutine(PotionCD());
                    }
                }
        }
    }

    public void GetPotions(IUseable useable)
    {
        if(useable is HealthsPotion)
        {
            _useables = InventoryScript.Instance.GetUseables(useable);
        }
       
        MyUseable = useable;
    }

    private IEnumerator PotionCD()
    {
        SoundManager.Instance.PlaySound(_healSound);
        _isCd = true;
        StartCoroutine(UIBarManager.MyInstance.Progress(5, _healCooldown));
        _useables.Pop().Use();
        yield return new WaitForSeconds(_healCooldown);
        _isCd = false;
    }
}
