using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private const float moveSpeed = 1.0f;
    private Vector2 direction;
    // Start is called before the first frame update

    void Start()
    {
        DetermineInitialDirection();
        DetermineInitialPos();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Invert direction after triggering a velocity inverter
        if (other.gameObject.CompareTag("VelocityInverter"))
        {
            if (direction == Vector2.left)
            {
                direction = Vector2.right;
            }
            else if (direction == Vector2.right)
            {
                direction = Vector2.left;
            }
        }
    }

    private void DetermineInitialDirection()
    {
        // Determine initial direction based on random number
        int r = Random.Range(0, 100);
        if (r >= 50)
        {
            direction = Vector2.left;
        }
        else
        {
            direction = Vector2.right;
        }
    }

    private void DetermineInitialPos()
    {
        // Bounds & Offsets
        float offset = GetComponent<SpriteRenderer>().size.x;
        float offsetLowBounds = 5f;
        float boundsLeft = GameObject.Find("Player Bound LEFT").transform.position.x + offset;
        float boundsRight = GameObject.Find("Player Bound RIGHT").transform.position.x - offset;
        float boundsDown = GameObject.Find("Player Bound DOWN").transform.position.y + offsetLowBounds;
        float boundsUp = GameObject.Find("Player Bound UP").transform.position.y - offset;

        // Set randomized initial position
        float initX = Random.Range(boundsLeft, boundsRight);
        float initY = Random.Range(boundsDown, boundsUp);
        transform.position = new Vector3(initX, initY);
    }
}
