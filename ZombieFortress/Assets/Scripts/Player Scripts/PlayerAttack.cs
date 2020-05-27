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

    public float range = 20f;  
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public float impactForce = 30f;
    public int maxAmo = 4;
    private int currentAmo;
    public float reloadTime = 3f;

    private bool isReloading = false;
    public Animator animator;

    void Start(){
        currentAmo = maxAmo;
    }

    void Awake() {
 
        weapon_Manager = GetComponent<WeaponManager>();

        mainCam = Camera.main;

        crosshair = GameObject.FindWithTag(Tags.CROSSHAIR);
        shotgun = GameObject.FindGameObjectWithTag("Shotgun");

    }

    // Update is called once per frame
    void Update() {

        if(isReloading)
            return;

        if(currentAmo <= 0){
            StartCoroutine(Reload());
            return;
        }
        if(Input.GetMouseButtonDown(0) && Time.time > nextTimeToFire ) {
 
                nextTimeToFire = Time.time + 1f / fireRate;

                weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();

                shoot();
        }        
    }

    IEnumerator Reload(){
        isReloading = true;
        Debug.Log("Reloading");
        animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime - .25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(.25f);


        currentAmo = maxAmo;
        isReloading = false;

    }
    
    /*
    void WeaponShoot() {
        if(Input.GetMouseButtonDown(0) && Time.time > nextTimeToFire ) {
 
                nextTimeToFire = Time.time + 1f / fireRate;

                weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();

                shoot();
            }
    }
    */

    void shoot(){
        muzzleFlash.Play();
        currentAmo--;
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)){
            Debug.Log(hit.transform.name);
            fireBullet(30);
            if(hit.rigidbody != null) {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
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
