using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DebrisPart : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public Collider2D collider;
    public List<FixedJoint2D> joints;
    public float mass;

    public void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.gravityScale = 0;
        collider = GetComponent<Collider2D>();
        joints = GetComponents<FixedJoint2D>().ToList();
    }
}