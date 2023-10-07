using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTask : MonoBehaviour
{
    public GameObject getObjectPrefab;     // place toget food
    public GameObject deliverObjectPrefab; // place to deliver food 
    
    private float earnedMoney = 10.0f;
    public GameObject taskDescriptionUIPrefab;
    private TextMeshProUGUI titleText;      // 标题TextMeshPro组件引用
    private TextMeshProUGUI contentText; // 描述TextMeshPro组件引用

    public void Initialize(float money, string title, string description)
    {
        earnedMoney = money;
        
    
        GameObject getObject = Instantiate(getObjectPrefab, transform);
        
        GameObject deliverObject = Instantiate(deliverObjectPrefab, transform);
        
        GameObject uiObj = Instantiate(taskDescriptionUIPrefab, transform);
        Transform titleTransform = uiObj.transform.Find("Canvas/Title");
        Transform contentTransform = uiObj.transform.Find("Canvas/Content");
        if (titleTransform != null)
        {
            titleText = titleTransform.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogError("Title not found!");
        }
        if (contentTransform != null)
        {
            contentText = contentTransform.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogError("Description not found!");
        }
        titleText.text = title;
        contentText.text = description;
        
    }
    
    public float getEarnedMoney()
    {
        return earnedMoney;
    }
    
    public string getTitle()
    {
        return titleText.text;
    }
}
