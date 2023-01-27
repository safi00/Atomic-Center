using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodesType : MonoBehaviour
{
    [HideInInspector] public int MyNodeID;
    [SerializeField] public NodesController.NodeEventType MyNodeType;
}