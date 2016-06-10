﻿using UnityEngine;
using System.Collections;

/*
 *	This class is the base class for the player controllers both AI and player based. It does not 
 * initalize the variables by default this is just a delegate class. It will rely upon the game controller
 * to set up the data and inital configurations.
*/
public class controller : MonoBehaviour {

	public game_controller game;		// This gives a reference to the game to asertain the global player variables

	// MeshFilters for the diferent forms of the player
	public MeshFilter rock;     // 0
	public MeshFilter paper;    // 1
	public MeshFilter scissors;  // 2

	private bool movement_enabled;		// This will track if the controller will allow the player to cause movement
	private int current_form;			// This tracks the current form of the player

	private float next_transform;		// This will track when the player is allowed to transform again

	public bool switchForm(int form){

		// If the transform ability is still on cooldown return failure (false)
		if (Time.time < next_transform) {
			return false;
		}

		// Switch forms based on the argument
		switch (form) {
		case 1:
			current_form = 1;
			GetComponent<MeshFilter> ().mesh = rock.sharedMesh;
			break;
		case 2:
			current_form = 2;
			GetComponent<MeshFilter> ().mesh = paper.sharedMesh;
			break;
		case 3:
			current_form = 3;
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
}
