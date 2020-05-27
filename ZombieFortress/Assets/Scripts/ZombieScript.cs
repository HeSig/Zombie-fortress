using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieScript : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float MAXHEALTH = 100f;
    public float health = 100f;
    public GameObject nextNode;
    public int damage = 1;
    GameObject goal;
    GameControllerScript mainControllerScript;
    GameObject mainController;
    public float rotationSpeed;
    private Quaternion _lookRotation;
    private Vector3 _direction;
    public bool dead = false;
    Animator animator;
    int scoreValue = 10;
    public int yoffset;

    [Header("Unity stuff")]
    public Image healthBar;
    public Canvas healthCanvas;
    public GameObject coin;
    public int coinDrop;


    // Start is called before the first frame update
    void Start()
    {
        yoffset = 0;
        scoreValue = 10;
        animator = GetComponentInChildren<Animator>();
        mainControllerScript = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameControllerScript>();
        rotationSpeed = 2f;
        healthBar.fillAmount = health / 100f;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = health / MAXHEALTH;

        if (health <= 0 && !dead)
        {
            mainControllerScript.zombieCount -= 1;
            mainControllerScript.score += scoreValue;
            animator.SetBool("Dead", true);
            GetComponent<Rigidbody>().freezeRotation = true;
            GetComponent<Rigidbody>().Sleep();
            dead = true;
            StartCoroutine("Die");
        }

        if (nextNode != null && !dead)
        {
            float step = moveSpeed * Time.deltaTime;

            _direction = (new Vector3(nextNode.transform.position.x, transform.position.y, nextNode.transform.position.z) - transform.position).normalized;
            _lookRotation = Quaternion.LookRotation(_direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * rotationSpeed);


            step = step / 7;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(nextNode.transform.position.x, transform.position.y, nextNode.transform.position.z), step);
            if (transform.position.x == nextNode.transform.position.x && transform.position.z == nextNode.transform.position.z)
            {
                if (nextNode.tag != "Finish")
                {
                    nextNode = nextNode.GetComponent<WalkNodeScript>().nextNode;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!dead)
        {
            if (other.gameObject.tag == "Finish")
            {
                Debug.Log("Collission");
                goal = other.gameObject;
                StartCoroutine("DealGateDamage");
            }
            if (other.gameObject.name == "Bullet_Prefab")
            {
                Debug.Log("HIT");
                //health -= 1;

            }

        }
    }

    IEnumerator Die()
    {

        if(Random.Range(0, coinDrop) == coinDrop - 1) {
            Instantiate(coin, gameObject.transform.position, Quaternion.identity);
        }

        healthCanvas.enabled = false;
        yield return new WaitForSeconds(3f);   //Wait
        Destroy(gameObject);
    }

    IEnumerator DealGateDamage()
    {
        goal.GetComponent<GateScript>().health -= damage;
        yield return new WaitForSeconds(1f);   //Wait
        StartCoroutine("DealGateDamage");
    }
}
