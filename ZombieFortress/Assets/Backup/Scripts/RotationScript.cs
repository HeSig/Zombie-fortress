using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    public Light pointLight;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
        pointLight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.one * Random.Range(0.5f, 1.5f);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y,Random.Range(0, 30.0f));
    }

    public void fire()
    {
        GetComponent<MeshRenderer>().enabled = true;
        pointLight.enabled = true;

        StartCoroutine("Reload");
    }

    IEnumerator Reload()
    {

        yield return new WaitForSeconds(0.1f);   //Wait
        GetComponent<MeshRenderer>().enabled = false;
        pointLight.enabled = false;


    }

}
