using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour {

    public static bool gameStarted;

    private float sphereSize = 2;
    private Rigidbody2D rb;
    private Vector2 currentVel;
    private float minX, minY, maxX, maxY;

    private void Start()
    {
        gameStarted = false;
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = false;
        minX = 2;
        maxX = 20;
        minY = 2;
        maxY = 20;

    }

    private void Update()
    {
        if (!gameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            rb.simulated = true;
            gameStarted = true;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("PlayerProjectiles"))
        {
            currentVel = rb.velocity;
            rb.velocity = new Vector2(currentVel.x, currentVel.y);
        }
        if (coll.gameObject.CompareTag("Ground") && transform.localScale.y > 2)
        {
            Debug.Log("");
            sphereSize = sphereSize - 3;
            transform.localScale = new Vector3(Mathf.Clamp(sphereSize, minX, maxX), Mathf.Clamp(sphereSize, minY, maxY), transform.localScale.z);
        }
        if (coll.gameObject.CompareTag("Powerup"))
        {
            Debug.Log("Nomnomnom");
            sphereSize = sphereSize + 1;
            transform.localScale = new Vector3(Mathf.Clamp(sphereSize, minX, maxX), Mathf.Clamp(sphereSize, minY, maxY), transform.localScale.z);
        }
    }


}
