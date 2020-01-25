using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
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
