using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class puzzleComplete : MonoBehaviour
{

    public Text dialog;
    public ParticleSystem congrats;
    public GameObject endportal;
    // Start is called before the first frame update
    void Start()
    {
        endportal.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            dialog.GetComponent<UnityEngine.UI.Text>().text = "You found the mast!";
            congrats.Play();
            endportal.gameObject.SetActive(true);
        }
        
    }
}
