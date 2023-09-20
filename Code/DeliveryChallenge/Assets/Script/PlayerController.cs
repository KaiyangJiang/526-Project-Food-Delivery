using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 10.0f;
    private float turnSpeed = 120f;
    private float horizontalInput;
    private float forwardInput;
    private Animator animator;
    private GameManager manager;
    private bool canGetPizza;
    private bool canDeliver;
    public float jumpAmount = 1000;

    private Vector3 startPosition; // Store the starting position of the player

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        startPosition = transform.position;  // Store the initial position of the player
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();


    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (manager.isGameActive)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            forwardInput = Input.GetAxis("Vertical");
            float curSpeed = Time.deltaTime * speed * forwardInput;
            transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
            transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);
            animator.SetFloat("speed", curSpeed);


            if (Input.GetKeyDown(KeyCode.E))
            {
                if (canGetPizza)
                {
                    if (manager.pizza == 0)
                    {
                        manager.AddPizza(1);
                        manager.showPizza = false;
                    }
                }
                if (canDeliver)
                {
                    if (manager.pizza == 1)
                    {
                        manager.DeliverPizza(1);
                        manager.showDeliver = false;
                    }
                }

            }
        }
        else
        {

        }
        



    }

    public void ResetToStartPosition()
    {
        Debug.Log("Player is reseting to start positon.");
        Rigidbody rb = GetComponent<Rigidbody>();

        if(rb)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

        }

        transform.position = startPosition;

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered with: " + other.gameObject.name); // This will print the name of the object the player collided with.
        manager.showText = true;
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("Collided with Obstacle!");
            ResetToStartPosition();

        }

        if (other.gameObject.tag == "PizzaPlane")
        {
            if (manager.pizza == 0)
            {
                canGetPizza = true;
                manager.showPizza = true;
            }
            else
            {
                canGetPizza = false;
                manager.showPizza = false;
            }
            
        }

        if (other.gameObject.tag == "DeliveryPlane")
        {
            if (manager.pizza == 1)
            {
                canDeliver = true;
                manager.showDeliver = true;
            }
            else
            {
                canDeliver = false;
                manager.showDeliver = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canGetPizza = false;
        canDeliver = false;
        manager.showText = false;
        
    }

}