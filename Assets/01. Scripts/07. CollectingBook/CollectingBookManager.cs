using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using TMPro;
using UnityEditor;

public class CollectingBookManager : MonoBehaviour
{
    private string addStoneUrl = "http://43.203.76.106:8080/collectingbook/add";
    private string getStoneUrl = "http://43.203.76.106:8080/collectingbook";

    [SerializeField] GameObject stoneFrame;

    public List<CollectingBook> books { get; private set; }

    private string memberNickName;

    void Start()
    {
    }

    public void AddStone(int id, string name)
    {
        StartCoroutine(AddStoneRequest(id, name));
    }

    public void GetStone()
    {
        StartCoroutine(GetStoneRequest());
    }

    public void StartSpawnStones()
    {
        StartCoroutine(SpawnStones());
    }

    public void LookMyBook()
    {
        GameManager.Instance._book.memberNickName = GameManager.Instance.nickname;
        GameManager.Instance.ChangeMap(MAP_TYPE.CollectingBook);
    }

    public void LookOtherUserBook(string nickName)
    {
        GameManager.Instance._book.memberNickName = nickName;
        GameManager.Instance.ChangeMap(MAP_TYPE.CollectingBook);
    }

    public IEnumerator SpawnStones()
    {
        if(memberNickName == null) memberNickName = GameManager.Instance.nickname;
        yield return StartCoroutine(GetStoneRequest());
        float yValue = 6;
        if (books == null) Debug.Log("Error: no book founded, " + memberNickName);
        foreach (CollectingBook element in books)
        {
            if (element.stoneName == "LimeStone")
            {
                Debug.Log(GameManager.Stone.collectingBook);
                GameObject st = Instantiate(stoneFrame, new Vector3(0, yValue, 0), new Quaternion(0, 0, 0, 0));
                st.AddComponent<LimeStoneController>();
                yValue += 1;
            }
            else if (element.stoneName == "Granite")
            {
                Debug.Log(GameManager.Stone.collectingBook);
                GameObject st = Instantiate(stoneFrame, new Vector3(0, yValue, 0), new Quaternion(0, 0, 0, 0));
                st.AddComponent<GraniteController>();
                yValue += 1;
            }
        }
    }

    public List<CollectingBook> JsonToList(string jsonString)
    {
        jsonString = "{\"stones\":" + jsonString + "}";

        CollectingBookList book = JsonUtility.FromJson<CollectingBookList>(jsonString);

        // Convert the array to a list if needed
        List<CollectingBook> stones = new List<CollectingBook>(book.stones);

        // Now you can use the 'stones' list
        foreach (var stone in stones)
        {
            Debug.Log("Name: " + stone.stoneName + ", Number: " + stone.stoneNumber);
        }

        return stones;
    }

    IEnumerator AddStoneRequest(int Id, string name)
    {
        CollectingBook cb = new CollectingBook();
        cb.stoneNumber = Id;
        cb.memberNickName = GameManager.Instance.nickname;
        cb.stoneName = name;
        string json = JsonUtility.ToJson(cb);

        UnityWebRequest www = new UnityWebRequest(addStoneUrl, "POST");
        byte[] jsonToSend = Encoding.UTF8.GetBytes(json);
        www.uploadHandler = new UploadHandlerRaw(jsonToSend);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        switch (www.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                Debug.Log("Error: " + www.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.Log("HTTP Error: " + www.error + " Response: " + www.downloadHandler.text);
                break;
            case UnityWebRequest.Result.Success:
                Debug.Log("Success: " + www.downloadHandler.text);
                break;
        }
    }

    /*CollectingBook getStoneFromFields()
    {
        string stoneName = stoneNameInputField.text;
        int stoneNumber = int.Parse(stoneNumberInputField.text);

        return new CollectingBook
        {
            stoneName = stoneName,
            stoneNumber = stoneNumber,
            memberNickName = GameManager.Instance.id
        };
    }*/

    IEnumerator GetStoneRequest()
    {
        string url = $"{getStoneUrl}/{memberNickName}";
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        switch (www.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                Debug.Log("Error: " + www.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.Log("HTTP Error: " + www.error + " Response: " + www.downloadHandler.text);
                break;
            case UnityWebRequest.Result.Success:
                Debug.Log("Success: " + www.downloadHandler.text);
                books = JsonToList(www.downloadHandler.text);
                break;
        }
        yield return null;
    }


}