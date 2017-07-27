using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(JumperController))]
public class JumperShooter : MonoBehaviour {

    public GameObject projectilePrefab, shotOrigin;
    private GameObject projectile;
    private Transform projectileParent;
    private JumperController jumperController;
    private KeyCode fireLeftKey, fireRightKey;

    public float shotStrength = 45f;
    private float fireRate = 5f;                         //Rate of fire in number of shots per second. Set to zero for single shots.
    private float timeToFire = 0f;

    private void Start()
    {
        jumperController = GetComponent<JumperController>();

        shotOrigin = GetComponentInChildren<ShotOrigin>().gameObject;
        if (!shotOrigin) {Debug.LogError("No Shot Origin found");}

        projectileParent = GameObject.Find("Projectiles").transform;
        if (!projectileParent) {Debug.LogError("No projectile parent object found");}

        fireLeftKey = KeyCode.Keypad1;
        fireRightKey = KeyCode.Keypad3;
    }

    private void Update() {
        if (fireRate == 0f) {                                                    //if single shot
            if (Input.GetKeyDown(fireLeftKey)) {
                jumperController.FaceLeft();                                     //TODO: move player facing to Fire() method
                Fire(-1f);                                                       //fire once
            } else if (Input.GetKeyDown(fireRightKey)) {
                jumperController.FaceRight();
                Fire(1f);
            }
        } else {                                        
            if (Input.GetKey(fireLeftKey) && Time.time > timeToFire) {           //if has fire rate, fire when enough time has passed
                timeToFire = Time.time + 1 / fireRate;                           //and increment timeToFire
                jumperController.FaceLeft();
                Fire(-1f);                               
            } else if (Input.GetKey(fireRightKey) && Time.time > timeToFire) {
                timeToFire = Time.time + 1 / fireRate;
                jumperController.FaceRight();
                Fire(1f);
            }
        }
    }

    public void Fire(float direction)
    {
        jumperController.timeSinceShot = 0f;
        projectile = Instantiate(projectilePrefab, shotOrigin.transform.position, Quaternion.identity) as GameObject;
        projectile.transform.parent = projectileParent;
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2 (direction * shotStrength,0);
    }
}
