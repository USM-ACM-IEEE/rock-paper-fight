using UnityEngine;
using System.Collections;

public class player_controller : MonoBehaviour
{
	public game_controller game;

    // GameObject for the diferent forms of the player
    public GameObject rock;     // 0
    public GameObject paper;    // 1
    public GameObject scissor;  // 2

    // The oppenents script for passing information between one another
    public player_controller opponent;
	public string other_player;
	public int player;
	public string transform_button;

    // Tacks the current state of the player
    private int current_form = 0;

    private Rigidbody rb;                   // This will be the ridgidbody to update the players position
    private float nextTransfrom = 0.0F;     // This will hold the next time when the player is allowed to transform
    public float transfromDelay;            // This is the delay between being allowed to transform
    public float speed;                     // Variable to alter the max speed of an object
    public int crit_damage;                 // The multiplier for when the super effective opponent is hit

    public int health;                      // This is the health the player will start with it will also track the players current health
	public bool canMove;

    void Start()
    {
		GameObject _game = GameObject.Find("Game Controller");
		game = _game.GetComponent<game_controller> ();

        // Initalize the ridgid body
        rb = GetComponent<Rigidbody>();

		GameObject opp = GameObject.Find(other_player);
        opponent = opp.GetComponent<player_controller>();

		canMove = true;
    }

    void FixedUpdate()
    {
		if (canMove) 
		{
			// Get the transform from the set controls
			float moveHorizontal = Input.GetAxis("Horizontal_" + player.ToString());
			float moveVeritcal = Input.GetAxis("Vertical_" + player.ToString());

			// Calculate and set the new movement variables
			Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVeritcal);
			rb.velocity = movement * speed;
			rb.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
		}
    }

    void Update()
    {
        // If the F key is hit transform the object
		if(Input.GetKey (transform_button) && Time.time > nextTransfrom)
        {
            transformation();
            nextTransfrom = Time.time + transfromDelay;
        }

        if (health <= 0)
        {
            // If the player has run out of health destroy the player
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
			if (opponent.getCurrentForm () == (current_form + 1) % 3) 
			{
				health -= 10 * crit_damage;
			} 
			else 
			{
				// Stop players from moving
				game.FreezeControlls(0.5F);
			}
        }
    }

    void transformation()
    {
        // Iterate the current form
        current_form = (current_form + 1) % 3;

        // Switch the object to the new form
        if (current_form == 0)
        {
            GetComponent<MeshFilter>().mesh = rock.GetComponent<MeshFilter>().sharedMesh;
        }
        else if (current_form == 1)
        {
            GetComponent<MeshFilter>().mesh = paper.GetComponent<MeshFilter>().sharedMesh;
        }
        else if (current_form == 2)
        {
            GetComponent<MeshFilter>().mesh = scissor.GetComponent<MeshFilter>().sharedMesh;
        }
    }

    public int getCurrentForm()
    {
        // Returns the current form of the player
        return current_form;
    }

    public int getHealth()
    {
        // Returns the health of the player
        return health;
    }
}