using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{

    public GameObject buttonDefualt;
    public GameObject buttonLocked;


    private ArrayList buttonList;
    private void Start()
    {
        AudioManager.singleton.Play("Menu");
        buttonList = new ArrayList();
        GenerateButtons();
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
                button.GetComponent<Button>().onClick.AddListener(() => LevelSelected(levelNum));
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
}
