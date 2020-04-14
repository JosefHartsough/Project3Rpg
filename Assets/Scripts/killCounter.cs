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

    void Start(){
        if (enemyCountText != null){
            enemyCountText.text = "Enemies Remaining: ";
        }
    }

    void Update() {
        // Array of our enemies. It will be
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        if (enemies != null)
        {
            if (enemyCountText != null) {
                enemyCountText.text = "Enemies Remaining: " + enemies.Length.ToString();
            }
            if (enemies.Length == 0)
            {
                WinUI.gameObject.SetActive(true);
                if (winCountText != null){
                    winCountText.text = "You had " + enemies.Length.ToString() + " enemies remaining!";
                }
            }
        }

    }
}
