using UnityEngine;
using System.Collections;

public class player1_controller : MonoBehaviour
{

    private Rigidbody rb;           // This will be the ridgidbody to update the players position
    public Boundary boundary;       // Tracks the boundry of for the object
    public float speed;             // Variable to alter the max speed of an object

    [System.Serializable]
    public class Boundary
    {
        public float xMin, xMax, zMin, zMax;
    }

    void Start()
    {
        // Initalize the ridgid body
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal_1");
        float moveVeritcal = Input.GetAxis("Vertical_1");

        if (moveHorizontal != 0)
            Debug.Log(moveHorizontal);

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVeritcal);
        rb.velocity = movement * speed;
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    }
}