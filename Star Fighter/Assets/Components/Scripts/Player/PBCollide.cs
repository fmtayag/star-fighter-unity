using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBCollide : MonoBehaviour
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
            // Destroy enemy
            other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(other.gameObject);

            // Destroy this bullet
            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(this);

            // Spawn explosion
            Transform explosionParent = GameObject.Find("Explosions").transform;
            Instantiate(explosionPrefab, new Vector2(other.transform.position.x, other.transform.position.y), Quaternion.identity, explosionParent);

            // Add score
            GameStats.Instance.Score += 1;

        }
    }
}
