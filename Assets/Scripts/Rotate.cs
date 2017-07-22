using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    private float rotationSpeed = 2f;                           //rotation in degrees (Euler)
    private Vector3 eulerRot;

    // Use this for initialization
    void Start () {
        eulerRot = new Vector3(0, 0, rotationSpeed);
    }

    public void RotateBarrel(string direction)
    {
        if(direction == "Left")
        {
            gameObject.transform.Rotate(eulerRot, Space.World);
        } else if (direction == "Right")
        {
            gameObject.transform.Rotate(-eulerRot, Space.World);
        } else
        {
            Debug.LogWarning("Invalid direction passed to RotateBarrel on " + this);
        }
    }
}
