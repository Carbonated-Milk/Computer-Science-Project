using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Level Settings")]
    public float levelTime = 30;

    public static LevelManager singleton;

    public static GameState gameState;
    public enum GameState
    {
        Playing,
        Paused,
        Win,
        Lose
    }

    private void Awake()
    {
        singleton = this;
        Coin.ResetCoins();
        gameState = GameState.Playing;
    }
    void Start()
    {
        Time.timeScale = 1;
        AudioManager.singleton.Play("Speedy");
        SetMouseFree(false);
        countDown = StartCoroutine(CountDown());
    }

    void Update()
    {
        UpdateUI();
    }

    private float remainingTime;

    private Coroutine countDown;

    public IEnumerator CountDown()
    {
        remainingTime = levelTime;
        while (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            timeText.text = remainingTime + "s";
            yield return null;
        }
        GameOver();
        PlayerHealth.singleton.Die();
    }
    public void Win()
    {
        if (gameState != GameState.Playing) return;
        gameState = GameState.Win;
        gameUI.SetActive(false);
        win.SetActive(true);
        StopGamePlay();

        AudioManager.singleton.Play("Win");
    }

    public void GameOver()
    {
        if (gameState != GameState.Playing) return;
        gameState = GameState.Lose;

        StopGamePlay();
        AudioManager.singleton.Play("PianoSlam");
        timeText.text = remainingTime > 0 ? "Game Over" : "Time's Up";

        gameUI.SetActive(false);
        death.SetActive(true);
    }

    private void StopGamePlay()
    {
        SetMouseFree(true);
        AudioManager.singleton.StopAllSongs();
        Player.singleton.enabled = false;
        StopCoroutine(countDown);
    }

    [Header("UI Stuff")]
    public TMP_Text timeText;
    public TMP_Text coinText;
    public void UpdateUI()
    {
        coinText.text = Coin.collectedCoins + "/" + Coin.coinCount + " coins collected";
    }

    [Header("UI Windows")]
    public GameObject gameUI;
    public GameObject win;
    public GameObject death;
    public void Reset()
    {
        AudioManager.singleton.StopAllSongs();

        gameUI.SetActive(true);
        death.SetActive(false);

        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }

    public static void SetMouseFree(bool isFree)
    {
        if(isFree)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void NextLevel()
    {
        GameManager.NextLevel();
    }

    public void MainMenu()
    {
        GameManager.ReturnToLevelMenu();
    }
}
