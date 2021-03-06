﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{

    public int startingHealth = 100;                            // The amount of health the player starts the game with.
    public int currentHealth;                                   // The current health the player has.
    public Slider healthSlider;                                 // Reference to the UI's health bar.
    public int regenAmount = 5;                                 // The amount of health the player will regenerate per second.
    public int regenWait = 2;                                   // The time the player must wait after damage to regen.
    private float timeSinceDamage = -1;                         // The last time the player was hit
    private float regeneratedHealth;                            // Health amount regenerated.
    public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
    public AudioClip deathClip;                                 // The audio clip to play when the player dies.
    public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.

    Animator anim;                                              // Reference to the Animator component.
    AudioSource playerAudio;                                    // Reference to the AudioSource component.
    PlayerMovement playerMovement;                              // Reference to the player's movement.
   // PlayerShooting playerShooting;                              // Reference to the PlayerShooting script.
    bool isDead;                                                // Whether the player is dead.
    bool damaged;                                               // True when the player gets damaged.

    // Access UI object in Unity
    [SerializeField]
    GameObject DeathUI;


    void Awake()
    {
        // Setting up the references.
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
       // playerShooting = GetComponentInChildren<PlayerShooting>();

        // Set the initial health of the player.
        currentHealth = startingHealth;
    }

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        // If the player has just been damaged...
        if (damaged)
        {
            // ... set the colour of the damageImage to the flash colour.
            damageImage.color = flashColour;
        }
        // Otherwise...
        else
        {
            // ... transition the colour back to clear.
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        // Reset the damaged flag.
        damaged = false;

        //If player has lost health and is past wait time
        if (timeSinceDamage >= 0 && Time.time - timeSinceDamage >= regenWait)
        {
            Regen();
        }
    }

    public void TakeDamage(int amount)
    {
        // Set the damaged flag so the screen will flash.
        damaged = true;

        // Reduce the current health by the damage amount.
        currentHealth -= amount;

        // Set the health bar's value to the current health.
        healthSlider.value = currentHealth;

        // Play the hurt sound effect.
        // playerAudio.Play();

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if (currentHealth <= 0 && !isDead)
        {
            // ... it should die.
            Death();
        }
        //Set time last taken damage
        timeSinceDamage = Time.time;

    }

    void Regen()
    {
        regeneratedHealth+= regenAmount * Time.deltaTime;
        int flooredRegeneratedHealth = Mathf.FloorToInt(regeneratedHealth);
        regeneratedHealth -= flooredRegeneratedHealth;
        Heal(flooredRegeneratedHealth);
    }

    void Heal(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, startingHealth);
        healthSlider.value = currentHealth;

    }

    void Death()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;

        // Turn off any remaining shooting effects.
        //playerShooting.DisableEffects();

        // Tell the animator that the player is dead.
        // anim.SetTrigger("Die");

        // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
        // playerAudio.clip = deathClip;
        // playerAudio.Play();

        // Turn off the movement and shooting scripts.
        playerMovement.enabled = false;
        //playerShooting.enabled = false;

       // Enables the Death UI gameobject in Unity
       DeathUI.gameObject.SetActive(true);
    }

}
