using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShop : MonoBehaviour
{

    BuildManager buildManager;

    void Start() { buildManager = BuildManager.instance; }

    public void PurchaseStandardTower() { 
        buildManager.BuildTower(buildManager.standardTowerPrefab); 
    }

}
