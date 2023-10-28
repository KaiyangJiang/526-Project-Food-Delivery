using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapToggle : MonoBehaviour
{
    public GameObject overviewMap; 
    public bool isMapVisible = true; 

    public void ToggleMapVisibility()
    {
        isMapVisible = !isMapVisible;
        overviewMap.SetActive(isMapVisible);
    }
}
