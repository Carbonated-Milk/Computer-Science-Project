using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class LevelSelector : MonoBehaviour
{
    [Header("Button Presets")]
    public GameObject buttonDefualt;
    public GameObject buttonLocked;

    [Header("Level Text")]
    public TMP_Text levelDescription;

    private ArrayList buttonList;
    private void Start()
    {
        AudioManager.singleton.Play("Menu");
        buttonList = new ArrayList();
        GenerateButtons();
        GameManager.OpenSave();
    }

    public void GenerateButtons()
    {
        if (buttonList.Count > 0) {
            foreach (GameObject g in buttonList)
            {
                Destroy(g);
            }
            buttonList.Clear();
        }

        SetActiveButtons(true);

        int buttonCount = SceneManager.sceneCountInBuildSettings - 1;
        for (int i = 0; i < buttonCount; i++)
        {
            bool isUnlocked = i < SaveData.current.levelsUnlocked;

            GameObject button = isUnlocked ? Instantiate(buttonDefualt) : Instantiate(buttonLocked);

            button.transform.parent = transform;

            RectTransform t = button.GetComponent<RectTransform>();
            t.position = buttonDefualt.GetComponent<RectTransform>().position + Vector3.right * (i - (buttonCount - 1) / 2f) * 50;

            if (isUnlocked)
            {
                int levelNum = i + 1;
                var trigger = button.GetComponent<EventTrigger>();
                var pointerOn = new EventTrigger.Entry();
                pointerOn.eventID = EventTriggerType.PointerEnter;
                pointerOn.callback.AddListener((data) => HoverOn(levelNum));
                trigger.triggers.Add(pointerOn);

                var pointerOff = new EventTrigger.Entry();
                pointerOff.eventID = EventTriggerType.PointerExit;
                pointerOff.callback.AddListener((data) => HoverOff(levelNum));
                trigger.triggers.Add(pointerOff);

                var buttonComponent = button.GetComponent<Button>();
                buttonComponent.onClick.AddListener(() => LevelSelected(levelNum));
                TMP_Text text = button.GetComponentInChildren<TMP_Text>();
                text.text = (levelNum).ToString();
            }
        }

        SetActiveButtons(false);
    }
    public void LevelSelected(int level)
    {
        GameManager.OpenScene(level);
    }

    public void SetActiveButtons(bool active)
    {
        buttonDefualt.SetActive(active);
        buttonLocked.SetActive(active);
    }

    public void UnlockAllLevels()
    {
        SaveData.current.levelsUnlocked = 90000;
        SaveManager.OnSave();
        GenerateButtons();
    }

    public void LockLevels()
    {
        SaveData.current.levelsUnlocked = 1;
        SaveManager.OnSave();
        GenerateButtons();
    }

    public void ResetSave()
    {
        GameManager.ResetSave();
        GenerateButtons();
    }

    public void HoverOn(int level)
    {
        levelDescription.DOKill();
        string description = "Level " + level;
        float record = SaveData.current.records[level - 1];
        if (record != float.MaxValue)
        {
            description += "\n" + "Best Time: " + record;
        }
        levelDescription.text = description;
        levelDescription.alpha = 1;
    }

    public void HoverOff(int level)
    {
        levelDescription.DOFade(0, .5f);
    }

    public void Back()
    {
        Play.singleton.MaskBack();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
