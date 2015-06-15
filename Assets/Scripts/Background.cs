using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

	public float speed = 0.1f;

	void Update () {
		var y = Mathf.Repeat( Time.time * speed, 1 );	
		var offset = new Vector2( 0, y );
		GetComponent<Renderer>().sharedMaterial.SetTextureOffset( "_MainTex", offset );
	}
}
