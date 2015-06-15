using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float speed;

	void FixedUpdate() {
		var mousePos = Camera.main.ScreenToWorldPoint( Input.mousePosition );
		var rotation = Quaternion.LookRotation( transform.position - mousePos, Vector3.forward );

		transform.rotation = rotation;
		transform.eulerAngles = new Vector3( 0, 0, transform.eulerAngles.z );
		GetComponent<Rigidbody2D>().angularVelocity = 0;

		var input = Input.GetAxis( "Vertical" );
		GetComponent<Rigidbody2D>().AddForce( gameObject.transform.up * speed * input );
	}
}
