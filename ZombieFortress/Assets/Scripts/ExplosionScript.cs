using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{

    float maxExplosionRadius = 5f;
    float explosionSpeed = 0.1f;
    Vector3 scaleChange;
    public int damage = 10;
    // Start is called before the first frame update
    void Start()
    {
        scaleChange = new Vector3(explosionSpeed, explosionSpeed, explosionSpeed);
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.localScale.x >= maxExplosionRadius)
        {
            Destroy(gameObject);
        }
        transform.localScale += scaleChange;
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy" && !col.GetComponent<ZombieScript>().dead)
        {
            col.gameObject.GetComponent<ZombieScript>().health -= damage;
        }
    }
}
