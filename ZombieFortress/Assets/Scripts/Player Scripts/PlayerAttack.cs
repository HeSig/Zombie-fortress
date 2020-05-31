using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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
    public int maxAmmoMagazine = 4;
    private int currentAmo;
    private bool isReloading = false, isShooting = false;
    public Animator animator;   
    public GameObject gameCamera;
    private TextMeshProUGUI ammo; 

    [SerializeField] private AudioSource reloadSound;

    [SerializeField] private AudioSource emptyMagazine;


    void Start(){

        currentAmo = maxAmmoMagazine;
        gameCamera = GameObject.FindGameObjectsWithTag("UI")[0];
        ammo = gameCamera.gameObject.transform.Find("Ammo").GetComponent<TMPro.TextMeshProUGUI>();
        ammo.text = "Ammo " + maxAmmoMagazine + " / ∞";
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
        
        if(currentAmo <= 0 && Input.GetMouseButtonDown(0) && !isShooting && Time.time > nextTimeToFire) {
            nextTimeToFire = Time.time + 0.5f / fireRate;
            emptyMagazine.Play();
        }
        
        if(Input.GetMouseButtonDown(0) && Time.time > nextTimeToFire && !isReloading && currentAmo > 0) {
 
                nextTimeToFire = Time.time + 1f / fireRate;
                shoot();
        }      
        if(Input.GetKeyDown(KeyCode.R) && currentAmo != 4 ) {
                StartCoroutine(Reload());
                return;
        }
        
    }
    IEnumerator Reload(){
            float reloadTime = 2f;
            isReloading = true;
            animator.SetBool("Reloading", true);
            yield return new WaitForSeconds(reloadTime - .25f);
            reloadSound.Play();
            animator.SetBool("Reloading", false);
            yield return new WaitForSeconds(.25f);
            currentAmo = maxAmmoMagazine;
            ammo.text = "Ammo " + currentAmo + " / ∞";
            isReloading = false;
    }

    void shoot(){
        isShooting = true;
        weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)){
            //Debug.Log(hit.transform.name);
            fireBullet(30);
            if(hit.rigidbody != null) {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
        }
        muzzleFlash.Play();
        currentAmo--;
        ammo.text = "Ammo " + currentAmo + " / ∞";
        isShooting = false;
    }

    void fireBullet(int damage) {
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