using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {
    [SerializeField] private float size = 5f;
    //public float Size { get { return size; }}

    public Vector3 GetNearestPointOnGrid(Vector3 position){

        position -= transform.position;
        int xCount = Mathf.RoundToInt(position.x / size);
        int yCount = Mathf.RoundToInt(position.y / size);
        int zCount = Mathf.RoundToInt(position.z / size);
        Vector3 result = new Vector3((float)xCount * size, (float)yCount * size, (float) zCount * size);
        result += transform.position;

        return result;
    } 

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        for(float x=50; x < 400; x += size) {      // x < 400
            for(float z=50; z < 400; z += size) {   // z < 400
                var point = GetNearestPointOnGrid(new Vector3(x, 20f, z));
                Gizmos.DrawSphere(point, 0.2f);
                //Gizmos.DrawCube(point, 0.2f);
            }
        }    
    }
}
