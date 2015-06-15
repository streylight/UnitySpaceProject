using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float speed = 10.0f;

	void Start () {
		GetComponent<Rigidbody2D>().velocity = transform.up.normalized * speed;
	}
}
