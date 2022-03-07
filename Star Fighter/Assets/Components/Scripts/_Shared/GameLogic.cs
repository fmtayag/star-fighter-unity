using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    private float timeLeft = 5.0f;
    private GameObject player;
    private GameObject dbText;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize Game Stats
        GameStats.Instance.Score = 0;
        GameStats.Instance.Lives = 3;

        // Hide game over text
        GameObject.Find("GameOverText").GetComponent<Text>().text = "";

        // Find game objects
        player = GameObject.FindWithTag("Player");
        dbText = GameObject.Find("DBText");
        dbText.GetComponent<Text>().text = "";

        // Fetch data
        StartCoroutine(RetrieveCollectedItems("http://localhost/unity/FetchData.php"));
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStats.Instance.Lives <= 0)
        {
            player.SetActive(false);
            // Show Game over screen
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                GameObject.Find("GameOverText").GetComponent<Text>().text = "Game Over!";
            }
            else
            {
                // Save score to DB
                StartCoroutine(StoreCollectedItem("http://localhost/unity/collectData.php", GameStats.Instance.Score));

                // Reset scene
                SceneManager.LoadScene("PlayScene");
            }
        }
    }

    IEnumerator StoreCollectedItem(string url, int score)
    {
        WWWForm form = new WWWForm();
        form.AddField("score", score);

        UnityWebRequest uwr = UnityWebRequest.Post(url, form);
        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            // text1.text=uwr.downloadHandler.text;
        }
    }


    IEnumerator RetrieveCollectedItems(string url)
    {
        WWWForm form = new WWWForm();
        UnityWebRequest uwr = UnityWebRequest.Post(url, form);
        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            //Debug.Log("Received data: " + uwr.downloadHandler.data);
            //Debug.Log("Received Data: " + uwr.downloadHandler.text);
            dbText.GetComponent<Text>().text = "Previous scores: " + uwr.downloadHandler.text;
        }
    }
}
