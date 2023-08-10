using UnityEngine;

public class Unvisable : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
}
