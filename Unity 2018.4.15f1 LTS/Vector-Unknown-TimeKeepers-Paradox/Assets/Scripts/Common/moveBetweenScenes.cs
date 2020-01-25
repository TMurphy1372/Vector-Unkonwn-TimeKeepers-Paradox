using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class moveBetweenScenes : MonoBehaviour
{
    //the scene that you want to load
    public string nScene;

    //gameobject to be moved
    //public GameObject movedObject;
    [HideInInspector]
    public bool stay = true;

    //for when the player activates trigger on portals colliders
    private float stayCount = 0.0f;
    void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            if (stayCount > 0.25f)
            {
                print("Portal activated");

                //StartCoroutine(LoadYourAsyncScene(other.gameObject));
                SceneManager.LoadScene(nScene, LoadSceneMode.Single);
                stayCount = stayCount - 0.25f;
            }
            else
            {
                stayCount = stayCount + Time.deltaTime;
            }
        }
    }

    IEnumerator LoadYourAsyncScene(GameObject movedObject)
    {
        //get current scene
        Scene current = SceneManager.GetActiveScene();

        //load next scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nScene, LoadSceneMode.Additive);

        //wait until scene has loaded
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        //destroys third person controller in main scene before going back
        //if (nScene == "MainScene")
        //{
        //    Destroy(GameObject.Find("ThirdPersonController"));
        //    //GameObject.Find("ThirdPersonController").SetActive(false);
        //}
        //move the gameobject to new scene 
        //SceneManager.MoveGameObjectToScene(movedObject, SceneManager.GetSceneByName(nScene));

        ////sets the location of the player when returning to the main scene
        //if (nScene == "MainScene")
        //{
        //    movedObject.transform.position = new Vector3(274.7f, 15.5f, 234.8f);
        //}
        //unload the previous scene
        SceneManager.UnloadSceneAsync(current);

    }
}
