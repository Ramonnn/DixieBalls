using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperShooter : MonoBehaviour {

    public GameObject projectilePrefab, shotOrigin;
    private GameObject projectile;
    private Transform projectileParent;
    private Animator anim;
    public float shotStrength = 45f;

    private void Start()
    {
        anim = GetComponent<Animator>();
        shotOrigin = GetComponentInChildren<ShotOrigin>().gameObject;
        projectileParent = GameObject.Find("Projectiles").transform;
        if (!projectileParent)
        {
            Debug.LogError("No projectile parent object found");
        }
    }

    public void Fire()
    {
        Debug.Log(shotOrigin.transform.position);
        projectile = Instantiate(projectilePrefab, shotOrigin.transform.position, Quaternion.identity) as GameObject;
        projectile.transform.parent = projectileParent;
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2 (anim.GetFloat("angle") * shotStrength,0);
    }
}
