﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{

    public GameObject firstNode;
    public GameObject normalZombieObject;
    public int numberOfZombies;

    // Start is called before the first frame update
    void Start()
    {
        SpawnZombies(5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnZombies(int i)
    {
        numberOfZombies = i;
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn()
    {
        if(numberOfZombies > 0) { 
            ZombieScript script;
            GameObject zombie = Instantiate(normalZombieObject, transform.position, Quaternion.identity);
            script = zombie.GetComponent<ZombieScript>();
            script.nextNode = firstNode;

            yield return new WaitForSeconds(1.0f);   //Wait
            numberOfZombies--;
            StartCoroutine("Spawn");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, new Vector3(1, 2, 1));
        if (firstNode != null) { 
            Gizmos.DrawLine(transform.position, firstNode.transform.position);
        }
        Gizmos.DrawIcon(transform.position, "ZombieSpawner.png", true);
    }

    private void OnDrawGizmosSelected()
    {
        if (firstNode != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, firstNode.transform.position);
        }
    }
}
