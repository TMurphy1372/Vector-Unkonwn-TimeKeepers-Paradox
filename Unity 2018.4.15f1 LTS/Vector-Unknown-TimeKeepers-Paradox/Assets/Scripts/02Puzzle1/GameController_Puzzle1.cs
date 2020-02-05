using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController_Puzzle1 : MonoBehaviour
{
    public static GameController_Puzzle1 gameController;

    public UI_Puzzle1 UI;
    public Database_Puzzle1 database;
    public GameObject player;
    public GameObject topCameraPlayer;
    public GameObject inputPanel;
    public List<GameObject> Bridges;

    private int questionNum;

    private void Awake()
    {
        gameController = this;
    }

    private void Start()
    {
        inputPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SwitchCamera();
        }
    }

    private void SwitchCamera()
    {
        switch(Camera.main.depth)
        {
            case 1:
                Camera.main.depth = -1;
                topCameraPlayer.GetComponent<TopCameraPlayer>().isTopCamera = true;
                player.SetActive(false);
                break;

            case -1:
                Camera.main.depth = 1;
                topCameraPlayer.GetComponent<TopCameraPlayer>().isTopCamera = false;
                player.SetActive(true);
                player.transform.position = new Vector3(topCameraPlayer.transform.position.x, player.transform.position.y, topCameraPlayer.transform.position.z);
                break;
        }
    }

    public void ShowInputPanel(bool value)
    {
        inputPanel.SetActive(value);
    }

    public void SetQuestionNumber(int value)
    {
        questionNum = value;
        Debug.Log(questionNum);
    }

    public void CheckAnswer(float scalar, float x, float y, float z)
    {
        if(database.Calculation(questionNum, scalar, x, y, z))
        {
            Bridges[questionNum - 1].SetActive(true);
        }
    }
}
