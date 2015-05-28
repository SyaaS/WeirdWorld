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

	// flee
	bool doesFleeNow = false;

	void Start () {
		this.characterController = GetComponent<CharacterController> ();

		StartCoroutine ("EnuFleeCheck");
	}
	
	void Update() {
		this.Move ();
		this.Rotate ();
	}

	void Move () {
		if (this.characterController.isGrounded) {
			if (doesFleeNow == true) {
				this.vec3MoveDir = this.transform.position - UserController.Instance.transform.position;
				this.vec3MoveDir.Normalize();
				this.vec3MoveDir *= moveSpeed;
			}
			else {
				this.vec3MoveDir.x = this.vec3MoveDir.z = 0;
			}
		}
		this.vec3MoveDir.y += Physics.gravity.y * Time.deltaTime;
		this.characterController.Move(this.vec3MoveDir * Time.deltaTime);
	}
	
	void Rotate () {
		transform.Rotate(0, Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime, 0);
	}

	IEnumerator EnuFleeCheck () {
		while (true) {
			if (Vector3.Distance ( this.transform.position, UserController.Instance.transform.position ) < 30f)
				doesFleeNow = true;
			else
				doesFleeNow = false;

			yield return new WaitForSeconds( 2f );
		}
	}

	public void CollidedByBullet () {
		Destroy (this.gameObject);
	}
}
