using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using UnityEngine.UI;


public class spawnOrbs : MonoBehaviour
{

    private int minX = 1;
    private int minY = 1;
    private int minZ = 1;
    private int maxX = 4;
    private int maxY = 4;
    private int maxZ = 4;

    private Vector3 vector0;
    private Vector3 vector1;
    private Vector3 vector2;

    private Vector3 vector3;
    private Vector3 vector4;
    private Vector3 vector5;

    private Vector3 zeroVector = new Vector3(12.7f, 0, 13.78f);

    private int dialogCount = 0;

    private int integerX;
    private int integerY;
    private int integerZ;

    private int[,] orbVecs = new int[3, 3];

    public Transform[] spawnLocations;
    public GameObject[] whatToSpawnPrefab;
    public GameObject[] whatToSpawnClone;

    //public Text PuzzleType;
    public Text choice;
    public Text dialog1;
    public Button goButton;
    public Button submitButton;
    public InputField xVal;
    public InputField zVal;
    public InputField yVal;
    public InputField aVal;
    public InputField cVal;
    public InputField bVal;
    public GameObject rrefPanel;
    public Text a11;
    public Text a12;
    public Text a13;
    public Text a21;
    public Text a22;
    public Text a23;
    public Text a31;
    public Text a32;
    public Text a33;

    public GameObject prefab; 
    Matrix<double> myMatrix = null;

    System.Random randNum = new System.Random();
    System.Random puzzleRandom = new System.Random();

