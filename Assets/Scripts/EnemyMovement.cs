using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

// TODO: Implement different states. Still need to work on:death, attack1, attack2, itemUse1,itemUse2.
// Enum for our enemys state. (This is a state machine)
public enum StateOfEnemy
{
    walk,
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

    // Variable to access the enemys animations
    private Animator enemyAnimations;
    // Start is called before the first frame update

    void Start()
    {
        enemyState = StateOfEnemy.walk;
        enemyAnimations = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        // enemyAnimations.SetFloat("Run", 1);
        // enemyAnimations.SetBool("Run", true);
        enemyAnimations.SetBool("Running", true);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("x " + transform.position.x);
        // Debug.Log("y " + transform.position.y);
        // enemyAnimations.SetFloat("LightGuard_Run", 1);
        // Every frame resets how much the enemy has changed
        changeInPosition = Vector3.zero;
        // changeInPosition.x = 1;
        changeInPosition.y = -2;
        Debug.Log("is this a thing?" + transform.position[1]);
        if (transform.position.y < -10)
        {
            Debug.Log("are we getting in here?");
            changeInPosition.y = 0;
            enemyAnimations.SetBool("Running", false);
            // StartCoroutine(animationCoroutine());
        }
        // Checks if our current state is walk, make sures the animation is set to move
        else if (enemyState == StateOfEnemy.walk)
        {
            UpdateAnimationAndMovement();
            // enemyAnimations.SetBool("Run", true);
        }
        UpdateAnimationAndMovement();
    }
    // These are called Coroutines. They are used for animations.
    // Creates a small delay between the attack animations and walking.
    private IEnumerator animationCoroutine()
    {
        // enemyAnimations.SetBool("attacking", true);
        enemyState = StateOfEnemy.attack;
        // Wait for 1 frame
        yield return null;
        // Leave false, otherwise our enemy goes back into the movement state
        // enemyAnimations.SetBool("attacking", false);
        // Waits an additional 3 seconds and sets our state back to walk for a more fluid animation
        yield return new WaitForSeconds(0.3f);
        enemyState = StateOfEnemy.walk;
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
            // enemyAnimations.SetFloat("moveX", changeInPosition.x);
            // Debug.Log(enemyAnimations);
            // enemyAnimations.SetFloat("moveY", changeInPosition.y);
            // enemyAnimations.SetBool("moving", true);

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
}
