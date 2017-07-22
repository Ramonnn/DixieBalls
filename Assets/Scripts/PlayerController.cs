using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 20f;
    public float jumpSpeed = 20f;
    [Range(1,2)]
    public int playerNumber;

    private GameObject player;
    private Rigidbody2D rb;
    private Camera cam;
    private KeyCode jumpKey, fireKey, leftRotationKey, rightRotationKey;
    private PlayerMovement playerMovement;
    private Shooter shooter;
    private Rotate rotate;

    private int jumpCount;
    private float playerPos, distance, targetPos, minX, maxX, camHeight, camWidth;

    // Use this for initialization
    void Start () {
        player = gameObject;
        rb = GetComponent<Rigidbody2D>();
        cam = GameObject.FindObjectOfType<Camera>();
        playerMovement = GetComponent<PlayerMovement>();
        shooter = GetComponentInChildren<Shooter>();
        rotate = GetComponentInChildren<Rotate>();

        camHeight = cam.orthographicSize * 2;                     //orthoSize = camera height half-size in game units
        camWidth = cam.aspect * camHeight;
        minX = -camWidth / 2 + 1;                                  //plus or minus 1 for the walls, .5 for half player
        maxX = camWidth / 2 - 1;

        if (playerNumber == 1)
        {
            jumpKey = KeyCode.W;
            fireKey = KeyCode.LeftShift;
            leftRotationKey = KeyCode.R;
            rightRotationKey = KeyCode.T;
        } else if (playerNumber == 2)
        {
            jumpKey = KeyCode.UpArrow;
            fireKey = KeyCode.RightControl;
            leftRotationKey = KeyCode.Keypad1;
            rightRotationKey = KeyCode.Keypad3;
        } else
        {
            Debug.LogError("No valid player number assigned");
        }

        jumpCount = 0;
    }
	
	void Update () {

        SetXPosition();

        if (Input.GetKey(leftRotationKey))
        {
            rotate.RotateBarrel("Left");
        } else if (Input.GetKey(rightRotationKey))
        {
            rotate.RotateBarrel("Right");
        }

        if (Input.GetKeyDown(jumpKey) && jumpCount < 2){
            playerMovement.Jump(jumpSpeed, rb);
            jumpCount++;
        }

        if (Input.GetKeyDown(fireKey))
        {
            shooter.Fire();
        }

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
        distance = Input.GetAxisRaw("Horizontal" + playerNumber) * Time.deltaTime * speed;
        targetPos = playerPos + distance;
        player.transform.position = new Vector2(Mathf.Clamp(targetPos,minX,maxX), player.transform.position.y);
    }

}