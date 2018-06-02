using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    public float Speed = 5f;
    public float BoundaryLeft = 0f;
    public float BoundaryRight = 0f;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
    }

    public void Move(int horizontal)
    {
        if (horizontal == 0) return;
        var speed = (horizontal < 0 ? -Speed : Speed) * Time.deltaTime;

        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(speed, 0);

        if (end.x > BoundaryLeft && end.x < BoundaryRight)
        {
            transform.position = end;
        }
    }

    public void Reset()
    {
        transform.position = new Vector3(0f, -4.74f, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //var point = transform.InverseTransformPoint(collision.contacts[0].point);
        //Debug.Log("Point:" + point.ToString());
        //Debug.Log("Velocity before:" + collision.rigidbody.velocity.ToString());
        //collision.rigidbody.AddForce(new Vector2(point.x*1000, 0));
        //Debug.Log("Velocity after:" + collision.rigidbody.velocity.ToString());
    }
}
