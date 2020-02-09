using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameController : MonoBehaviour
{
    public static bool[] PuzzleComplete = { false, false, false };

    public Transform player;
    public GameObject[] portals;
    public MeshRenderer mast, rudder, sail;
    public Material wood, sailM;
    public TextMesh dialogueMessage;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        if(PuzzleComplete[0] == true)           //Puzzle 1 is complete
        {
            player.transform.position = portals[0].transform.position;
            portals[0].SetActive(false);        //Inactive puzzle 1, active puzzle 2
            portals[1].SetActive(true);
            mast.material = wood;
            dialogueMessage.text = "You found the mast, now you need to find the rudder. \n" +
                                   "You hear a buzzing and feel a cold breeze coming from \n" +
                                   "the hill to the right.";

            if (PuzzleComplete[1] == true)      //Puzzle 2 is complete
            {
                player.transform.position = portals[1].transform.position;
                portals[1].SetActive(false);    //Inactive puzzle 2, active puzzle 3
                portals[2].SetActive(true);
                rudder.material = wood;
                dialogueMessage.text = "You found the rudder, now you need to find the sail. \n" +
                                       "You think you hear an evil laughter coming from the base \n" +
                                       "of the mountain, but no one was with you on the raft....";

                if (PuzzleComplete[2] == true)   //Puzzle 3 is complete
                {
                    player.transform.position = portals[2].transform.position;
                    portals[2].SetActive(false);    //Inactive puzzle 3
                    sail.material = sailM;
                    dialogueMessage.text = "You found all of the parts to your raft! \n" +
                                           "Time to sail home.";
                }
            }
        }
        else
        {
            portals[0].SetActive(true);
            dialogueMessage.text = "You wake up on an island with pieces of\n" +
                                   "your raft missing go find them.";
        }
    }
}
