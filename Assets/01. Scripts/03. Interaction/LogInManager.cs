using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;


public class LogInManager : MonoBehaviour
{
    private string signUpUrl = "http://localhost:8080/member/save";
    private string logInUrl = "http://localhost:8080/member/login";

    public TMP_InputField emailInput;
    public TMP_InputField idInput;
    public TMP_InputField passwordInput;

    public void SignUp()
    {
        StartCoroutine(SignUpRequest());
    }

    public void LogIn()
    {
        StartCoroutine(LogInRequest());
    }

    IEnumerator SignUpRequest()
    {
        string json = getMemberFromFields();
        UnityWebRequest www = UnityWebRequest.Post(signUpUrl, json, "application/json");
        
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
                Debug.Log("Member sent successfully");
                break;
        }
    }

    IEnumerator LogInRequest()
    {
        UnityWebRequest www = UnityWebRequest.Get(logInUrl);

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
                string json = www.downloadHandler.text;
                parseResult(json);
                break;
        }
    }

    private string getMemberFromFields()
    {
        Member m = new Member();
        m.memberEmail = emailInput.text;
        m.memberId = idInput.text;
        m.memberPassword = passwordInput.text;

        return JsonUtility.ToJson(m);
    }

    private void parseResult(string json)
    {
        Member m = JsonUtility.FromJson<Member>(json);
        if(m != null){
            emailInput.text = m.memberEmail;
            idInput.text = m.memberId;
            passwordInput.text = m.memberPassword;
        }
    }
}


