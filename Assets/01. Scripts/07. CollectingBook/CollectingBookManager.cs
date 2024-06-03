using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using TMPro;

public class CollectingBookManager : MonoBehaviour
{
    private string addStoneUrl = "http://43.203.76.106:8080/collectingbook/add";
    private string getStoneUrl = "http://43.203.76.106:8080/collectingbook";
   

    public TMP_InputField stoneNameInputField;
    public TMP_InputField stoneNumberInputField;
    private string memberNickName;

    void Start()
    {
        memberNickName = GameManager.Instance.id; 
    }

    public void AddStone()
    {
        StartCoroutine(AddStoneRequest());
    }

    public void GetStone()
    {
        StartCoroutine(GetStoneRequest());
    }

  

    IEnumerator AddStoneRequest()
    {
        CollectingBook cb = getStoneFromFields();
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

    CollectingBook getStoneFromFields()
    {
        string stoneName = stoneNameInputField.text;
        int stoneNumber = int.Parse(stoneNumberInputField.text);

        return new CollectingBook
        {
            stoneName = stoneName,
            stoneNumber = stoneNumber,
            memberNickName = GameManager.Instance.id
        };
    }

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
                break;
        }
    }


}