using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SaveData
{
    private static SaveData _current;
    public static SaveData current
    {
        get
        {
            if (_current == null)
            {
                _current = new SaveData();

                _current.records = new float[SceneManager.sceneCountInBuildSettings - 1];
                for (int i = 0; i < _current.records.Length; i++)
                {
                    _current.records[i] = float.MaxValue;
                }
            }
            return _current;
        }
        set
        {
            if (value != null)
            {
                _current = value;
            }
        }
    }

    ///<summary>
    ///with 1 being the first level
    ///</summary>
    public int levelsUnlocked = 1;

    public float[] records; 

    public float sensitivity = 0.5f;
    public float masterVolume = 0.5f;
}
