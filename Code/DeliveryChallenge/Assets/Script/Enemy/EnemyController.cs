using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{

    public float hp = 100f;
    public Transform patrolRoute;
    public List<Transform> locations;
    private UnityEngine.AI.NavMeshAgent agent;
    private int locationIndex;
    private Transform Player;
    private GameManager manager;
    private float bonus = 20;

    private bool isDestroyed = false;

    public Image imageobj;
    public GameDataCollector gameDataCollector;
    public GameObject exclamationMark;

    private void OnEnable()
    {
        hp = 100;
        imageobj.fillAmount = hp / 100;
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializePatrolRoute();
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        Player = GameObject.Find("Player").transform;
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        GameDataCollector dataCollector = FindObjectOfType<GameDataCollector>();
        exclamationMark.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.isInVisible)
        {
            agent.speed = 0f;
        }
        else if(manager.inTresureBox){
            agent.speed = 0f;
        }
        else
        {
            agent.speed = 3.0f;
        }
        if(agent.remainingDistance < 0.01f && !agent.pathPending)
        {
            MoveToNextLocation();
            Debug.Log("Change destination to next point..");
        }
    }

    void MoveToNextLocation()
    {
        if (locations.Count == 0)
            return;

        agent.destination = locations[locationIndex].position;
        locationIndex = (locationIndex + 1) % locations.Count;
    }

    void InitializePatrolRoute()
    {
        foreach(Transform t in patrolRoute)
        {
            locations.Add(t);
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.name == ("Player"))
        {
            agent.destination = Player.position;
            //exclamationMark.SetActive(true);
          
            print("xxx cap colid");
           
            
        } 
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == ("Player"))
        {
            agent.destination = Player.position;
            exclamationMark.SetActive(true);


        }
    }
    private void OnTriggerExit(Collider other)
    {
        agent.destination = locations[locationIndex].position;
        exclamationMark.SetActive(false);
    }

    public void Damage(int dmg)
    {
        hp -= dmg;
        imageobj.fillAmount = hp / 100;
        //Debug.Log("hp: " + hp);
        if(hp <= 0)
        {
            hp = 0;
            Eliminate(true);
            if(gameDataCollector != null)
            {
                gameDataCollector.monstersKilled++;
            }
        }
    }

    public void Eliminate(bool isKilled)
    {
        EnemyManager.Instance.initEnemy2(gameObject);
        isDestroyed = true;
        gameObject.SetActive(false);
        if (isKilled)
        {
            manager.AddMoney(bonus);
            manager.updateStatus("Killing enemy bonus +20$", 1, Color.green);
            if(SceneManager.GetActiveScene().buildIndex == 1)
            {
                manager.updateTutorial(4);
                manager.finishTutorialPanel.SetActive(true);
            }
            
        }
        else
        {
            if (manager.shield == true)
            {
                manager.updateStatus("Shield Off", 1, Color.red);
                manager.shield = false;
                manager.shieldIcon.SetActive(false);
            }
            else
            {
                manager.DecreaseMoney(5);
                manager.updateStatus("Hit by enemy -5$", 1, Color.red);
            }
            
        }
        
        //Destroy(this.gameObject);
    }

    public bool IsDestroyed() 
    {
        return isDestroyed;
    }
}
