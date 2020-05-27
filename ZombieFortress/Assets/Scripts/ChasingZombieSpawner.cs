using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingZombieSpawner : MonoBehaviour
{

    public GameObject zombieType;
    //GameObject zombie;

    //Function to start spawning zombies.
    public void SpawnZombie()
    {
        Instantiate(zombieType, transform.position, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawCube(transform.position, new Vector3(1, 2, 1));
        Gizmos.DrawIcon(transform.position, "ZombieSpawner.png", true);
    }
}
