using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    [SerializeField] private Mesh spawnerMesh;
    private float currentTime, spawnDelay;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
    
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.gray;
        //Gizmos.DrawWireCube(transform.position, new Vector3(2,1,2));
        //Gizmos.DrawWireSphere(transform.position, 2);
        Gizmos.DrawMesh(spawnerMesh, transform.position, Quaternion.identity, Vector3.one * .6f);
        Gizmos.DrawIcon(transform.position, "ZombieSpawner.png", true);

    }
}
