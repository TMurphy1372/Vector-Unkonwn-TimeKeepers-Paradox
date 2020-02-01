using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController_Puzzle1 : MonoBehaviour
{
    public static GameController_Puzzle1 gameController;

    public UI_Puzzle1 UI;
    public Database_Puzzle1 database;
    public GameObject Player;
    public GameObject TopCameraPlayer;
    public GameObject InputPanel;
    public List<GameObject> Bridges;

    private int questionNum;

    private void Awake()
    {
        gameController = this;
    }

    private void Start()
    {
        InputPanel.SetActive(false);
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
                TopCameraPlayer.GetComponent<TopCameraPlayer>().isTopCamera = true;
                Player.SetActive(false);
                break;

            case -1:
                Camera.main.depth = 1;
                TopCameraPlayer.GetComponent<TopCameraPlayer>().isTopCamera = false;
                Player.SetActive(true);
                Player.transform.position = new Vector3(TopCameraPlayer.transform.position.x, Player.transform.position.y, TopCameraPlayer.transform.position.z);
                break;
        }
    }

    public void ShowInputPanel(bool value)
    {
        InputPanel.SetActive(value);
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
