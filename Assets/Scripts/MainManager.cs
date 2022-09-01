using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Mono.Cecil.Cil;
using static UnityEditor.PlayerSettings;
using static UnityEngine.Rendering.DebugUI.Table;
using UnityEditor.PackageManager;

public class MainManager : MonoBehaviour
{
    // As soon as you add a get or set accessor to a variable, that variable becomes a Property.
    // It provides access to internal data through these specialized methods. 
    // Without a set accessor, this property is strictly read-only, its value cannot be set anywhere.
    // This is good because we don’t want any other classes to be able to reset it or alter it.

    // Since the property is strictly read-only now, it can’t even be set in its own class.
    // This is causing an error lower down in MainManager.cs on the line Instance = this;.
    // To fix this, add a private setter to the property, which should resolve that error.

    // You can now set the property’s value from within the class, but only get it from outside the class.
    // It’s encapsulated to only accept modifications from its own class.
    // This getter and setter represents the most basic form of encapsulation, where you are simply getting or setting the value.
    // This simple implementation is called an auto-implemented property.
    public static MainManager Instance { get; private set; }
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
