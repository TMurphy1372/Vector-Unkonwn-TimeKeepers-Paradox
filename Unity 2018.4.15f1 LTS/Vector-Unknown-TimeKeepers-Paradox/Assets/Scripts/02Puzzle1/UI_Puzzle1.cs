using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Puzzle1 : MonoBehaviour
{
    public InputField iptScalar;
    public InputField iptX, iptY, iptZ;

    [HideInInspector]
    public float defaultScalar;
    [HideInInspector]
    public float defaultX, defaultY, defaultZ;

    public void InitUI()
    {
        if(defaultScalar != 0)
        {
            iptScalar.text = defaultScalar.ToString();
            iptScalar.interactable = false;
        }
    }

    public void SubmitAnswer()
    {
        float scalarValue;
        float xValue, yValue, zValue;

        if (iptScalar.text == "")
        {
            GameController_Puzzle1.instance.SetText("Please input the scalar value");
        }
        else if (iptX.text == "" || iptY.text == "" || iptZ.text == "")
        {
            GameController_Puzzle1.instance.SetText("Please input the vector value");
        }
        else if (!float.TryParse(iptScalar.text, out scalarValue) || !float.TryParse(iptX.text, out xValue) || !float.TryParse(iptY.text, out yValue) || !float.TryParse(iptZ.text, out zValue))
        {
            GameController_Puzzle1.instance.SetText("Please input numbers"); 
        }
        else
        {
            GameController_Puzzle1.instance.CheckAnswer(scalarValue, xValue, yValue, zValue);
            Debug.Log("Your answer is Scalar: " + iptScalar.text + ", X: " + iptX.text + ", Y: " + iptY.text + ", Z: " + iptZ.text);

            GameController_Puzzle1.instance.ShowInputPanel(false);

            GameController_Puzzle1.instance.Continue();

            ClearInputField();
        }
    }

    private void ClearInputField()
    {
        iptScalar.text = "";
        iptX.text = "";
        iptY.text = "";
        iptZ.text = "";
        iptScalar.interactable = true;
        iptX.interactable = true;
        iptY.interactable = true;
        iptZ.interactable = true;
    }

    
}
