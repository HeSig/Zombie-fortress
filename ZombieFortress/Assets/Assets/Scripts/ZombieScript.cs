using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    public float moveSpeed = 0.0f;
    public float health = 15f;
    public GameObject nextNode;


    void Create()
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
}
