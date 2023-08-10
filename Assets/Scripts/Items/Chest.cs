using System.Collections;
using UnityEngine;

public class Chest : MonoBehaviour
{

    [SerializeField]
    private float _lifeTime;
    [SerializeField]
    private bool isQuest;

    public bool IsQuest => isQuest;

    private void Update()
    {
        DestroyChest();
    }
    public void DestroyChest()
    {
        if(LootWindow.Instance.droppedLoot == null)
        {
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(DestroyCoroutine());
        }
    }

    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(_lifeTime);
        if(!IsQuest)
        Destroy(gameObject);
    }

}

