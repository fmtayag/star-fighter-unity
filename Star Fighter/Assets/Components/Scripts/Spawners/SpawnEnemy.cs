using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public const float timeDelay = 1f;
    public float timeLeft = timeDelay;
    public GameObject enemyPrefab;
    public const int MAX_ENEMIES = 8;

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
            Spawn();
        }
    }

    private void Spawn()
    {
        if (GameObject.Find("Enemies").transform.hierarchyCount <= MAX_ENEMIES)
        {
            Transform parent = GameObject.Find("Enemies").transform;
            Instantiate(enemyPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity, parent);
        }
    }

    private void Countdown()
    {
        timeLeft -= Time.deltaTime;
    }

    private void ResetTimer()
    {
        // Reset timer
        timeLeft = timeDelay;
    }
}
