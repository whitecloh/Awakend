using UnityEngine;
using UnityEngine.EventSystems;

public class TargetComponent : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    [SerializeField]
    private LayerMask _npc;

    private Enemy _currentTarget;

    private void Update()
    {
        ClickTarget();
    }

    private void ClickTarget()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray agentRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(agentRay, out hitInfo, 100, _npc))
            {
                _currentTarget = hitInfo.collider.GetComponent<Enemy>();
                if (_currentTarget!=null && _currentTarget.IsAlive)
                {
                    _player.MyTarget = _currentTarget.HitBox;
                }
            }
            else
            {
                _currentTarget = null;
                _player.MyTarget = null;
            }
        }
    }
}
