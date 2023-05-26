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
        Time.timeScale = 1;
        play.DOScale(1.3f, 1).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        singleton = this;
        running = false;

        if (GameManager.loaded)
        {
            QuickDisable();
            return;
        }

        GameManager.OpenSave();
    }
    public void MaskOut()
    {
        if (running) { return; }
        running = true;

        StopAllCoroutines();
        StartCoroutine(MoveMask());
    }

    public void MaskBack()
    {
        if (running) { return; }
        running = true;

        gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(MoveMaskBack());
    }

    private float transitionTime = 1;
    private bool running = false;
    private IEnumerator MoveMask()
    {
        bar.DOKill();
        bar.DOMoveX(Screen.width + bar.rect.width * 2, transitionTime).SetEase(Ease.InCubic);

        float time = Time.time;
        while (Time.time - time < transitionTime)
        {
            mask.padding = new Vector4(bar.position.x, 0,0,0);
            yield return null;
        }
        gameObject.SetActive(false);
        running = false;
    }

    private IEnumerator MoveMaskBack()
    {
        bar.DOKill();
        bar.DOMoveX(0, transitionTime).SetEase(Ease.OutCubic);

        float time = Time.time;
        while (Time.time - time < transitionTime)
        {
            mask.padding = new Vector4(bar.position.x, 0, 0, 0);
            yield return null;
        }

        running = false;
    }

    public void QuickDisable()
    {
        bar.DOMoveX(Screen.width + bar.rect.width * 2, 0);
        gameObject.SetActive(false);
    }
}
