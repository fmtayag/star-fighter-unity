using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollide : MonoBehaviour
{
    public GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Spawn explosion on enemy
            Transform explosionParent = GameObject.Find("Explosions").transform;
            Instantiate(explosionPrefab, new Vector2(other.transform.position.x, other.transform.position.y), Quaternion.identity, explosionParent);

            // Destroy enemy
            other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(other.gameObject);

            // Set lives to 0
            GameStats.Instance.Lives = 0;

            // Screen shake
            Camera.main.GetComponent<CameraFollow>().AddScreenshakeTime(GameStats.Instance.RECOMMENDED_SHAKE_DURATION * 2f);
        }
    }
}
