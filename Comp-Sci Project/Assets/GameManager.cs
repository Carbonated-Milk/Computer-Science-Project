using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    private int levelsUnlocked = 100000;

    private void Awake()
    {
        if(singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }
    public bool OpenScene(int level)
    {
        if (level > levelsUnlocked || level > SceneManager.sceneCountInBuildSettings) return false;

        AudioManager.singleton.StopAllSongs();
        SceneManager.LoadScene(level);
        return true;
    }

    public bool NextLevel()
    {
        int netLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        UnlockLevel(netLevelIndex);
        return OpenScene(netLevelIndex);
    }

    private void UnlockLevel(int level)
    {
        levelsUnlocked = Mathf.Max(levelsUnlocked, level);
    }
}
