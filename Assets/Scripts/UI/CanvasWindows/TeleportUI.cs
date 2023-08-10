using UnityEngine;

public class TeleportUI : MonoBehaviour
{
    private Player _player;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        gameObject.SetActive(false);
    }
    public void NoTeleport()
    {
        _player.GetComponent<Player>().enabled = true;
        gameObject.SetActive(false);
    }
}
