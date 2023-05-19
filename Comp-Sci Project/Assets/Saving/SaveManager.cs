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
        Debug.Log("saved");
    }

    public static void OnLoad()
    {
        SaveData.current = (SaveData)SerializationManager.Load(fileName);
    }
}
