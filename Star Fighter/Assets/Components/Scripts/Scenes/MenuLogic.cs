using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MenuLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            SceneManager.LoadScene("PlayScene");
        }
        else if (Input.GetKey(KeyCode.S))
        {
            SceneManager.LoadScene("ScoresScene");
        }
        else if (Input.GetKey(KeyCode.H))
        {
            SceneManager.LoadScene("HelpScene");
        }
    }
}
