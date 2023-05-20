using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    public static bool loaded = false;
    public static void OpenSave()
    {
        SaveManager.OnLoad();
        loaded = true;
    }

    public static void ResetSave()
    {
        SaveData.current.ResetSave();
    }

    public static bool OpenScene(int level)
    {
        if (level > SaveData.current.levelsUnlocked) return false;

        if (level >= SceneManager.sceneCountInBuildSettings)
        {
            ReturnToLevelMenu();
            return false;
        }

        AudioManager.singleton.StopAllSongs();

        StaticTransition.TransitionToScene(level);

        return true;
    }

    public static bool NextLevel()
    {
        int netLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        UnlockLevel(netLevelIndex);
        return OpenScene(netLevelIndex);
    }

    public static void ReturnToLevelMenu()
    {
        loaded = true;
        OpenScene(0);
    }

    public static void UnlockLevel(int level)
    {
        SaveData.current.levelsUnlocked = Mathf.Max(SaveData.current.levelsUnlocked, level);
        SaveManager.OnSave();
    }
}
