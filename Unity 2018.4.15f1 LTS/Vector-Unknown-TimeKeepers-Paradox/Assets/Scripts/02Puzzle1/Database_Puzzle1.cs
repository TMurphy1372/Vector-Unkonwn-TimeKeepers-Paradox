using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database_Puzzle1 : MonoBehaviour
{
    public List<Transform> points;
    public List<Vector3> pointVectors;

    private void Start()
    {
        foreach(Transform T in points)
        {
            pointVectors.Add(new Vector3(T.position.x, T.position.z, T.position.y));
        }
    }

    public bool Calculation(int QuestionNum, float scalar, float x, float y, float z)
    {
        Vector3 answer = pointVectors[QuestionNum] - pointVectors[QuestionNum - 1];
        Vector3 playerAnswer = new Vector3(x, y, z) * scalar;
        Debug.Log(answer);
        if(answer == playerAnswer)
        {
            return true;
        }

        return false;
    }
}
