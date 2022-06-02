using UnityEngine;

class CameraController : MonoBehaviour {
	[SerializeField]
	float movementSpeed = 10f;
	[SerializeField]
	float turnSpeed = 10f;
	[SerializeField]
	float runMultiplier = 2.5f;

	Vector3 rotation = new Vector3(0, 90, 0);

	void Awake() {
		Cursor.lockState = CursorLockMode.Locked;
		transform.rotation = Quaternion.Euler(rotation);
	}

	void Update() {
		Vector3 horizontal = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
		bool running = Input.GetButton("Fire3");
		transform.position += horizontal * Time.deltaTime * movementSpeed * (running ? runMultiplier : 1);

		rotation += new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * turnSpeed;
		rotation.x = Mathf.Clamp(rotation.x, -90, 90);

		transform.rotation = Quaternion.Euler(rotation);
	}
}
