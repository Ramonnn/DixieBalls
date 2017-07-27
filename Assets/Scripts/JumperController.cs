using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(JumperMovement))]
[RequireComponent (typeof(Rigidbody2D))]
public class JumperController : MonoBehaviour {

    public float speed = 20f;
    public float jumpSpeed = 20f;
    public float timeSinceShot;
    public Transform target;

    private GameObject player;
    private Rigidbody2D rb;
    private Camera cam;
    private KeyCode jumpKey;
    private JumperMovement movement;

    private int jumpCount;
    private float playerPos, distance, targetPos, minX, maxX, camHeight, camWidth, shotCooldown;

    void Start () {
        player = gameObject;
        rb = GetComponent<Rigidbody2D>();
        movement = GetComponent<JumperMovement>();
        cam = Camera.main;

        camHeight = cam.orthographicSize * 2;                     //orthoSize = camera height half-size in game units
        camWidth = cam.aspect * camHeight;
        minX = -camWidth / 2 + 1;                                  //plus or minus 1 for the walls, .5 for half player
        maxX = camWidth / 2 - 1;
        timeSinceShot = 10f;
        shotCooldown = .5f;

        jumpKey = KeyCode.UpArrow;

        jumpCount = 0;
    }

    void Update()
    {
        SetXPosition();
        JumpCommand();
        timeSinceShot += Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")){
            jumpCount = 0;
        }
    }

    private void SetXPosition()
    {
        playerPos = player.transform.position.x;
        distance = Input.GetAxisRaw("Jumper") * Time.deltaTime * speed;
        if(distance > 0 && timeSinceShot > shotCooldown)
        {
            FaceRight();
        }
        else if (distance < 0 && timeSinceShot > shotCooldown)
        {
            FaceLeft();
        }

        targetPos = playerPos + distance;
        player.transform.position = new Vector2(Mathf.Clamp(targetPos,minX,maxX), player.transform.position.y);
    }

    public void FaceLeft()
    {
        player.transform.localRotation = Quaternion.Euler(new Vector2(player.transform.localRotation.x, 0f));
    }

    public void FaceRight()
    {
        player.transform.localRotation = Quaternion.Euler(new Vector2(player.transform.localRotation.x, 180f));
    }

    private void JumpCommand()
    {
        if (Input.GetKeyDown(jumpKey) && jumpCount < 2)
        {
            movement.Jump(jumpSpeed, rb);
            jumpCount++;
        }
    }
}