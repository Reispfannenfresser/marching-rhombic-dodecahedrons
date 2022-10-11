using UnityEngine;
using MRD.Data;

class CameraController : MonoBehaviour
{
	[SerializeField]
	float movementSpeed = 10f;
	[SerializeField]
	float movementMultiplier = 2f;
	[SerializeField]
	float turnSpeed = 10f;
	[SerializeField]
	float reach = 5f;
	[SerializeField]
	LayerMask terrain = 0;

	Vector3 rotation = new Vector3(0, 0, 0);

	void Awake()
	{
		Cursor.lockState = CursorLockMode.Locked;
		transform.rotation = Quaternion.Euler(rotation);
	}

	void Update()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			BlockData block = BlockToBreak();
			if (block != null)
			{
				GameController.instance.worldData.blocks[block.pos] = null;
			}
		}

		if (Input.GetButtonDown("Fire2"))
		{
			Vector3Int? placePos = PosToPlace();
			if (placePos.HasValue)
			{
				GameController.instance.worldData.blocks[placePos.Value] = new BlockData(placePos.Value, Blocks.GetBlock("ground"));
			}
		}

		rotation += new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * turnSpeed;
		rotation.x = Mathf.Clamp(rotation.x, -90, 90);

		transform.rotation = Quaternion.Euler(rotation);


		float currentMovementMultiplier = Input.GetButton("Fire3") ? movementMultiplier : 1;
		float currentMovementSpeed = movementSpeed * Time.deltaTime * currentMovementMultiplier;

		transform.position += transform.forward * Input.GetAxis("Vertical") * currentMovementSpeed;
		transform.position += transform.right * Input.GetAxis("Horizontal") * currentMovementSpeed;
	}

	private BlockData BlockToBreak()
	{
		RaycastHit hitInfo;
		if (Physics.Raycast(transform.position, transform.forward, out hitInfo, reach, terrain))
		{
			Vector3Int blockPos = RDGrid.FromLocal(hitInfo.point - hitInfo.normal * 0.5f);
			BlockData blockData = GameController.instance.worldData.blocks[blockPos];
			if (blockData != null && !blockData.block.indestructible)
			{
				return blockData;
			}
		}
		return null;
	}

	private Vector3Int? PosToPlace()
	{
		RaycastHit hitInfo;
		if (Physics.Raycast(transform.position, transform.forward, out hitInfo, reach, terrain))
		{
			Vector3Int blockPos = RDGrid.FromLocal(hitInfo.point + hitInfo.normal * 0.5f);
			BlockData blockData = GameController.instance.worldData.blocks[blockPos];
			if (blockData == null)
			{
				return blockPos;
			}
		}
		return null;
	}
}
