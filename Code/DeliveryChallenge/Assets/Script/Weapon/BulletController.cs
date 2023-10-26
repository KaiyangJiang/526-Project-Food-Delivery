using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private GameObject target;

    public float speed = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.transform);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        if((target.transform.position - transform.position).magnitude < 0.1f)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetTarget(GameObject _target)
    {
        this.target = _target;
    }
}
