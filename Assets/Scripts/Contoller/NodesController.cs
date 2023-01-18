using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodesController : MonoBehaviour
{
    [HideInInspector] public Transform[] ChildObjects;
    [SerializeField]  public List<Transform> ChildNodeList = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        FillNodes();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        for (int i = 0; i < ChildNodeList.Count; i++)
        {
            Vector3 CurrentNode = ChildNodeList[i].position;
            if (i>0)
            {
                Vector3 PrevNode = ChildNodeList[i - 1].position;
                Gizmos.DrawLine(PrevNode, CurrentNode);
            }
        }
    }
    private void FillNodes() 
    {
        ChildNodeList.Clear();
        ChildObjects = GetComponentsInChildren<Transform>();
        foreach (Transform Child in ChildObjects) 
        {
            if (Child != this.transform)
            {
                ChildNodeList.Add(Child);
            }
        }
    }
}
