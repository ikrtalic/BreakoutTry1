using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

    public int hits;
    public bool indestructible;
    public int points;

    private BrickType brickType;

    public delegate void OnBrickDestroyed(BrickType bt, Transform t);
    public static event OnBrickDestroyed BrickDestroyed;

    private void Start()
    {
        brickType = new BrickType
        {
            Hits = hits,
            Indestructible = indestructible,
            Points = points
        };
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            brickType.Hits--;
            if (!brickType.Indestructible && brickType.Hits <= 0)
            {
                Destroy(gameObject);
                BrickDestroyed(brickType, transform);
            }
        }
    }
}
