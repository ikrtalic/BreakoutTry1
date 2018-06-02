using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public float StartForce = 200f;

    private Rigidbody2D rigidBody;
    private string ID;

    public delegate void OnBallLost(string ID);
    public static event OnBallLost BallLost;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        ID = Guid.NewGuid().ToString();
	}

    private void Update()
    {
        if (transform.position.y < -5.2f)
        {
            BallLost(ID);
        }
    }

    public void Kick()
    {
        rigidBody.bodyType = RigidbodyType2D.Dynamic;
        rigidBody.AddForce(new Vector2(StartForce, StartForce));
    }

    public void Reset()
    {
        rigidBody.bodyType = RigidbodyType2D.Static;
        transform.position = new Vector3(0f, -4.37f, 0f);
    }

    public void MoveTo(Vector2 newPosition)
    {
        var position = transform.position;
        position.x = newPosition.x;
        transform.position = position;
    }
}
