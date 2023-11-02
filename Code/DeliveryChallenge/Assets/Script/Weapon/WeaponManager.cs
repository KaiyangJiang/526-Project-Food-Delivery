using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public int maxWeaponNum = 6;

    private Dictionary<string, GameObject> weaponDictionary = new Dictionary<string, GameObject>();
    private Transform playerTransform;
    private List<string> weaponList = new List<string>();
    public GameDataCollector gameDataCollector;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] prefabs = Resources.LoadAll<GameObject>("Weapons");

        foreach (GameObject prefab in prefabs)
        {
            // Add each prefab to the dictionary using its name as the key
            weaponDictionary[prefab.name] = prefab;
            Debug.Log("weapon: " + prefab.name);
        }
        
        playerTransform = GameObject.Find("Player").transform;
        //this.Add("M1911");
        //this.Add("AK74");
        //this.Add("Uzi");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool Add(string weaponName) {
        if(weaponList.Count >= maxWeaponNum)
        {
            Debug.Log("You can only have a maximum of 6 weapons");
            return false;
        }
        CreateWeapon(weaponName, weaponList.Count);
        weaponList.Add(weaponName);
        GameDataCollector dataCollector = FindObjectOfType<GameDataCollector>();
        if (dataCollector != null)
        {
            dataCollector.weaponsBought++;
        }

        return true;
    }

    public void Remove(string weaponName) {
        foreach (string name in weaponList)
        {
            if(name == weaponName)
            {
            }
        }
    }

    private void CreateWeapon(string weaponName, int index) 
    {
        GameObject weaponPrefab = weaponDictionary[weaponName];
        float angle = index * 360f / maxWeaponNum;
        float radius = 3f;
        float x = radius * Mathf.Cos(Mathf.Deg2Rad * angle);
        float z = radius * Mathf.Sin(Mathf.Deg2Rad * angle);

        GameObject weapon = Instantiate(weaponPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        weapon.transform.parent = playerTransform;
        weapon.transform.localPosition = new Vector3(x, 3f, z);
        weapon.transform.localScale = new Vector3(6, 6, 6);
        weapon.transform.Rotate(0, 180 - angle, 0);
    }
}
