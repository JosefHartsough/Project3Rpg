using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



// Class for the Main Menu
public class StartMenu : MonoBehaviour
{
    [SerializeField]
    GameObject MainMenu;
    public void NewGame()
    {
        SceneManager.LoadScene("PlayerTestScene");
        
    }

    //public void QuitToDesktop()
    //{
    //    Application.Quit();
    //}
}
    
