using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    public bool state = false;

    public GameObject enemy1;
    public Transform enemy1Pos;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (state)
        {
            state = false;

            StartCoroutine("initEnemy");
        }
    }

    public void initEnemy2(GameObject obj)
    {
        enemy1 = obj;
        enemy1Pos = obj.transform;
        state = true;
    }

    IEnumerator initEnemy()
    {
        yield return new WaitForSeconds(5);
        GameObject obj = Instantiate(enemy1);
        obj.SetActive(true);
        obj.transform.position = enemy1Pos.position;
    }
}
