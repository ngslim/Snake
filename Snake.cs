using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public float moveSpeed = 0.05f;
    public Transform segmentPrefab;
    public int startLength = 3;

    Vector2 direction;

    List<Transform> segments;

    bool isMovingUp;
    bool isMovingDown;
    bool isMovingLeft;
    bool isMovingRight;

    void Start()
    {
        Time.fixedDeltaTime = moveSpeed;

        segments = new List<Transform> ();
        segments.Add(this.transform);
        for (int i = 1; i < startLength; i++)
        {
            segments.Add(Instantiate (segmentPrefab));
        }

        isMovingUp = false;
        isMovingDown = false;
        isMovingLeft = false;
        isMovingRight = false;
    }
    
    void Update()
    {
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && !isMovingDown)
        {
            direction = Vector2.up;
            isMovingUp = true;
            isMovingDown = false;
            isMovingLeft = false;
            isMovingRight = false;
        }
        else if ((Input.GetKey(KeyCode.S) ||Input.GetKey(KeyCode.DownArrow)) && !isMovingUp)
        {
            direction = Vector2.down;
            isMovingDown = true;
            isMovingUp = false;
            isMovingLeft = false;
            isMovingRight = false;
        }
        else if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && !isMovingRight)
        {
            direction = Vector2.left;
            isMovingLeft = true;
            isMovingRight = false;
            isMovingUp = false;
            isMovingDown = false;
        }
        else if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && !isMovingLeft)
        {
            direction = Vector2.right;
            isMovingLeft = false;
            isMovingRight = true;
            isMovingUp = false;
            isMovingDown = false;
        }
    }

    void FixedUpdate()
    {
        for(int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + direction.x,
            Mathf.Round(this.transform.position.y) + direction.y,
            0.0f
        );
    }

    void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = segments[segments.Count - 1].position;

        segments.Add(segment);
    }

    void Reset()
    {
        for(int i = 1; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }

        this.transform.position = Vector3.zero;

        segments.Clear();
        segments.Add(this.transform);
        for (int i = 1; i < startLength; i++)
        {
            segments.Add(Instantiate(segmentPrefab));
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Food")
        {
            Grow();
        }

        if(collider.tag == "Obstacle")
        {
            Reset();
        }
    }
}
