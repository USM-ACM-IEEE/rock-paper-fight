using UnityEngine;
using System.Collections;

public class player1_controller : MonoBehaviour
{
    // GameObject for the diferent forms of the player
    public GameObject rock;     // 1
    public GameObject paper;    // 2
    public GameObject scissor;  // 3

    // Tacks the current state of the player
    private int current_form;

    private Rigidbody rb;                   // This will be the ridgidbody to update the players position
    private float nextTransfrom = 0.0F;     // This will hold the next time when the player is allowed to transform
    public float transfromDelay;            // This is the delay between being allowed to transform
    public float speed;                     // Variable to alter the max speed of an object

    void Start()
    {
        // Initalize the ridgid body
        rb = GetComponent<Rigidbody>();
        // Initalize to rock
        current_form = 1;
    }

    void FixedUpdate()
    {
        // Get the transform from the set controls
        float moveHorizontal = Input.GetAxis("Horizontal_1");
        float moveVeritcal = Input.GetAxis("Vertical_1");

        // Calculate and set the new movement variables
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVeritcal);
        rb.velocity = movement * speed;
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    }

    void Update()
    {
        // If the F key is hit transform the object
        if(Input.GetKey (KeyCode.F) && Time.time > nextTransfrom)
        {
            transform();
            nextTransfrom = Time.time + transfromDelay;
        }
    }

    void transform()
    {
        // Iterate the current form
        current_form = (current_form + 1) % 3 + 1;

        // Switch the object to the new form
        if(current_form == 1)
        {
            GetComponent<MeshFilter>().mesh = rock.GetComponent<MeshFilter>().sharedMesh;
        }
        else if(current_form == 2)
        {
            GetComponent<MeshFilter>().mesh = paper.GetComponent<MeshFilter>().sharedMesh;
        }
        else if(current_form == 3)
        {
            GetComponent<MeshFilter>().mesh = scissor.GetComponent<MeshFilter>().sharedMesh;
        }
    }
}