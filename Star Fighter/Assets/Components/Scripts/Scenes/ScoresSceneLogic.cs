using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ScoresSceneLogic : MonoBehaviour
{
    enum ARRAY_ID
    {
        SCORE,
        PLAYER_NAME
    }
    string[] resultsArr;

    private GameObject dataExistsParent;
    private GameObject dataNotExistsParent;
    private GameObject highestScoreText;
    public Font scoreFont;
    public GameObject scoresParent;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize results array
        resultsArr = new string[] { };

        // Find game objects
        dataExistsParent = GameObject.Find("DataExists");
        dataNotExistsParent = GameObject.Find("DataNotExists");
        highestScoreText = GameObject.Find("HighestScore");

        // Fetch data
        StartCoroutine(RetrieveCollectedItems("http://localhost/unity/FetchData.php"));
    }

    // Update is called once per frame
    void Update()
    {
        if (resultsArr.Length != 0)
        {
            // Set text objects parents' state
            dataExistsParent.SetActive(true);
            dataNotExistsParent.SetActive(false);
        }
        else
        {
            // Set text objects parents' state
            dataExistsParent.SetActive(false);
            dataNotExistsParent.SetActive(true);
        }

        // Get keyboard input
        if (Input.GetKey(KeyCode.X))
        {
            SceneManager.LoadScene("MenuScene");
        }
        else if (Input.GetKey(KeyCode.Z))
        {
            SceneManager.LoadScene("PlayScene");
        }
    }

    IEnumerator StoreCollectedItem(string url, int score, string playerName)
    {
        WWWForm form = new WWWForm();
        form.AddField("score", score);
        form.AddField("name", playerName);

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
            string results = uwr.downloadHandler.text;

            // Process results string
            if (results != "")
            {
                results = results.Remove(results.Length - 1, 1); // Remove last character, which is expected to be a ','.
                resultsArr = results.Split(','); // Convert string into array
                CreateTextObjects();
            }

        }
    }

    private void CreateTextObjects()
    {

        for (int i = 0; i < resultsArr.Length; i++)
        {
            int curScore = int.Parse(resultsArr[i].Split('-')[(int)ARRAY_ID.SCORE]);
            string curName = resultsArr[i].Split('-')[(int)ARRAY_ID.PLAYER_NAME];

            if (i == 0)
            {
                highestScoreText.GetComponent<Text>().text = $"<#{i + 1}> " + $"{curScore:00000}" + " " + curName.ToUpper();
            }
            else
            {
                GameObject scoreTextBox = new GameObject();
                scoreTextBox.transform.parent = scoresParent.transform;
                scoreTextBox.AddComponent<Text>();

                Text text = scoreTextBox.GetComponent<Text>();
                text.font = scoreFont;
                text.text = $"<#{i + 1}> " + $"{curScore:00000}" + " " + curName.ToUpper();
                text.fontSize = 24;
                text.alignment = TextAnchor.MiddleCenter;

                RectTransform rectTransform;
                rectTransform = text.GetComponent<RectTransform>();
                rectTransform.localPosition = new Vector2(0, 50f - (i * 32));
                rectTransform.sizeDelta = new Vector2(320, 50);
            }

        }

    }
}
