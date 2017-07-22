using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperController : MonoBehaviour {

    public float speed = 20f;
    public float jumpSpeed = 20f;

    private GameObject player;
    private Rigidbody2D rb;
    private Camera cam;
    private KeyCode jumpKey, fireLeftKey, fireRightKey;
    private JumperMovement movement;
    private Animator anim;

    private int jumpCount;
    private float playerPos, distance, targetPos, minX, maxX, camHeight, camWidth;

    // Use this for initialization
    void Start () {
        player = gameObject;
        rb = GetComponent<Rigidbody2D>();
        cam = GameObject.FindObjectOfType<Camera>();
        movement = GetComponent<JumperMovement>();
        anim = GetComponent<Animator>();

        camHeight = cam.orthographicSize * 2;                     //orthoSize = camera height half-size in game units
        camWidth = cam.aspect * camHeight;
        minX = -camWidth / 2 + 1;                                  //plus or minus 1 for the walls, .5 for half player
        maxX = camWidth / 2 - 1;

        jumpKey = KeyCode.UpArrow;
        fireLeftKey = KeyCode.Keypad1;
        fireRightKey = KeyCode.Keypad3;

        jumpCount = 0;
    }
	
	void Update () {

        SetXPosition();

        if (Input.GetKeyDown(jumpKey) && jumpCount < 2){
            movement.Jump(jumpSpeed, rb);
            jumpCount++;
        }

        if (Input.GetKeyDown(fireLeftKey))
        {
            anim.SetFloat("angle", -1f);
            anim.SetTrigger("FireLeft Trigger");
        } else if (Input.GetKeyDown(fireRightKey))
        {
            anim.SetFloat("angle", 1f);
            anim.SetTrigger("FireRight Trigger");
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
        distance = Input.GetAxisRaw("Jumper") * Time.deltaTime * speed;
        if(distance > 0)
        {
            player.transform.localRotation = Quaternion.Euler(new Vector2(player.transform.localRotation.x, 180f));
        } else if (distance < 0)
        {
            player.transform.localRotation = Quaternion.Euler(new Vector2(player.transform.localRotation.x, 0f));
        }

        targetPos = playerPos + distance;
        player.transform.position = new Vector2(Mathf.Clamp(targetPos,minX,maxX), player.transform.position.y);
    }

}