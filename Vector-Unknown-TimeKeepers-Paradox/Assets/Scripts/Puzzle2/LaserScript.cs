using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    public Transform startPoint;
    public int hitDamage = 1;
    private LineRenderer laserLine;
    private ParticleSystem emitter;
    public ParticleSystem newPartSyst;
    private bool hitting;

    public GameObject pickup;

    //used in place of start since parent object pulled from resources at runtime
    //using start causes laserLine to not get set before first call to function that references.
    public void Setup()
    {
        Vector3 startRef = startPoint.position;

        laserLine = GetComponent<LineRenderer>();
        laserLine.SetWidth(0.2F, 0.2F);

        laserLine.SetPosition(0, startPoint.position);
        laserLine.SetPosition(1, new Vector3(startRef.x, 0, startRef.z)); //point it at the ground
        startPoint.LookAt(new Vector3(startRef.x, 0, startRef.z));
        laserLine.positionCount = 2;

        emitter = this.transform.parent.GetComponentInChildren<ParticleSystem>();
        emitter.transform.position = startPoint.position; //align ob with parent
    }


    // Update is called once per frame
    void Update()
    {
        Vector3[] positions = new Vector3[laserLine.positionCount];
        Vector3 start, end;
        RaycastHit hit;
        Ray lineRay;
        Collider ob;

        laserLine.GetPositions(positions);

        //constantly check for raycast based collisions
        for (int i = 0; i < positions.Length; i++)
        {
            start = positions[i];

            if (i + 1 >= positions.Length)
            {
                break;
            }
            else
            {
                end = positions[i + 1];
            }

            lineRay = new Ray(start, GetDirection(start, end));

            if (Physics.Raycast(lineRay, out hit))
            {
                ob = hit.collider;

                if (ob.name.Contains("SimpleMirror") && !PositionInLine(ob.transform.position))
                { //bounce condition
                    ob.GetComponent<InteractOnOff>().LineHit(true);
                    BounceLine(ob.transform.position, ob.GetComponent<Transformer>().TransformedVector());
                    PassEmitter(ob.transform, ob.transform.position, ob.transform.forward * 25);
                    //ReEvaluteEmitter();
                }
                //else if (ob.name.Contains("Player") && !hitting)
                //{ //hitting player
                //    hitting = true; //reset on lateupdate
                //    ob.GetComponent<PlayerHealth>().TakeDamage(hitDamage);
                //}
                else if (ob.name.Contains("Wizard"))
                { //win condition
                  //********************************************************************************************
                  //add new win script here 

                    Instantiate(pickup, ob.transform.position, ob.transform.rotation);
                    Destroy(ob.gameObject);

                    //old win script
                    //GameObject.Find("MirrorPuzzle").GetComponent<MirrorPuzzle>().Win();
                }
            }
        }
    }

    //needed to slow down collision hit, without, drains health too fast
    void LateUpdate()
    {
        if (hitting)
        {
            hitting = false;
        }
    }

    

    //copy emitter to next line segment
    public void PassEmitter(Transform copyTo, Vector3 start, Vector3 end)
    {
        GameObject container = new GameObject();
        GameObject newPs = Resources.Load<GameObject>("Assets/Models/Prefabs/Puzzle2/LaserParticle");
        ParticleSystem newPsEmitter = newPartSyst.GetComponent<ParticleSystem>();

        if (copyTo.GetComponentInChildren<ParticleSystem>() == null)
        {
            return;
        }

        container.name = "MirrorBox";
        container.tag = "MirrorBox";

        newPs.transform.position = start; //set start to parent;
        newPs.transform.LookAt(end); //orient face
        newPs.transform.rotation.SetLookRotation(end); //orient rotation

        newPsEmitter.startLifetime = Vector3.Distance(newPsEmitter.transform.position, end) / newPsEmitter.startSpeed;

        container = Instantiate(container, copyTo.transform.parent, true);
        copyTo.transform.parent = container.transform;
        newPs = Instantiate(newPs, copyTo.transform.parent, true);
        newPsEmitter.Play();
    }

    //adjust particle emitter's rate by distance it needs to travel
    //public void ReEvaluteEmitter()
    //{
    //    GameObject[] boxes = GameObject.FindGameObjectsWithTag("MirrorBox");
    //    ParticleSystem tempEmit = null;
    //    Vector3 start = Vector3.zero, end = Vector3.zero;
    //    Vector3[] positions = new Vector3[laserLine.positionCount];
    //    GameObject mirror;

    //    if (boxes.Length <= 2)
    //    {
    //        return;
    //    }

    //    laserLine.GetPositions(positions);

    //    for (int i = 0; i < boxes.Length; i++)
    //    {
    //        tempEmit = boxes[i].GetComponentInChildren<ParticleSystem>();
    //        mirror = MPTools.FindObjectInChildren(boxes[i], "SimpleMirror").gameObject;

    //        if (mirror != null)
    //        {
    //            for (int j = 0; j < positions.Length - 1; j++)
    //            {
    //                if (Vector3.Distance(mirror.transform.position, positions[j]) <= 5)
    //                {
    //                    start = positions[j];
    //                    end = positions[j + 1];
    //                    break;
    //                }
    //            }
    //        }

    //        if (start != Vector3.zero && end != Vector3.zero && tempEmit != null)
    //        {
    //            tempEmit.startLifetime = Vector3.Distance(start, end) / tempEmit.startSpeed;
    //        }
    //    }
    //}

    //point the laser source towards the first object it's attracted to
    public void SetStartDirection(Transform towards)
    {
        float distance;
        Vector3 endPoint;

        startPoint.LookAt(towards);
        distance = Vector3.Distance(towards.position, startPoint.transform.position);
        emitter.startLifetime = Vector3.Distance(emitter.transform.position, towards.position) / emitter.startSpeed;

        endPoint = startPoint.position + (startPoint.forward * 20);
        laserLine.SetPosition(0, startPoint.position);
        laserLine.SetPosition(1, endPoint);
        emitter.Play();
    }

    //get line direction as direction unit vectory
    private Vector3 GetDirection(Vector3 start, Vector3 end)
    {
        return (end - start).normalized;
    }

    //check if position is somewhere on line
    private bool PositionInLine(Vector3 lookFor)
    {
        bool result = false;
        Vector3[] positions = new Vector3[laserLine.positionCount];

        laserLine.GetPositions(positions);

        foreach (Vector3 pos in positions)
        {
            if (pos == lookFor)
            {
                result = true;
                break;
            }
        }

        return result;
    }

    //direct line towards position
    public void Attract(GameObject ob)
    {
        if (laserLine.positionCount == 2)
        { //start case
            SetStartDirection(ob.transform);
        }
        else
        {
            BounceLine(ob.transform.position, ob.transform.forward * 25);
        }
    }

    //break attraction
    public void Detract()
    {
        Setup();
        InteractOnOff.TurnAllOff();
    }

    //change line direction
    private void BounceLine(Vector3 hitObPos, Vector3 newEnd)
    {
        int currPositions = laserLine.positionCount;
        Vector3[] positions = new Vector3[currPositions];

        Debug.Log(newEnd);

        laserLine.GetPositions(positions);
        laserLine.positionCount = currPositions + 1;
        laserLine.SetPosition(positions.Length - 1, hitObPos);
        laserLine.SetPosition(positions.Length, newEnd);
    }
}
