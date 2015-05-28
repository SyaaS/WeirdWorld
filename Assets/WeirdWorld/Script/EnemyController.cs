using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	
	public const int LayerEnemy = 10;

	CharacterController characterController;
	
	// move
	Vector3 vec3MoveDir = Vector3.zero;
	float moveSpeed = 8f;

	// flee
	bool doesFleeNow = false;

	void Start () {
		this.characterController = GetComponent<CharacterController> ();

		StartCoroutine ("EnuFleeCheck");
	}
	
	void Update() {
		this.Move ();
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

	IEnumerator EnuFleeCheck () {
		while (true) {
			doesFleeNow = false;
			if (Vector3.Distance ( this.transform.position, UserController.Instance.transform.position ) < 30f) {
				RaycastHit hit;
				
				if ( Physics.Raycast( this.transform.position, UserController.Instance.transform.position - this.transform.position, out hit ) ) {
					if ( hit.transform.gameObject.layer == UserController.LayerUser ) {
						doesFleeNow = true;
					}
				}
			}

			yield return new WaitForSeconds( 2f );
		}
	}

	public void CollidedByBullet () {
		Destroy (this.gameObject);
	}
}
