using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPizza : MonoBehaviour, IInteractable
{
    private GameManager manager;

    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void Interact()
    {
        Debug.Log("Interacted with this object.");  
    }
 

}
