using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentElementButtons : MonoBehaviour
{
    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<ElementsButtonController>().MyElementNum = i + 1;
        }
    }
}
