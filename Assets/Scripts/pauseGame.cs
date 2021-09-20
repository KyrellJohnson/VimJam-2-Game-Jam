using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class pauseGame : MonoBehaviour
{
    private PlayerControls playerControls;
    private bool isPaused = false;

    [SerializeField]
    private CanvasGroup options;

    [SerializeField]
    private AudioSource levelMusic;

    

    [SerializeField]
    private AudioSource pauseSound;

    [SerializeField]
    private CanvasGroup optionsGroup;

    private void Awake()
    {
        playerControls = new PlayerControls();
        options.alpha = 0;
        options.interactable = false;
        optionsGroup.alpha = 0;
        optionsGroup.interactable = false;
    }

    private void Start()
    {
      
        playerControls.Player.Pause.performed += _ => PauseGame();
    }
    
    public void disablePause()
    {
        playerControls.Player.Disable();
    }

    public void PauseGame()
    {
        if(isPaused == false) // pause game
        {
            // mute game
            levelMusic.Pause();
            pauseSound.Play();
            AudioListener.volume = 0f;
            //stop game
            Time.timeScale = 0;
            playerControls.Player.Disable();
            //show  UI
            options.alpha = 1;
            options.interactable = true;
            isPaused = true;

        }
    }

    public void UnPause()
    {

        // unmute game
        levelMusic.UnPause();
        AudioListener.volume = 1f;
        //play game
        Time.timeScale = 1;
        playerControls.Player.Enable();

        //hide UI
        options.alpha = 0;
        options.interactable = false;
        isPaused = false;
    }

    public void stopGame()
    {
        levelMusic.Pause();
        AudioListener.volume = 0f;
        Time.timeScale = 0;
    }

    public bool isGamePaused()
    {
        return isPaused;
    }



    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
       playerControls.Disable();
    }

    public void HidePauseScreen()
    {
        options.alpha = 0;
        options.interactable = false;
  
    }

    public void ShowPauseScreen()
    {
        options.alpha = 1;
        options.interactable = true;
        
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        UnPause();
    }
}
