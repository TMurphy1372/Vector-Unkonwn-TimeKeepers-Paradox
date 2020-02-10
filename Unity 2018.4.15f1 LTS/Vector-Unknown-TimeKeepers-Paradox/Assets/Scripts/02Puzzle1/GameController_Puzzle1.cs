using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController_Puzzle1 : MonoBehaviour
{
    public static GameController_Puzzle1 instance;

    public UI_Puzzle1 UI;
    public Database_Puzzle1 database;
    public GameObject Player;
    public GameObject TopCameraPlayer;
    public GameObject InputPanel;
    public Text txtInstruction;

    private int questionNum;
    private bool isTriggerQuesion = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UI.GetComponent<UI_Puzzle1>();
        database.GetComponent<Database_Puzzle1>();

        InputPanel.SetActive(false);

        database.Init();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            SwitchCamera();
        }

        if(isTriggerQuesion && Input.GetKeyDown(KeyCode.E))
        {
            Pause();
            ShowInputPanel(true);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Continue();
            ShowInputPanel(false);
        }
    }

    private void SwitchCamera()
    {
        switch(Camera.main.depth)
        {
            case 1:
                Camera.main.depth = -1;
                TopCameraPlayer.GetComponent<TopCameraPlayer>().isTopCamera = true;
                Pause();
                break;

            case -1:
                Camera.main.depth = 1;
                TopCameraPlayer.GetComponent<TopCameraPlayer>().isTopCamera = false;
                Continue();
                break;
        }
    }

    public void SendConstrains(float defaultScalar, float defaultX, float defaultY, float defaultZ)
    {
        UI.defaultScalar = defaultScalar;
        UI.defaultX = defaultX;
        UI.defaultY = defaultY;
        UI.defaultZ = defaultZ;
    }

    public void CheckAnswer(float scalar, float x, float y, float z)
    {
        database.Calculation(questionNum, scalar, x, y, z);
    }

    public void ShowInputPanel(bool value)
    {
        UI.InitUI();
        InputPanel.SetActive(value);
    }

    public void SetQuestionNumber(int value)
    {
        questionNum = value;
        Debug.Log(questionNum);
    }

    public void SetIsTriggerQuestion(bool value)
    {
        isTriggerQuesion = value;
    }

    public void SetText(string context)
    {
        txtInstruction.text = context;
    }

    public void Pause()
    {
        Camera.main.GetComponent<CameraManager>().enabled = false;
        Time.timeScale = 0;
    }

    public void Continue()
    {
        Camera.main.GetComponent<CameraManager>().enabled = true;
        Time.timeScale = 1;
    }
}
