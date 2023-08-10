using UnityEngine;

public class ToggleAudio : MonoBehaviour
{
    [SerializeField] 
    private bool _toggleMusic, _toggleSounds;

    public void Toggle()
    {
        if (_toggleSounds) SoundManager.Instance.ToggleS();
        if (_toggleMusic) SoundManager.Instance.ToggleM();
    }
}
