using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PickUp")
        {
            if (gameManager.Puzzle1Complete == false)
            {
                other.gameObject.SetActive(false);
                gameManager.Puzzle1Complete = true;
            }
            else
            {
                other.gameObject.SetActive(false);
                gameManager.Puzzle2Complete = true;
            }
        }
    }
}
