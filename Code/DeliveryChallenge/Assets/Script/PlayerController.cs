using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float speed = 10.0f;
    private float turnSpeed = 120f;
    private float jumpVelocity = 500.0f;
    private float PushForce = 100f;
    private bool EnemyTrigger = false;
    private float horizontalInput;
    private float forwardInput;
    private bool triggerSkill;
    private float jumpInput;
    private Rigidbody _rb;
    private Vector3 collisionDir;
    public GameObject Overviewmap;


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
    bool firstTime = true;
    public GameDataCollector gameDataCollector;

    private WeaponManager weaponManager;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        startPosition = transform.position;  // Store the initial position of the player
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        taskManager = GameObject.Find("TaskManager").GetComponent<TaskManager>();
        _rb = GetComponent<Rigidbody>();
        //gameDataCollector = FindObjectOfType<GameDataCollector>();

        weaponManager = GameObject.Find("WeaponManager").GetComponent<WeaponManager>();
        // weaponManager.Add("M1911");
        // weaponManager.Add("AK74");
        // weaponManager.Add("Uzi");
        Overviewmap.SetActive(false);
    }

    void Update()
    {
        if (manager.isGameActive)
        {
            horizontalInput = Input.GetAxis("Horizontal") * turnSpeed;
            forwardInput = Input.GetAxis("Vertical") * speed;
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (triggerSkill)
                {

                    instatMove();
                }
            }
            if (Input.GetKeyDown(KeyCode.Space) && isGround)
            {
                //jumpInput = jumpVelocity;
                _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
                Debug.Log("Jump!");
                isGround = false;
            }
        }
        else
        {
            //handel game not active
        }
    }

    void FixedUpdate()
    {
        //transform.Translate(-new Vector3(1, 0, 1) * Time.deltaTime * 10f);
        //mjumpInput = 0f;
        if (manager.isGameActive)
        {
            Vector3 rotation = Vector3.up * horizontalInput;
            Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
            _rb.MovePosition(this.transform.position + this.transform.forward * forwardInput * Time.fixedDeltaTime);
            _rb.MoveRotation(_rb.rotation * angleRot);
            if (EnemyTrigger)
            {
                _rb.AddForce((collisionDir + new Vector3(0f, 0.5f, 0f)) * PushForce + Vector3.up, ForceMode.Impulse);

                EnemyTrigger = false;
            }
        }
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
                        manager.updateStatus(currentTaskTitle + " Picked up", 1.5f, Color.white);
                        manager.itemsInHand.Add(currentTaskTitle);
                        taskManager.updateTask(currentTaskTitle);
                        manager.unshowHint();
                    }
                }
                else if (canDeliver)
                {
                    if (manager.itemsInHand.Contains(currentTaskTitle))
                    {
                        manager.itemsInHand.Remove(currentTaskTitle);
                        float money = taskManager.completeTask(currentTaskTitle);
                        manager.updateStatus(currentTaskTitle + " Delivered + "+money+"$", 1.5f, Color.yellow);
                        manager.AddMoney(money);
                        manager.unshowHint();
                        canDeliver = false;
                    }
              
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
    public void instatMove()
    {
        if (firstTime)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed * 100);
            firstTime = false;
        }
        else
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed * 300);
        }
        //_rb.MovePosition(this.transform.position + this.transform.forward * speed * 100 * Time.fixedDeltaTime);
        Debug.Log("Instant move");
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
        /*int randomNumber = Random.Range(0, 4); // Generates a random number between 0 (inclusive) and 4 (exclusive)
        
        if (randomNumber == 0)
        {
            TimeDecrease();
            manager.updateStatus("Time -15s", 1,Color.green);
        }
        else if(randomNumber == 1)
        {
            TimerIncrease();
            manager.updateStatus("Time +15s", 1, Color.green);
        }
        else if(randomNumber == 2)
        {
            MoneyDecrease();
            manager.updateStatus("Money -10$", 1,Color.yellow);
        }
        else
        {
            MoneyIncrease();
            manager.updateStatus("Money +10$", 1,Color.yellow);
        }*/

        manager.TreasureBoxPanel.SetActive(true);
    }

    void ShowMap()
    {
        Overviewmap.SetActive(true);
    }

    public void CloseMap()
    {
        Overviewmap.SetActive(false);
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
        /*
        if (other.gameObject.tag == "Door")
        {
            ResetToRandomPosition();
            gameDataCollector.magicDoorsUsed++;

        }
        */
        if (other.gameObject.tag == "Door")
        {
            ShowMap();
        }
        //if(other.gameObject.tag == "Skulls")
        //{
        //    TimeDecrease();

        //}
        if (other.CompareTag("Obstacle"))
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
            if(!manager.itemsInHand.Contains(currentTaskTitle)) manager.showHint("Get", currGameTask.getTitle());
        }
        
        if(other.gameObject.CompareTag("DeliveryPlane"))
        {
            canDeliver = true;
            GameTask currGameTask = other.transform.parent.GetComponent<GameTask>();
            string title = currGameTask.getTitle();
            currentTaskTitle = title;
            if(manager.itemsInHand.Contains(currentTaskTitle)) manager.showHint("Deliver",currGameTask.getTitle());
        }
        if(other.transform.name.Contains("Road"))
        {
            isGround = true;
        }

        if(other.gameObject.name == "Enemy")
        {
            EnemyTrigger = true;
            collisionDir = (this.transform.position - other.gameObject.transform.position).normalized;
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

    public void setTriggerSkill(bool canTrigger)
    {
        Debug.Log("set trigger skill");
        this.triggerSkill = canTrigger;
        manager.skillHint.gameObject.SetActive(canTrigger);
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


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == ("Enemy"))
        {
           
            //d=Debug.Log(Vector3.Distance(Player.position, transform.position));
            if (Vector3.Distance(other.transform.position, transform.position) < 0.5f && !other.GetComponent<EnemyController>().IsDestroyed())
            {
                other.GetComponent<EnemyController>().Eliminate();
                manager.DecreaseMoney(10);
                manager.updateStatus("Hit by enemy -10$", 1, Color.red);
            }

        }
    }

}