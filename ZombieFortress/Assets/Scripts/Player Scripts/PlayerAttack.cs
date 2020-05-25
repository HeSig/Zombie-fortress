using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private WeaponManager weapon_Manager;
    private Camera mainCam;
    private GameObject crosshair;
    public GameObject bulletObject;
    public GameObject shotgun;

    private float nextTimeToFire;


    public float fireRate = 1f;

    public float damage = 20f;

    void Awake() {
 
        weapon_Manager = GetComponent<WeaponManager>();

        mainCam = Camera.main;

        crosshair = GameObject.FindWithTag(Tags.CROSSHAIR);
        shotgun = GameObject.FindGameObjectWithTag("Shotgun");

    }

    // Update is called once per frame
    void Update() {

        WeaponShoot();
        
    }
    
    void WeaponShoot() {

            if(Input.GetMouseButtonDown(0) && Time.time > nextTimeToFire ) {
 
                nextTimeToFire = Time.time + 1f / fireRate;

                weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();

                fireBullet(30);

            }
        
    }

    void fireBullet(int damage)
    {
        shotgun = GameObject.FindGameObjectWithTag("Shotgun");
        float shotgunY = shotgun.transform.position.y;
        float shotgunX = shotgun.transform.position.x;
        Vector3 shotgunVector = new Vector3(shotgunX, shotgunY, shotgun.transform.position.z);
        GameObject bullet = Instantiate(bulletObject, shotgunVector, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = shotgun.transform.forward * 20;
        bullet.GetComponent<TowerBulletScript>().damage = damage;
        //rotationScript.fire();
    }

}
