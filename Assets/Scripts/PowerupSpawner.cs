using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour {

    public GameObject powerupPrefab;
    public LayerMask mask;

    private GameObject powerup;
    private Camera cam;
    private Transform powerupParent;
    private Vector2 spawnPosition;
    private Collider2D[] results = new Collider2D[4];

    private float spawnPercent = 1.5f;
    private float spawnCheckRadius = 0.5f;
    private float randomPercent, camHeight, camWidth, minX, maxX, minY, maxY;

    private void Start()
    {
        cam = Camera.main;
        camHeight = cam.orthographicSize * 2;
        camWidth = cam.aspect * camHeight;

        minY = cam.transform.position.y;
        maxY = cam.transform.position.y + camHeight / 2 - .5f;        //padding
        minX = -camWidth / 2 + 1;                                  
        maxX = camWidth / 2 - 1;

        powerupParent = GameObject.Find("PowerUpSpawner").transform;
        if (!powerupParent) {Debug.LogError("No powerup parent object found");}
    }

    private void Update()
    {
        randomPercent = Random.value * 100 * Time.deltaTime;
        
        if(SphereController.gameStarted && randomPercent < spawnPercent * Time.deltaTime)
        {
            SpawnPowerup();
        }
    }

    private void SpawnPowerup()
    {
        if (findEmptySpawnPosition()) {
            powerup = Instantiate(powerupPrefab, spawnPosition, Quaternion.identity) as GameObject;
            powerup.transform.parent = powerupParent;
        }
    }

    private bool findEmptySpawnPosition() {
        for(int i = 0; i < 15; i++) {
            spawnPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

            if (Physics2D.OverlapCircleNonAlloc(spawnPosition, spawnCheckRadius, results, mask) == 0) {
                return true;
            }
        }
        Debug.LogWarning("Failed to find a suitable powerup spawnPosition within 15 attempts");
        return false;
    }
}