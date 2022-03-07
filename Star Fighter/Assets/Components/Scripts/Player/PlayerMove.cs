using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Animator animator;
    public float moveSpeed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isSpawning", false);
        animator.SetBool("isNormal", true);
        animator.SetBool("isMovingLeft", false);
        animator.SetBool("isMovingRight", false);

        if (Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }
        if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }
        if (Input.GetKey(KeyCode.W))
        {
            MoveUp();
        }
        if (Input.GetKey(KeyCode.S))
        {
            MoveDown();
        }
    }

    void MoveLeft()
    {
        animator.SetBool("isSpawning", false);
        animator.SetBool("isNormal", false);
        animator.SetBool("isMovingLeft", true);
        animator.SetBool("isMovingRight", false);

        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }

    void MoveRight()
    {
        animator.SetBool("isSpawning", false);
        animator.SetBool("isNormal", false);
        animator.SetBool("isMovingLeft", false);
        animator.SetBool("isMovingRight", true);

        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }

    void MoveUp()
    {
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
    }

    void MoveDown()
    {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
    }
}
