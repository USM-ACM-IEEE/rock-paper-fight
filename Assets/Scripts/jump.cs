using UnityEngine;
using System.Collections;

public class jump : MonoBehaviour {

	public float jump_velocity;		// This is the amount of velocity that will be multiplied to cause the jump

	void Update()
	{
		float x_diff = 0;
		float z_diff = 0;
	
		Vector3 force = new Vector3 (x_diff, 0.0f, z_diff);
		force.Normalize();

		Rigidbody rb = GetComponent<Rigidbody> ();
		rb.AddForce(force * jump_velocity, ForceMode.Acceleration);
	}
}