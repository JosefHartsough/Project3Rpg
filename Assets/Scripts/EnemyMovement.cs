using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

// TODO: Implement different states. Still need to work on:death, attack1, attack2, itemUse1,itemUse2.
// Enum for our enemys state. (This is a state machine)
public enum StateOfEnemy
{
    idle,
    moving,
    attack,
    interact

}

// Main class to manage our enemys movement
/* <summary>
 * Has all the variables for our enemy.
 * <function> void Start() -This function is called everytime we start
 * <function> void Update() -This function is calle every frame
 * </summary>
 */
public class EnemyMovement : MonoBehaviour
{
    // Varaible for state of enemy
    public StateOfEnemy enemyState;

    // Variable for speed of the enemy
    public float speed;

    // Variable for rigidbody movement
    private Rigidbody2D rigidBody;

    // Vector for how much enemys position changes
    private Vector3 changeInPosition;

    // position of enemy
    private Vector3 enemy_position;

    // Variable to access the enemys animations
    private Animator enemyAnimations;

    // Get the player sprite
    private GameObject player;
    private Vector3 player_position;

    // Start is called before the first frame update
    void Start()
    {
        enemyState = StateOfEnemy.idle;
        enemyAnimations = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Every frame resets how much the enemy has changed
        changeInPosition = Vector3.zero;
        // reset the positions of the player and enemy
        enemy_position = transform.position;
        player_position = player.transform.position;
        determineMove(player_position, enemy_position);
        checkState();
        UpdateAnimationAndMovement();
    }

    void determineMove(Vector3 player_position, Vector3 enemy_position){
        Debug.Log("player poostin: " + player_position);
        Debug.Log("enemy poostin: " + enemy_position);
    }

    void checkState(){
        // Checks if our current state is moving, make sures the animation is set to move
        switch (enemyState){
            case StateOfEnemy.idle:
                Debug.Log("state is idle");
                UpdateAnimationAndMovement();
                enemyAnimations.SetBool("Attacking", false);
                enemyAnimations.SetBool("Running", false);
                break;

            case StateOfEnemy.moving:
                Debug.Log("state is moving");
                UpdateAnimationAndMovement();
                enemyAnimations.SetBool("Running", true);
                break;

            case StateOfEnemy.attack:
                Debug.Log("state is attacking");
                enemyAnimations.SetBool("Attacking", true);
                StartCoroutine(animationCoroutine());
                UpdateAnimationAndMovement();
                break;

            default:
                Debug.Log("Default case");
                break;
        }
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
        }
        else
        {
            // enemyAnimations.SetBool("moving", false);
        }
    }
    // Function for enemy movement. Change speed variable in Unity
    void MoveCharacter()
    {
        changeInPosition.Normalize();
        rigidBody.MovePosition(transform.position + changeInPosition * speed * Time.deltaTime);
    }
    // These are called Coroutines. They are used for animations.
    // Creates a small delay between the attack animations and moving.
    private IEnumerator animationCoroutine()
    {
        // Wait for 1 frame
        yield return null;
        // Leave false, otherwise our enemy goes back into the movement state
        // enemyAnimations.SetBool("Attacking", false);
        // Waits an additional 3 seconds and sets our state back to moving for a more fluid animation
        yield return new WaitForSeconds(0.3f);
        enemyState = StateOfEnemy.idle;
    }
}
