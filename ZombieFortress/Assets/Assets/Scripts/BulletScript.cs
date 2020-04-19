using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    int damage = 2;
    public Vector3 direction;

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
        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<ZombieScript>().health -= damage;
            
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(direction, 1f);
    }
}
