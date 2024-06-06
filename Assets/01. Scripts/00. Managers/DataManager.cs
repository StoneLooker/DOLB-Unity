using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    static GameObject container;

    static DataManager instance;

    public static DataManager Instance
    {
        get
        {
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

    public void LoadGameData(string playerId)
    {
        string filePath = Application.persistentDataPath + "/" + playerId + "_GameData.json";

        if (File.Exists(filePath))
        {
            string FromJsonData = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<GameData>(FromJsonData);
            Debug.Log("불러오기 완료");
        }
        else
        {
            Debug.Log("저장된 데이터가 없습니다.");
        }
    }

    public void SaveGameData(string playerId)
    {
        string ToJsonData = JsonUtility.ToJson(data, true);
        string filePath = Application.persistentDataPath + "/" + playerId + "_GameData.json";

        File.WriteAllText(filePath, ToJsonData);
    }
}
