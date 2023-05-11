using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinZone : MonoBehaviour
{
    public TMP_Text needMoreCoins;
    private void OnTriggerEnter(Collider other)
    {
        if (!Coin.CollectedAll())
        {
            //needMoreCoins. NEED DOTWEEEN
        }
        LevelManager.singleton.Win();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!Coin.CollectedAll())
            LevelManager.singleton.Win();
    }
}
