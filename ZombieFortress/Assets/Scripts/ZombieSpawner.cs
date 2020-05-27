using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{

    public GameObject firstNode;
    public GameObject[] ZombieList;
    public int numberOfZombies;
    //GameObject zombie;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Function to start spawning zombies.
    public void SpawnZombies(int i)
    {
        Debug.Log("Spawning " + i + " zombies.");
        numberOfZombies = i;
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn()
    {
        if(numberOfZombies > 0) {
            Instantiate(ZombieList[Random.Range(0, ZombieList.Length)], transform.position, Quaternion.identity).GetComponent<ZombieScript>().nextNode = firstNode;
            yield return new WaitForSeconds(2.5f);   //Wait
            numberOfZombies--;
            StartCoroutine("Spawn");
        }
        else
        {
            Debug.Log("No more zombos");
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
