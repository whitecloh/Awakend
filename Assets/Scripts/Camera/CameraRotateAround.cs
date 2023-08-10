using UnityEngine;

	public class CameraRotateAround : MonoBehaviour
	{
	[SerializeField]
    private Transform target;
	[SerializeField]
	private Vector3 offset;
	[SerializeField]
	private float sensitivity = 3; // чувствительность мышки
	[SerializeField]
	private float limit = 80; // ограничение вращения по Y
	[SerializeField]
	private float zoom = 0.25f; // чувствительность при увеличении, колесиком мышки
	[SerializeField]
	private float zoomMax = 10; // макс. увеличение
	[SerializeField]
	private float zoomMin = 3; // мин. увеличение

	private float X, Y;

	void Start()
		{
			limit = Mathf.Abs(limit);
			if (limit > 90) limit = 90;
			offset = new Vector3(0, 0, Mathf.Abs(zoomMax));
			transform.position = target.position + new Vector3(0,20,20);
		    transform.LookAt(target);
		}

	void Update()
	{
			if (Input.GetAxis("Mouse ScrollWheel") > 0) offset.z += zoom;
			else if (Input.GetAxis("Mouse ScrollWheel") < 0) offset.z -= zoom;
			offset.z = Mathf.Clamp(offset.z, -Mathf.Abs(zoomMax), -Mathf.Abs(zoomMin));
		if (Input.GetKey(KeyCode.Mouse2))
        {
			Mouse();
        }
        else
        {
			Keys();
        }

			transform.localEulerAngles = new Vector3(-Y, X, 0);

			transform.position = transform.localRotation * offset + target.position;
	}

	private void Mouse()
	{
		X = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
		Y += Input.GetAxis("Mouse Y") * sensitivity;
		Y = Mathf.Clamp(Y, -limit, 0);
	}

	private void Keys()
    {
		X = transform.localEulerAngles.y + Input.GetAxis("Horizontal") * sensitivity;
		Y += Input.GetAxis("Vertical") * sensitivity;
		Y = Mathf.Clamp(Y, -limit, 0);
	}
	} 
