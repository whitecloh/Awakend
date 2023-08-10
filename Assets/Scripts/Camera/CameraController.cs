using UnityEngine;

public class CameraController : MonoBehaviour
{    
	[SerializeField]
	private Vector3 _offset;
	[SerializeField]
	private float _zoomSpeed = 4f;
	[SerializeField]
	private float _minZoom = 5f;
	[SerializeField]
	private float _maxZoom = 15f;
	[SerializeField]
	private float _yawSpeed = 100f;
	[SerializeField]
	private Player _target = null;

	private float _pitch = 1.8f;        
	private float _currentZoom = 10f;
	private float _currentYaw = 0f;


    void Update()
	{
		
		_currentZoom -= Input.GetAxis("Mouse ScrollWheel") * _zoomSpeed;
		_currentZoom = Mathf.Clamp(_currentZoom, _minZoom, _maxZoom);
		_currentYaw += Input.GetAxis("Horizontal") * _yawSpeed * Time.deltaTime;
	}

	void LateUpdate()
	{
		
		transform.position = _target.transform.position - _offset * _currentZoom;
		
		transform.LookAt(_target.transform.position + Vector3.up * _pitch);
		
		transform.RotateAround(_target.transform.position, Vector3.up, _currentYaw);
	}

    private void OnDrawGizmos()
    {
		Gizmos.color = Color.red;
		Gizmos.DrawRay(transform.position,_target.transform.position - transform.position);
    }
}