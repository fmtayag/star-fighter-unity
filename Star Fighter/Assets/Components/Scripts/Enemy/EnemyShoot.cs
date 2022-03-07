using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public const float timeDelayMax = 2f;
    public float timeLeft = timeDelayMax;
    public GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft > 0)
        {
            Countdown();
        }
        else
        {
            ResetTimer();
            Shoot();
        }
    }

    private void Shoot()
    {
        Transform parent = GameObject.Find("Bullets").transform;
        Instantiate(bulletPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity, parent);
    }

    private void Countdown()
    {
        timeLeft -= Time.deltaTime;
    }

    private void ResetTimer()
    {
        // Reset timer
        timeLeft = Random.Range(0.5f, timeDelayMax);
    }
}
