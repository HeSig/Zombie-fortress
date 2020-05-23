using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour {

    public int value = 10;
 
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other) {

        if(other.gameObject == GameObject.FindWithTag(Tags.PLAYER_TAG)) {
            
            GameControllerScript.Money += value;
            Destroy(gameObject);
        }
    }

}
