using UnityEngine;
using MRD.Data;

[RequireComponent(typeof(Rigidbody))]
class WorldModifyingBat : MonoBehaviour{
	[SerializeField]
	private bool eating = false;
	[SerializeField]
	private float smoothTime = 1f;
	[SerializeField]
	private float speed = 10f;

	[SerializeField]
	private float cooldown = 2f;
	private float currentCooldown;

	Vector3 wantedRotation;
	Vector3 angularVelocity;

	Rigidbody rb = null;

	void Awake() {
		rb = GetComponent<Rigidbody>();
		angularVelocity = rb.angularVelocity;
		currentCooldown = cooldown * Random.value;
		transform.rotation = Quaternion.Euler(Random.value * 360, Random.value * 360, Random.value * 360);
		wantedRotation = new Vector3(Random.value * 360, Random.value * 360, Random.value * 360);
	}

	void Update() {
		currentCooldown -= Time.deltaTime;
		if (currentCooldown <= 0) {
			currentCooldown += cooldown;
			wantedRotation = new Vector3(Random.value * 360, Random.value * 360, Random.value * 360);
		}

		Vector3 currentRotation = transform.rotation.eulerAngles;
		for (int i = 0; i < 3; i++) {
			float velocity = angularVelocity[i];
			currentRotation[i] = Mathf.SmoothDampAngle(currentRotation[i], wantedRotation[i], ref velocity, smoothTime);
			angularVelocity[i] = velocity;
		}
		transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, currentRotation.z);

		rb.velocity = transform.forward * speed;
	}

	void FixedUpdate() {
		Vector3Int gridPos = RDGrid.FromLocal(transform.position);
		BlockData blockData = GameController.instance.worldData.blocks[gridPos];
		if (blockData == null) {
			//Destroy(gameObject);
			return;
		}
		if (blockData.block == Blocks.GetBlock("ground") == eating) {
			GameController.instance.worldData.blocks[gridPos] = new BlockData(gridPos, eating ? Blocks.GetBlock("air") : Blocks.GetBlock("ground"));
		}
	}
}
