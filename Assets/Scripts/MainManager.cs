using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public Color TeamColor;

    private void Awake()
    {
        /* This pattern is called a singleton. You use it to ensure that only a single instance 
         * of the MainManager can ever exist, so it acts as a central point of access. */
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadColor();
    }

    // A serializable class for saving data.
    [System.Serializable]
    class SaveData
    {
        public Color TeamColor;
    }

    public void SaveColor()
    {
        SaveData data = new SaveData(); // Create a new instance of the SaveData class.
        data.TeamColor = TeamColor; // Save the TeamColor Variable in MainManager inside the TeamColor in the SaveData variable.

        string json = JsonUtility.ToJson(data); // Transform the SaveData instance to json.

        // Write a string to a file. Application.persistentDataPath is the path to the file.
        // It creates a folder where you can save data that will survive between application reinstall or update.
        // The filename savefile.json will appended to it.
        // The second parameter is the tet that you want to write into the file, which is the json variable we created earlier.
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json); 
    }

    // Reverse of SaveColor()
    public void LoadColor()
    {
        // Save the path of the file into the path variable.
        string path = Application.persistentDataPath + "/savefile.json";

        // Checks to see if the json file in the path exists or not.
        if (File.Exists(path))
        {
            // Read the content of the file and store it in json.
            string json = File.ReadAllText(path);
            // The content of the file will be transformed back into a SaveData instance.
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            // It will save jteamColor to the color saved in the SaveData.
            TeamColor = data.TeamColor;
        }
    }
}
