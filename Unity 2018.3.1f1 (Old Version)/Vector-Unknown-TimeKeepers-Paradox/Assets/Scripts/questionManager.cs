using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class questionManager : MonoBehaviour
{
    public LineRenderer line;
    public Vector3 VectorAnswer;
    public float ScalarAnswer;
    public GameObject Platform;

    //UI Elements
    public InputField X;
    public InputField Y;
    public InputField Z;
    public Button SubmitButton;
    public Button background;
    public Button background2;
    public InputField Scalar;
    public Text dialog1;
    public Text dialog2;
    public Text dialog3;

    private bool positionCorrect = false;
    private Color wrong = Color.red;
    private Color correct = Color.green;
    private bool q1 = false;
    private bool q2 = false;
    private bool q3 = false;

    void Start()
    {
        Platform.gameObject.SetActive(false);
        X.gameObject.SetActive(false);
        Y.gameObject.SetActive(false);
        Z.gameObject.SetActive(false);
        SubmitButton.gameObject.SetActive(false);
        background.gameObject.SetActive(false);
        background2.gameObject.SetActive(false);
        Scalar.gameObject.SetActive(false);
        dialog2.gameObject.SetActive(false);
        dialog3.gameObject.SetActive(false);

        Button btn = SubmitButton.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);


        LineRenderer lineR = line.gameObject.GetComponent<LineRenderer>();
        lineR.startColor = wrong;
        lineR.positionCount = 2;
    }


    void Update()
    {
        //LineRenderer lineR = line.gameObject.GetComponent<LineRenderer>();
        

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //when player stand on platform UI elements are shown
            X.gameObject.SetActive(true);
            Y.gameObject.SetActive(true);
            Z.gameObject.SetActive(true);
            SubmitButton.gameObject.SetActive(true);
            background.gameObject.SetActive(true);
            background2.gameObject.SetActive(true);
            dialog2.gameObject.SetActive(true);
            dialog3.gameObject.SetActive(true);
            Scalar.gameObject.SetActive(true);
            //dialog2.GetComponent<UnityEngine.UI.Text>().text = "Enter Vector";
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //when player leaves platform UI elements are disabled 
            X.gameObject.SetActive(false);
            Y.gameObject.SetActive(false);
            Z.gameObject.SetActive(false);
            SubmitButton.gameObject.SetActive(false);
            background.gameObject.SetActive(false);
            background2.gameObject.SetActive(false);
            dialog2.gameObject.SetActive(false);
            dialog3.gameObject.SetActive(false);
            Scalar.gameObject.SetActive(false);
        }
    }

    //when the button is pressed
    void OnClick()
    {
        LineRenderer lineR = line.gameObject.GetComponent<LineRenderer>();

        //first question
        if (positionCorrect == false)
        {
            try
            {
                float integerX = float.Parse(X.text);
                float integerY = float.Parse(Y.text);
                float integerZ = float.Parse(Z.text);
                float InputScalar = float.Parse(Scalar.text);

                if ((integerX * InputScalar) == VectorAnswer.x && (integerY * InputScalar) == VectorAnswer.z && (integerZ * InputScalar) == VectorAnswer.y)
                {
                    Scalar.gameObject.SetActive(false);
                    X.gameObject.SetActive(false);
                    Y.gameObject.SetActive(false);
                    Z.gameObject.SetActive(false);
                    Platform.gameObject.SetActive(true);
                    dialog1.GetComponent<UnityEngine.UI.Text>().text = "That is correct!";

                    //lineR.SetPosition(1, new Vector3(integerX / 5.6f, integerZ, integerY / 3));
                    lineR.SetPosition(1, new Vector3(integerX * InputScalar, integerZ * InputScalar, integerY * InputScalar));
                    lineR.startColor = correct;
                    lineR.endColor = correct;
                    
                    positionCorrect = true;

                    if (q1 == false)
                        q1 = true;
                    else if (q2 == false)
                        q2 = true;
                    else if (q3 == false)
                        q3 = true;
                    //dialog2.GetComponent<UnityEngine.UI.Text>().text = "Enter Scalar";
                }
                else
                {
                    dialog1.GetComponent<UnityEngine.UI.Text>().text = "That is NOT correct!. Try again";
                    //lineR.SetPosition(1, new Vector3(integerX / 5.6f, integerZ, integerY / 3));
                    lineR.SetPosition(1, new Vector3(integerX * InputScalar, integerZ * InputScalar, integerY * InputScalar));
                }
            }
            catch (Exception e)
            {
                Debug.Log("An Exception was thrown and handled");
                dialog1.GetComponent<UnityEngine.UI.Text>().text = "You have entered invalid input. Try again";
                
            }
        }

        //for scalar question
        //if (positionCorrect == true)
        //{
        //    float InputScalar = float.Parse(Scalar.text);

        //    try
        //    {
        //        if (InputScalar == ScalarAnswer)
        //        {

        //            Scalar.gameObject.SetActive(false);
        //            Platform.gameObject.SetActive(true);
        //            dialog1.GetComponent<UnityEngine.UI.Text>().text = "That is correct!";

        //            if (q1 == false)
        //                q1 = true;
        //            else if (q2 == false)
        //                q2 = true;
        //            else if (q3 == false)
        //                q3 = true;
                    
        //        }
        //        else
        //        {
        //            dialog1.GetComponent<UnityEngine.UI.Text>().text = "That is NOT correct!. Try again";
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Debug.Log("An Exception was thrown and handled");
        //        dialog1.GetComponent<UnityEngine.UI.Text>().text = "You have entered invalid input. Try again";

        //    }
        //}
    }
}
