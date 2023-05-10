using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static int coinCount, collectedCoins = 0;

    private void Awake()
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
}
