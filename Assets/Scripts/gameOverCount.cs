using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameOverCount : MonoBehaviour
{
    GameObject[] enemies;

    // Text for displaying the remaining enemies

    public Text winCountText;
    void Update()
    {
        
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        if (enemies != null)
        {
            winCountText.text = "Game Over!\nYou had " + enemies.Length.ToString() + " enemies remaining!";
        }
    }
}