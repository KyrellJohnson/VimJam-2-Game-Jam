using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerControl : MonoBehaviour
{


    [SerializeField]
    private AudioSource
        menuAudioClip,
        onHoverSource;
        

    [SerializeField]
    private GameObject optionsUI, mainMenuUI;

    private CanvasGroup optionsLayer, mainMenuLayer;

    private void Awake()
    {
        optionsLayer = optionsUI.GetComponent<CanvasGroup>();
        mainMenuLayer = mainMenuUI.GetComponent<CanvasGroup>();
        mainMenuLayer.alpha = 1;
        mainMenuLayer.interactable = true;
        optionsLayer.alpha = 0;
        optionsLayer.interactable = false;
        onHoverSource.mute = false;
    }

    public void StartGame()
    {

        
        //menuAudioClip.Play();
        SceneManager.LoadScene(1);
        
        
    }

    public void OptionsMenu()
    {
        mainMenuLayer.alpha = 0;
        mainMenuLayer.interactable = false;

        onHoverSource.mute = true;

        // Send to Options Menu
        Debug.Log(mainMenuUI.name);

        optionsLayer.alpha = 1;
        optionsLayer.interactable = true;
    }

    public void MainMenu()
    {
        // Send to main menu
        if(SceneManager.GetActiveScene().isLoaded.Equals(0))
        {
            onHoverSource.mute = false;
            optionsLayer.alpha = 0;
            optionsLayer.interactable = false;
            mainMenuLayer.alpha = 1;
            mainMenuLayer.interactable = true;

            // Send to Options Menu
            Debug.Log(mainMenuUI.name);

            
        }
        else
        {
            SceneManager.LoadScene(0);

        }
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
                // Application.Quit() does not work in the editor so
                // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                 Application.Quit();
        #endif

    }

    private void Load_Level_1()
    {

    }

    private void Load_Level_2()
    {

    }

    private void Load_Level_3()
    {

    }

    private void Load_Boss_Level()
    {

    }

    private void Load_Game_Over_Screen()
    {

    }


}
