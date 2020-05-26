using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;
    
    void Awake() {

        if(instance != null) { return; }
        instance = this;
        
    }

    private TowerBlueprint towerToBuild;
    private BuildNode currentNode;

 
    public void BuildTower()  {

        if (GameControllerScript.Money < towerToBuild.cost)
            return;

        

        GameObject tower = (GameObject)Instantiate(towerToBuild.prefab, currentNode.GetBuildPosition(), Quaternion.identity);
        if(tower != null)
        {
            GameControllerScript.Money -= towerToBuild.cost;
        }
        currentNode.tower = tower;

        currentNode.towerShop.SetActive(false);

    } 

    public void SetCurrentNode(BuildNode node) {
        currentNode = node;
    }

    public void setTowerToBuild(TowerBlueprint tower) {

        towerToBuild = tower;
        
        if (towerToBuild == null)
            return;
        
        BuildTower();

    }

}
