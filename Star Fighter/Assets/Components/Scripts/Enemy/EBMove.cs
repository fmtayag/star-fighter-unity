using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBMove : MonoBehaviour
{
    private const float moveSpeed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
    }


}
