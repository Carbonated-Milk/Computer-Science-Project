using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class Records : MonoBehaviour
{
    public static Records singleton;

    [Header("Finish Types")]
    public GameObject regular;
    public GameObject newRecord;

    [Header("Text")]
    public TMP_Text oldRecordText;
    public TMP_Text yourRecordText;
    public TMP_Text newRecordText;

    private void Awake()
    {
        singleton = this;

    }
    public void Display(float finishTime)
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;

        if (finishTime >= SaveData.current.records[currentLevel - 1])
        {
            OpenType(regular);

            oldRecordText.text = "Best Time: " + SaveData.current.records[currentLevel - 1];
            yourRecordText.text = "Your Time: " + finishTime;
        }
        else
        {
            OpenType(newRecord);

            SaveData.current.records[currentLevel - 1] = finishTime;

            newRecordText.text = "New Record\n" + finishTime;

            newRecordText.GetComponent<RectTransform>().DOScale(1.2f, 1).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
            newRecordText.DOColor(Color.red, 1.5f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        }
        SaveManager.OnSave();
    }

    private void OpenType(GameObject type)
    {
        regular.SetActive(false);
        newRecord.SetActive(false);

        type.SetActive(true);
    }
}
