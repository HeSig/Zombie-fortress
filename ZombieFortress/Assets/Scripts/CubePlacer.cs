﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePlacer : MonoBehaviour {
    private Grid grid;
    public GameObject prefabGrid;

    private void Awake(){
        grid = FindObjectOfType<Grid>();
    }
    private void Update(){
        if(Input.GetMouseButtonDown(0)){
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hitInfo)){
                PlaceCubeNear(hitInfo.point);
            }
        }
    }
    private void PlaceCubeNear(Vector3 clickPoint){
        var finalPosition = grid.GetNearestPointOnGrid(clickPoint);
        //GameObject.CreatePrimitive(PrimitiveType.Sphere).transform.position = nearPoint;

        Instantiate(prefabGrid, finalPosition, transform.rotation);

    }


   
}
