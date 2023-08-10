using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _musicSource, _soundSource;
    public AudioClip _menuClip,_gameClip,_endGameClip;
    private Toggle _sound, _music;

    private ToggleAudio[] _toggles;

    public AudioSource MusicSource => _musicSource;

    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if(instance==null)
            {
                instance = FindObjectOfType<SoundManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<SoundManager>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "MenuScene")
        {
            _toggles = FindObjectsOfType<ToggleAudio>();
            _sound = _toggles[0].GetComponent<Toggle>();
            _music = _toggles[1].GetComponent<Toggle>();
        }
        ToggleMusic();
        ToggleSounds();
    }
    public void PlaySound(AudioClip clip)
    {
        _soundSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        //_musicSource.Stop();
        _musicSource.PlayOneShot(clip);
    }

    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }

    private void ToggleSounds()
    {
        if (_sound.isOn==false)
        {
            _soundSource.mute = false;
        }
        else
        { 
            _soundSource.mute = true;
        }
    }
    private void ToggleMusic()
    {
        if (_music.isOn==false)
        {
            _musicSource.mute = false;
        }
        else
        {
            _musicSource.mute = true;
        }
    }

    public void ToggleS()
    {
        _soundSource.mute = !_soundSource.mute;
    }
    public void ToggleM()
    {
        _musicSource.mute = !_musicSource.mute;
    }
}
