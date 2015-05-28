using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	
	public const int LayerEnemy = 10;

	CharacterController characterController;
	
	// move
	Vector3 vec3MoveDir = Vector3.zero;
	float moveSpeed = 5f;
	float jumpSpeed = 5f;
	
	// rotate
	float rotSpeed = 90f;

	void Awake () {
		this.characterController = GetComponent<CharacterController> ();
	}
	
	void Update() {
		this.Move ();
		this.Rotate ();
	}

	void Move () {
		if (this.characterController.isGrounded) {
			this.vec3MoveDir = new Vector3(0, 0, Input.GetAxis("Vertical"));
			this.vec3MoveDir = this.transform.TransformDirection(vec3MoveDir);
			this.vec3MoveDir *= moveSpeed;
			if (Input.GetButton("Jump"))
				this.vec3MoveDir.y = jumpSpeed;
			
		}
		this.vec3MoveDir.y += Physics.gravity.y * Time.deltaTime;
		this.characterController.Move(this.vec3MoveDir * Time.deltaTime);
	}
	
	void Rotate () {
		transform.Rotate(0, Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime, 0);
	}

	public void CollidedByBullet () {
		Destroy (this.gameObject);
	}
}
