using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class VectorPoint : MonoBehaviour
{
    public int QuestionNumber;
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
                GameController_Puzzle1.gameController.ShowInputPanel(true);
                GameController_Puzzle1.gameController.SetQuestionNumber(QuestionNumber);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GameController_Puzzle1.gameController.ShowInputPanel(false);
            GameController_Puzzle1.gameController.SetQuestionNumber(0);
        }
    }
}
