using UnityEngine;
using System.Collections;

public class UserController : MonoBehaviour {
	
	// singleton --->
	protected static UserController instance = null;
	public static UserController Instance { get { if (instance == null) { Debug.LogError("Instance is not set"); } return instance; } }
	void Awake () {
		if ( instance != null && instance != this ) { Destroy ( this.gameObject ); return; }
		else { instance = this; }
		DontDestroyOnLoad (this.gameObject);
	}
	void OnApplicationQuit() { instance = null; }
	// <--- singleton
	
	public const int LayerUser = 8;

	CharacterController characterController;

	// children
	Transform gunpointTrans;

	// move
	Vector3 vec3MoveDir = Vector3.zero;
	float moveSpeed = 5f;
	float jumpSpeed = 5f;

	// rotate
	float rotSpeed = 90f;

	// shoot
	GameObject bulletPrefObj;
	float shootPower = 20f;

	void Start () {
		this.characterController = GetComponent<CharacterController> ();

		this.gunpointTrans = this.transform.Find ("Gunpoint");

		this.bulletPrefObj = Resources.Load ("Prefab/User Bullet") as GameObject;
	}

	void Update() {
		this.Move ();
		this.Rotate ();
		this.Shoot ();
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

	void Shoot () {
		if (Input.GetMouseButtonDown (0)) {
			Transform bulletTrans = (Instantiate (this.bulletPrefObj) as GameObject).transform;
			bulletTrans.position = this.gunpointTrans.position;
			bulletTrans.rotation = this.gunpointTrans.rotation;
			bulletTrans.GetComponent<Rigidbody> ().AddForce (this.transform.forward * this.shootPower, ForceMode.Impulse);
		}
	}
}
