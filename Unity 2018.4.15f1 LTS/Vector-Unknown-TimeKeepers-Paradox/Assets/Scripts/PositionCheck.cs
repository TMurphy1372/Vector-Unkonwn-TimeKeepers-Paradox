using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionCheck : MonoBehaviour
{
    public GameObject correctPosition;
    public Color wrong;
    public Color right;

    MeshRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        renderer.material.color = wrong;
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position == correctPosition.transform.position)
        {
            renderer.material.color = right;
        }
        else
        {
            renderer.material.color = wrong;
        }
    }
}
