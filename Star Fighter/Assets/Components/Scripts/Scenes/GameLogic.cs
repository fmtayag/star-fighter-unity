using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class GameLogic : MonoBehaviour
{
    private float timeLeft = 2.0f;
    private GameObject player;
    private GameObject dbText;
    private GameObject canvas;
    public Texture2D texture;
    private List<GameObject> livesImages;
    public InputField nameInputField;
    public GameObject inputErrorText;
    public GameObject gameOverText;
    public GameObject pressEnterText;
    private bool nameInputHasError;
    // Start is called before the first frame update
    void Start()
    {
        Init();

        // Fetch data
        StartCoroutine(RetrieveCollectedItems("http://localhost/unity/FetchData.php"));
    }


    // Update is called once per frame
    void Update()
    {
        CheckIfPlayerIsAlive();
        UpdateLivesUI();
        CheckIfExiting();
    }

    private void Init()
    {
        // Initialize Game Stats
        GameStats.Instance.Score = 0;
        GameStats.Instance.Lives = GameStats.Instance.MAX_LIVES;

        // Hide game over text
        gameOverText.GetComponent<Text>().text = "";

        // Find game objects
        player = GameObject.FindWithTag("Player");
        canvas = GameObject.Find("Canvas");
        dbText = GameObject.Find("DBText");
        dbText.GetComponent<Text>().text = "";
        dbText.SetActive(false);

        // Init list
        livesImages = new List<GameObject>();

        // Init Lives UI
        InitLivesUI();

        // Init input field
        nameInputField.gameObject.SetActive(false);

        // Init text
        inputErrorText.gameObject.SetActive(false);
        pressEnterText.gameObject.SetActive(false);
    }

    private void CheckIfPlayerIsAlive()
    {
        if (GameStats.Instance.Lives <= 0)
        {
            player.SetActive(false); // deactive player

            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                // Show Game over screen
                gameOverText.GetComponent<Text>().text = "GAME OVER";
            }
            else
            {
                MoveAsideGameOverScreen();
                ProcessInput();
            }

            ShowPressEnterText();
        }

    }

    private void MoveAsideGameOverScreen()
    {
        // Gradually move aside the game over screen
        RectTransform rectTransform = gameOverText.GetComponent<RectTransform>();
        float interpolation = 4.0f * Time.deltaTime;
        Vector3 newPos = new Vector3(rectTransform.position.x, 0);
        newPos.y = Mathf.Lerp(rectTransform.position.y, 400, interpolation);
        //newPos.x = Mathf.Lerp(this.transform.position.x, rectTransform.position.x, interpolation);
        rectTransform.position = newPos;
    }

    private void ProcessInput()
    {
        nameInputHasError = false;
        nameInputField.gameObject.SetActive(true); // activate input field
        nameInputField.Select();
        nameInputField.ActivateInputField();
        nameInputField.image.color = Color.white;

        if (Input.GetKey(KeyCode.Return))
        {
            // ------ Validate data and warn user about violations ------
            if (nameInputField.text.Length < GameStats.Instance.MINIMUM_NAME_LENGTH)
            {
                nameInputHasError = true;
                inputErrorText.GetComponent<Text>().text = $"MINIMUM OF {GameStats.Instance.MINIMUM_NAME_LENGTH} CHARACTERS";

            }

            if (!Regex.IsMatch(nameInputField.text, @"^[a-zA-Z]+$"))
            {

                nameInputHasError = true;

                if (nameInputField.text == "")
                {
                    inputErrorText.GetComponent<Text>().text = "BLANK NOT ALLOWED";
                }
                else
                {
                    inputErrorText.GetComponent<Text>().text = "ONLY LETTERS";
                }

            }

            // ----- Try to enter data -------
            if (!nameInputHasError)
            {
                // Save score to DB
                StartCoroutine(StoreCollectedItem("http://localhost/unity/collectData.php", GameStats.Instance.Score, nameInputField.text));

                // Go back to menu scene
                SceneManager.LoadScene("MenuScene");
            }
            else
            {
                nameInputField.image.color = Color.red;
                inputErrorText.SetActive(true);
            }
        }
    }

    private void ShowPressEnterText()
    {
        // Show press enter only when input length meets requirements
        if (nameInputField.text.Length >= GameStats.Instance.MINIMUM_NAME_LENGTH)
        {
            pressEnterText.SetActive(true);
        }
        else
        {
            pressEnterText.SetActive(false);
        }
    }

    private void UpdateLivesUI()
    {
        for (int i = 0; i < livesImages.Count; i++)
        {
            if (i > GameStats.Instance.Lives - 1)
            {
                livesImages[i].SetActive(false);
            }
        }
    }

    private void CheckIfExiting()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            // Go back to menu scene
            SceneManager.LoadScene("MenuScene");
        }
    }

    private void InitLivesUI()
    {

        for (int i = 0; i < GameStats.Instance.Lives; i++)
        {
            GameObject livesImg = new GameObject("LivesImg" + i.ToString());

            RectTransform rect = livesImg.AddComponent<RectTransform>();
            rect.transform.SetParent(canvas.transform);
            rect.localScale = Vector3.one;
            rect.anchoredPosition = new Vector2(134f - (32f * i), 216);
            rect.sizeDelta = new Vector2(32, 32);

            Image img = livesImg.AddComponent<Image>();
            img.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            livesImg.transform.SetParent(canvas.transform);

            livesImages.Add(livesImg);
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
            //Debug.Log("Received data: " + uwr.downloadHandler.data);
            //Debug.Log("Received Data: " + uwr.downloadHandler.text);
            //dbText.GetComponent<Text>().text = "Previous scores: " + uwr.downloadHandler.text;
        }
    }
}
