using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacementScript : MonoBehaviour {

    public Color hoverColor;
    public Vector3 positionOffset;
    public GameObject towerShop;

    private Renderer rend;
    private Color startColor;

    private BuildManager buildManager;

    void Start() {

        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    } 

    void OnMouseDown() {

        buildManager.SetBuildPosition(transform, positionOffset);
        
        towerShop.SetActive(true);
        
    }

    void OnMouseEnter() { rend.material.color = hoverColor; }

    void OnMouseExit() { rend.material.color = startColor; }

    void CloseTowerShop() {
        towerShop.SetActive(false);
    }
}
