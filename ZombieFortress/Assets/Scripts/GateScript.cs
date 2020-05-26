using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour
{

    public int health = 10;
    public bool destroyed = false;
    // Start is called before the first frame update
    void Start()
    {
        destroyed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            destroyed = true;
        }
    }
}
