using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildNode : MonoBehaviour {

    public Color hoverColor;
    public Vector3 positionOffset;
    public GameObject towerShop;

    [Header("Optional")]
    public GameObject tower;

    private Renderer rend;
    private Color startColor;

    private BuildManager buildManager;
    private GameObject crosshair;

    void Start() {

        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
        crosshair = GameObject.FindWithTag(Tags.CROSSHAIR);

    }

    public Vector3 GetBuildPosition() {
        return transform.position + positionOffset;
    }

    void OnTriggerEnter(Collider other) {
    
        if(other.gameObject == GameObject.FindWithTag(Tags.PLAYER_TAG)) {

            if(tower != null)
                 return;

            else {

                buildManager.SetCurrentNode(this);
        
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

}
