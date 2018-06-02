using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Paddle paddle;
    public List<Ball> balls;

    public float LimitTop = 4.59f;
    public float LimitLeft = -2.87f;
    public float LimitRight = 2.87f;
    public float LimitBottom = -1.33f;
    public float DistanceX = 0.95f;
    public float DistanceY = 0.35f;
    public GameObject[] BrickTypes;

    public int BrickCountWidth;
    public int BrickCountHeight;

    [HideInInspector]
    public bool BallInPlay = false;

    public bool Paused = false;

    private Dictionary<int, Brick> _bricks;
    private GameObject[,] bricks;
    private int score;

    // Use this for initialization
    void Start () {
        Ball.BallLost += BallLost;
        Brick.BrickDestroyed += BrickDestroyed;
        LoadLevel();
	}
	
	// Update is called once per frame
	private void Update () {

        // User input
        // Jump - kick the ball at start
        if (Input.GetButtonDown("Jump"))
        {
            if (!BallInPlay)
            {
                BallInPlay = true;
                balls[0].Kick();
            }
            else
            {
                Time.timeScale = Paused ? 0f : 1f;
                Paused = !Paused;
            }
        }

        // Escape - reset
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Reset();
        }

        // Movement
        int horizontal = (int)Input.GetAxisRaw("Horizontal");
        if (horizontal != 0)
        {
            paddle.Move(horizontal);
            if (!BallInPlay)
            {
                balls[0].MoveTo(paddle.transform.position);
            }
        }
	}

    private void Reset()
    {
        Paused = false;
        BallInPlay = false;
        if (balls.Count > 1)
        {
            balls.RemoveRange(1, balls.Count - 1);
        }
        balls[0].Reset();
        paddle.Reset();
        for (int x = 0; x < BrickCountWidth; x++)
        {
            for (int y = 0; y < BrickCountHeight; y++)
            {
                Destroy(bricks[x, y].gameObject);
            }
        }
        score = 0;
        LoadLevel();
    }

    private void BallLost(string ballID)
    {
        if (balls.Count == 1)
        {
            // Lose life first, ToDo
            Reset();
        }
        CheckGameOver();
    }

    private void LoadLevel(string name = "")
    {
        Debug.Log("Loading level " + (string.IsNullOrEmpty(name) ? "[Debug]" : name));
        if (BrickTypes.Length == 0)
        {
            Debug.Log("Error loading level, no bricks defined.");
            return;
        }
        if (name == "")
        {
            // Debugging level, auto generate
            bricks = new GameObject[BrickCountWidth,BrickCountHeight];
            float xpos = LimitLeft;
            for (int x = 0; x < BrickCountWidth; x++)
            {
                float ypos = LimitTop;
                for (int y = 0; y < BrickCountHeight; y++)
                {
                    bricks[x,y] = Instantiate(BrickTypes[0], new Vector3(xpos, ypos, 0), Quaternion.identity);
                    ypos -= DistanceY;
                }
                xpos += DistanceX;
            }
        }
        else
        {
            // ...
        }
    }

    private void BrickDestroyed(BrickType brickType, Transform brickTransform)
    {
        score += brickType.Points;
        Debug.Log("Score: " + score);
        CheckGameOver();
    }

    private void CheckGameOver()
    {
        Debug.Log("Brick count: " + bricks.Length);
    }
}
