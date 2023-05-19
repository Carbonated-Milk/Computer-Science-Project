using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Play : MonoBehaviour
{
    public static Play singleton;

    public RectTransform bar;
    public RectMask2D mask;
    private void Awake()
    {
        singleton = this;
        if(GameManager.loaded)
        {
            QuickDisable();
        }
    }
    public void MaskOut()
    {
        bar.DOMoveX(8000, 2).SetEase(Ease.InCubic);
        StartCoroutine(MoveMask());
    }

    private IEnumerator MoveMask()
    {
        GameManager.loaded = true;
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
