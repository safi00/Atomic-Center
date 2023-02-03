using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodesController : MonoBehaviour
{
    [HideInInspector] public Transform[] ChildObjects;
    [HideInInspector] public NodesType[] ChildObjectTypes;
    [SerializeField]  public List<Transform> ChildNodeList = new List<Transform>();
    [SerializeField]  public List<int> ChildNodeIDList = new List<int>();
    [SerializeField]  public List<NodeEventType> ChildNodeTypeList = new List<NodeEventType>();
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
    /// <summary>
    /// This method fills in each tile and add them together to become route
    /// </summary>
    private void FillNodes() 
    {
        ClearNodeList();

        ChildObjects = GetComponentsInChildren<Transform>();
        ChildObjectTypes = GetComponentsInChildren<NodesType>();

        foreach (Transform Child in ChildObjects)
        {
            if (Child != this.transform)
            {
                ChildNodeList.Add(Child);
            }
        }

        foreach (NodesType Child in ChildObjectTypes)
        {
            if (Child != this.transform)
            {
                ChildNodeIDList.Add(Child.MyNodeID);
                ChildNodeTypeList.Add(Child.MyNodeType);
            }
        }
    }
    private void ClearNodeList()
    {
        ChildNodeList.Clear();
        ChildNodeIDList.Clear();
        ChildNodeTypeList.Clear();
    }
    public enum NodeEventType
    {
        Green,
        White,
        Purple,
        Aquamarine,
        Red,
    }
}
