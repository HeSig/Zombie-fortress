using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;


public class GameControllerScript : MonoBehaviour
{
    GameObject gate; //The player gate
    GameObject gameCamera; //The game camera
    public int zombieCount; //Number of zombies currently in the game
    public GameObject zombieSpawner; //The zombie spawner game object
    int[] levels = { 5, 10, 15, 20, 25, 30 }; //Number of zombies spawned in each level
    int currentLevel = 0; //Counter for the current level
    bool[] levelsCleared = { false, false, false, false, false, false }; //Array of boolean values that show if a level has been completed or not (Can be removed at some point)
    public GameObject[] chasingZombieSpawners;
    bool levelRunning;
    public int score;
    public static int Money;
    public int startMoney = 100;
    GameObject player;
    TextMeshProUGUI scoreText;
    TextMeshProUGUI loseText;
    TextMeshProUGUI zombiesLeft;
    TextMeshProUGUI gateHealth;
    TextMeshProUGUI moneyText;
    TextMeshProUGUI playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        Money = startMoney;
        gate = GameObject.FindGameObjectsWithTag("Finish")[0];
        gameCamera = GameObject.FindGameObjectsWithTag("UI")[0];

        //zombieSpawner = GameObject.FindGameObjectWithTag("Spawner"); //The zombiespawner object, to be corrected later.
        player = GameObject.FindGameObjectWithTag("Player");
        zombieCount = levels[currentLevel];
        levelRunning = false;
        loseText = gameCamera.gameObject.transform.Find("LoseText").GetComponent<TMPro.TextMeshProUGUI>();
        scoreText = gameCamera.gameObject.transform.Find("Score").GetComponent<TMPro.TextMeshProUGUI>();
        zombiesLeft = gameCamera.gameObject.transform.Find("ZombiesLeft").GetComponent<TMPro.TextMeshProUGUI>();
        gateHealth = gameCamera.gameObject.transform.Find("GateHealth").GetComponent<TMPro.TextMeshProUGUI>();
        moneyText = gameCamera.gameObject.transform.Find("Money").GetComponent<TMPro.TextMeshProUGUI>();
        playerHealth = gameCamera.gameObject.transform.Find("PlayerHP").GetComponent<TMPro.TextMeshProUGUI>();
        playerHealth.SetText("Player Health " + player.GetComponent<HealthScript>().health);
        loseText.text = "Press space to start next level";


        //Start first level
        /*
        for (int i = 0; i < levelsCleared.Length; i++)
        {
            if (levelsCleared[i] == false)
            {
                zombieSpawner.GetComponent<ZombieSpawner>().SpawnZombies(levels[i]);
                currentLevel = i;
                gameCamera.gameObject.transform.Find("LoseText").GetComponent<TextMesh>().text = "Level " + i;
                break;
            }
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        playerHealth.SetText("Player Health " + player.GetComponent<HealthScript>().health);
        int numberOfZombiesLeft = 0;
        int walkingZombies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        int chasingZombies = GameObject.FindGameObjectsWithTag("Zombie").Length;
        numberOfZombiesLeft = chasingZombies + zombieCount;

        scoreText.text = "Score " + score;
        zombiesLeft.text = "Zombies Left " + numberOfZombiesLeft.ToString();
        gateHealth.text = "Gate Health " + gate.GetComponent<GateScript>().health.ToString();
        moneyText.text = "Money " + Money;

        if (numberOfZombiesLeft == 0 && levelRunning)
        {
            levelsCleared[currentLevel] = true;
            levelRunning = false;
            loseText.text = "Press space to start next level";
        }

        //If the gate is destroyed the player loses
        if (gate.GetComponent<GateScript>().destroyed)
        {
            loseText.text = "YOU LOSE, press space to restart";

            if (SpacePress())
            {
                SceneManager.LoadScene(1);
            }
        }

        //Checks if there are any zombies left, and if the zombieSpawner has spawned all of its zombies.
        if (SpacePress())
        {
            if (!levelRunning)
            {
                Debug.Log("Game start");
                //Go through the list of levels and choose the next one that's not been completed
                for (int i = 0; i < levelsCleared.Length; i++)
                {
                    if (levelsCleared[i] == false)
                    {
                        currentLevel = i;
                        zombieCount = levels[currentLevel];
                        levelRunning = true;
                        //Call the zombie spawner to start spawning zombies according to the level
                        zombieSpawner.GetComponent<ZombieSpawner>().SpawnZombies(levels[currentLevel]);

                        //Update UI-text to the new level.
                        loseText.text = "Level " + (currentLevel + 1);
                        break;
                    }
                }
                for(int i = 0; i < chasingZombieSpawners.Length; i++)
                {
                    chasingZombieSpawners[i].GetComponent<ChasingZombieSpawner>().SpawnZombie();
                }
            }
        }
        if (levelsCleared[levelsCleared.Length-1])
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position, new Vector3(3, 3, 3));
    }

    private bool SpacePress()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
}
