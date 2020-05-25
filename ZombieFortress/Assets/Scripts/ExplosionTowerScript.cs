using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 
 * Script for the standard tower prefab.
 * 
 * 
 **/
public class ExplosionTowerScript : MonoBehaviour
{
    List<GameObject> zombies;
    public GameObject bulletObject;
    GameObject towerBall;
    float strength = 1005.5f;
    bool ready = true;
    int bulletSpeed = 20;
    int bulletDamage = 10;
    RotationScript rotationScript;
    int level = 1;
    int target;
    AudioSource audioSource;


    void Start()
    {
        zombies = new List<GameObject>();
        towerBall = gameObject.transform.Find("Ball").gameObject;
        rotationScript = GetComponentInChildren<RotationScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (zombies.Count > 0)
        {

            if (zombies[0] == null || zombies[0].GetComponent<ZombieScript>().dead)
            {
                while(zombies[0] == null || zombies[0].GetComponent<ZombieScript>().dead) { 
                    zombies.RemoveAt(0);
                }
            }
            else
            {
                //var dir = (zombies[0].transform.position - transform.position).normalized;
                //bullet.GetComponent<BulletScript>().direction = -dir;
                if(zombies.Count >= 2) {
                    target = 1;
                }
                else
                {
                    target = 0;
                }
                var targetRotation = Quaternion.LookRotation(zombies[target].transform.position - towerBall.transform.position);
                var str = Mathf.Min(strength * Time.deltaTime, 1);
                towerBall.transform.rotation = Quaternion.Lerp(towerBall.transform.rotation, targetRotation, str);

                if (ready == true)
                {
                    fireBullet(bulletDamage * level);
                    ready = false;
                    StartCoroutine("Reload");
                }
            }
        }
    }

    IEnumerator Reload()
    {

        yield return new WaitForSeconds(2f);   //Wait
        ready = true;
    }

    void fireBullet(int damage)
    {
        GameObject bullet = Instantiate(bulletObject, towerBall.transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = towerBall.transform.forward * bulletSpeed;
        bullet.GetComponent<BombBulletScript>().damage = damage;
        rotationScript.fire();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            zombies.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (zombies.Contains(other.gameObject))
        {
            zombies.Remove(other.gameObject);
        }
    }

    Vector3 CalculateMidVector(Vector3 first, Vector3 second)
    {
        return second - first;
    }
}
