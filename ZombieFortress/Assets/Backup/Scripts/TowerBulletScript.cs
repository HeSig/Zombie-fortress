using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBulletScript : MonoBehaviour
{
    public int damage = 10;
    public Vector3 direction;
    bool active = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(gameObject.transform.position.y);
        //float step = 1 * Time.deltaTime;
        //step = step * 7;
        //transform.position = Vector3.MoveTowards(transform.position, direction, step);

        if (transform.position.y < 0)
        {
            
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy" && active && !col.GetComponent<ZombieScript>().dead)
        {
            col.gameObject.GetComponent<ZombieScript>().health -= damage;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<TrailRenderer>().emitting = false;
            active = false;
        }
    }
}
