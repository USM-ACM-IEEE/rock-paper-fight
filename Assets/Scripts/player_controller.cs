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

		controlerIsEnabled = Input.GetJoystickNames ().Length >= player_number;
		// Configure controlls, if there is a controller present set the boolean
		if (controlerIsEnabled) {
			Rock_Transform_Button = "joystick " + player_number + " button 2";
			Paper_Transform_Button = "joystick " + player_number + " button 3";
			Scissors_Transform_button = "joystick " + player_number + " button 1";
		} else {
			// Use what is in the unity editior
		}
    }

    void FixedUpdate()
    {
		// Variables to track the amont of movement
		float moveHorizontal, moveVeritcal;

		if (canMove()) 
		{
			if (controlerIsEnabled) {
				// Get the transform from the set controls
				moveHorizontal = Input.GetAxis ("x axis " + controller_name);
				moveVeritcal = -1 * Input.GetAxis ("y axis " + controller_name);

				// Calculate and set the new movement variables
				Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVeritcal);
				rb.velocity = movement * game.player_configurations.speed;
				rb.rotation = Quaternion.Euler (0.0f, 0.0f, 0.0f);
			} else {
				// Get the transform from the set controls
				moveHorizontal = Input.GetAxis ("alt x axis " + controller_name);
				moveVeritcal = -1 * Input.GetAxis ("alt y axis " + controller_name);

				// Calculate and set the new movement variables
				Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVeritcal);
				rb.velocity = movement * game.player_configurations.speed;
				rb.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
			}
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
				game.FreezeControls(game.player_configurations.controls_delay_on_hit);

			}
        }
    }
}