using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodesController : MonoBehaviour
{
    [SerializeField] public Transform[] ChildObjects;
    [SerializeField] public List<Transform> ChildNodeList = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {        
    }
    // Update is called once per frame
    void Update()
    {        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;


    }
    private void FillNodes() 
    {
        ChildNodeList.Clear();
        foreach (Transform t in ChildObjects) { }
    }
}
