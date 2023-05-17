using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseOptions : MonoBehaviour
{
    public float maxVolume = 2;
    public float maxSensitivity = 2;

    private GameObject pauseCanvas;
    void Start()
    {
        pauseCanvas = transform.GetChild(0).gameObject;
    }

    private bool isOpen = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab) && LevelManager.gameState == LevelManager.GameState.Playing)
        {
            if (!isOpen) OpenPauseMenu();
            else ReturnToGame();
        }

    }

    public void SetVolume(float amount)
    {
        AudioManager.ChangeMasterVolume(amount * maxVolume);
    }

    public void SetSensitivity(float amount)
    {
        Player.sensitivity = amount * maxSensitivity;
    }

    public void OpenPauseMenu()
    {
        LevelManager.SetMouseFree(true);
        pauseCanvas.SetActive(true);
        Time.timeScale = 0;
    }
    public void ReturnToGame()
    {
        LevelManager.SetMouseFree(false);
        pauseCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void ReturnToMainMenu()
    {
        GameManager.ReturnToLevelMenu();
    }
}
