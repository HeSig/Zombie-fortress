using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{

    public GameObject firstNode;
    public GameObject normalZombieObject;
    public GameObject BigZombieObject;
    GameObject ZombieToSpawn;
    public int numberOfZombies;
    GameObject zombie;

    // Start is called before the first frame update
    void Start()
    {
        //SpawnZombies(20);
        zombie = null;
        ZombieToSpawn = null;
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
            ZombieScript script;

            ZombieToSpawn = normalZombieObject;
            int num = Random.Range(0, 2);
            if(num == 1)
            {
                ZombieToSpawn = BigZombieObject;
            }

            zombie = Instantiate(ZombieToSpawn, transform.position, Quaternion.identity);
            script = zombie.GetComponent<ZombieScript>();
            script.nextNode = firstNode;

            yield return new WaitForSeconds(2.0f);   //Wait
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
