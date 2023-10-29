using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TextHandler : MonoBehaviour, IPointerClickHandler
{
    private TaskManager taskManager;

    public TextMeshProUGUI text;

    private void Start()
    {
        taskManager = GameObject.Find("TaskManager").GetComponent<TaskManager>();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Debug.Log("text clicked: " + text.text);
        taskManager.SetClickedTask(text.text);
    }

}
