using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{

    public static bool Puzzle1Complete = false;
    public static bool Puzzle2Complete = false;
    public static bool Puzzle3Complete = false;
    public GameObject portal2;
    public GameObject portal3;

    public GameObject dialogue;

    //pickup objects
    public GameObject sail;
    public GameObject rudder;
    public GameObject mast;

    //materials 
    public Material missingItem;
    public Material wood;
    public Material sailText;


    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        portal2.gameObject.SetActive(false);
        portal3.gameObject.SetActive(false);

        mast.GetComponent<MeshRenderer>().material = missingItem;
        sail.GetComponent<MeshRenderer>().material = missingItem;
        rudder.GetComponent<MeshRenderer>().material = missingItem;
    }

    // Update is called once per frame
    void Update()
    {
        if (Puzzle1Complete == true)
        {
            if (SceneManager.GetActiveScene().name == "MainScene")
            {
               
                if (portal2 == null || mast == null || dialogue == null)
                {
                    portal2 = GameObject.Find("portalForLevel2");
                    mast = GameObject.Find("Mast");
                    dialogue = GameObject.Find("dialogueMessage");
                    portal2.gameObject.SetActive(true);
                    mast.GetComponent<MeshRenderer>().material = wood;
                    dialogue.GetComponent<TextMesh>().text = "You found the mast, now you need to find the rudder. \n" + 
                                                             "You hear a buzzing and feel a cold breeze coming from \n" +
                                                             "the hill to the right.";

                }
                else
                {
                    portal2.gameObject.SetActive(true);
                    mast.GetComponent<MeshRenderer>().material = wood;
                    dialogue.GetComponent<TextMesh>().text = "You found the mast, now you need to find the rudder. \n" +
                                                             "You hear a buzzing and feel a cold breeze coming from \n" +
                                                             "the hill to the right.";
                }
            }
        }


        if (Puzzle2Complete == true)
        {
            if (SceneManager.GetActiveScene().name == "MainScene")
            {
               
                if (portal3 == null || rudder == null || dialogue == null)
                {
                    portal3 = GameObject.Find("portalForLevel3");
                    portal3.gameObject.SetActive(true);
                    rudder = GameObject.Find("Ruder");
                    rudder.GetComponent<MeshRenderer>().material = wood;
                    dialogue = GameObject.Find("dialogueMessage");
                    dialogue.GetComponent<TextMesh>().text = "You found the rudder, now you need to find the sail. \n" +
                                                             "You think you hear an evil laughter coming from the base \n" +
                                                             "of the mountain, but no one was with you on the raft....";
                }
                else
                {
                    portal3.gameObject.SetActive(true);
                    rudder.GetComponent<MeshRenderer>().material = wood;
                    dialogue.GetComponent<TextMesh>().text = "You found the rudder, now you need to find the sail. \n" +
                                                             "You think you hear an evil laughter coming from the base \n" +
                                                             "of the mountain, but no one was with you on the raft....";
                }
            }
        }

        if (Puzzle3Complete == true)
        {
            if (SceneManager.GetActiveScene().name == "MainScene")
            {
                if (sail == null || dialogue == null)
                {
                    sail = GameObject.Find("SailMiddle");
                    sail.GetComponent<MeshRenderer>().material = sailText;
                    dialogue.GetComponent<TextMesh>().text = "You found all of the parts to your raft! \n" +
                                                             "Time to sail home.";
                }
                
            }
        }
    }
}
