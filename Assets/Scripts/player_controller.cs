using UnityEngine;
using System.Collections;

public class player_controller : controller
{
	private Rigidbody rb;
	public string Rock_Transform_Button;
	public string Paper_Transform_Button;
	public string Scissors_Transform_button;

	private bool controlerIsEnabled;	// Tracks is a controller was detected on the device

	protected override void Start()
    {
		// Be sure to call the base classes start as well
		base.Start ();
        // Initalize the ridgid body
        rb = GetComponent<Rigidbody>();

		// Configure controlls, if there is a controller present set the boolean
		if (Input.GetJoystickNames ().Length != 0) {
			Rock_Transform_Button = "joystick button 16";
			Paper_Transform_Button = "joystick button 17";
			Debug.Log("here");
		}
    }

    void FixedUpdate()
    {
		// Variables to track the amont of movement
		float moveHorizontal, moveVeritcal;

		if (canMove()) 
		{
			// Get the transform from the set controls
			moveHorizontal = Input.GetAxis ("Horizontal_" + controller_name);
			moveVeritcal = Input.GetAxis ("Vertical_" + controller_name);
		
			// Calculate and set the new movement variables
			Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVeritcal);
			rb.velocity = movement * game.player_configurations.speed;
			rb.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
		}
    }

    void Update()
    {
        // If the transform button key is hit transform the object
		if (Input.GetKey (Rock_Transform_Button)) {
			switchForm (0);
		}
		if (Input.GetKey (Paper_Transform_Button)) {
			switchForm (1);
		}
		if(Input.GetKey (Scissors_Transform_button))
        {
			// Currently pressing the transform button iterates through the forms
			switchForm (2);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
			// Check if the enemy player is in the form that beats you
			if (collision.gameObject.GetComponent<controller>().getForm() == (getForm() + 1) % 3) 
			{
				// Player has died remove gameObject
				Destroy (gameObject);
			} 
			else 
			{
				// Stop players from moving so the push_back animation is not interupted
				game.FreezeControlls(game.player_configurations.controlls_delay_on_hit);
			}
        }
    }
}