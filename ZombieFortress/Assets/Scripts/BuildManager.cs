using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;
    
    void Awake() {

        if(instance != null) { return; }
        instance = this;
        
    }

    public GameObject standardTowerPrefab;

    private Transform buildPosition; 
    private Vector3 buildOffset;
 

    public void BuildTower(GameObject tower) {
        Instantiate(tower, buildPosition.position + buildOffset, buildPosition.rotation);
        SendMessage("CloseTowerShop");
    }

    public void SetBuildPosition(Transform pos, Vector3 offset) {
        buildPosition = pos;
        buildOffset = offset;
    }
}
