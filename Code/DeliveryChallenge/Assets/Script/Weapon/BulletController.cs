using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private GameObject target;
    private bool isDestroyed = false;

    public float speed = 20.0f;
    public int damage = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target==null || target.GetComponent<EnemyController>().IsDestroyed())
        {
            isDestroyed = true;
            Destroy(this.gameObject);
            return;
        }
        transform.LookAt(target.transform);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        if((target.transform.position - transform.position).magnitude < 0.1f && !isDestroyed)
        {
            target.GetComponent<EnemyController>().Damage(damage);
            isDestroyed = true;
            Destroy(this.gameObject);
        }
    }

    public void SetTarget(GameObject _target)
    {
        this.target = _target;
    }
}
