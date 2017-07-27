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
    private float spawnPercent = 25f;
    private float randomPercent, camHeight, camWidth, minX, maxX, minY, maxY;

    private void Start()
    {
        cam = Camera.main;
        camHeight = cam.orthographicSize * 2;
        camWidth = cam.aspect * camHeight;

        minY = cam.transform.position.y;
        maxY = cam.transform.position.y + camHeight / 2;
        minX = -camWidth / 2 + 2;                                  //padding
        maxX = camWidth / 2 - 2;

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
        findEmptySpawnPosition();
        powerup = Instantiate(powerupPrefab,spawnPosition,Quaternion.identity) as GameObject;
        powerup.transform.parent = powerupParent;
    }

    private void findEmptySpawnPosition() {
        int loopnumber = 0;
        while (true) {
            spawnPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            Collider[] hitColliders = Physics.OverlapSphere(spawnPosition, 3f, mask, QueryTriggerInteraction.Collide);
            Debug.Log(loopnumber++);
            if(hitColliders.Length == 0) {                                                          //if no hit colliders are returned by OverlapSphere, spawnPosition is empty
                break;                                                                              //so we stop iterating to find a new spawnPosition
            }
        }
    }
}
