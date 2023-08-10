using UnityEngine;

public class Vendor : MonoBehaviour
{
    [SerializeField]
    private Window _vendorWindow;
    public void Interact()
    {
       // _vendorWindow.Open(this);
    }

    public void StopInteract()
    {
        _vendorWindow.Close();
    }
}
