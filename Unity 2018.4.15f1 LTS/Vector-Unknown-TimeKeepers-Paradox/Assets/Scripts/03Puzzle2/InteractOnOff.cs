using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractOnOff : MonoBehaviour
{

    // Vars
    public GameObject pressE;

    Color color;
    MeshRenderer myRenderer;
    bool hitting;
    bool blocked;

    // Use this for initialization
    void Start()
    {
        pressE.SetActive(false);
        myRenderer = GetComponent<MeshRenderer>();
        myRenderer.material.color = Color.red;
    }

    void Update()
    {
        if (hitting)
        {
            if (Input.GetKeyDown("e"))
            {
                FlipSwitch();
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Player" && !blocked)
        {
            pressE.SetActive(true);
            hitting = true;
        }
    }

    void OnCollisionExit(Collision col)
    {
        pressE.SetActive(false);
        hitting = false;
    }

    //block/unblock others if this one has been selected
    //this allows bouncing to take place naturually without the need for complex logic to reattract
    void InteractOthers(bool set)
    {
        InteractOnOff[] all;

        all = GameObject.FindObjectsOfType<InteractOnOff>();

        foreach (InteractOnOff ob in all)
        {
            if (ob == this)
            {
                blocked = !set;
            }
            else
            {
                ob.SetInteractive(set);
            }
        }
    }

    public void LineHit(bool hit)
    {
        if (hit)
        {
            myRenderer.material.color = Color.green;
        }
        else
        {
            myRenderer.material.color = Color.red;
        }
    }

    public void SetInteractive(bool set)
    {
        blocked = set;
    }

    // Callable function to demonstrate player interaction
    public void FlipSwitch()
    {
        GameObject mp = GameObject.Find("MirrorPuzzle");
        GameObject laser = GameObject.Find("LaserBeam");

        if (myRenderer.material.color == Color.green)
        {
            myRenderer.material.color = Color.red;
            //mp.GetComponentInChildren<LaserScript>().Detract();
            laser.GetComponentInChildren<LaserScript>().Detract();
            InteractOthers(false);
        }
        else if (myRenderer.material.color == Color.red)
        {
            myRenderer.material.color = Color.green;
            //mp.GetComponentInChildren<LaserScript>().Attract(this.gameObject);
            laser.GetComponentInChildren<LaserScript>().Attract(this.gameObject);
            InteractOthers(true);
            mp.GetComponentInChildren<MirrorPuzzle>().IncrementChoice();
        }
    }

    public static void TurnAllOff()
    {
        InteractOnOff[] all = GameObject.FindObjectsOfType<InteractOnOff>();

        foreach (InteractOnOff ob in all)
        {
            ob.myRenderer.material.color = Color.red;
            ob.SetInteractive(true);
        }
    }
}
