using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 10.0f;
    private float turnSpeed = 120f;
    private float jumpVelocity = 500.0f;
    private float horizontalInput;
    private float forwardInput;
    private float jumpInput;
    private Rigidbody _rb;

    private Animator animator;
    private GameManager manager;
    private TaskManager taskManager;
    private bool canGet;
    private bool canDeliver;
    public float jumpAmount = 1000;
    private Vector3 bounceOffSet = new Vector3(-5, 0, 0);
    private Vector3 startPosition; // Store the starting position of the player
    
    private string currentTaskTitle = "";

    bool isGround = true;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        startPosition = transform.position;  // Store the initial position of the player
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        taskManager = GameObject.Find("TaskManager").GetComponent<TaskManager>();
        _rb = GetComponent<Rigidbody>();


    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal")*turnSpeed;
        forwardInput = Input.GetAxis("Vertical")*speed;
        if (Input.GetKeyDown(KeyCode.Space)  && isGround)
        {
            //jumpInput = jumpVelocity;
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
            Debug.Log("Jump!");
            isGround = false;
        }
    }

    void FixedUpdate()
    {
        //transform.Translate(-new Vector3(1, 0, 1) * Time.deltaTime * 10f);
        //mjumpInput = 0f;

        Vector3 rotation = Vector3.up * horizontalInput;
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        _rb.MovePosition(this.transform.position + this.transform.forward * forwardInput * Time.fixedDeltaTime);
        _rb.MoveRotation(_rb.rotation * angleRot);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Debug.Log("--- manager show pizza " + manager.showPizza);
        //Debug.Log("--- can get pizza " + canGet);
        //Debug.Log("--- manager show text " + canGet);
        if (manager.isGameActive)
        {
            //horizontalInput = Input.GetAxis("Horizontal");
            //forwardInput = Input.GetAxis("Vertical");
            float curSpeed = Time.deltaTime * speed * forwardInput;
            //transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
            //transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);
            animator.SetFloat("speed", curSpeed);

            
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (canGet)
                {
                    if (!manager.itemsInHand.Contains(currentTaskTitle))
                    {
                        manager.itemsInHand.Add(currentTaskTitle);
                        taskManager.updateTask(currentTaskTitle);
                    }
                    /*if (manager.pizza == 0)
                    {
                        manager.AddPizza(1);
                        manager.showPizza = false;
                    }*/
                }
                else if (canDeliver)
                {
                    if (manager.itemsInHand.Contains(currentTaskTitle))
                    {
                        manager.itemsInHand.Remove(currentTaskTitle);
                        float money = taskManager.completeTask(currentTaskTitle);
                        manager.AddMoney(money);
                        manager.unshowHint();
                        canDeliver = false;
                    }
                    /*if (manager.pizza == 1)
                    {
                        manager.DeliverPizza(1);
                        manager.showDeliver = false;
                    }*/
                }

            }

            if (transform.position.y <= -1)
            {
                ResetToStartPosition();
                TimeDecrease();
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
        transform.Translate(-new Vector3(1, 1, 1) * Time.deltaTime * 5f);

        //rb.MovePosition(-new Vector3(1,0,1) * 500f);
        //if(rb)
        //{
        //    rb.velocity = Vector3.zero;
        //    rb.angularVelocity = Vector3.zero;

        //}

        //transform.position = startPosition;

    }
    public void ResetToRandomPosition()
    {
        Debug.Log("Player is reseting to random positon.");
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

        }
        Vector3 randomPosition = new Vector3(Random.Range(0.0f, 120.0f), Random.Range(0.0f, 0.0f), Random.Range(0.0f, 120.0f));
        transform.position = randomPosition;
    }
    public void TimeDecrease()
    {
        Debug.Log("Time decrease");
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

        }
        manager.timeLeft -= 15.0f;
    }

    public void TimerIncrease()
    {
        Debug.Log("Time increase");
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

        }
        manager.timeLeft += 15.0f;
    }
    public void MoneyDecrease()
    {
        Debug.Log("Money decrease");
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

        }
        double curMoney = manager.GetMoney();
        if (curMoney >= 10)
        {
            manager.SetMoney(curMoney - 10);
        }
        else
        {
            manager.SetMoney(0);
        }
     
    }

    public void MoneyIncrease()
    {
        Debug.Log("Money increase");
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

        }
        double curMoney = manager.GetMoney();
        manager.SetMoney(curMoney + 10);

    }

    public void openMagicBox()
    {
        int randomNumber = Random.Range(0, 4); // Generates a random number between 0 (inclusive) and 4 (exclusive)
        if (randomNumber == 0)
        {
            TimeDecrease();
        }
        else if(randomNumber == 1)
        {
            TimerIncrease();
        }
        else if(randomNumber == 2)
        {
            MoneyDecrease();
        }
        else
        {
            MoneyIncrease();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("---Trigger entered with: " + other.gameObject.name); // This will print the name of the object the player collided with.
        if (other.gameObject.tag == "TreasureBox")
        {
            //TimerIncrease();
        }
        //if (other.gameObject.tag == "Bomb")
        //{
        //    MoneyDecrease();
         
        //}
        if (other.gameObject.tag == "Door")
        {
            ResetToRandomPosition();
          
        }
        //if(other.gameObject.tag == "Skulls")
        //{
        //    TimeDecrease();
           
        //}
        if(other.CompareTag("Obstacle"))
        {
            Debug.Log("--- Collided with Obstacle!");
            ResetToStartPosition();
        }

        if(other.gameObject.CompareTag("GetPlane"))
        {
            canGet = true;
            GameTask currGameTask = other.transform.parent.GetComponent<GameTask>();
            string title = currGameTask.getTitle();
            currentTaskTitle = title;
            manager.showHint("Get", currGameTask.getTitle());
        }
        
        if(other.gameObject.CompareTag("DeliveryPlane"))
        {
            canDeliver = true;
            GameTask currGameTask = other.transform.parent.GetComponent<GameTask>();
            string title = currGameTask.getTitle();
            currentTaskTitle = title;
            manager.showHint("Deliver",currGameTask.getTitle());
        }
        if(other.transform.name.Contains("Road"))
        {
            isGround = true;
        }

        /*if(other.gameObject.CompareTag("GetPlane"))
        {
            manager.showText = true;
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
            
        }*/

        /*if(other.gameObject.tag == "DeliveryPlane")
        {
            manager.showText = true;
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
        }*/
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("--- exit trigger "+ other.gameObject.tag);
        if ( other.gameObject.tag == "DeliveryPlane" || other.gameObject.tag == "GetPlane")
        {
            canGet = false;
            canDeliver = false;
            manager.showText = false;
            manager.unshowHint();
            currentTaskTitle = "";
        }

    }

}