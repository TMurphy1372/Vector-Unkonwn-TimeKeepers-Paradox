using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rampManager : MonoBehaviour
{
    public GameObject[] Ramps; //array of ramps
    public GameObject[] startPositions; //array of starting poristions for resetting
    public GameObject[] positions; //array of empty gameObjects to move the ramps to
    private int rampPosition = 0; // incraments the loctions the ramps are placed

    //setting text to be displayed when player enters trigger
    public string Text = "Click on a ramp to move it";
    public Rect displayBox = new Rect(0, 0, 200, 100);
    public GUISkin displayText;
    public bool guiOn = false;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "Ramp")
                {
                    if (rampPosition < 3)
                    {
                        hit.collider.transform.position = positions[rampPosition].transform.position;
                        rampPosition++;
                    }
                    else
                        rampPosition = 0;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            for (int i = 0; i < Ramps.Length; i++)
            {
                Ramps[i].transform.position = startPositions[i].transform.position;
            }
            rampPosition = 0;
        }
    }

    //checks that the player is near the ramps starting positions
    void OnTriggerEnter()
    {
        guiOn = true;

    }

    //turns off gui 
    void OnTriggerExit()
    {
        guiOn = false;
    }

    //displays text when set to true
    void OnGUI()
    {
        if (GUI.skin != null)
            GUI.skin = displayText;

        if (guiOn == true)
        {
            //make group at center of the screen 
            GUI.BeginGroup(new Rect((Screen.width - displayBox.width) / 2, (Screen.height - displayBox.height) / 2,
                displayBox.width, displayBox.height));

            //display text
            GUI.Label(displayBox, Text);
    
            GUI.EndGroup();

        }
    }
}
