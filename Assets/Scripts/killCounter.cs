using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Class to manage the killcount on the UI
public class killCounter : MonoBehaviour
{
    // Array of our enemies. It will be
    GameObject[] enemies;

    // Text for displaying the remaining enemies
    public Text enemyCountText;

    void Update() {

        enemies = GameObject.FindGameObjectsWithTag("enemy");
        enemyCountText.text = "Enemies Remaining: " + enemies.Length.ToString();
    }
}
