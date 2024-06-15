using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class DataManager : MonoBehaviour
{
    static GameObject container;

    static DataManager instance;

    public static DataManager Instance
    {
        get
        {
            // If there is no instance, create one
            if (!instance)
            {
                container = new GameObject();
                container.name = "DataManager";
                instance = container.AddComponent(typeof(DataManager)) as DataManager;
                DontDestroyOnLoad(container);
            }
            return instance;
        }
    }

    public GameData data = new GameData();

    // Method to load game data from a file
    public void LoadGameData(string playerId)
    {
        // Construct the file path based on the player ID
        string filePath = Application.persistentDataPath + "/" + playerId + "_GameData.json";

        // Check if the file exists
        if (File.Exists(filePath))
        {
            // Read the data from the file and deserialize it
            string FromJsonData = File.ReadAllText(filePath);
            data = JsonConvert.DeserializeObject<GameData>(FromJsonData);
            Debug.Log($"Load complete: {FromJsonData}");
        }
        else
        {
            Debug.Log("No saved data found.");
        }
    }

    // Method to save game data to a file
    public void SaveGameData(string playerId)
    {
        // Serialize the data to JSON format
        string ToJsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
        // Construct the file path based on the player ID
        string filePath = Application.persistentDataPath + "/" + playerId + "_GameData.json";

        // Write the JSON data to the file
        File.WriteAllText(filePath, ToJsonData);
        Debug.Log($"Save complete: {ToJsonData}");
    }
}
