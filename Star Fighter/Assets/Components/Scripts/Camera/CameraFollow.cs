using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject player;
    public float speed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float interpolation = speed * Time.deltaTime;

        Vector3 position = this.transform.position;
        position.y = Mathf.Lerp(this.transform.position.y, player.transform.position.y, interpolation);
        position.x = Mathf.Lerp(this.transform.position.x, player.transform.position.x, interpolation);

        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        // Clamp x-axis so camera doesn't go over the edge
        float minPosX = GameObject.Find("Player Bound LEFT").transform.position.x + (width / 2) + 0.05f;
        float maxPosX = GameObject.Find("Player Bound RIGHT").transform.position.x - (width / 2) - 0.05f;
        position.x = Mathf.Clamp(position.x, minPosX, maxPosX);

        // Clamp y-axis so camera doesn't go over the edge
        //float minPos = -1.75f;
        float minPosY = GameObject.Find("Player Bound DOWN").transform.position.y + (height / 2);
        float maxPosY = GameObject.Find("Player Bound UP").transform.position.y - (height / 2);
        position.y = Mathf.Clamp(position.y, minPosY, maxPosY);

        this.transform.position = position;
    }
}
