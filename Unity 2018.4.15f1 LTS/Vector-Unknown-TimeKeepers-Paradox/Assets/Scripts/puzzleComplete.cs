using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class puzzleComplete : MonoBehaviour
{
    public Text dialog;
    public ParticleSystem congrats;
    public GameObject endportal;

    private void Start()
    {
        endportal.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            dialog.text = "You found the mast!";
            congrats.Play();
            endportal.gameObject.SetActive(true);

            MainGameController.PuzzleComplete[0] = true;
        }
        
    }
}
