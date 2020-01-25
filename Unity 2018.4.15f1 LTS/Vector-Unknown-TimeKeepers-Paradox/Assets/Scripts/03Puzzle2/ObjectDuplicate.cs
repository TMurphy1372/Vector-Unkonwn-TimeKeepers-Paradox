using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDuplicate
{
    public static GameObject[] Duplicate(GameObject parent, GameObject toDup, int duplicateTo)
    {
        GameObject[] result = new GameObject[duplicateTo];

        result[0] = toDup; //pre collect original object

        for (int i = 1; i < duplicateTo; i++)
        { //1 to account for original object;
            result[i] = GameObject.Instantiate(toDup, parent.transform, true);
            result[i].name = toDup.name + i;
        }

        return result;
    }
}
