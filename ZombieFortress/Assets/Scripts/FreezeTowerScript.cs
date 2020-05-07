using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTowerScript : MonoBehaviour
{
    List<GameObject> zombies;
    public GameObject freezeBulletObject;
    GameObject towerBall;
    float strength = 1005.5f;
    bool ready = true;
    // Start is called before the first frame update
    int bulletSpeed = 15;
    RotationScript rotationScript;


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
                zombies.RemoveAt(0);
            }
            else
            {
                var targetRotation = Quaternion.LookRotation(zombies[0].transform.position - towerBall.transform.position);
                var str = Mathf.Min(strength * Time.deltaTime, 1);
                towerBall.transform.rotation = Quaternion.Lerp(towerBall.transform.rotation, targetRotation, str);

                if (ready == true)
                {
                    GameObject freezeBullet = Instantiate(freezeBulletObject, towerBall.transform.position, Quaternion.identity);
                    freezeBullet.GetComponent<Rigidbody>().velocity = towerBall.transform.forward * bulletSpeed;
                    rotationScript.fire();
                    ready = false;
                    StartCoroutine("Reload");
                }
            }
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(2f);   //Wait 2 sec
        ready = true;
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
