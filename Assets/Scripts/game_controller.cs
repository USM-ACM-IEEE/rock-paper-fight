using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class playerConfig: System.Object {
	public float transform_cooldown;
	public float speed;
	public float controlls_delay_on_hit;
}


public class game_controller : MonoBehaviour {

    // holds a refernce to the scripts for each player
	public controller player1;
    public controller player2;

	public string player1_object_name;
	public string player2_object_name;

	public playerConfig player_configurations;

	// Use this for initialization
	void Start () 
    {
        // Initialize the scripts
		GameObject player_1 = GameObject.Find(player1_object_name);
        player1 = player_1.GetComponent<controller>();

		GameObject player_2 = GameObject.Find(player2_object_name);
        player2 = player_2.GetComponent<controller>();
	}

	// This function will stop the players from adding movement to their respective objects
	public void FreezeControlls(float delay)
	{
		player1.setCanMove(false);
		player2.setCanMove(false);

		StartCoroutine (UnfreezeControlls(delay));
	}

	// This is a coroutine that will unfreeze the players controllers after the given number of seconds
	IEnumerator UnfreezeControlls(float delay)
	{
		yield return new WaitForSeconds(delay);

		player1.setCanMove(true);
		player2.setCanMove(true);
	}
}
