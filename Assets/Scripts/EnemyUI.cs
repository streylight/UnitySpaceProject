using UnityEngine;
using System.Collections;

public class EnemyUI : MonoBehaviour {

	public float speed;
	public Transform player;
	public GameObject explosion;
	//public GameObject Enemy;

	public void Explosion() {
		Instantiate (explosion, transform.position, transform.rotation);
	}

	void FixedUpdate() {
		var z = Mathf.Atan2( (player.transform.position.y - transform.position.y), (player.transform.position.x - transform.position.x) ) * Mathf.Rad2Deg - 90;

		transform.eulerAngles = new Vector3( 0, 0, z );
		GetComponent<Rigidbody2D>().AddForce( gameObject.transform.up * speed );
	}

	void OnTriggerEnter2D( Collider2D collider ) {
		Debug.Log( "nme collide" );
		Destroy( collider.gameObject );

		this.Explosion();

		Destroy ( gameObject );
	}
}
