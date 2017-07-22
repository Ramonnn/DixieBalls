﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public void Jump(float jumpSpeed, Rigidbody2D rb)
    {
       rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
    }
}
