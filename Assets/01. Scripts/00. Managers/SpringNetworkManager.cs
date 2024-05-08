using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class SpringNetworkManager : MonoBehaviour
{
   /* private string randomURL = "http://localhost:8080/random";
    private string addPersonURL = "http://localhost:8080/add";

    public TMP_InputField nameInput;
    public TMP_InputField ageInput;
    public TMP_InputField hobbiesInput;
    public TMP_InputField majorInput;

    public void GetRandomPerson()
    {
        // start a coroutine to make a GET request to get a random person
        StartCoroutine(RandomRequest());
    }

    // make a POST request to add new person
    public void AddPerson()
    {
        StartCoroutine(AddRequest());
    }

    // Make a GET request to get a random person
    IEnumerator RandomRequest()
    {
        // UnityWebRequest can do GET, POST, etc web requests.
        UnityWebRequest webRequest = UnityWebRequest.Get(randomURL);

        // set the request header: tell the server that we accept json as a return value
        webRequest.SetRequestHeader("Accept", "application/json");

        // Make the request and wait for it to complete.
        yield return webRequest.SendWebRequest();


        // check the result 
        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                Debug.LogError("Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError("HTTP Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.Success:
                // everything is ok parse the JSON and show the results

                string json = webRequest.downloadHandler.text;
                parseResult(json);
                break;
        }


    }
    // fills in the fields with the given JSON object. 
    private void parseResult(string json)
    {
        // convert JSON string to Person        
        Person p = JsonUtility.FromJson<Person>(json);

        nameInput.text = p.name;
        ageInput.text = "" + p.age;
        hobbiesInput.text = p.hobbies;
        majorInput.text = p.major;

    }

    private string getPersonFromFields()
    {
        Person p = new Person();
        p.name = nameInput.text;
        p.age = int.Parse(ageInput.text);
        p.hobbies = hobbiesInput.text;
        p.major = majorInput.text;

        // convert Person to a JSON string
        string json = JsonUtility.ToJson(p);
        return json;
    }

    // Make a POST request to send new person
    IEnumerator AddRequest()
    {
        string personJSON = getPersonFromFields();

        // Prepare a webrequest by specifying the URL, JSON data (string), and value for the Content-Type header.
        UnityWebRequest webRequest = UnityWebRequest.Post(addPersonURL, personJSON, "application/json");

        // Make the request and wait for it to complete.
        // Before this you might show a label like "Sending..." to the user.
        yield return webRequest.SendWebRequest();

        // check the result. This code runs after the webrequest completes or an error occurs.
        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                Debug.LogError("Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError("HTTP Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.Success:
                // everything is ok.
                Debug.Log("Person sent successfully!");
                break;
        }
    }*/
}
