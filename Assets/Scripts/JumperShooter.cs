using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperShooter : MonoBehaviour {

    public GameObject projectilePrefab, shotOrigin;
    private GameObject projectile;
    private Transform projectileParent;
    private JumperController jumperController;
    private IEnumerator coroutine;
    public bool isShooting;
    public float shotStrength = 45f;
    private float fireRate = .2f;

    private void Start()
    {
        isShooting = false;
        jumperController = GetComponent<JumperController>();
        shotOrigin = GetComponentInChildren<ShotOrigin>().gameObject;
        projectileParent = GameObject.Find("Projectiles").transform;
        if (!projectileParent)
        {
            Debug.LogError("No projectile parent object found");
        }
    }

    public void StartFire(float angle)
    {
        isShooting = true;
        coroutine = FireRepeating(angle);
        StartCoroutine(coroutine);
    }

    public void StopFire(float angle)
    {
        isShooting = false;
        Debug.Log("stopping fire");
        StopCoroutine(coroutine);
    }

    IEnumerator FireRepeating(float angle)
    {
        while (isShooting)
        {
            Fire(angle);
            yield return new WaitForSeconds(fireRate);
        }
        
    }

    public void Fire(float angle)
    {
        jumperController.timeSinceShot = 0f;
        projectile = Instantiate(projectilePrefab, shotOrigin.transform.position, Quaternion.identity) as GameObject;
        projectile.transform.parent = projectileParent;
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2 (angle * shotStrength,0);
    }
}
