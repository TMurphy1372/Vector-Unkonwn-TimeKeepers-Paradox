using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("01Main", LoadSceneMode.Single);
    }

    public void StartTutorial()
    {
        Debug.Log("Tutorial");
    }
}
