using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MPTools
{
    //helper to build text string in R2
    public static string GetPositionText(int p1, int p2)
    {
        return "(" + p1.ToString() + ", " + p2.ToString() + ")";
    }

    //helper to build text string in R3
    public static string GetPositionText(int p1, int p2, int p3)
    {
        return "(" + p1.ToString() + ", " + p2.ToString() + ", " + p3.ToString() + ")";
    }

    //helper to build text string in Matrix format
    public static string GetTransformText(int[] set)
    {
        string result = "";
        int size = (int)Mathf.Sqrt(set.Length);

        for (int i = 0; i < set.Length; i++)
        {
            if ((i + 1) % size == 0)
            {
                result += (set[i] + "\n");
            }
            else
            {
                result += (set[i] + "  ");
            }
        }

        return result;
    }

    //helper to easily find child component by name
    public static Component FindObjectInChildren(GameObject parent, string name)
    {
        Component[] children = parent.GetComponentsInChildren<Component>();
        Component result = null;

        foreach (Component child in children)
        {
            if (child.name == name)
            {
                result = child;
                break;
            }
        }

        return result;
    }
}
