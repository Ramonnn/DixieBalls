using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {

    public GameObject projectilePrefab;
    private GameObject projectile;
    private Transform projectileParent;
    public float shotStrength;

    private void Start()
    {
        projectileParent = GameObject.Find("Projectiles").transform;
        if (!projectileParent)
        {
            Debug.LogError("No projectile parent object found");
        }
    }

    public void Fire()
    {
        projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity) as GameObject;
        projectile.transform.parent = projectileParent;

        Vector2 direction = transform.position - transform.parent.position;
        projectile.GetComponent<Rigidbody2D>().velocity = direction.normalized * shotStrength;
    }
}
