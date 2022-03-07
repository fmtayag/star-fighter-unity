using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject bulletPrefab;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Transform parent = GameObject.Find("Bullets").transform;
        Instantiate(bulletPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity, parent);
    }
}
