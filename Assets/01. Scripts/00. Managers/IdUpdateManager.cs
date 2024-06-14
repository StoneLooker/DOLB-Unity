using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using TMPro;

public class IdUpdateManager : MonoBehaviour
{
    private string editUrl = "http://43.203.76.106:8080/update";

    public TMP_InputField idInput;
    public TMP_Text idText;

    // Screens for editing and displaying information
    public GameObject editScreen;
    public GameObject infoScreen;
    

    void Start()
    {
        // Add listener to the button to call updateId method when clicked
        GetComponentInChildren<Button>().onClick.AddListener(updateId);
    }

    // Method to update the ID text
    public void updateId()
    {
        idText.text = GameManager.Instance.nickname;
    }

    // Method to initiate the editing process
    public void Edit()
    {
        StartCoroutine(EditRequest());
    }

    // Coroutine to send the edit request to the server
    IEnumerator EditRequest()
    {
        // Get the ID from input fields
        Member m = getIDFromFields();

        if (m == null) yield break;

        // Convert the member object to JSON
        string json = JsonUtility.ToJson(m);

        // Append parameters to the URL
        editUrl = editUrl + "?memberNickName=" + GameManager.Instance.nickname + "&newNickName=" + m.memberNickName;

        // Create a new UnityWebRequest for the POST method
        UnityWebRequest www = new UnityWebRequest(editUrl, "POST");
        byte[] jsonToSend = Encoding.UTF8.GetBytes(json);
        www.uploadHandler = new UploadHandlerRaw(jsonToSend);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        // Wait for the request to complete
        yield return www.SendWebRequest();

        // Handle the response based on the result
        switch(www.result){
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                Debug.Log("Error: " + www.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.Log("HTTP Error: " + www.error + " Response: " + www.downloadHandler.text);
                if(www.downloadHandler.text == "member not found")
                    Debug.Log("User not found.");
                break;
            case UnityWebRequest.Result.Success:
                Debug.Log("Success");
                GameManager.Instance.nickname = m.memberNickName;
                updateId();
                editScreen.SetActive(false);
                break;
        }
    }

    // Method to update the information text on the info screen
    private void UpdateInfoText(string str)
    {
        infoScreen.SetActive(true);
        infoScreen.transform.GetChild(0).GetComponent<TMP_Text>().text = str;
    }

    // Method to get the ID from the input fields
    private Member getIDFromFields()
    {
        Member m = new Member();

        // Validate the input length
        if(idInput.text.Length < 10)
            m.memberNickName = idInput.text;
        else
        {   
            UpdateInfoText("Nickname up to 10 characters.");
            return null;
        }

        return m;
    }
}
