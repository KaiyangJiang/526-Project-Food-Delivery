using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public Transform patrolRoute;
    public List<Transform> locations;
    private UnityEngine.AI.NavMeshAgent agent;
    private int locationIndex;
    private Transform Player;

    // Start is called before the first frame update
    void Start()
    {
        InitializePatrolRoute();
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        Player = GameObject.Find("Player").transform;
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
            if(this.GetType().ToString().Equals("UnityEngine.CapsuleCollider")){
                print("xxx cap colid");
                Destroy(this.gameObject);
            }
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
}
