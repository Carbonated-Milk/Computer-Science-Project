using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseOptions : MonoBehaviour
{
    [Header("Settings")]
    public float maxVolume = 2;
    public float maxSensitivity = 2;

    [Header("Sliders")]
    public Slider volume;
    public Slider sensitivity;

    private GameObject pauseCanvas;
    void Start()
    {
        pauseCanvas = transform.GetChild(0).gameObject;
        volume.value = SaveData.current.masterVolume;
        sensitivity.value = SaveData.current.sensitivity;
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
        SaveData.current.masterVolume = amount;
        SaveManager.OnSave();
    }

    public void SetSensitivity(float amount)
    {
        Player.sensitivity = amount * maxSensitivity;
        SaveData.current.sensitivity = amount;
        SaveManager.OnSave();
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
