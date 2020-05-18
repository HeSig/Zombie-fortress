using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerShop : MonoBehaviour {

    public TowerBlueprint[] towerBlueprints;

    BuildManager buildManager;

    void Start() { 

        buildManager = BuildManager.instance;

        Text text;
        int i = 0;
        
        foreach (Transform child in this.transform) {
            text = child.GetChild(0).GetComponent<Text>();
            text.text = towerBlueprints[i].cost + " kr";
            i++;
        }

    }

    void Update() {

         if(Input.GetKeyDown(KeyCode.Alpha1)) {
            buildManager.setTowerToBuild(towerBlueprints[0]);
        }

        if(Input.GetKeyDown(KeyCode.Alpha2)) {
            buildManager.setTowerToBuild(towerBlueprints[1]);
        }

        if(Input.GetKeyDown(KeyCode.Alpha3)) {
            
        }

        if(Input.GetKeyDown(KeyCode.Alpha4)) {
            
        }

        if(Input.GetKeyDown(KeyCode.Alpha5)) {
            
        }

        if(Input.GetKeyDown(KeyCode.Alpha6)) {
            
        }

    }


}
