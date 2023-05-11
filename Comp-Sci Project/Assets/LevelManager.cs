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

    public void GameOver()
    {
        StopCoroutine(countDown);
        timeText.text = remainingTime > 0 ? "Game Over" : "Time's Up";
        died.SetActive(true);
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
    public GameObject died;
    public void Reset()
    {
        died.SetActive(false);
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }
}
