using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBulletScript : MonoBehaviour
{
    public int damage = 10;
    public Vector3 direction;
    public GameObject explosionObject;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 0)
        {
            explode();
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy" && !col.GetComponent<ZombieScript>().dead)
        {
            explode();
        }
    }

    void explode()
    {
        GameObject explosion = Instantiate(explosionObject, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

