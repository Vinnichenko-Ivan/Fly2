using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Engine : MonoBehaviour
{
    public GameObject smokePrefab;
    public float maxPower;
    public float angleReg;
    public float power;
    public float angle;
    public float smokeAngle = 30f;
    
    public Rigidbody2D rigidBody;

    public void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        maxPower = 55f;
        angleReg = 15f;
        smokeAngle = 30f;
        power = 0f;
    }

    public void FixedUpdate()
    {
        Vector2 pos = transform.position;
        Vector2 direction = new Vector2(-transform.right.x, -transform.right.y);
        var _rotatedVector = RotateVector(direction, angle);
            
        var powerVec = _rotatedVector * power;
            
        Debug.DrawRay(pos, powerVec, Color.red);
        rigidBody.AddForce(powerVec);
            
        GameObject gameObject = Instantiate(smokePrefab, pos, Quaternion.identity);
        var rb = gameObject.GetComponent<Rigidbody2D>();
        rb.linearVelocity = rigidBody.linearVelocity;
        rb.AddForce(pos-RotateVector(pos - powerVec, Random.Range(-smokeAngle, smokeAngle)));
    }
    
    public Vector2 RotateVector(Vector2 v, float angle)
    {
        float radian = angle*Mathf.Deg2Rad;
        float _x = v.x*Mathf.Cos(radian) - v.y*Mathf.Sin(radian);
        float _y = v.x*Mathf.Sin(radian) + v.y*Mathf.Cos(radian);
        return new Vector2(_x,_y);
    }
}