using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class game_controller : MonoBehaviour {

    // holds a refernce to the scripts for each player
    public player_controller player1;
    public player_controller player2;

    // Holds a reference to the players heath text
    public Text player1_health_text;
    public Text player2_health_text;

	// Use this for initialization
	void Start () 
    {
        // Initialize the scripts
        GameObject player_1 = GameObject.Find("player_1");
        player1 = player_1.GetComponent<player_controller>();

        GameObject player_2 = GameObject.Find("player_2");
        player2 = player_2.GetComponent<player_controller>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        player1_health_text.text = player1.getHealth().ToString();
        player2_health_text.text = player2.getHealth().ToString();
	}

	public void FreezeControlls(float delay)
	{
		player1.canMove = false;
		player2.canMove = false;

		StartCoroutine (UnfreezeControlls(delay));
	}

	IEnumerator UnfreezeControlls(float delay)
	{
		yield return new WaitForSeconds(delay);

		player1.canMove = true;
		player2.canMove = true;
	}
}
