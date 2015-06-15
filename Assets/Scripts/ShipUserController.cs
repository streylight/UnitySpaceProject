using UnityEngine;

[RequireComponent(typeof(ShipController))]
public class ShipUserController : MonoBehaviour {

	public GameObject shot;
	public GameObject explosion;

	public Transform shotSpawn;
	public float rotationSpeed = 160; // 160 = best rotation speed
	public float fireRate = 0.5f;

	private float nextFire = 0.0f;
	private ShipController ship;

	void Awake () {
		ship = GetComponent<ShipController>();
	}

//	public void Explosion() {
//		Instantiate( explosion, transform.position, transform.rotation );
//	}

	void FixedUpdate() {
		// get input for rotation and the forward/reverse movement
		var rotate = Input.GetAxisRaw( "Horizontal" );
		var throttle = Input.GetAxis( "Vertical" );

		// pass the input to the ship
		ship.Move(rotate, throttle);
	}

	void Update() {
		// rotate ship around y axis
		if (Input.GetButton( "Fire1" ) && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate( shot, shotSpawn.position, shotSpawn.rotation );
		}
		transform.Rotate( 0.0f, 0.0f, -1 * Input.GetAxisRaw( "Horizontal" ) * rotationSpeed * Time.deltaTime );



		// clamps boundries for map which are currently hardcoded
		// the clamp is needed to prevent the rotation physics to move the ship +- on the y axis which is baaaaad
		//transform.position = new Vector3(Mathf.Clamp(transform.position.x, -500, 500),0.0f,Mathf.Clamp(transform.position.z,-500,500));
	}
}
