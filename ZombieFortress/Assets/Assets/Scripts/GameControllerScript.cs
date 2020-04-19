using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    GameObject gate; //The player gate
    GameObject gameCamera; //The game camera
    public int zombieCount; //Number of zombies currently in the game
    public GameObject zombieSpawner; //The zombie spawner game object
    int[] levels = {5, 10, 15, 20, 25, 30}; //Number of zombies spawned in each level
    int currentLevel = 0; //Counter for the current level
    bool[] levelsCleared = { false, false, false, false, false, false }; //Array of boolean values that show if a level has been completed or not (Can be removed at some point)

    // Start is called before the first frame update
    void Start()
    {
        gate = GameObject.FindGameObjectsWithTag("Finish")[0];
        gameCamera = GameObject.FindGameObjectsWithTag("MainCamera")[0];
        
        //zombieSpawner = GameObject.FindGameObjectWithTag("Spawner"); //The zombiespawner object, to be corrected later.

        //Start first level
        for(int i = 0; i < levelsCleared.Length; i++)
        {
            if(levelsCleared[i] == false)
            {
                zombieSpawner.GetComponent<ZombieSpawner>().SpawnZombies(levels[i]);
                currentLevel = i;
                gameCamera.gameObject.transform.Find("LoseText").GetComponent<TextMesh>().text = "Level " + i;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //If the gate is destroyed the player loses
        if(gate == null)
        {
            gameCamera.gameObject.transform.Find("LoseText").GetComponent<TextMesh>().text = "YOU LOSE";
        }

        //Check number of zombies
        zombieCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        //Checks if there are any zombies left, and if the zombieSpawner has spawned all of its zombies.
        if(zombieCount == 0 && zombieSpawner.GetComponent<ZombieSpawner>().allSpawned)
        {
            levelsCleared[currentLevel] = true;

            //Go through the list of levels and choose the next one that's not been completed
            for (int i = 0; i < levelsCleared.Length; i++)
            {
                if (levelsCleared[i] == false)
                {
                    //Call the zombie spawner to start spawning zombies according to the level
                    zombieSpawner.GetComponent<ZombieSpawner>().SpawnZombies(levels[i]);
                    currentLevel = i;
                    //Update UI-text to the new level.
                    gameCamera.gameObject.transform.Find("LoseText").GetComponent<TextMesh>().text = "Level " + i;
                    break;
                }
            }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position, new Vector3(3, 3, 3));
    }
}
