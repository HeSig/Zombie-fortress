using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBulletScript : MonoBehaviour
{
    public int damage = 10;
    public Vector3 direction;
    bool exploding = false;
    float maxExplosionRadius = 5f;
    float explosionSpeed = 0.1f;

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
        if (exploding)
        {

            if(GetComponent<SphereCollider>().radius >= maxExplosionRadius)
            {
                Destroy(gameObject);
            }
            GetComponent<SphereCollider>().radius += explosionSpeed;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy" && !exploding && !col.GetComponent<ZombieScript>().dead)
        {
            explode();
        }else if(col.gameObject.tag == "Enemy" && exploding && !col.GetComponent<ZombieScript>().dead)
        {
            Debug.Log("TEST");
            col.gameObject.GetComponent<ZombieScript>().health -= damage;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    void explode()
    {
        exploding = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, GetComponent<SphereCollider>().radius/2);
    }
}
