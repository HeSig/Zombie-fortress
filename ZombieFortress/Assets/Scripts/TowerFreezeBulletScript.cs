using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFreezeBulletScript : MonoBehaviour
{
    int damage = 5;
    public Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (transform.position.y < 0)
        {
            
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {

            col.gameObject.GetComponent<ZombieScript>().moveSpeed = 2f;
            
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<TrailRenderer>().emitting = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(direction, 1f);
    }
}