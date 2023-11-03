using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickHandler : MonoBehaviour, IPointerClickHandler
{
    private TaskManager taskManager;
    private GameManager gameManager;
    private bool clickedInTutorial = false;
    
    private void Start()
    {
        taskManager = GameObject.Find("TaskManager").GetComponent<TaskManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("999:"+gameObject.name);
        taskManager.SetClickedTask(gameObject.name);
        if(taskManager.inTutorial && !clickedInTutorial)
        {
            clickedInTutorial = true;
            gameManager.updateTutorial(3);
        }
    }
}
