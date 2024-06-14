using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text;

public class LogInManager : MonoBehaviour
{
    // URLs for sign-up, log-in, and log-out requests
    private string signUpUrl = "http://43.203.76.106:8080/save";
    private string logInUrl = "http://43.203.76.106:8080/login";
    private string logOutUrl = "http://43.203.76.106:8080/logout";

    // Input fields for ID and password
    public TMP_InputField idInput;
    public TMP_InputField passwordInput;

    // Screens for displaying information and logging in
    public GameObject infoScreen;
    public GameObject logInScreen;

    // Buttons for log in and log out
    public GameObject logInBtn;
    public GameObject logOutBtn;
    
    // Boolean to track log-in status
    private bool isLoggedIn = false;

    public void SignUp()
    {
        StartCoroutine(SignUpRequest());
    }

    public void LogIn()
    {
        StartCoroutine(LogInRequest());
    }

    public void LogOut()
    {
        StartCoroutine(LogOutRequest());
    }

    // Coroutine to handle sign-up request
    IEnumerator SignUpRequest()
    {
        Member m = getMemberFromFields();

        if (m == null) yield break;

        string json = JsonUtility.ToJson(m);

        Debug.Log(json);

        signUpUrl = signUpUrl + "?memberNickName=" + m.memberNickName + "&memberPassword=" + m.memberPassword;

        UnityWebRequest www = new UnityWebRequest(signUpUrl, "POST");
        byte[] jsonToSend = Encoding.UTF8.GetBytes(json);
        www.uploadHandler = new UploadHandlerRaw(jsonToSend);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        switch(www.result){
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                Debug.Log("Error: " + www.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.Log("HTTP Error: " + www.error + " Response: " + www.downloadHandler.text);
                if(www.downloadHandler.text == "Save fail")
                    UpdateInfoText("ID already exists.");
                break;
            case UnityWebRequest.Result.Success:
                UpdateInfoText("Sign Up Successful.");
                break;
        }
    }

    // Coroutine to handle log-in request
    IEnumerator LogInRequest()
    {
        Member m = getMemberFromFields();

        string json = JsonUtility.ToJson(m);

        string requestUrl = logInUrl + "?memberNickName=" + m.memberNickName + "&memberPassword=" + m.memberPassword;

        Debug.Log(json);

        UnityWebRequest www = new UnityWebRequest(requestUrl, "POST");
        byte[] jsonToSend = Encoding.UTF8.GetBytes(json);
        www.uploadHandler = new UploadHandlerRaw(jsonToSend);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        www.SetRequestHeader("Cache-Control", "no-cache");
        www.SetRequestHeader("Pragma", "no-cache");

        
        yield return www.SendWebRequest();

        Debug.Log(www.downloadHandler.text);

        switch(www.result){
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                Debug.Log("Error: " + www.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.Log("HTTP Error: " + www.error + " Response: " + www.downloadHandler.text);
                break;
            case UnityWebRequest.Result.Success:
                if(www.downloadHandler.text == "login fail")
                    UpdateInfoText("Incorrect user ID or Password.");
                else{
                    isLoggedIn = true;
                    UpdateButtonListener();
                    logInBtn.SetActive(false);
                    logOutBtn.SetActive(true);
                    logInScreen.SetActive(false);
                    GameManager.Instance.nickname = m.memberNickName;
                    GameManager.Instance.id = www.downloadHandler.text;
                    DataManager.Instance.LoadGameData(GameManager.Instance.id);
                    GameManager.Instance.ApplyGameData(DataManager.Instance.data);
                }
                break;
        }
    }

    // Coroutine to handle log-out request
    IEnumerator LogOutRequest()
    {
        UnityWebRequest www = UnityWebRequest.Get(logOutUrl);

        www.SetRequestHeader("Access", "application/json");
        
        yield return www.SendWebRequest();

        switch(www.result){
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                Debug.Log("Error: " + www.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.Log("HTTP Error: " + www.error);
                break;
            case UnityWebRequest.Result.Success:
                isLoggedIn = false;
                UpdateButtonListener();
                logInBtn.SetActive(true);
                logOutBtn.SetActive(false);
                UpdateInfoText("Log Out successful.");
                break;
        }
    }

    // Method to update button listeners based on log-in status
    public void UpdateButtonListener()
    {
        GameManager.Instance._controller._button.gameStart.onClick.RemoveAllListeners();
        if (isLoggedIn)
            GameManager.Instance._controller._button.gameStart.onClick.AddListener(GameManager.Instance._controller._button.gameStart.GetComponent<FadeOut>().LoadingSceneLoad);
        else
            GameManager.Instance._controller._button.gameStart.onClick.AddListener(() => UpdateInfoText("Log In is required."));
    }

    // Method to update the information text on the info screen
    private void UpdateInfoText(string str)
    {
        infoScreen.SetActive(true);
        infoScreen.transform.GetChild(0).GetComponent<TMP_Text>().text = str;
    }

    // Method to get member information from input fields
    private Member getMemberFromFields()
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
        m.memberPassword = passwordInput.text;
        return m;
    }
}