using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    private static int levelsUnlocked = 100000;

    public static bool OpenScene(int level)
    {
        if (level > levelsUnlocked || level > SceneManager.sceneCountInBuildSettings) return false;

        AudioManager.singleton.StopAllSongs();
        SceneManager.LoadScene(level);
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
        OpenScene(0);
    }

    private static void UnlockLevel(int level)
    {
        levelsUnlocked = Mathf.Max(levelsUnlocked, level);
    }
}
