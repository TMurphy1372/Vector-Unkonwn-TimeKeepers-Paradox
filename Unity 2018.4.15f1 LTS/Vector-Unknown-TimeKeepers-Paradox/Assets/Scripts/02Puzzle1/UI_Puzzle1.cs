using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Puzzle1 : MonoBehaviour
{
    public InputField scalar;
    public InputField x, y, z;

    public void SubmitAnswer()
    {
        float scalarValue;
        float xValue, yValue, zValue;

        if (scalar.text == "")
        {
            Debug.Log("Please input the scalar value");
        }
        else if (x.text == "" || y.text == "" || z.text == "")
        {
            Debug.Log("Please input the vector value");
        }
        else if (!float.TryParse(scalar.text, out scalarValue) || !float.TryParse(x.text, out xValue) || !float.TryParse(y.text, out yValue) || !float.TryParse(z.text, out zValue))
        {
            Debug.Log("Please input numbers"); 
        }
        else
        {
            GameController_Puzzle1.gameController.CheckAnswer(scalarValue, xValue, yValue, zValue);
            Debug.Log("Your answer is Scalar: " + scalar.text + ", X: " + x.text + ", Y: " + y.text + ", Z: " + z.text);
            ClearInputField();
        }
    }

    private void ClearInputField()
    {
        scalar.text = "";
        x.text = "";
        y.text = "";
        z.text = "";
    }

}
