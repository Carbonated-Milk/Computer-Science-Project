using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{

    public GameObject buttonDefualt;

    private void Awake()
    {
        int buttonCount = SceneManager.sceneCountInBuildSettings - 1;
        for (int i = 0; i < buttonCount; i++)
        {
            GameObject button = Instantiate(buttonDefualt);
            button.transform.parent = transform;

            int levelNum = i + 1;
            button.GetComponent<Button>().onClick.AddListener(() => LevelSelected(levelNum));
            TMP_Text text = button.GetComponentInChildren<TMP_Text>();
            text.text = (levelNum).ToString();
            RectTransform t = button.GetComponent<RectTransform>();
            t.position = buttonDefualt.GetComponent<RectTransform>().position + Vector3.right * (i - (buttonCount - 1) / 2f) * 50;
        }

        Destroy(buttonDefualt);
    }

    private void Start()
    {
        AudioManager.singleton.Play("Menu");
    }
    public void LevelSelected(int level)
    {
        Debug.Log("opening " + level);
        GameManager.OpenScene(level);
    }
}
