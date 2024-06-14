using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using TMPro;

public class SearchingUserManager : MonoBehaviour
{
    //Define url
    private string searchUserUrl = "http://43.203.76.106:8080/search";

    public TMP_InputField searchUserInputField;

    // Start the request to search for a user
    public void SearchUser()
    {
        StartCoroutine(SearchUserRequest());
    }

    string getMemberNickNameFromField()
    {
        return searchUserInputField.text;
    }

    // Coroutine to handle the search user request
    IEnumerator SearchUserRequest()
    {
        //request
        string memberNickName = getMemberNickNameFromField();
        string url = $"{searchUserUrl}?memberNickName={memberNickName}";
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        // Handle the request result
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
