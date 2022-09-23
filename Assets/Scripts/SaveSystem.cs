using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for saving the session stats
public class SaveSystem : MonoBehaviour
{
    // Takes the stats from the current play session and creates a new "SaveObject".
    // This object is then converted to a Json format and saved.
    public void SaveToJson(string time, int score, int shapesCollided)
    {
        SaveObject saveObject = new SaveObject {
            time = time,
            score = score,
            shapesCollided = shapesCollided,
        };
        string json = JsonUtility.ToJson(saveObject);
        Debug.Log(json);
    }
}
