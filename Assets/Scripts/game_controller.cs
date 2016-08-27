using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class playerConfig: System.Object {
	public float transform_cooldown;
	public float speed;
	public float controls_delay_on_hit;
}


public class game_controller : MonoBehaviour {

    // Holds a reference to the scripts for each player
	public controller player1;
    public controller player2;

	private GameObject player_1;
	private GameObject player_2;

	public int p1Score;
	public int p2Score;

	public string player1_object_name;
	public string player2_object_name;

	public Vector3 player1StartPosition;
	public Vector3 player2StartPosition;

	public playerConfig player_configurations;

	public float resetDelay = 1.5F;

	// Use this for initialization
	void Start () 
    {
        // Initialize the scripts
		player_1 = GameObject.Find(player1_object_name);
        player1 = player_1.GetComponent<controller>();
		player1StartPosition = player_1.GetComponent<Transform>().position;
		Debug.Log(player1StartPosition);

		player_2 = GameObject.Find(player2_object_name);
        player2 = player_2.GetComponent<controller>();
		player2StartPosition = player_2.GetComponent<Transform>().position;
		Debug.Log(player2StartPosition);
	}

	// This function will stop the players from adding movement to their respective objects
	public void FreezeControls(float delay)
	{
		player1.setCanMove(false);
		player2.setCanMove(false);

		StartCoroutine (UnfreezeControls(delay));
	}

	// This is a coroutine that will unfreeze the players controllers after the given number of seconds
	IEnumerator UnfreezeControls(float delay)
	{
		yield return new WaitForSeconds(delay);

		player1.setCanMove(true);
		player2.setCanMove(true);
	}

	//
	public void ResetGame()
	{
		StartCoroutine (ResetPositions());	 
	}

	IEnumerator ResetPositions()
	{
		yield return new WaitForSeconds(resetDelay);

		//Call the prompt for continue or reset

		player_1.GetComponent<Transform>().position = player1StartPosition;

		player_2.GetComponent<Transform>().position = player2StartPosition;

		//If reset, reset the player scores to 0
		/*if(NewGamePrompt())
		 	{
		  		p1Score = 0;
		  		p2Score = 0;
		    }
		    */
	}
}
