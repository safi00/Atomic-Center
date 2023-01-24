using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUIController : MonoBehaviour
{
    [HideInInspector] public Transform[] ChildObjects;
    [SerializeField] public List<Transform> ChildUIList = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        FillObjects();
    }
    private void FillObjects()
    {
        ChildUIList.Clear();
        ChildObjects = GetComponentsInChildren<Transform>();
        foreach (Transform Child in ChildObjects)
        {
            if (Child != this.transform)
            {
                ChildUIList.Add(Child);
            }
        }
    }
}