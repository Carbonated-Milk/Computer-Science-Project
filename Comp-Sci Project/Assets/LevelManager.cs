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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
    public void Win()
    {
        gameWon = true;
        gameUI.SetActive(false);
        win.SetActive(true);
        GameOver();
    }

    public void GameOver()
    {
        AudioManager.singleton.Stop("Speedy");
        Player.singleton.enabled = false;
        StopCoroutine(countDown);
        if (gameWon) return;
        timeText.text = remainingTime > 0 ? "Game Over" : "Time's Up";

        gameUI.SetActive(false);
        death.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
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
        gameUI.SetActive(true);
        death.SetActive(false);

        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }
}
