using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform target;
    public float rotatedSpeed = 5.0f;
    public float rotatedSmoothTime = 0.12f;
    public float dstFromTarget = 2f;
    public Vector2 verticalMinMax = new Vector2(-30, 60);

    private Vector3 rotationSmoothVelocity, currentRotation;
    private float horizontal, vertical;

    private void LateUpdate()
    {
        CameraMovement();
    }

    private void CameraMovement()
    {
        horizontal += Input.GetAxis("Mouse X") * rotatedSpeed;
        vertical -= Input.GetAxis("Mouse Y") * rotatedSpeed;
        vertical = Mathf.Clamp(vertical, verticalMinMax.x, verticalMinMax.y);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(vertical, horizontal), ref rotationSmoothVelocity, rotatedSmoothTime);
        this.transform.eulerAngles = currentRotation;

        this.transform.position = target.position - this.transform.forward * dstFromTarget;
    }

    /*
    public Transform playerTransform;
    [Range(0.01f, 1.0f)]
    public float smoothFactor = 0.5f;
    public float rotatedSpeed = 5.0f;
    public bool lookAtPlayer = false;
    public bool rotateAroundPlayer = true;

    private Vector3 cameraOffset;

    private void Start()
    {
        cameraOffset = transform.position - playerTransform.position;
    }

    private void LateUpdate()
    {
        if(rotateAroundPlayer)
        {
            Quaternion camTurnAngleX = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotatedSpeed, Vector3.up);
            Debug.Log(Input.GetAxis("Mouse Y"));
            cameraOffset = camTurnAngleX * cameraOffset;
        }

        Vector3 newPosition = playerTransform.position + cameraOffset;

        this.transform.position = Vector3.Slerp(transform.position, newPosition, smoothFactor);

        if (lookAtPlayer || rotateAroundPlayer)
            this.transform.LookAt(playerTransform);
    }
    */
}
