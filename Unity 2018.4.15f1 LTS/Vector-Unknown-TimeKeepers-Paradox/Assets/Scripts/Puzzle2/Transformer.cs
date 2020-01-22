using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformer : MonoBehaviour
{
    public GameObject textGroup;
    private Vector3 worldPos;
    private int[] transformation;

    void Update()
    {
        textGroup.transform.LookAt(Camera.main.transform); //follow camera position so text always faces
        textGroup.transform.Rotate(Vector3.up - new Vector3(0, 180, 0)); //text is backwards otherwise
    }

    public int[] GetTransformation()
    {
        return transformation;
    }

    //set displayed position
    public void SetPosition(Vector3 pos)
    {
        TextMesh posText = MPTools.FindObjectInChildren(textGroup, "Position").GetComponent<TextMesh>();

        worldPos = pos;

        if (posText != null)
        {
            posText.text = MPTools.GetPositionText((int)pos.x, (int)pos.z);
        }
    }

    //set displayed and stored transformation matrix
    public void SetTransform(int[] set)
    {
        Vector3 pos;
        TextMesh tf = null;
        int len = set.Length;

        transformation = set;
        tf = MPTools.FindObjectInChildren(textGroup, "Transform").GetComponent<TextMesh>();

        if ((len % 2 != 0 && len % 3 != 0) || len > 9)
        {
            throw new UnityException("Transform set not appropriate size");
        }

        if (tf != null)
        {
            tf.text = MPTools.GetTransformText(set);

            //just for spacing, otherwis 3x3 is too tall
            pos = tf.gameObject.transform.position;
            tf.gameObject.transform.position = new Vector3(pos.x, (set.Length <= 4 ? pos.y : pos.y - 0.15F), pos.z);
        }
    }

    //get point between transformers position and it's transformed position (win condition)
    //only return value if the vector is within the rooms bounds (manually determined)
    public Vector3 GetHitPoint(float distance)
    {
        float dist = distance;
        Vector3 result = Vector3.Lerp(worldPos, TransformedVector(), dist);

        while ((result.z < -12f || result.z > 9f || result.x < -10f || result.x > 12f) && dist > 0)
        {
            dist = dist - 0.5f;
            result = Vector3.Lerp(worldPos, TransformedVector(), dist);
        }

        return result;
    }

    //takes stored object position and it's given transformation matrix and
    //performs the transformation calculation
    public Vector3 TransformedVector()
    {
        float x, y, z;
        int len = transformation.Length;

        x = 0;
        y = (len % 2 == 0 ? worldPos.y : 0);
        z = 0;

        //if 4 then 2x2 else 3x3
        if (len == 4)
        {
            x = ((int)worldPos.x * transformation[0]) + ((int)worldPos.z * transformation[1]);
            z = ((int)worldPos.x * transformation[2]) + ((int)worldPos.z * transformation[3]);
        }
        else
        {
            x = (worldPos.x * transformation[0]) + (worldPos.y * transformation[1]) + (worldPos.z * transformation[2]);
            y = (worldPos.x * transformation[3]) + (worldPos.y * transformation[4]) + (worldPos.z * transformation[5]);
            z = (worldPos.x * transformation[6]) + (worldPos.y * transformation[7]) + (worldPos.z * transformation[8]);
        }

        return new Vector3(x, y, z);
    }
}
