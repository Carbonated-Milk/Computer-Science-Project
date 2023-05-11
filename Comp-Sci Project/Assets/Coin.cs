using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static int coinCount, collectedCoins = 0;

    private void Start()
    {
        coinCount++;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            AudioManager.singleton.Play("Coin");
            collectedCoins++;
            Destroy(gameObject);
        }
    }

    public static void ResetCoins()
    {
        coinCount = 0;
        collectedCoins = 0;
    }
}
