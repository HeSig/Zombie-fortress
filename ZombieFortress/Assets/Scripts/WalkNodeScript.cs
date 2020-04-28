using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkNodeScript : MonoBehaviour
{

    public GameObject nextNode;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Draw gizmos in editor mode
    private void OnDrawGizmos()
    {
        //Create fancy sphere
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(transform.position, 0.5f);
        Gizmos.color = new Color(1, 0.92f, 0.016f, 1);
        Gizmos.DrawSphere(transform.position, 0.25f);
        Gizmos.DrawLine(new Vector3(transform.position.x, 0f, transform.position.z), new Vector3(transform.position.x, 5f, transform.position.z));
        //Draw a line to the next node
        if (nextNode != null) {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, nextNode.transform.position);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (nextNode != null)
        {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, nextNode.transform.position);
        }
    }
}
