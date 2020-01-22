using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardPosition : MonoBehaviour
{

    private Vector3 worldPos;

    void Update()
    {
        TextMesh posText = MPTools.FindObjectInChildren(this.gameObject, "Position").GetComponent<TextMesh>();
        Vector3 pos;

        pos = this.transform.position;
        worldPos = pos;//this.transform.TransformPoint(pos);
        posText.text = MPTools.GetPositionText((int)worldPos.x, (int)worldPos.z);

        posText.transform.LookAt(Camera.main.transform); //follow camera position so text always faces
        posText.transform.Rotate(Vector3.up - new Vector3(0, 180, 0)); //text is backwards otherwise
    }

    //parameter should be world location
    public void SetPosition(Vector3 newPos)
    {
        TextMesh posText = MPTools.FindObjectInChildren(this.gameObject, "Position").GetComponent<TextMesh>();

        if (posText != null)
        {
            this.transform.position = newPos;
            posText.text = MPTools.GetPositionText((int)newPos.x, (int)newPos.z);
        }
    }

    public Vector3 GetPosition()
    {
        return this.transform.position;
    }
}
