using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private WeaponManager weapon_Manager;
    private Camera mainCam;
    private GameObject crosshair;

    private float nextTimeToFire;


    public float fireRate = 1f;

    public float damage = 20f;

    void Awake() {
 
        weapon_Manager = GetComponent<WeaponManager>();

        mainCam = Camera.main;

        crosshair = GameObject.FindWithTag(Tags.CROSSHAIR);

    }

    // Update is called once per frame
    void Update() {

        WeaponShoot();
        
    }
    
    void WeaponShoot() {

            if(Input.GetMouseButtonDown(0) && Time.time > nextTimeToFire ) {
 
                nextTimeToFire = Time.time + 1f / fireRate;

                weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();

                BulletFired();

            }
        
    }

    void BulletFired() {

        RaycastHit hit;

        if(Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit)) {

            if(hit.transform.tag == Tags.ENEMY_TAG) {
                hit.transform.GetComponent<ZombieScript>().health -= damage;        
            }   
            if(hit.transform.tag == Tags.ZOMBIE_TAG){
                hit.transform.GetComponent<HealthScript>().ApplyDamage(damage);
                //print("We hit: "+ hit.transform.gameObject.tag);
            }

        }

    }

}
