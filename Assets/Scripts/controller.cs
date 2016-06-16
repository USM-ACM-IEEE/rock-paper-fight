using UnityEngine;
using System.Collections;

/*
 *	This class is the base class for the player controllers both AI and player based. By default this class will
 *	allow the players to move on start and will start them in rock form with the ability to transform right away
 *
 *	NOTE: The controller name must also be the name extension in the Input.Axis configurations
*/
public class controller : MonoBehaviour {

	public game_controller game;		// This gives a reference to the game to asertain the global player variables
	public string controller_name;		// This will be used to identify which controller each instance of a controller object is controllign


	// MeshFilters for the diferent forms of the player
	public MeshFilter rock;     	// 0
	public MeshFilter paper;    	// 1
	public MeshFilter scissors;  	// 2

	private bool movement_enabled;		// This will track if the controller will allow the player to cause movement
	private int current_form;			// This tracks the current form of the player

	private float next_transform;		// This will track when the player is allowed to transform again

	protected virtual void Start()
	{
		// Ensures they are in rock form with the movement enabled and ability to transform up
		switchForm (0);
		next_transform = Time.time;
		movement_enabled = true;
	}

	public bool switchForm(int form){

		// If the transform ability is still on cooldown return failure (false)
		if (Time.time < next_transform) {
			return false;
		}

		// Switch forms based on the argument
		switch (form) {
		case 0:
			current_form = 0;
			GetComponent<MeshFilter> ().mesh = rock.sharedMesh;
			break;
		case 1:
			current_form = 1;
			GetComponent<MeshFilter> ().mesh = paper.sharedMesh;
			break;
		case 2:
			current_form = 2;
			GetComponent<MeshFilter> ().mesh = scissors.sharedMesh;
			break;
		}

		// Set the next time the player can transform
		next_transform = Time.time + game.player_configurations.transform_cooldown;

		// Return succsess
		return true;
	}

	// Returns the form the player is currently in
	public int getForm(){
		return current_form;
	}

	// This fucntion returns if the player is allowed to move
	public bool canMove(){
		return movement_enabled;
	}

	// This function sets whether the player is allowed to move
	public void setCanMove(bool enabled){
		movement_enabled = enabled;
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
