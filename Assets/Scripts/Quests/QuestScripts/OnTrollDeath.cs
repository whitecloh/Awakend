using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTrollDeath : MonoBehaviour
{
    [SerializeField]
    private AudioClip _catSpech;

    private void Awake()
    {
        SoundManager.Instance.PlayMusic(_catSpech);
    }

}
