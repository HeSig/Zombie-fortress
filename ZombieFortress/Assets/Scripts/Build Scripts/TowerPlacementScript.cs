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
    private GameObject crosshair;
    public bool hasTower;

    void Start() {

        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
        crosshair = GameObject.FindWithTag(Tags.CROSSHAIR);
        hasTower = false;

    }

    void OnTriggerEnter(Collider other) {

        if(other.gameObject == GameObject.FindWithTag(Tags.PLAYER_TAG)) {

            if(hasTower) {

                 return;

            } else {

                buildManager.SetBuildPosition(transform, positionOffset);
        
                towerShop.SetActive(true);
                crosshair.SetActive(false);

            }
        }
    }

    void OnTriggerExit() {

        towerShop.SetActive(false);
        crosshair.SetActive(true);

    }


    void OnMouseEnter() { rend.material.color = hoverColor; }

    void OnMouseExit() { rend.material.color = startColor; }

    void TowerBuilt() { 

        towerShop.SetActive(false);
        hasTower = true;
        
    }
}
