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
    public float powerUpSpeed = 10f;
    public GameObject powerupInd;
        
    bool hasPowerUp = false;
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


        if(forwardInput > 0)
        {
            rendererPlayer.material.color = new Color(1.0f - magnitude, 1.0f, 1.0f - forwardInput);
        }
        else
        {
            rendererPlayer.material.color = new Color(1.0f + magnitude, 1.0f, 1.0f + forwardInput);
        }
        powerupInd.transform.position = transform.position;
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerUp= true;
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountDown());
            powerupInd.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Player has collid with" + collision.gameObject + "with powerup set to: " + hasPowerUp);
        if (hasPowerUp && collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player has collid with" + collision.gameObject + "with powerup set to: " + hasPowerUp);
            Rigidbody rbEnemy = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayDir = collision.gameObject.transform.position - transform.position;
            rbEnemy.AddForce(awayDir * powerUpSpeed, ForceMode.Impulse);
        }
    }

    IEnumerator PowerUpCountDown()
    {
        yield return new WaitForSeconds(8);
        hasPowerUp = false;
        powerupInd.SetActive(false);
    }
}
