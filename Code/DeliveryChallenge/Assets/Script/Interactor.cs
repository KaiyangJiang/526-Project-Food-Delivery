using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable
{
    public void Interact();
}

public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed");
            Ray ray = new Ray(InteractorSource.position, InteractorSource.up);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, InteractRange))
            {
                Debug.Log("Hit an object: " + hitInfo.collider.gameObject.name);
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    Debug.Log("here: ");
                    interactObj.Interact();
                }
            }
        }
    }
}
