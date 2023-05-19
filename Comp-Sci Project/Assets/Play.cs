using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Play : MonoBehaviour
{
    public static Play singleton;

    public RectTransform play;
    public RectTransform bar;
    public RectMask2D mask;
    private void Awake()
    {
        singleton = this;
        if(GameManager.loaded)
        {
            QuickDisable();
            return;
        }

        GameManager.OpenSave();
        play.DOScale(1.3f, 1).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }
    public void MaskOut()
    {
        bar.DOMoveX(8000, 2).SetEase(Ease.InCubic);
        StartCoroutine(MoveMask());
    }

    private IEnumerator MoveMask()
    {
        float time = Time.time;
        while (Time.time - time < 2)
        {
            mask.padding = new Vector4(bar.position.x, 0,0,0);
            yield return null;
        }
        gameObject.SetActive(false);
    }

    public void QuickDisable()
    {
        gameObject.SetActive(false);
    }
}
