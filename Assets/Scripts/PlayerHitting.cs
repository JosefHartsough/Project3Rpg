using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerHitting : MonoBehaviour
{
    public int damagePerSwing = 20;                  // The damage inflicted by each bullet.
    public float timeBetweenSwings = 0.15f;        // The time between each shot.
    public float range = 100f;                      // The distance the gun can fire.
    float timer;                                    // A timer to determine when to fire.

    private GameObject[] enemies;

    private Vector3 player_position;

    void Awake()
    {
        enemies = new GameObject[50];
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // get our current position
        player_position = transform.position;

        // Get the enemy sprite
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject enemy in enemies) {
            if (enemy != null){
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                Vector3 enemy_position = enemy.transform.position;
                // determine enemy distance
                double dist_to_enemy_x = (double)(player_position.x - enemy_position.x);
                double dist_to_enemy_y = (double)(player_position.y - enemy_position.y);
                double diagonal_distance = Math.Sqrt(dist_to_enemy_x * dist_to_enemy_x + dist_to_enemy_y * dist_to_enemy_y);

                // If the Fire1 button is being press and it's time to fire...
                if(Input.GetButton ("attack") && timer >= timeBetweenSwings && diagonal_distance < 2.5)
                {
                    // ... swing your sword.
                    timer = 0f;
                    enemyHealth.TakeDamage(damagePerSwing);
                }
            }
        }
    }
}
