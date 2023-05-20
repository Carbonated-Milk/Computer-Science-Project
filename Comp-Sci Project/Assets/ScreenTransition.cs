using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ScreenTransition : MonoBehaviour
{
    public static ScreenTransition singleton;

    [Header("Objects")]
    public RectTransform arrow;
    public RectMask2D mask;

    private void Awake()
    {
        if (singleton == null || singleton == this)
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }

    private float arrowSize;
    private void Start()
    {
        arrowSize = arrow.rect.width / 5;
    }

    public void TransitionIN(float transitionTime = 1f)
    {
        Transition(true, transitionTime);
    }

    public void TransitionOUT(float transitionTime = 1f)
    {
        Transition(false, transitionTime);
    }

    private void Transition(bool inTransition, float transitionTime = 1f)
    {
        StartCoroutine(MoveMask(transitionTime, inTransition));
    }

    private bool isRunning;

    private IEnumerator MoveMask(float timeLength, bool isIn)
    {
        if (isRunning) yield break;
        isRunning = true;

        SetActiveChildren(true);

        arrow.DOKill();

        float halfSize = isIn ? Screen.width / 2 : (Screen.width + arrowSize) / 2;
        arrow.localPosition = Vector3.left * halfSize;

        float endValue = !isIn ? Screen.width : Screen.width + arrowSize;
        Tween arrowMove = arrow.DOMoveX(endValue, timeLength).SetEase(Ease.InSine).SetUpdate(true);

        float halfScreen = Screen.width / 2;
        while (arrowMove.active)
        {
            Vector4 paddingSize = isIn ? new Vector4(1, 0, 0, 0) : new Vector4(0, 0, 1, 0);
            float padMult = isIn ? arrow.localPosition.x + halfScreen: halfScreen - arrow.localPosition.x;
            paddingSize *= padMult;
            mask.padding = paddingSize;
            yield return null;
        }

        //one last time because it would leave off a little
        Vector4 paddingSize2 = isIn ? new Vector4(1, 0, 0, 0) : new Vector4(0, 0, 1, 0);
        float padMult2 = isIn ? arrow.localPosition.x + halfScreen : halfScreen - arrow.localPosition.x;
        paddingSize2 *= padMult2;
        mask.padding = paddingSize2;

        isRunning = false;
        if (isIn) SetActiveChildren(false);
    }
    private void SetActiveChildren(bool active)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(active);
        }
    }
}
