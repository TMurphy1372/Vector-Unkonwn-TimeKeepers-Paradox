using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartInstructionManager : MonoBehaviour
{

    public Button button;
    public Text dialog;
    public Image image;

    public InputField X;
    public InputField Y;
    public InputField Z;
    public Button SubmitButton;
    public Button background;
    public Button background2;
    public InputField Scalar;
    public Text dialog1;
    public Text dialog2;


    // Start is called before the first frame update
    void Start()
    {
        button.gameObject.SetActive(true);
        dialog.gameObject.SetActive(true);
        image.gameObject.SetActive(true);

        X.gameObject.SetActive(true);
        Y.gameObject.SetActive(true);
        Z.gameObject.SetActive(true);
        SubmitButton.gameObject.SetActive(true);
        background.gameObject.SetActive(true);
        background2.gameObject.SetActive(true);
        Scalar.gameObject.SetActive(true);
        dialog1.gameObject.SetActive(true);
        dialog2.gameObject.SetActive(true);

        //connects button action to button 
        Button btn = button.GetComponent<Button>();
        btn.onClick.AddListener(onClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //activated when button is pressed
    void onClick()
    {
        button.gameObject.SetActive(false);
        dialog.gameObject.SetActive(false);
        image.gameObject.SetActive(false);
        X.gameObject.SetActive(false);
        Y.gameObject.SetActive(false);
        Z.gameObject.SetActive(false);
        SubmitButton.gameObject.SetActive(false);
        background.gameObject.SetActive(false);
        background2.gameObject.SetActive(false);
        Scalar.gameObject.SetActive(false);
        dialog1.gameObject.SetActive(false);
        dialog2.gameObject.SetActive(false);
    }
}
