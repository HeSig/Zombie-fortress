﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{

    private Animator anim;

    //[SerializeField] private GameObject muzzleFlash;

    [SerializeField] private AudioSource shootSound, pump_Sound;

    void Awake() {
        anim = GetComponent<Animator>();
    }

    public void ShootAnimation() {
          anim.SetTrigger(AnimationTags.SHOOT_TRIGGER);  
    }
    
    /*
    void Turn_On_MuzzleFlash() {
        muzzleFlash.SetActive(true);
    }

    void Turn_Off_MuzzleFlash() {
        muzzleFlash.SetActive(false);
    }
    */

    void Play_ShootSound() {
        shootSound.Play();
    }
    
    void Play_PumpSound() {
        pump_Sound.Play();
    }
    

}
