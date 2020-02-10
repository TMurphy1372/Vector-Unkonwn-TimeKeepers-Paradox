using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database_Puzzle1 : MonoBehaviour
{
    public List<Transform> points;
    public List<Transform> Bridges;
    public List<Vector3> pointVectors;
    public LineRenderer[] lineTips = new LineRenderer[2];
    public Transform wrongAnswerPoint;

    public void Init()
    {
        //Convert all data in Unity xyz standard into math xyz standard
        foreach (Transform T in points)
        {
            pointVectors.Add(new Vector3(T.position.x, T.position.z, T.position.y));
        }

        //Init first line tip from point 1 to point 2
        InitLineTips(lineTips[0], points[0].position, points[1].position, Color.yellow, Color.yellow);
    }


    public void Calculation(int questionNum, float scalar, float x, float y, float z)
    {
        Vector3 answer = pointVectors[questionNum] - pointVectors[questionNum - 1];
        Vector3 playerAnswer = new Vector3(x, y, z) * scalar;

        Vector3 targetPositionDisplay = pointVectors[questionNum - 1] + new Vector3(x, y, z) * scalar;
        Vector3 targetPosition = new Vector3(targetPositionDisplay.x, targetPositionDisplay.z, targetPositionDisplay.y);
        Debug.Log(answer);
        Debug.Log(playerAnswer);
        Debug.Log(answer == playerAnswer);
        if (answer == playerAnswer)
        {
            GameController_Puzzle1.instance.SetText("Correct");

            //Open the bridge for current question
            Bridges[questionNum - 1].gameObject.SetActive(true);

            //If there are more question, move line tip to next question
            if(questionNum + 1 < points.Count)
            {
                InitLineTips(lineTips[0], points[questionNum].position, points[questionNum + 1].position, Color.yellow, Color.yellow);

                //Make sure wrong answer tips line is inactive
                lineTips[1].gameObject.SetActive(false);
            }
        }
        else
        {
            //Show wrong answer tips
            GameController_Puzzle1.instance.SetText("Why you want to move to " + targetPositionDisplay + "?" + " I don't understand. Press 'E' to try again.");

            InitLineTips(lineTips[1], points[questionNum - 1].position, targetPosition, Color.red, Color.red);

            wrongAnswerPoint.position = targetPosition;
            wrongAnswerPoint.gameObject.SetActive(true);
        }
    }

    private void InitLineTips(LineRenderer lineTips, Vector3 startPoint, Vector3 endPoint, Color startColor, Color endColor)
    {
        lineTips.gameObject.SetActive(true);
        lineTips.SetPosition(0, startPoint);
        lineTips.SetPosition(1, endPoint);
        lineTips.startColor = startColor;
        lineTips.endColor = endColor;
    }
}
