using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBMove : MonoBehaviour
{
    private const float moveSpeed = 3.0f;
    public GameObject player;
    private Vector2 velocity;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        velocity = new Vector2(0, -1);
        FindPlayerPos();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(velocity * moveSpeed * Time.deltaTime);
    }

    private void FindPlayerPos()
    {
        if (player != null)
        {
            // Calculate radians

            float rel_y = this.transform.position.y - player.transform.position.y;
            float rel_x = this.transform.position.x - player.transform.position.x;
            float radians = Mathf.Atan2(rel_y, rel_x);

            // Find dx and dy
            float dx = Mathf.Cos(radians);
            float dy = Mathf.Sin(radians);

            velocity.x = -dx;
            velocity.y = -dy;
        }

    }


}
