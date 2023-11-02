using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickHandler : MonoBehaviour, IPointerClickHandler
{
    private TaskManager taskManager;
    
    private void Start()
    {
        taskManager = GameObject.Find("TaskManager").GetComponent<TaskManager>();
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("999:"+gameObject.name);
        taskManager.SetClickedTask(gameObject.name);
    }
}
