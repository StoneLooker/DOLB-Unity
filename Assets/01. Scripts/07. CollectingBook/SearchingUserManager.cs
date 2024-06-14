using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using TMPro;
using UnityEditor;
using System;

[Serializable]
public class User
{
    public int ID;
    public string memberNickName;
}

public class UserList
{
    public List<User> users;
}

public class SearchingUserManager : MonoBehaviour
{
    //Define url
    private string searchUserUrl = "http://43.203.76.106:8080/search";


    List<User> users;

    public GameObject userInfoPrefab;
    bool SearchComplete = false;

    public void StartShowUserList()
    {
        if (SearchComplete) Debug.Log("Already Searchged");
        else
        {
            StartCoroutine(ShowUserList());
            SearchComplete = true;
        }
    }
    public IEnumerator ShowUserList()
    {
        yield return StartCoroutine(SearchUserRequest());
        Debug.Log("1");
        int yValue = 0;
        foreach (User element in users)
        {
            var index = Instantiate(userInfoPrefab, new Vector3(0, yValue, 0), Quaternion.identity);
            index.transform.SetParent(GameObject.Find("Content").transform);
            index.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = element.memberNickName;
            index.transform.GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners();
            index.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => GameManager.Instance._book.LookOtherUserBook(element.memberNickName));
            //.SetText(element.memberNickName);
            yValue -= 200;
        }
    }

    public List<User> JsonToList(string jsonString)
    {
        jsonString = "{\"users\":" + jsonString + "}";
        //Debug.Log(jsonString);

        UserList userList = JsonUtility.FromJson<UserList>(jsonString);

        //Debug.Log(userList.users);
        // Convert the array to a list if needed
        List<User> users = new List<User>(userList.users);

        // Now you can use the 'stones' list
        foreach (var user in users)
        {
            Debug.Log("ID: " + user.ID + ", NickName: " + user.memberNickName);
        }

        return users;
    }

    // Start the request to search for a user
    public void SearchUser()
    {
        StartCoroutine(SearchUserRequest());
    }

    public IEnumerator SearchUserFirst()
    {
        yield return StartCoroutine(SearchUserRequest());
    }

    string getMemberNickNameFromField()
    {
        return "";
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
                users = JsonToList(www.downloadHandler.text);
                break;
        }
        yield return null;
    }

}
