﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum ZombieState {
    PATROL,
    CHASE, 
    ATTACK
}

public class ZombieController : MonoBehaviour {

    private ZombieAnimator enemy_Anim;
    private NavMeshAgent navAgent;
    private ZombieState enemy_State;
    public float walk_Speed = 0.5f;
    public float run_Speed = 4f;
    public float chase_Distance = 7f;  //increase this
    private float current_Chase_Distance;
    public float attack_Distance = 1.8f;
    public float chase_After_Attack_Distance = 2f;
    public float patrol_Radius_Min = 20f, patrol_Radius_Max = 60f;
    public float patrol_For_This_Time = 15f;
    private float patrol_Timer;
    public float wait_Before_Attack = 2f;
    private float attack_Timer;
    private Transform target;
    public GameObject attack_Point;

    private ZombieAudio zombie_Audio;

    void Awake() {
        enemy_Anim = GetComponent<ZombieAnimator>();
        navAgent = GetComponent<NavMeshAgent>();

        target = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;
        zombie_Audio = GetComponentInChildren<ZombieAudio>();
     }

    // Start is called before the first frame update
    void Start() {
        enemy_State = ZombieState.PATROL;
        patrol_Timer = patrol_For_This_Time;
        //when the enemy first gets to the player attack right away 
        attack_Timer = wait_Before_Attack;
        //memorize the value of chase distance so that we can put it back
        current_Chase_Distance = chase_Distance;

    }

    // Update is called once per frame
    void Update() {
        if(enemy_State == ZombieState.PATROL)
        {
            Patrol();
        }
        if(enemy_State == ZombieState.ATTACK)
        {
            Attack();
        }
        if(enemy_State == ZombieState.CHASE)
        {
            Chase();
        }
    }



    void Patrol() {
        navAgent.isStopped = false;
        navAgent.speed = walk_Speed;
        patrol_Timer += Time.deltaTime;
        if(patrol_Timer > patrol_For_This_Time)
        {
            SetNewRandomDestination();
            patrol_Timer = 0f;
        }

        if(navAgent.velocity.sqrMagnitude > 0)
        {
            enemy_Anim.Walk(true);
        }else
        {
            enemy_Anim.Walk(false);
        }
        if(Vector3.Distance(transform.position, target.position) <= chase_Distance) {
            enemy_Anim.Walk(false);
            enemy_State = ZombieState.CHASE;
            zombie_Audio.Play_ScreamSound();
        }


    }

    void Chase()
    {
        //enable the agent to move again
        navAgent.isStopped = false;
        navAgent.speed = run_Speed;
        //set the players position as the destination necause we are chasing the player
        navAgent.SetDestination(target.position);
        if(navAgent.velocity.sqrMagnitude > 0) {
            enemy_Anim.Run(true);
        }else {
            enemy_Anim.Run(false);
        }
        //if the distance between enemy and the player is less than attack distance 
        if(Vector3.Distance(transform.position, target.position)<= attack_Distance) {
            //stop the animation
            enemy_Anim.Run(false);
            enemy_Anim.Walk(false);
            enemy_State = ZombieState.ATTACK;
            //reset the chase distance to previous
            if(chase_Distance != current_Chase_Distance) {
                chase_Distance = current_Chase_Distance;
            }
        }else if(Vector3.Distance(transform.position, target.position) > chase_Distance) {
            //player run away from enemy 
            //stop running
            enemy_Anim.Run(false);
            enemy_State = ZombieState.PATROL;
            //reset the patrol timer so that the function can calculate the new patrol destination right away
            patrol_Timer = patrol_For_This_Time;
            //reset the chase distance to previous
            if(chase_Distance != current_Chase_Distance)
            {
                chase_Distance = current_Chase_Distance;
            }
        }
    }

    void Attack()
    {
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;
        attack_Timer += Time.deltaTime;
        if(attack_Timer > wait_Before_Attack)
        {
            enemy_Anim.Attack();
            attack_Timer = 0f;
            zombie_Audio.Play_AttackSound();
        }
        if(Vector3.Distance(transform.position, target.position) > attack_Distance + chase_After_Attack_Distance)
        {
            enemy_State = ZombieState.CHASE;
        }
    }
    
    void SetNewRandomDestination()
    {
        float rand_Radius = Random.Range(patrol_Radius_Min, patrol_Radius_Max);
        Vector3 randDir = Random.insideUnitSphere * rand_Radius;
        randDir += transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDir, out navHit, rand_Radius, -1);
        navAgent.SetDestination(navHit.position);
    }

    void Turn_On_AttackPoint(){
        attack_Point.SetActive(true);
    }

    void Turn_Off_AttackPoint(){
        if(attack_Point.activeInHierarchy){
            attack_Point.SetActive(false);
        }
    }

    public ZombieState Enemy_State {
        get {
            return enemy_State;
        }
        set {
            enemy_State = value;
        }
    }
}

