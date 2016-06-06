using UnityEngine;
using System.Collections;

public class collisionBounce : MonoBehaviour {

	public float push_back_velocity;		// This is the amount of velocity that will be multiplied to cause the push_back

	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			float x_diff = collision.transform.position.x - transform.position.x;
			float z_diff = collision.transform.position.z - transform.position.z;

			Vector3 force = new Vector3 (x_diff, 0.0f, z_diff);
			force.Normalize();
			force *= -1;


			Rigidbody rb = GetComponent<Rigidbody> ();
			rb.AddForce(force * push_back_velocity, ForceMode.Acceleration);
		}
	}
}
