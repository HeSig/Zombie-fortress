using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class HealthScript : MonoBehaviour
{

    private ZombieAnimator enemy_Anim;
    private NavMeshAgent navAgent;
    private ZombieController enemy_Controller;
    public float MAXHEALTH = 100f;
    public float health = 100f;
    public bool is_Zombie, is_Player;
    private bool is_Dead;
    private ZombieAudio zombieAudio;
    // Start is called before the first frame update



    [Header("Unity stuff")]
    public Image healthBar;
    public Canvas healthCanvas;
    public GameObject coin;
    public int coinDrop;

    void Awake()
    {
        if (is_Zombie)
        {
            enemy_Anim = GetComponent<ZombieAnimator>();
            enemy_Controller = GetComponent<ZombieController>();
            navAgent = GetComponent<NavMeshAgent>();
            //get enemy Audio
            zombieAudio = GetComponentInChildren<ZombieAudio>();
            healthBar.fillAmount = health / 100f;
        }
        if (is_Player)
        {

        }
    }

    void Update()
    {
        if (is_Zombie) { 
            healthBar.fillAmount = health / MAXHEALTH;
        }
    }

    public void ApplyDamage(float damage)
    {
        if (is_Dead)
            return;

        health -= damage;
        if (is_Player)
        {
            //display health


        }
        if (is_Zombie)
        {
            if (enemy_Controller.Enemy_State == ZombieState.PATROL)
            {
                enemy_Controller.chase_Distance = 50f;
            }
            print("Health is: " + health);
        }
        if (health <= 0f)
        {
            PlayerDied();
            is_Dead = true;
        }

    } //apply damage

    void PlayerDied()
    {
        if (is_Zombie)
        {
            navAgent.velocity = Vector3.zero;
            navAgent.isStopped = true;
            enemy_Controller.enabled = false;
            enemy_Anim.Dead();
            //StartCoroutine
            StartCoroutine(DeadSound());
            healthCanvas.enabled = false;
        }
        if (is_Player)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ZOMBIE_TAG);
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<ZombieController>().enabled = false;
            }
            // call enemy manager to stop spawning enemies
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<WeaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(false);


        }
        if (tag == Tags.PLAYER_TAG)
        {
            Invoke("RestartGame", 3f);
        }
        else
        {
            Invoke("TurnOffGameObject", 3f);
        }

    }
    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }

    IEnumerator DeadSound()
    {
        yield return new WaitForSeconds(0.3f);
        zombieAudio.Play_DeadSound();
    }



}
