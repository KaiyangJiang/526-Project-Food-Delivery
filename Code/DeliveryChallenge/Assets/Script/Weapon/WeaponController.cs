using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private Transform centerTransform;
    private float orbitSpeed = 75f;
    private List<GameObject> enemies = new List<GameObject>();
    private float detectRate = 0.1f;
    private GameObject enemyToShoot;
    private float shootTimer;

    public GameObject bulletPrefab;
    public float sphereRadius;
    public float shootRate = 1.0f;

    
    // Start is called before the first frame update
    void Start()
    {
        centerTransform = transform.parent;
        shootTimer = shootRate;
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(centerTransform.position, transform.up, orbitSpeed * Time.deltaTime);

        detectRate -= Time.deltaTime;
        if(detectRate <= 0)
        {
            detectRate = 0.1f;
            Debug.Log("shooting: here");
            Collider[] colliders = Physics.OverlapSphere(transform.parent.position, sphereRadius);
            Debug.Log("shooting: colliders size " + colliders.Length);
            foreach (Collider collider in colliders) {
                if(collider.tag == "Enemy") 
                {
                    Debug.Log("shooting: " + collider.gameObject.name);
                    enemyToShoot = collider.gameObject;
                    break;
                }
                enemyToShoot = null;
            }
        }

        if(enemyToShoot != null)
        {
            // adjust direction of weapons
            Vector3 directionToDestination = (enemyToShoot.transform.position - transform.position).normalized;
            directionToDestination.y = 0;
            transform.forward = -directionToDestination;

            // shoot bullets
            shootTimer -= Time.deltaTime;
            if(shootTimer <= 0) 
            {
                GameObject bullet = GameObject.Instantiate(bulletPrefab, centerTransform.position, Quaternion.identity);
                //bullet.transform.position += new Vector3(0, 3f, 0);
                bullet.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                bullet.GetComponent<BulletController>().SetTarget(enemyToShoot);
                shootTimer = shootRate;
            }
        } 
        else
        {
            shootTimer = shootRate;
        }
    }
}
