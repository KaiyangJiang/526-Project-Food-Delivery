using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombs : MonoBehaviour
{

    public float spawnInterval = 5.0f;
    private Vector3 minSpawnPoint = new Vector3(0, 0, 0);
    private Vector3 maxSpawnPoint = new Vector3(120, 0, 0);
    // 120
    private MeshRenderer bombRenderer;  // Reference to the MeshRenderer

   
    // Start is called before the first frame update
    void Start()
    {
        bombRenderer = GetComponent<MeshRenderer>();
        if (bombRenderer == null)
        {
            Debug.LogError("No MeshRenderer found on the bomb object. Make sure the script is attached to the correct object.");
            return;
        }
        StartCoroutine(SpawnBombRoutine());
    }




    IEnumerator SpawnBombRoutine()
    {
        while (true)
        {
            // Spawn the bomb at a random position
            transform.position = GetRandomSpawnPoint();

            // Make the bomb visible
            bombRenderer.enabled = true;

            // Wait for 5 seconds
            yield return new WaitForSeconds(5.0f);

            // Hide the bomb
            bombRenderer.enabled = false;

            // Wait for the spawn interval before next spawn
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    Vector3 GetRandomSpawnPoint()
    {
        float x = Random.Range(minSpawnPoint.x, maxSpawnPoint.x);
        float y = 0; // As specified
        //float z = Random.Range(minSpawnPoint.z, maxSpawnPoint.z);
        float z = 60;
        return new Vector3(x, y, z);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
