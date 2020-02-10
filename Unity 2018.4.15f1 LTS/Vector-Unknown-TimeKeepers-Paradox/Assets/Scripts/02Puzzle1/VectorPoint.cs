using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class VectorPoint : MonoBehaviour
{
    public int QuestionNumber;
    public float defaultScalar = 0;
    public float defaultX = 0, defaultY = 0, defaultZ = 0;
    public Text info;
    public Text infoTop;

    private void LateUpdate()
    {
        info.text = "(" + this.transform.position.x + "," + this.transform.position.z + "," + this.transform.position.y + ")";
        infoTop.text = info.text;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if(QuestionNumber != 0)
            {
                GameController_Puzzle1.instance.SetText("- Press 'E' to input your answer.\n" +
                                                        "- Press 'Z' to switch into the top-down camera.\n" +
                                                        "- Press 'Esc' to exit the question.");
                GameController_Puzzle1.instance.SetIsTriggerQuestion(true);
                GameController_Puzzle1.instance.SetQuestionNumber(QuestionNumber);
                GameController_Puzzle1.instance.SendConstrains(defaultScalar, defaultX, defaultY, defaultZ);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GameController_Puzzle1.instance.SetText("- Press Z for top view.\n" +
                                                    "- Round to whole numbers" +
                                                    "- Stand on a platform to enter values.");
            GameController_Puzzle1.instance.SetIsTriggerQuestion(false);
            GameController_Puzzle1.instance.SetQuestionNumber(0);
        }
    }
}
