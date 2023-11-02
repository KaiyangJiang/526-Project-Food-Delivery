using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{

    public float hp = 100f;
    public Transform patrolRoute;
    public List<Transform> locations;
    private UnityEngine.AI.NavMeshAgent agent;
    private int locationIndex;
    private Transform Player;
    private GameManager manager;
    private float bonus = 15;

    private bool isDestroyed = false;

    public Image imageobj;

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
    }

    // Update is called once per frame
    void Update()
    {
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
          
            print("xxx cap colid");
           
            
        } 
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == ("Player"))
        {
            agent.destination = Player.position;
          
          
        }
    }
    private void OnTriggerExit(Collider other)
    {
        agent.destination = locations[locationIndex].position;
    }

    public void Damage(int dmg)
    {
        hp -= dmg;
        imageobj.fillAmount = hp / 100;
        //Debug.Log("hp: " + hp);
        if(hp <= 0)
        {
            hp = 0;
            Eliminate();
        }
    }

    public void Eliminate() 
    {
        EnemyManager.Instance.initEnemy2(gameObject);
        isDestroyed = true;
        gameObject.SetActive(false);
        manager.AddMoney(bonus);
        manager.updateStatus("Killing enemy bonus +15$", 1, Color.red);
        //Destroy(this.gameObject);
    }

    public bool IsDestroyed() 
    {
        return isDestroyed;
    }
}
