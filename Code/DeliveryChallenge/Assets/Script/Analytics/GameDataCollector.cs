using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Networking;


public class GameDataCollector : MonoBehaviour
{
    public int goldCollected = 0;
    public int tasksCompleted = 0;
    public List<string> taskTypes = new List<string>();
    public int treasureBoxesCollected = 0;
    public int magicDoorsUsed = 0;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void RecordTaskType(string taskType)
    {
        taskTypes.Add(taskType);
    }

    public void ClearData()
    {
        goldCollected = 0;
        tasksCompleted = 0;
        taskTypes.Clear();
        treasureBoxesCollected = 0;
        magicDoorsUsed = 0;
    }

    public void SendDataToGoogleForm()
    {
        string formURL = "https://docs.google.com/forms/d/e/1FAIpQLSdIk_Q8YsQSsgmyZ0zMUpjHQj7rDrG-R0UHz79F4kUO-GoX1Q/formResponse";
        string taskTypesString = string.Join(", ", taskTypes);

        WWWForm form = new WWWForm();
        form.AddField("entry.356164657", goldCollected.ToString());
        form.AddField("entry.242404563", tasksCompleted.ToString());
        form.AddField("entry.1412186571", taskTypesString);  // Example data, replace with actual
        form.AddField("entry.1391756352", treasureBoxesCollected.ToString());
        form.AddField("entry.80712744", magicDoorsUsed.ToString());

        UnityWebRequest www = UnityWebRequest.Post(formURL, form);
        StartCoroutine(WaitForRequest(www));
    }

    IEnumerator WaitForRequest(UnityWebRequest www)
    {
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form submitted successfully");
        }
    }
}
