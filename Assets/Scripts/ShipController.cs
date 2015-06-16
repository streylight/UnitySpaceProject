using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipController : MonoBehaviour {

	[SerializeField] 
	private float maxEnginePower = 10.0f;
	[SerializeField] 
	private float throttleChangeSpeed = 0.3f; 

	public float maxForwardVelocity = 3.0f;
	public float maxReverseVelocity = -2.5f;

	public float sqrMaxVelocity { get; private set; }

	public float Throttle { get; private set; }                         
	public float ThrottleSpeed { get; private set; }                  
	public float EnginePower { get; set; }            
	public float RotationInput { get; private set; }
	public float ThrottleInput { get; private set; }
	public float BankThrottle { get; private set; }  
	public float BankPower { get; private set; } 

	private int reverse;

	private Rigidbody2D rigidBody2D { get; set;}

	void Awake () {
		rigidBody2D = GetComponent<Rigidbody2D>();
		SetMaxVelocity( maxForwardVelocity );
	}

	public void SetMaxVelocity( float maxVelocity ) {
		this.maxForwardVelocity = maxVelocity;
		this.sqrMaxVelocity = Mathf.Pow( maxVelocity, 2 );
	}
	public void Move( float rotationInput, float throttleInput ) {
		this.RotationInput = rotationInput;
		this.ThrottleInput = throttleInput;

		if( Input.GetKey( KeyCode.LeftShift ) ) {
			maxForwardVelocity = 7.0f;
			ThrottleInput += 0.5f;
		} else {
			maxForwardVelocity = 3.0f;
		}

		// negate throttle if in reverse
		// needed for the "realistic" velocity change when changing from forward to reverse
		if ( throttleInput < 0 ) {
			this.reverse = -1;
			this.ThrottleInput *= reverse;
		} else {
			this.reverse = 1;
		}


		// set/calculate movement factors
		ControlThrottle();
		CalculateLinearForces();
	}

	// calculate throttle speed and set it
	void CalculateThrottleSpeed() {
		var localVelocity = transform.InverseTransformDirection(GetComponent<Rigidbody2D>().velocity);
		localVelocity.y = 0.0f;
		ThrottleSpeed = Mathf.Max(0, localVelocity.z);
	}

	// self explanatory
	void ControlThrottle() {
		Throttle = Mathf.Clamp01( Throttle + ThrottleInput * Time.deltaTime * throttleChangeSpeed );
		EnginePower = Mathf.Min( (Throttle * maxEnginePower), maxForwardVelocity );
	}
	
	void CalculateLinearForces() {
		var forces = Vector3.zero;



		var velocity = rigidBody2D.velocity;

		if(velocity.sqrMagnitude > sqrMaxVelocity) {
			rigidBody2D.velocity = velocity.normalized * maxForwardVelocity;
		}
		Debug.Log("Velocity: " + GetComponent<Rigidbody2D>().velocity);
		//used for the zero gravity type effect

		forces += EnginePower * ((reverse * transform.up) * ThrottleInput);


		Debug.Log("Forces: " + forces);

		GetComponent<Rigidbody2D>().AddForce(forces);
		//GetComponent<Rigidbody2D>().velocity = new Vector3(Mathf.Clamp(velocity.x, maxReverseVelocity, maxForwardVelocity), Mathf.Clamp(velocity.y, maxReverseVelocity, maxForwardVelocity), 0);
	}
}
