using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HelpSceneLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
}
