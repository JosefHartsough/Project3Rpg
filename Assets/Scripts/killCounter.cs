using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Class to manage the killcount on the UI
public class killCounter : MonoBehaviour
{
    [SerializeField]
    GameObject WinUI;


    // Text for displaying the remaining enemies
    public Text enemyCountText;
    public Text winCountText;

    void Update() {
        // Array of our enemies. It will be
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        if (enemies != null)
        {
            enemyCountText.text = "Enemies Remaining: " + enemies.Length.ToString();
            if (enemies.Length == 0)
            {
                WinUI.gameObject.SetActive(true);
                winCountText.text = "You had " + enemies.Length.ToString() + " enemies remaining!";
            }
        }
        
    }
}
