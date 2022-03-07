using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : MonoBehaviour
{
    // Bob alpha
    private float alpha = 0;
    private float origY;

    // Start is called before the first frame update
    void Start()
    {
        origY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Increment alpha
        alpha += 0.02f;

        // Calculate new bob value
        float bob = Mathf.Sin(alpha) * 0.05f;

        // Set new bob value
        Vector2 newPos = new Vector2(transform.position.x, origY + bob);
        transform.position = newPos;
    }
}
