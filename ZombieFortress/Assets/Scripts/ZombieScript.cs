using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public float health = 15f;
    public GameObject nextNode;
    public int damage = 1;
    GameObject goal;
    GameControllerScript mainControllerScript;
    GameObject mainController;

    // Start is called before the first frame update
    void Start()
    {
        mainControllerScript = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameControllerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            mainControllerScript.zombieCount -= 1;
            Destroy(gameObject);
        }

        if(nextNode != null) {
            float step = moveSpeed * Time.deltaTime;
            step = step / 7;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(nextNode.transform.position.x, transform.position.y, nextNode.transform.position.z), step);
            if(transform.position.x == nextNode.transform.position.x && transform.position.z == nextNode.transform.position.z)
            {
                if(nextNode.tag != "Finish") { 
                    nextNode = nextNode.GetComponent<WalkNodeScript>().nextNode;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "Finish")
        {
            Debug.Log("Collission");
            goal = other.gameObject;
            StartCoroutine("DealGateDamage");
        }
        if (other.gameObject.name == "Bullet_Prefab")
        {
            Debug.Log("HIT");
            health -= 1;
        }
    }

    IEnumerator DealGateDamage()
    {
        goal.GetComponent<GateScript>().health -= damage;
        yield return new WaitForSeconds(1f);   //Wait
        StartCoroutine("DealGateDamage");
    }
}
