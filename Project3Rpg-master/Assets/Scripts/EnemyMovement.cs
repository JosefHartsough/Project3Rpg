﻿using System.Collections;
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

    // Variable for health of the enemy
    public float health;
    // Variable for max health of enemy set in Unity (deafult 2)
    public FloatValue maxHealth;

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

    // track enemy position
    private List<Vector3> move_history;
    private bool good_move;
    private int frames_since_good_move;

    // Used in modular math to keep track of frames
    private int frame_count;

    EnemyAttack enemyAttack;

    private void Awake()
    {
        health = maxHealth.intialValue;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyState = StateOfEnemy.idle;
        enemyAnimations = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        move_history = new List<Vector3>();
        good_move = true;
        frame_count = 0;
        frames_since_good_move = 0;

        enemyAttack = GetComponent<EnemyAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        // Every frame resets how much the enemy has changed
        changeInPosition = Vector3.zero;
        // reset the positions of the player and enemy
        enemy_position = transform.position;
        // Debug.Log(transform.name + " position: " + enemy_position);
        move_history.Add(enemy_position);
        player_position = player.transform.position;

        determineMove(player_position, enemy_position);
        checkState();
        UpdateAnimationAndMovement();

        // update the frame count
        frame_count++;
    }

    void determineMove(Vector3 player_position, Vector3 enemy_position){
        double dist_to_player_x = (double)(player_position.x - enemy_position.x);
        double dist_to_player_y = (double)(player_position.y - enemy_position.y);
        double diagonal_distance = Math.Sqrt(dist_to_player_x * dist_to_player_x + dist_to_player_y * dist_to_player_y);
        // Debug.Log("diagonal_distance to player: " + diagonal_distance);
        determineGoodOrBadMove();

        // If the enemy is too far away, don't move
        if (diagonal_distance > 20 ){
            enemyState = StateOfEnemy.idle;
            changeInPosition.x = 0;
            changeInPosition.y = 0;
        }
        // When the enemy is closer than the furthest distance not on top of the player
        else if (diagonal_distance > 1.5 && diagonal_distance <= 20){
            enemyState = StateOfEnemy.moving;

            // if it's a good move, take the shortest way, otherwise switch
            if (good_move) {
                // I am further from the player in the x direction
                if (Math.Abs(dist_to_player_x) >= Math.Abs(dist_to_player_y)){

                    // negative x means the player is to my left
                    if (dist_to_player_x < 0) {
                        changeInPosition.x = -1;
                    }
                    // positive x means the player is to my right
                    else {
                        changeInPosition.x = 1;
                    }
                }
                // I am further from the player in the y direction
                else {

                    // negative y means the player is below me
                    if (dist_to_player_y < 0){
                        changeInPosition.y = -1;
                    }
                    // positive y means the player is above me
                    else {
                        changeInPosition.y = 1;
                    }
                }
            }
            // we are getting stuck so we need to do the opposite
            else {
                // I am further from the player in the x direction
                if (Math.Abs(dist_to_player_x) < Math.Abs(dist_to_player_y)){

                    // negative x means the player is to my left
                    if (dist_to_player_x < 0) {
                        changeInPosition.x = -1;
                    }
                    // positive x means the player is to my right
                    else {
                        changeInPosition.x = 1;
                    }
                }
                // I am further from the player in the y direction
                else {

                    // negative y means the player is below me
                    if (dist_to_player_y < 0){
                        changeInPosition.y = -1;
                    }
                    // positive y means the player is above me
                    else {
                        changeInPosition.y = 1;
                    }
                }
            }
        }
        // I am close to the player, so stop moving
        else {
            enemyState = StateOfEnemy.attack;
            changeInPosition.x = 0;
            changeInPosition.y = 0;
        }
    }

    // Will tell us whether the enemy is running into walls or not
    void determineGoodOrBadMove(){
        if (move_history.Count > 100 && enemyState == StateOfEnemy.moving){
            // Debug.Log(Math.Abs(move_history[move_history.Count - 1].x - move_history[move_history.Count - 10].x));
            if (Math.Abs(move_history[move_history.Count - 1].x - move_history[move_history.Count - 20].x) < 0.2 &&
                Math.Abs(move_history[move_history.Count - 1].y - move_history[move_history.Count - 20].y) < 0.2 )
            {
                good_move = false;
                frames_since_good_move = 0;
            }
        }

        // doing this so we stay in the correctional way a little longer
        frames_since_good_move++;
        if (frames_since_good_move > 20){
            good_move = true;
        }
        if (move_history.Count > 121){
            move_history.RemoveRange(0, 20);
        }
    }

    void checkState(){
        // Checks if our current state is moving, make sures the animation is set to move
        switch (enemyState){
            case StateOfEnemy.idle:
                UpdateAnimationAndMovement();
                // enemyAttack.playerInRange = false;
                enemyAnimations.SetBool("Idle", true);
                enemyAnimations.SetBool("Attacking", false);
                enemyAnimations.SetBool("Running", false);
                break;

            case StateOfEnemy.moving:
                // Debug.Log("state is moving");
                enemyAttack.playerInRange = false;
                enemyAnimations.SetBool("Idle", false);
                enemyAnimations.SetBool("Running", true);
                enemyAnimations.SetBool("Attacking", false);
                UpdateAnimationAndMovement();
                break;

            case StateOfEnemy.attack:
                // Debug.Log("state is attacking");
                enemyAttack.playerInRange = true;
                enemyAnimations.SetBool("Idle", false);
                enemyAnimations.SetBool("Attacking", true);
                enemyAnimations.SetBool("Running", false);
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