    private void Start()
    {

        submitButton.gameObject.SetActive(false);
        xVal.gameObject.SetActive(false);
        zVal.gameObject.SetActive(false);
        yVal.gameObject.SetActive(false);
        aVal.gameObject.SetActive(false);
        cVal.gameObject.SetActive(false);
        bVal.gameObject.SetActive(false);
        rrefPanel.gameObject.SetActive(false);
        dialog1.GetComponent<UnityEngine.UI.Text>().text = "You've encountered an orb casting Wizard!  " +
            "Click the 'Next' button with the mouse.";

        Button btn = goButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);


    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            Ray orbRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(orbRay, out hitInfo, 100.0f))
            {

                if (hitInfo.transform.tag == "first")
                {

                    //DisplayChoice();
                    choice.GetComponent<UnityEngine.UI.Text>().text = "Orb 1 Selected";

                }
                if (hitInfo.transform.tag == "second")
                {

                    //DisplayChoice();
                    choice.GetComponent<UnityEngine.UI.Text>().text = "Orb 2 Selected";


                }
                if (hitInfo.transform.tag == "third")
                {

                    //DisplayChoice();
                    choice.GetComponent<UnityEngine.UI.Text>().text = "Orb 3 Selected";

                }
                if (hitInfo.transform.tag == "fourth")
                {
                    //DisplayChoice();
                    choice.GetComponent<UnityEngine.UI.Text>().text = "Orb 4 Selected";

                }
                if (hitInfo.transform.tag == "fifth")
                {
                    //DisplayChoice();
                    choice.GetComponent<UnityEngine.UI.Text>().text = "Orb 5 Selected";

                }
                if (hitInfo.transform.tag == "sixth")
                {
                    //DisplayChoice();
                    choice.GetComponent<UnityEngine.UI.Text>().text = "Orb 6 Selected";

                }
            }
        }
    }


    void TaskOnClick()
    {
        Debug.Log("You have clicked the button!");

        dialogCount++;

        if (dialogCount == 1)
        {
            dialog1.GetComponent<UnityEngine.UI.Text>().text = "He uses 3 different formations for his orbs defense. He " +
                "likes to cast them using vectors for precision.";
        }

        if (dialogCount == 2)
        {
            dialog1.GetComponent<UnityEngine.UI.Text>().text = "The secret to defeating him is to sneak in an exploding " +
                "orb un-noticed that follows his orb scheme.";
        }

        if (dialogCount == 3)
        {
            dialog1.GetComponent<UnityEngine.UI.Text>().text = "Walk over to the wizard using the 'asdw' keys. ";
        }

        if (dialogCount == 4)
        {
            dialog1.GetComponent<UnityEngine.UI.Text>().text = "If you ever need to look around press " +
                "the 'r' key and use the mouse, then press it again to return to the normal view.";
        }

        if (dialogCount == 5)
        {
            dialog1.GetComponent<UnityEngine.UI.Text>().text = "Here all 3 yellow orbs are positioned as parallel vectors on a line through the origin. " +
                "Their vectors are all scalar multiples of each other.";
            CheckLineSpan();

            spawnLocations[0].GetComponent<TextMesh>().text = "";
            spawnLocations[1].GetComponent<TextMesh>().text = "";
            spawnLocations[2].GetComponent<TextMesh>().text = "";
            spawnLocations[3].GetComponent<TextMesh>().text = "";
            spawnLocations[4].GetComponent<TextMesh>().text = "";
            spawnLocations[5].GetComponent<TextMesh>().text = "";

        }

        if (dialogCount == 6)
        {
            dialog1.GetComponent<UnityEngine.UI.Text>().text = "The vectors span must then consist of the vectors along the line " +
                "containing the three vectors. This is the span of a line. ";
        }

        if (dialogCount == 7)
        {
            dialog1.GetComponent<UnityEngine.UI.Text>().text = "If were were to choose a blue orb in the span of the yellow orbs we would choose the " +
                "blue orb on this line.";
        }


        if (dialogCount == 8)
        {
            dialog1.GetComponent<UnityEngine.UI.Text>().text = "Lets take a look at another formation that is casted. ";
        }

        if (dialogCount == 9)
        {
            dialog1.GetComponent<UnityEngine.UI.Text>().text = "There is something noticeable about the vectors that create the 3 yellow orbs.";


            Destroy(whatToSpawnClone[0]);
            Destroy(whatToSpawnClone[1]);
            Destroy(whatToSpawnClone[2]);
            Destroy(whatToSpawnClone[3]);
            Destroy(whatToSpawnClone[4]);
            Destroy(whatToSpawnClone[5]);

            CreateR3Span();
        }

        if (dialogCount == 10)
        {
            dialog1.GetComponent<UnityEngine.UI.Text>().text = "Lets take the RREF of the yellow orb vectors and see " +
                "if they tell us anything about them.";
        }

        if (dialogCount == 11)
        {
            dialog1.GetComponent<UnityEngine.UI.Text>().text = "Here we enter the yellow orb vectors as columns into a matrix " +
                "and use row operations to put the matrix in RREF";
            rrefPanel.gameObject.SetActive(true);
            a11.text = vector0[0].ToString();
            a12.text = vector1[0].ToString();
            a13.text = vector2[0].ToString();
            a21.text = vector0[1].ToString();
            a22.text = vector1[1].ToString();
            a23.text = vector2[1].ToString();
            a31.text = vector0[2].ToString();
            a32.text = vector1[2].ToString();
            a33.text = vector2[2].ToString();

        }

        if (dialogCount == 12)
        {
            dialog1.GetComponent<UnityEngine.UI.Text>().text = "The RREF of this matrix shows us that (0,0,0) is the only " +
                "unique solution. Therefore these orb vectors are linearly independent and Span all R3 space.";

            int[,] someMatrix = new int[,] {
            { Mathf.RoundToInt(vector0[0]), Mathf.RoundToInt(vector1[0]),  Mathf.RoundToInt(vector2[0])},
            { Mathf.RoundToInt(vector0[1]), Mathf.RoundToInt(vector1[1]), Mathf.RoundToInt(vector2[1])},
            {Mathf.RoundToInt(vector0[2]),Mathf.RoundToInt(vector1[2]),Mathf.RoundToInt(vector2[2])} };

            int[,] someRREF = new int[3, 3];
            someRREF = rref(someMatrix);


            a11.text = someRREF[0, 0].ToString();
            a12.text = someRREF[0, 1].ToString();
            a13.text = someRREF[0, 2].ToString();
            a21.text = someRREF[1, 0].ToString();
            a22.text = someRREF[1, 1].ToString();
            a23.text = someRREF[1, 2].ToString();
            a31.text = someRREF[2, 0].ToString();
            a32.text = someRREF[2, 1].ToString();
            a33.text = someRREF[2, 2].ToString();

        }

        if (dialogCount == 13)
        {
            dialog1.GetComponent<UnityEngine.UI.Text>().text = "If we wanted to chose a blue orb vector that was in the span of the yellow " +
                "orb vectors we could chose any blue orb.";
            rrefPanel.gameObject.SetActive(false);
        }

        if (dialogCount == 14)
        {
            dialog1.GetComponent<UnityEngine.UI.Text>().text = "Lets look at the third formation.";
        }

        if (dialogCount == 15)
        {
            dialog1.GetComponent<UnityEngine.UI.Text>().text = "Here the yellows orbs also have a formation of note. The span " +
                "of the yellow orb vectors forms a plane.";

            Destroy(whatToSpawnClone[0]);
            Destroy(whatToSpawnClone[1]);
            Destroy(whatToSpawnClone[2]);
            Destroy(whatToSpawnClone[3]);
            Destroy(whatToSpawnClone[4]);
            Destroy(whatToSpawnClone[5]);
            CheckPlaneSpan();

        }

        if (dialogCount == 16)
        {
            dialog1.GetComponent<UnityEngine.UI.Text>().text = "Two of the yellow orb vectors are linearly independednt and the 3rd yellow " +
                "orb vector is a linear combination of the other two.";
        }

        if (dialogCount == 17)
        {
            dialog1.GetComponent<UnityEngine.UI.Text>().text = "Enter the coordinates of the yellow orb vector that is a linearly combination " +
                "of the other two yellow orb vectors.";

            xVal.gameObject.SetActive(true);
            zVal.gameObject.SetActive(true);
            yVal.gameObject.SetActive(true);
        }

        if (dialogCount == 18)
        {
            try
            {
                int integerX = int.Parse(xVal.text);
                int integerY = int.Parse(yVal.text);
                int integerZ = int.Parse(zVal.text);

                if (integerX == vector2[0] && integerY == vector2[1] && integerZ == vector2[2])
                {
                    xVal.gameObject.SetActive(false);
                    zVal.gameObject.SetActive(false);
                    yVal.gameObject.SetActive(false);
                    dialog1.GetComponent<UnityEngine.UI.Text>().text = "That is correct!";
                }
                else
                {
                    dialog1.GetComponent<UnityEngine.UI.Text>().text = "That is NOT correct!. Try again";
                    dialogCount = dialogCount - 2;
                }
            }

            catch (Exception e)
            {
                Debug.Log("An Exception was thrown and handled");
                dialog1.GetComponent<UnityEngine.UI.Text>().text = "You have entered invalid input. Try again";
                dialogCount = dialogCount - 2;
            }

        }

        if (dialogCount == 19)
        {
            dialog1.GetComponent<UnityEngine.UI.Text>().text = "Now lets enter the blue orb that is in the span of the plane of the 3 yellow " +
                "orb vectors.";
            aVal.gameObject.SetActive(true);
            cVal.gameObject.SetActive(true);
            bVal.gameObject.SetActive(true);

        }

        if (dialogCount == 20)
        {

            try
            {
                int integerA = int.Parse(aVal.text);
                int integerB = int.Parse(bVal.text);
                int integerC = int.Parse(cVal.text);

                if (integerA == vector3[0] && integerB == vector3[1] && integerC == vector3[2])
                {
                    aVal.gameObject.SetActive(false);
                    cVal.gameObject.SetActive(false);
                    bVal.gameObject.SetActive(false);
                    dialog1.GetComponent<UnityEngine.UI.Text>().text = "That is correct";
                }
                else
                {
                    dialog1.GetComponent<UnityEngine.UI.Text>().text = "That is NOT correct. Try again";
                    dialogCount = dialogCount - 2;
                }
            }

            catch (Exception e)
            {
                Debug.Log("An Exception was thrown and handled");
                dialog1.GetComponent<UnityEngine.UI.Text>().text = "You have entered invalid input. Try again";
                dialogCount = dialogCount - 2;
            }

        }

        if (dialogCount == 21)
        {
            dialog1.GetComponent<UnityEngine.UI.Text>().text = "Congratulations, You have defeated the wizard!" + 
                "      Collect your reward!";
            Instantiate(prefab, this.transform.position, prefab.transform.rotation);
        }

        if (dialogCount == 22)
        {
            dialog1.GetComponent<UnityEngine.UI.Text>().text = "This cmd line should go back to the main menu. ";
        }

    }

    public void SpawnLocations()
    {
        // Main Orbs //
        spawnLocations[0].transform.position = zeroVector;

        spawnLocations[0].transform.Translate(Vector3.right * vector0[0]);    // x-coord
        spawnLocations[0].transform.Translate(Vector3.forward * vector0[1]);      // z-coord
        spawnLocations[0].transform.Translate(Vector3.up * vector0[2]);         // y-cord

        spawnLocations[1].transform.position = zeroVector;

        spawnLocations[1].transform.Translate(Vector3.right * vector1[0]);    // x-coord
        spawnLocations[1].transform.Translate(Vector3.forward * vector1[1]);      // y-coord
        spawnLocations[1].transform.Translate(Vector3.up * vector1[2]);         // z-cord

        spawnLocations[2].transform.position = zeroVector;

        spawnLocations[2].transform.Translate(Vector3.right * vector2[0]);    // x-coord
        spawnLocations[2].transform.Translate(Vector3.forward * vector2[1]);      // z-coord
        spawnLocations[2].transform.Translate(Vector3.up * vector2[2]);         // y-cord


        // Choice Orbs //
        spawnLocations[3].transform.position = zeroVector;

        spawnLocations[3].transform.Translate(Vector3.right * vector3[0]);    // x-coord
        spawnLocations[3].transform.Translate(Vector3.forward * vector3[1]);      // z-coord
        spawnLocations[3].transform.Translate(Vector3.up * vector3[2]);         // y-cord

        spawnLocations[4].transform.position = zeroVector;

        spawnLocations[4].transform.Translate(Vector3.right * vector4[0]);    // x-coord
        spawnLocations[4].transform.Translate(Vector3.forward * vector4[1]);      // z-coord
        spawnLocations[4].transform.Translate(Vector3.up * vector4[2]);         // y-cord

        spawnLocations[5].transform.position = zeroVector;

        spawnLocations[5].transform.Translate(Vector3.right * vector5[0]);    // x-coord
        spawnLocations[5].transform.Translate(Vector3.forward * vector5[1]);      // z-coord
        spawnLocations[5].transform.Translate(Vector3.up * vector5[2]);         // y-cord

        SpawnPrimaryOrbs();
    }


    void SpawnPrimaryOrbs()
    {
        // Main Orbs //

        whatToSpawnClone[0] = Instantiate(whatToSpawnPrefab[0], spawnLocations[0].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        whatToSpawnClone[0].tag = "first";
        spawnLocations[0].GetComponent<TextMesh>().text = "(" + vector0[0] + "," + vector0[1] + "," + vector0[2] + ")";

        whatToSpawnClone[1] = Instantiate(whatToSpawnPrefab[1], spawnLocations[1].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        whatToSpawnClone[1].tag = "second";
        spawnLocations[1].GetComponent<TextMesh>().text = "(" + vector1[0] + "," + vector1[1] + "," + vector1[2] + ")";

        whatToSpawnClone[2] = Instantiate(whatToSpawnPrefab[2], spawnLocations[2].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        whatToSpawnClone[2].tag = "third";
        spawnLocations[2].GetComponent<TextMesh>().text = "(" + vector2[0] + "," + vector2[1] + "," + vector2[2] + ")";

        // Choice Orbs //
        whatToSpawnClone[3] = Instantiate(whatToSpawnPrefab[3], spawnLocations[3].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        whatToSpawnClone[3].tag = "fourth";
        spawnLocations[3].GetComponent<TextMesh>().text = "(" + vector3[0] + "," + vector3[1] + "," + vector3[2] + ")";

        whatToSpawnClone[4] = Instantiate(whatToSpawnPrefab[4], spawnLocations[4].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        whatToSpawnClone[4].tag = "fifth";
        spawnLocations[4].GetComponent<TextMesh>().text = "(" + vector4[0] + "," + vector4[1] + "," + vector4[2] + ")";

        whatToSpawnClone[5] = Instantiate(whatToSpawnPrefab[5], spawnLocations[5].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        whatToSpawnClone[5].tag = "sixth";
        spawnLocations[5].GetComponent<TextMesh>().text = "(" + vector5[0] + "," + vector5[1] + "," + vector5[2] + ")";
    }

    public void CreateR3Span()
    {
        FormLinIndCoords();
        CreateMatrixAsCol();
        //makeMatrix();

        if (CalcDet() != 0.0 &&

            //make sure v0 doesnt equal others 
            vector0 != vector1 && vector0 != vector2 && vector0 != vector3
            && vector0 != vector4 && vector0 != vector5

            // Make sure v1 doesnt equal others
            && vector1 != vector2 && vector1 != vector3
            && vector1 != vector4 && vector1 != vector5

            // Make sure v2 doesnt equal others
            && vector2 != vector3 && vector2 != vector4
            && vector2 != vector5

            // Make sure v3 doesnt equal others
            && vector3 != vector4 && vector3 != vector5

            // Make sure v4 doesnt equal others
            && vector4 != vector5)

        // v5 comparison eliminated

        {
            SpawnLocations();
        }

        else CreateR3Span();
    }

    public void FormLinIndCoords()
    {
        // Main Orbs //
        vector0[0] = randNum.Next(minX, maxX);
        vector0[1] = randNum.Next(minY, maxY);
        vector0[2] = randNum.Next(minZ, maxZ);

        vector1[0] = randNum.Next(minX, maxX);
        vector1[1] = randNum.Next(minY, maxY);
        vector1[2] = randNum.Next(minZ, maxZ);

        vector2[0] = randNum.Next(minX, maxX);
        vector2[1] = randNum.Next(minY, maxY);
        vector2[2] = randNum.Next(minZ, maxZ);

        vector3[0] = randNum.Next(minX, maxX);
        vector3[1] = randNum.Next(minY, maxY);
        vector3[2] = randNum.Next(minZ, maxZ);

        vector4[0] = randNum.Next(minX, maxX);
        vector4[1] = randNum.Next(minY, maxY);
        vector4[2] = randNum.Next(minZ, maxZ);

        vector5[0] = randNum.Next(minX, maxX);
        vector5[1] = randNum.Next(minY, maxY);
        vector5[2] = randNum.Next(minZ, maxZ);

        // Debuging //
        Debug.Log(" vector 1 = [" + vector0[0] + "," + vector0[1] + "," + vector0[2] + "]");
        Debug.Log(" vector 2 = [" + vector1[0] + "," + vector1[1] + "," + vector1[2] + "]");
        Debug.Log(" vector 3 = [" + vector2[0] + "," + vector2[1] + "," + vector2[2] + "]");
        Debug.Log(" vector 4 = [" + vector3[0] + "," + vector3[1] + "," + vector3[2] + "]");
        Debug.Log(" vector 5 = [" + vector4[0] + "," + vector4[1] + "," + vector4[2] + "]");
        Debug.Log(" vector 6 = [" + vector5[0] + "," + vector5[1] + "," + vector5[2] + "]");

        int[,] orbVecs = new int[,] {
            { Mathf.RoundToInt(vector0[0]), Mathf.RoundToInt(vector1[0]),  Mathf.RoundToInt(vector2[0])},
            { Mathf.RoundToInt(vector0[1]), Mathf.RoundToInt(vector1[1]), Mathf.RoundToInt(vector2[1])},
            {Mathf.RoundToInt(vector0[2]),Mathf.RoundToInt(vector1[2]),Mathf.RoundToInt(vector2[2])} };

        Debug.Log("test rref dialog ");
        int rowLength = orbVecs.GetLength(0);
        int colLength = orbVecs.GetLength(1);

        for (int i = 0; i < rowLength; i++)
        {
            for (int j = 0; j < colLength; j++)
            {
                Debug.Log(string.Format("{0} ", orbVecs[i, j]));
            }
            Debug.Log(Environment.NewLine + Environment.NewLine);
        }
        int[,] myRREF = new int[3, 3];
        myRREF = rref(orbVecs);

        for (int i = 0; i < rowLength; i++)
        {
            for (int j = 0; j < colLength; j++)
            {
                Debug.Log(string.Format("{0} ", myRREF[i, j]));
            }
            Debug.Log(Environment.NewLine + Environment.NewLine);
        }

    }


    // Matrix Form of Vectors //
    public void CreateMatrixAsCol()
    {
        myMatrix = DenseMatrix.OfArray(new double[,] {
        {vector0[0],vector1[0],vector2[0]},
        {vector0[1],vector1[1],vector2[1]},
        {vector0[2],vector1[2],vector2[2]}});
        //Vector<double>[] nullspace = myMatrix.Kernel();
    }

    public Double CalcDet()
    {
        double det = myMatrix.Determinant();

        Debug.Log("the determinent = " + det);

        return det;
    }



    public void CreateLineSpan()
    {
        // The origin  = (0,0,0) transoformed to location of the wizard//


        // direction vector //      
        int lineGenX = randNum.Next(1, 4); //x coord
        int lineGenZ = randNum.Next(1, 4);  //z coord
        int lineGenY = randNum.Next(1, 4);  //y coord

        vector3[0] = lineGenX;
        vector3[1] = lineGenZ;
        vector3[2] = lineGenY;

        // Unit Vector Equation
        double unitVectorX = (lineGenX / (Math.Sqrt(Math.Pow(lineGenX, 2) + Math.Pow(lineGenZ, 2) + Math.Pow(lineGenY, 2))));
        double unitVectorZ = (lineGenZ / (Math.Sqrt(Math.Pow(lineGenX, 2) + Math.Pow(lineGenZ, 2) + Math.Pow(lineGenY, 2))));
        double unitVectorY = (lineGenY / (Math.Sqrt(Math.Pow(lineGenX, 2) + Math.Pow(lineGenZ, 2) + Math.Pow(lineGenY, 2))));

        // int linScalar1 = randNum.Next(1, 3);
        // int linScalar0 = randNum.Next(1, 3);

        vector0[0] = Convert.ToSingle(unitVectorX);
        vector0[1] = Convert.ToSingle(unitVectorZ);
        vector0[2] = Convert.ToSingle(unitVectorY);

        vector1[0] = 2 * vector0[0];
        vector1[1] = 2 * vector0[1];
        vector1[2] = 2 * vector0[2];

        vector2[0] = 3 * vector0[0];
        vector2[1] = 3 * vector0[1];
        vector2[2] = 3 * vector0[2];

        vector4[0] = randNum.Next(1, 4);
        vector4[1] = randNum.Next(1, 4);
        vector4[2] = randNum.Next(1, 4);

        vector5[0] = randNum.Next(1, 4);
        vector5[1] = randNum.Next(1, 4);
        vector5[2] = randNum.Next(1, 4);

        // Create Mathematical Computational Form
        myMatrix = DenseMatrix.OfArray(new double[,] {
        {vector0[0],vector1[0],vector2[0]},
        {vector0[1],vector1[1],vector2[1]},
        {vector0[2],vector1[2],vector2[2]}});

        // Debuging //
        Debug.Log(" vector 1 = [" + vector0[0] + "," + vector0[1] + "," + vector0[2] + "]");
        Debug.Log(" vector 2 = [" + vector1[0] + "," + vector1[1] + "," + vector1[2] + "]");
        Debug.Log(" vector 3 = [" + vector2[0] + "," + vector2[1] + "," + vector2[2] + "]");
        Debug.Log(" vector 4 = [" + vector3[0] + "," + vector3[1] + "," + vector3[2] + "]");
        Debug.Log(" vector 5 = [" + vector4[0] + "," + vector4[1] + "," + vector4[2] + "]");
        Debug.Log(" vector 6 = [" + vector5[0] + "," + vector5[1] + "," + vector5[2] + "]");

        //SpawnLocations();
    }

    public void CheckPlaneSpan()
    {
        CreatePlaneSpan();
        if ( //make sure v0 doesnt equal others 
            vector0 != vector1 && vector0 != vector2 && vector0 != vector3
            && vector0 != vector4 && vector0 != vector5

            // Make sure v1 doesnt equal others
            && vector1 != vector2 && vector1 != vector3
            && vector1 != vector4 && vector1 != vector5

            // Make sure v2 doesnt equal others
            && vector2 != vector3 && vector2 != vector4
            && vector2 != vector5

            // Make sure v3 doesnt equal others
            && vector3 != vector4 && vector3 != vector5

            // Make sure v4 doesnt equal others
            && vector4 != vector5)

        // v5 comparison eliminated

        {
            SpawnLocations();
        }
        else
        {
            CheckPlaneSpan();
        }
    }


    public void CheckLineSpan()
    {
        CreateLineSpan();
        if ( //make sure v0 doesnt equal others 
            vector0 != vector1 && vector0 != vector2 && vector0 != vector3
            && vector0 != vector4 && vector0 != vector5

            // Make sure v1 doesnt equal others
            && vector1 != vector2 && vector1 != vector3
            && vector1 != vector4 && vector1 != vector5

            // Make sure v2 doesnt equal others
            && vector2 != vector3 && vector2 != vector4
            && vector2 != vector5

            // Make sure v3 doesnt equal others
            && vector3 != vector4 && vector3 != vector5

            // Make sure v4 doesnt equal others
            && vector4 != vector5)

        // v5 comparison eliminated

        {
            SpawnLocations();
        }
        else
        {

            CheckLineSpan();
        }
    }



    public void CreatePlaneSpan()
    {
        // Using 2 of 3 of the basis of R^3 for 2 linearly independent cases and creating an arbitrary to ensure student learning of SPAN

        // create vector 0 off basis
        vector0[0] = 1;
        vector0[1] = 1;
        vector0[2] = 0;

        // create vector 1 off basis
        vector1[0] = 0;
        vector1[1] = 1;
        vector1[2] = 1;

        // Create Randomness for Vectors
        int randScalar0 = randNum.Next(1, 4);
        int randScalar1 = randNum.Next(1, 4);
        int randScalar2 = randNum.Next(1, 4);

        int randScalarA = randNum.Next(1, 4);
        int randScalarB = randNum.Next(1, 4);
        int randScalarC = randNum.Next(1, 4);

        vector0[0] = randScalar0 * vector0[0];
        vector0[1] = randScalar1 * vector0[1];
        vector0[2] = randScalar2 * vector0[2];

        vector1[0] = randScalarA * vector1[0];
        vector1[1] = randScalarB * vector1[1];
        vector1[2] = randScalarC * vector1[2];

        // create arbitrary vector 2 from linear combination of vector 0 and vector 1
        vector2[0] = vector0[0] + vector1[0];
        vector2[1] = vector0[1] + vector1[1];
        vector2[2] = vector0[2] + vector1[2];

        vector3[0] = vector0[0] + vector1[0] + vector2[0];
        vector3[1] = vector0[1] + vector1[1] + vector2[0];
        vector3[2] = vector0[2] + vector1[2] + vector2[0];

        vector4[0] = vector0[0];
        vector4[1] = vector0[1];
        vector4[2] = vector0[2] + 1;

        vector5[0] = vector1[0] + 1;
        vector5[1] = vector2[1];
        vector5[2] = vector2[2];

        // Create Mathematical Computational Form
        myMatrix = DenseMatrix.OfArray(new double[,] {
        {vector0[0],vector1[0],vector2[0]},
        {vector0[1],vector1[1],vector2[1]},
        {vector0[2],vector1[2],vector2[2]}});

        // Debuging //
        Debug.Log(" vector 1 = [" + vector0[0] + "," + vector0[1] + "," + vector0[2] + "]");
        Debug.Log(" vector 2 = [" + vector1[0] + "," + vector1[1] + "," + vector1[2] + "]");
        Debug.Log(" vector 3 = [" + vector2[0] + "," + vector2[1] + "," + vector2[2] + "]");
        Debug.Log(" vector 4 = [" + vector3[0] + "," + vector3[1] + "," + vector3[2] + "]");
        Debug.Log(" vector 5 = [" + vector4[0] + "," + vector4[1] + "," + vector4[2] + "]");
        Debug.Log(" vector 6 = [" + vector5[0] + "," + vector5[1] + "," + vector5[2] + "]");

    }


    /* Algorithm for RREF calculating from 
     * https://rosettacode.org/wiki/Reduced_row_echelon_form
     * Used for calculating RREF of matrices.
     */
    private static int[,] rref(int[,] matrix)
    {
        int lead = 0, rowCount = matrix.GetLength(0), columnCount = matrix.GetLength(1);
        for (int r = 0; r < rowCount; r++)
        {
            if (columnCount <= lead) break;
            int i = r;
            while (matrix[i, lead] == 0)
            {
                i++;
                if (i == rowCount)
                {
                    i = r;
                    lead++;
                    if (columnCount == lead)
                    {
                        lead--;
                        break;
                    }
                }
            }
            for (int j = 0; j < columnCount; j++)
            {
                int temp = matrix[r, j];
                matrix[r, j] = matrix[i, j];
                matrix[i, j] = temp;
            }
            int div = matrix[r, lead];
            if (div != 0)
                for (int j = 0; j < columnCount; j++) matrix[r, j] /= div;
            for (int j = 0; j < rowCount; j++)
            {
                if (j != r)
                {
                    int sub = matrix[j, lead];
                    for (int k = 0; k < columnCount; k++) matrix[j, k] -= (sub * matrix[r, k]);
                }
            }
            lead++;
        }
        return matrix;
    }
}