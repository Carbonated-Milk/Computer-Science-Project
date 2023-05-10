using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [Header("Level Settings")]
    public float levelTime = 30;

    public static LevelManager singleton;

    private void Awake()
    {
        singleton = this;
    }
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine(CountDown());
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();

    }

    private float remainingTime;

    public IEnumerator CountDown()
    {
        remainingTime = levelTime;
        while (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;

            yield return null;
        }
        timeText.text = "Time's Up";
        PlayerHealth.singleton.Die();
        Debug.Log("lose");
    }

    [Header("UI Stuff")]
    public TMP_Text timeText;
    public TMP_Text coinText;
    public void UpdateUI()
    {
        timeText.text = remainingTime + "s";
        coinText.text = Coin.collectedCoins + "/" + Coin.coinCount + " coins collected";
    }
}
