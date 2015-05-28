using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public const int LayerUserBullet = 9;

	void Awake () {

		StartCoroutine (EnuDestroySelf ());
	}

	IEnumerator EnuDestroySelf () {
		yield return new WaitForSeconds( 2f );

		Destroy ( this.gameObject );
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.layer == EnemyController.LayerEnemy) {
			collision.gameObject.GetComponent<EnemyController>().CollidedByBullet();
			Destroy (this.gameObject);
		}
	}
}
