using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ZAST_PlayerController: MonoBehaviour
{

    public float speed;             //Floating point variable to store the player's movement speed.
    public Text countText;          //Store a reference to the UI Text component which will display the number of pickups collected.
    public Text endText;            //Store a reference to the UI Text component which will display the 'You win' message.
    public Text timerText;
    public AudioClip Audio;
    //public bool gameOn = true;
    AudioSource audioSource;


    private GameObject GameLoader;
    private float time;
    private float timer;
    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.
    private int count;              //Integer to store the number of pickups collected so far.


    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();

        //Initialize count to zero.
        count = 0;

        //Initialze winText to a blank string since we haven't won yet at beginning.
        endText.text = "";

        //Call our SetCountText function which will update the text with the current value for count.
        SetCountText();

        audioSource = GetComponent<AudioSource>();
        timer = 0;

    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis("Vertical");

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (gameObject.CompareTag("Player"))
                rb2d.gravityScale = 1;
        }

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.AddForce(movement * speed);

        timer = timer + Time.deltaTime;

        timerText.text = "Timer: " + timer.ToString("N2");

        if (timer >= 10 && count < 1)
        {
            endText.text = "You Lose! :(";
            //GameLoader.AddScore(0);
            //StartCoroutine(ByeAfterDelay(2));

        }
    }

    //OnTriggerEnter2D is called whenever this object overlaps with a trigger collider.
    void OnTriggerEnter2D(Collider2D other)
    {
        //Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
        if (other.gameObject.CompareTag("Pickup"))
        {
           
            audioSource.PlayOneShot(Audio, 0.7f);
        }

        //Add one to the current value of our count variable.
        count = 1;

        //Update the currently displayed count by calling the SetCountText function.
        SetCountText();
    }

    //This function updates the text displaying the number of objects we've collected and displays our victory message if we've collected all of them.
    void SetCountText()
    {
        //Set the text property of our our countText object to "Count: " followed by the number stored in our count variable.
        countText.text = "Count: " + count.ToString();

        //Check if we've collected all 12 pickups. If we have...
        if (count >= 1)
        {
            //... then set the text property of our winText object to "You win!"
            endText.text = "You win!";
            //GameLoader.AddScore(10);
            //StartCoroutine(ByeAfterDelay(2));
        }
    }

    /*IEnumerator ByeAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        GameLoader.gameOn = false;
    }*/
}