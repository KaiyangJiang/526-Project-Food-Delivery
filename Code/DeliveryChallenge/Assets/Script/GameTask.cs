using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTask : MonoBehaviour
{
    public GameObject getObjectPrefab;     // place toget food
    public GameObject deliverObjectPrefab; // place to deliver food 
    
    private float earnedMoney = 10.0f;
    private string destination = "Music Store";
    private string title = "Pizza";
    private int index = 0;
    private float distance = 80.0f;
    
    //public GameObject taskDescriptionUIPrefab;
    private Canvas canvas;
    public GameObject titlePrefab;
    public GameObject contentPrefab;
    private TextMeshProUGUI titleText;      // 标题TextMeshPro组件引用
    private TextMeshProUGUI contentText; // 描述TextMeshPro组件引用

    public void Initialize(float money, string title, string description, Color color, Vector3 getPosition, Vector3 deliverPosition)
    {
        earnedMoney = money;
        GameObject getObject = Instantiate(getObjectPrefab, transform);
        Renderer getObjectRenderer = getObject.GetComponent<Renderer>();
        getObjectRenderer.material.SetColor("_Color", color);
        getObject.transform.localPosition = getPosition;
        
        
        GameObject deliverObject = Instantiate(deliverObjectPrefab, transform);
        Renderer deliverObjectRenderer = deliverObject.GetComponent<Renderer>();
        deliverObjectRenderer.material.SetColor("_Color", color);
        deliverObject.transform.localPosition = deliverPosition;
        
        /*GameObject uiObj = Instantiate(taskDescriptionUIPrefab, transform);
        Transform titleTransform = uiObj.transform.Find("Canvas/Title");
        Transform contentTransform = uiObj.transform.Find("Canvas/Content");*/
        GameObject titleInstance = Instantiate(titlePrefab, canvas.transform);
        GameObject contentInstance = Instantiate(contentPrefab, canvas.transform);

        titleText = titleInstance.GetComponent<TextMeshProUGUI>();
        titleText.color = color;

        contentText = contentInstance.GetComponent<TextMeshProUGUI>();
        contentText.color = color;

        titleText.text = title;
        contentText.text = description;
        
    }

    public void Initialize(TaskInfo taskInfo, int index)
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        destination = taskInfo.destination;
        earnedMoney = taskInfo.money;
        title = taskInfo.title;
        this.index = index;
        GameObject getObject = Instantiate(getObjectPrefab, transform);
        Renderer getObjectRenderer = getObject.GetComponent<Renderer>();
        getObjectRenderer.material.SetColor("_Color", taskInfo.color);
        getObject.transform.localPosition = taskInfo.getPosition;
        getObject.transform.rotation = taskInfo.getRotation;
        
        
        GameObject deliverObject = Instantiate(deliverObjectPrefab, transform);
        Renderer deliverObjectRenderer = deliverObject.GetComponent<Renderer>();
        deliverObjectRenderer.material.SetColor("_Color", taskInfo.color);
        deliverObject.transform.localPosition = taskInfo.deliverPosition;
        deliverObject.transform.rotation = taskInfo.deliverRotation;
        
        /*GameObject uiObj = Instantiate(taskDescriptionUIPrefab, transform);
        Transform titleTransform = uiObj.transform.Find("Canvas/Title");
        Transform contentTransform = uiObj.transform.Find("Canvas/Content");*/
        GameObject titleInstance = Instantiate(titlePrefab, canvas.transform);
        GameObject contentInstance = Instantiate(contentPrefab, canvas.transform);

        titleText = titleInstance.GetComponent<TextMeshProUGUI>();
        titleText.color = taskInfo.color;

        contentText = contentInstance.GetComponent<TextMeshProUGUI>();
        contentText.color =  taskInfo.color;

        titleText.text = taskInfo.title;
        contentText.text = taskInfo.description;
        
        Vector3 offset = new Vector3(0, -distance*this.index, 0);
        titleInstance.transform.localPosition += offset;
        contentInstance.transform.localPosition += offset;
    }
    
    public float getEarnedMoney()
    {
        return earnedMoney;
    }
    
    public string getTitle()
    {
        return titleText.text;
    }
    
    public void setContent(string content)
    {
        contentText.text = content;
    }

    public void updateTask()
    {
        contentText.text = "Deliver " + title + " to " + destination;
    }
    
    public int getIndex()
    {
        return index;
    }
    public string getDestination()
    {
        return destination;
    }

    public void updateTaskInfoDisplay()
    {
        Vector3 offset = new Vector3(0, distance, 0);
        titleText.transform.localPosition += offset;
        contentText.transform.localPosition += offset;
        index--;
        
    }

    public void completeTask()
    {
        Destroy(titleText.gameObject);
        Destroy(contentText.gameObject);
    }
}
