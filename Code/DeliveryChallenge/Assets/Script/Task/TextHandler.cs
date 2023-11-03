using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TextHandler : MonoBehaviour, IPointerClickHandler
{
    private TaskManager taskManager;
    private GameManager gameManager;

    public TextMeshProUGUI text;
    private bool clickedInTutorial = false;

    private void Start()
    {
        taskManager = GameObject.Find("TaskManager").GetComponent<TaskManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Debug.Log("text clicked: " + text.text);
        taskManager.SetClickedTask(text.text);
        if(taskManager.inTutorial && !clickedInTutorial)
        {
            Debug.Log("clicked task");
            clickedInTutorial = true;
            gameManager.updateTutorial(3);
        }
    }

}
