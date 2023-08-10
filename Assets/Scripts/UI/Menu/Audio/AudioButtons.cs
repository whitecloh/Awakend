using UnityEngine;

public class AudioButtons : MonoBehaviour
{
    [SerializeField]
    private AudioClip hover;
    [SerializeField]
    private AudioClip click;
    [SerializeField]
    private AudioClip parameter;

    public void HoverSound()
    {
        SoundManager.Instance.PlaySound(hover);
    }

    public void ClickSound()
    {
        SoundManager.Instance.PlaySound(click);
    }

    public void ParameterSound()
    {
        SoundManager.Instance.PlaySound(parameter);
    }

}
