using System.Collections.Generic;
using UnityEngine;

public class NPCDetector : MonoBehaviour // сфера для подсветки ui врагов
{
    private List<Enemy> _enemies;

    private void Awake()
    {
        _enemies = new List<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<Enemy>();
        var chest = other.GetComponent<Chest>();
        if (enemy != null)
        {
            enemy.Select();
            _enemies.Add(enemy);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        var enemy = other.GetComponent<Enemy>();
        var chest = other.GetComponent<Chest>();
        if (enemy != null)
        {
            enemy.DeSelect();
           _enemies.Remove(enemy);
        }
        if (chest != null&&!chest.IsQuest)
        {
            Destroy(chest.gameObject);
        }
    }
}
