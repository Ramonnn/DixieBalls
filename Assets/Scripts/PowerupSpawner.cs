using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour {

    public GameObject powerupPrefab;

    private GameObject powerup;
    private Camera cam;
    private Transform powerupParent;
    private float spawnPercent = 1f;
    private float randomPercent, camHeight, camWidth, minX, maxX, minY, maxY;
    private Vector2 spawnPosition;

    private void Start()
    {
        cam = GameObject.FindObjectOfType<Camera>();
        camHeight = cam.orthographicSize * 2;
        camWidth = cam.aspect * camHeight;

        minY = cam.transform.position.y;
        maxY = cam.transform.position.y + camHeight / 2;
        minX = -camWidth / 2 + 2;                                  //padding
        maxX = camWidth / 2 - 2;


        powerupParent = GameObject.Find("PowerUpSpawner").transform;
        if (!powerupParent)
        {
            Debug.LogError("No powerup parent object found");
        }
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
        spawnPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        powerup = Instantiate(powerupPrefab,spawnPosition,Quaternion.identity) as GameObject;
        powerup.transform.parent = powerupParent;
    }
}
