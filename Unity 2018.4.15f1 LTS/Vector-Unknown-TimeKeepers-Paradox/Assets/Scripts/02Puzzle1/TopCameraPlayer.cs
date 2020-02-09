using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopCameraPlayer : MonoBehaviour
{
    public float movementSpeed = 5f;
    [HideInInspector]
    public bool isTopCamera = false;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if(isTopCamera)
        {
            //Movement();
        }
        else
        {
            this.transform.position = new Vector3(player.position.x, player.transform.position.y + 5f, player.position.z);
        }
    }

    private void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float movementSpeedTemp = movementSpeed;

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            movementSpeedTemp = movementSpeed * 2f;
        }

        this.transform.position += new Vector3(horizontal, 0f, vertical) * Time.deltaTime * movementSpeedTemp;
    }
}
