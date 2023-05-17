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

    private void Awake()
    {
        singleton = this;
        Coin.ResetCoins();
    }
    void Start()
    {
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
        Debug.Log("lose");
    }

    private bool gameWon = false;
    private bool gameLost = false;
    public void Win()
    {
        gameWon = true;
        gameUI.SetActive(false);
        win.SetActive(true);
        GameOver();
        AudioManager.singleton.Play("Win");
    }

    public void GameOver()
    {
        if (gameLost) return;
        gameLost = true;
        AudioManager.singleton.StopAllSongs();
        Player.singleton.enabled = false;
        StopCoroutine(countDown);
        if (gameWon)
        {
            return;
        }
        AudioManager.singleton.Play("PianoSlam");
        timeText.text = remainingTime > 0 ? "Game Over" : "Time's Up";

        gameUI.SetActive(false);
        death.SetActive(true);

        SetMouseFree(true);
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
}
