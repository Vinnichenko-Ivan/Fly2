using System;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public float mass;
    public float size;
    public float angle;
    public float speed;
    public float radius;
    public Color color;
    public List<Planet> children = new List<Planet>();

    public void init()
    {
        transform.localScale = new Vector3(size, size, 1);
        // foreach (Planet child in children)
        // {
        //     child.transform.parent = transform;
        // }
    }

    public void rotateChildren()
    {
        foreach (Planet child in children)
        {
            child.angle += child.speed;
            if (child.angle >= 360)
            {
                child.angle -= 360;
            }
            
            float x = Mathf.Sin(child.angle * Mathf.Deg2Rad) * child.radius;
            float y = Mathf.Cos(child.angle * Mathf.Deg2Rad) * child.radius;
            child.transform.position = new Vector3(transform.position.x + x, transform.position.y + y, child.transform.position.z);
        }
    }
}