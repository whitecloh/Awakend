using UnityEngine;

public delegate void KillConfirmed(Character character);
public class KillManager : MonoBehaviour
{
    public event KillConfirmed _killConfirmedEvent;
    private static KillManager _instance;
    public static KillManager Instance
    {
        get
        {
            if(_instance ==null)
            {
                _instance = FindObjectOfType<KillManager>();
            }
            return _instance;
        }
    }
    public void OnKillConfirmed(Character character)
    {
        if(_killConfirmedEvent!=null)
        {
            _killConfirmedEvent(character);
        }
    }
}
