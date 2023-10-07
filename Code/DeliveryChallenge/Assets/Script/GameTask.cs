using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTask : MonoBehaviour
{
    public GameObject getObjectPrefab;     // place toget food
    public GameObject deliverObjectPrefab; // place to deliver food 

    public void Initialize()
    {
        // 生成“get”对象并设置为当前任务的子对象
        GameObject getObject = Instantiate(getObjectPrefab, transform);

        // 生成“deliver”对象并设置为当前任务的子对象
        GameObject deliverObject = Instantiate(deliverObjectPrefab, transform);
    }
}
