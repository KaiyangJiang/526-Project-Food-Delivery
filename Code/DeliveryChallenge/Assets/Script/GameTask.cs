using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTask : MonoBehaviour
{
    public GameObject getObjectPrefab;    // 这是一个预制体，带有tag "get"
    public GameObject deliverObjectPrefab; // 这是另一个预制体，带有tag "deliver"

    public void Initialize()
    {
        // 生成“get”对象并设置为当前任务的子对象
        GameObject getObject = Instantiate(getObjectPrefab, transform);
        getObject.tag = "get";

        // 生成“deliver”对象并设置为当前任务的子对象
        GameObject deliverObject = Instantiate(deliverObjectPrefab, transform);
        deliverObject.tag = "deliver";
    }
}
