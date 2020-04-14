using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Implement different states. Still need to work on:death, attack1, attack2, itemUse1,itemUse2.
// Enum for our players state. (This is a state machine)
public enum StateOfPlayer
{
    walk,
    attack,
    interact

}

// Main class to manage our players movement
/* <summary>
 * Has all the variables for our player.
 * <function> void Start() -This function is called everytime we start
 * <function> void Update() -This function is calle every frame
 * </summary>
 */
public class PlayerMovement : MonoBehaviour
{
    // Varaible for state of player
    public StateOfPlayer playerState;

    // Variable for speed of the player
    public float speed;

    // Variable for rigidbody movement
    private Rigidbody2D rigidBody;

    // Vector for how much players position changes
    private Vector3 changeInPosition;

    // Variable to access the players animations
    private Animator playerAnimations;

    // Start is called before the first frame update
    void Start()
    {
        playerState = StateOfPlayer.walk;
        playerAnimations = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        playerAnimations.SetFloat("moveX", 0);
        playerAnimations.SetFloat("moveY", -1);
    }

    // Update is called once per frame
    void Update()
    {
        // Every frame resets how much the player has changed
        changeInPosition = Vector3.zero;
        changeInPosition.x = Input.GetAxis("Horizontal");
        changeInPosition.y = Input.GetAxis("Vertical");
        if (Input.GetButtonDown("attack") && playerState != StateOfPlayer.attack)
        {
            StartCoroutine(animationCoroutine());
        }
        // Checks if our current state is walk, make sures the animation is set to move
        else if(playerState == StateOfPlayer.walk)
        {

            UpdateAnimationAndMovement();
        }
        UpdateAnimationAndMovement();
    }
    // These are called Coroutines. They are used for animations.
    // Creates a small delay between the attack animations and walking.
    private IEnumerator animationCoroutine()
    {
        playerAnimations.SetBool("attacking", true);
        playerState = StateOfPlayer.attack;
        // Wait for 1 frame
        yield return null;
        // Leave false, otherwise our player goes back into the movement state
        playerAnimations.SetBool("attacking", false);
        // Waits an additional 3 seconds and sets our state back to walk for a more fluid animation
        yield return new WaitForSeconds(0.3f);
        playerState = StateOfPlayer.walk;
    }
    /* <summary>
     * Changes the variables set in Unity to change our X
     * position and our y position to our variables.
     * </summary>
     */
    void UpdateAnimationAndMovement()
    {
        if (changeInPosition != Vector3.zero)
        {
            MoveCharacter();
            playerAnimations.SetFloat("moveX", changeInPosition.x);
            playerAnimations.SetFloat("moveY", changeInPosition.y);
            playerAnimations.SetBool("moving", true);

        }
        else
        {
            playerAnimations.SetBool("moving", false);
        }
    }
    // Function for player movement. Change speed variable in Unity
    void MoveCharacter()
    {
        changeInPosition.Normalize();
        rigidBody.MovePosition(transform.position + changeInPosition * speed * Time.deltaTime);
    }
}
