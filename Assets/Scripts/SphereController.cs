using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class SphereController : MonoBehaviour {

    public static bool gameStarted;                     //TODO: move this bool to a GameManager object

    private Rigidbody2D rb;
    private float sphereSize = 2;
    private float minX, minY, maxX, maxY;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameStarted = false;
        rb.simulated = false;
        minX = 2;
        maxX = 20;
        minY = 2;
        maxY = 20;
    }

    private void Update()
    {
        if (!gameStarted && Input.GetKeyDown(KeyCode.Space))        //TODO: make game starting an event in GameManager, subscribing to it from here and UI?
        {
            rb.simulated = true;
            gameStarted = true;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ground") && transform.localScale.y > 2)
        {
            Debug.Log("Sphere size decreased");
            sphereSize -= 3;
            scaleSphere();
        }
    }

    private void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.CompareTag("Powerup")) {
            Debug.Log("Nomnomnom");
            sphereSize += 1;
            scaleSphere();
        }
    }

    private void scaleSphere() {
        transform.localScale = new Vector3(Mathf.Clamp(sphereSize, minX, maxX), Mathf.Clamp(sphereSize, minY, maxY), transform.localScale.z);
    }


}
