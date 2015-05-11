using UnityEngine;
using System.Collections;

public class UserController : MonoBehaviour {

	CharacterController characterController;

	// move
	Vector3 vec3MoveDir = Vector3.zero;
	float moveSpeed = 3f;
	float jumpSpeed = 2f;

	void Awake () {
		characterController = GetComponent<CharacterController> ();


	}

	void Update() {

		if (characterController.isGrounded) {
			vec3MoveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			vec3MoveDir = transform.TransformDirection(vec3MoveDir);
			vec3MoveDir *= moveSpeed;
			if (Input.GetButton("Jump"))
				vec3MoveDir.y = jumpSpeed;
			
		}
		vec3MoveDir.y += Physics.gravity.y * Time.deltaTime;
		characterController.Move(vec3MoveDir * Time.deltaTime);
	}
}
