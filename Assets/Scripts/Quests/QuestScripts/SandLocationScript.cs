using UnityEngine;

public class SandLocationScript : MonoBehaviour
{
    [SerializeField]
    private Citizen _citizen;
    private static SandLocationScript instance;
    public static SandLocationScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SandLocationScript>();
            }
            return instance;
        }
    }

    public void OnComplete()
    {
        _citizen.GetComponent<Teleport>().OnSetActive();
    }
}
