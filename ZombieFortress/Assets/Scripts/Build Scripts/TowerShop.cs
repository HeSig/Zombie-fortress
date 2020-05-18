using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShop : MonoBehaviour
{

    public TowerBlueprint gunTower;
    public TowerBlueprint explosionTower;

    BuildManager buildManager;

    void Start() { buildManager = BuildManager.instance; }

    void Update() {

         if(Input.GetKeyDown(KeyCode.Alpha1)) {
            buildManager.setTowerToBuild(gunTower);
        }

        if(Input.GetKeyDown(KeyCode.Alpha2)) {
            buildManager.setTowerToBuild(explosionTower);
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
