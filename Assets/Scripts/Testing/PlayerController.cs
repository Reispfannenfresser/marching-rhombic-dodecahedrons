using UnityEngine;
using MRD.Data;

[RequireComponent(typeof(Rigidbody))]
class PlayerController : MonoBehaviour {
	[SerializeField]
	float movementSpeed = 10f;
	[SerializeField]
	float turnSpeed = 10f;
	[SerializeField]
	float runMultiplier = 2.5f;
	[SerializeField]
	float jumpStrength = 5f;
	[SerializeField]
	GameObject head = null;
	[SerializeField]
	float reach = 5f;
	[SerializeField]
	LayerMask terrain = 0;

	Vector3 rotation = new Vector3(0, 90, 0);
	Rigidbody rb = null;

	void Awake() {
		rb = GetComponent<Rigidbody>();
		Cursor.lockState = CursorLockMode.Locked;
		transform.rotation = Quaternion.Euler(rotation);
	}

	void Update() {
		Vector3 horizontal = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
		bool running = Input.GetButton("Fire3");
		rb.velocity = horizontal * movementSpeed * (running ? runMultiplier : 1) + Vector3.up * rb.velocity.y;

		if (Input.GetButtonDown("Jump")) {
			rb.velocity = new Vector3(rb.velocity.x, jumpStrength, rb.velocity.x);
		}

		if (Input.GetButtonDown("Fire1")) {
			BlockData block = BlockToBreak();
			if (block != null) {
				GameController.instance.worldData.blocks[block.pos] = new BlockData(block.pos, Blocks.GetBlock("air"));
			}
		}

		if (Input.GetButtonDown("Fire2")) {
			BlockData block = BlockToPlace();
			if (block != null) {
				GameController.instance.worldData.blocks[block.pos] = new BlockData(block.pos, Blocks.GetBlock("ground"));
			}
		}

		rotation += new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * turnSpeed;
		rotation.x = Mathf.Clamp(rotation.x, -90, 90);

		transform.rotation = Quaternion.Euler(0, rotation.y, 0);
		head.transform.rotation = Quaternion.Euler(rotation);
	}

	private BlockData BlockToBreak() {
		RaycastHit hitInfo;
		if (Physics.Raycast(head.transform.position, head.transform.forward, out hitInfo, reach, terrain)) {
			Vector3Int blockPos = RDGrid.FromLocal(hitInfo.point);
			BlockData block = GameController.instance.worldData.blocks[blockPos];
			if (block.block == Blocks.GetBlock("air")) {
				blockPos = RDGrid.FromLocal(hitInfo.point - hitInfo.normal * 0.5f);
				block = GameController.instance.worldData.blocks[blockPos];
			}

			return block;
		}
		return null;
	}

	private BlockData BlockToPlace() {
		RaycastHit hitInfo;
		if (Physics.Raycast(head.transform.position, head.transform.forward, out hitInfo, reach, terrain)) {
			Vector3Int blockPos = RDGrid.FromLocal(hitInfo.point);
			BlockData block = GameController.instance.worldData.blocks[blockPos];
			if (block.block != Blocks.GetBlock("air")) {
				blockPos = RDGrid.FromLocal(hitInfo.point + hitInfo.normal * 0.5f);
				block = GameController.instance.worldData.blocks[blockPos];
			}

			return block;
		}
		return null;
	}
}
