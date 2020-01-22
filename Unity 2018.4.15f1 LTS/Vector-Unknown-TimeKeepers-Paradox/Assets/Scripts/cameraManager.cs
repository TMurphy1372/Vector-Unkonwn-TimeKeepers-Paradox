using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour
{

    public Camera CharCamera;
    public Camera TopCamera;

    public GameObject[] CharLables;
    public GameObject[] TopLables;

    bool topview = false;

    // Start is called before the first frame update
    void Start()
    {

        //sets all top view components to not active. 
        TopCamera.gameObject.SetActive(false);
        topview = false; 
        for (int i = 0; i < TopLables.Length; i++)
        {
            TopLables[i].gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (topview == true)
            {
                //set the character camera as active
                CharCamera.gameObject.SetActive(true);
                for (int i = 0; i < CharLables.Length; i++)
                {
                    CharLables[i].gameObject.SetActive(true);
                }

                //set the top camera as not active
                TopCamera.gameObject.SetActive(false);
                topview = false;
                for (int i = 0; i < TopLables.Length; i++)
                {
                    TopLables[i].gameObject.SetActive(false);
                }
            }
            else if (topview == false)
            {
                //set the character camera as not active
                CharCamera.gameObject.SetActive(false);
                for (int i = 0; i < CharLables.Length; i++)
                {
                    CharLables[i].gameObject.SetActive(false);
                }

                //set the top camera as active
                TopCamera.gameObject.SetActive(true);
                topview = true;
                for (int i = 0; i < TopLables.Length; i++)
                {
                    TopLables[i].gameObject.SetActive(true);
                }
            }
        }
    }
}
