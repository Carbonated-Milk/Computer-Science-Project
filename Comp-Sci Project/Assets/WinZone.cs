using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class WinZone : MonoBehaviour
{
    public TMP_Text needMoreCoins;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (!Coin.CollectedAll())
        {
            needMoreCoins.text = "Only Collected " + Coin.collectedCoins + "/" + Coin.coinCount + " coins";
            needMoreCoins.DOKill();
            needMoreCoins.DOFade(1, 1f);
            return;
        }
        LevelManager.singleton.Win();
        Destroy(GetComponent<Collider>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (!Coin.CollectedAll())
        {
            needMoreCoins.DOKill();
            needMoreCoins.DOFade(0, 1f);
        }
    }
}
