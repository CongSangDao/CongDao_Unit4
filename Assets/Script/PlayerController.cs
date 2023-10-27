using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rbPlayer;
    public float speed = 10f;
    GameObject focalPoint;
    Renderer rendererPlayer;
    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
        rendererPlayer = GetComponent<Renderer>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float magnitude = forwardInput * speed * Time.deltaTime;
        rbPlayer.AddForce(focalPoint.transform.forward * forwardInput * speed * Time.deltaTime, ForceMode.Force);

        Debug.Log("Mag:" + magnitude);
        Debug.Log("FI;" + forwardInput);

        if(forwardInput > 0)
        {
            rendererPlayer.material.color = new Color(1.0f - magnitude, 1.0f, 1.0f - forwardInput);
        }
        else
        {
            rendererPlayer.material.color = new Color(1.0f + magnitude, 1.0f, 1.0f + forwardInput);
        }
        
    }
}
