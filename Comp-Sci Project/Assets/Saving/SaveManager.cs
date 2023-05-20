using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using DG.Tweening;

[DisallowMultipleComponent]
public static class SaveManager
{
    private static readonly string fileName = "save";
    public static void OnSave()
    {
        SerializationManager.Save(fileName, SaveData.current);
    }

    public static void OnLoad()
    {
        string filePath = Application.persistentDataPath + "/saves/" + fileName + ".save";
        SaveData.current = (SaveData)SerializationManager.Load(filePath);
    }
}
