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

    public GameObject editScreen;
    

    void Start()
    {
        GetComponentInChildren<Button>().onClick.AddListener(updateId);
    }

    public void updateId()
    {
        idText.text = GameManager.Instance.id;
    }

    public void Edit()
    {
        StartCoroutine(EditRequest());
    }

    IEnumerator EditRequest()
    {
        Member m = getIDFromFields();
        string json = JsonUtility.ToJson(m);

        //editUrl = editUrl + "?memberNickName=" + m.memberNickName;

        Debug.Log("들어옴");

        UnityWebRequest www = new UnityWebRequest(editUrl, "POST");
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
                if(www.downloadHandler.text == "member not found")
                    Debug.Log("해당 유저를 찾을 수 없습니다.");
                break;
            case UnityWebRequest.Result.Success:
                //UpdateInfoText("Edit Successful.");
                Debug.Log("성공");
                editScreen.SetActive(false);
                break;
        }
    }

    private Member getIDFromFields()
    {
        Member m = new Member();
        m.memberNickName = idInput.text;
        return m;
    }
}
