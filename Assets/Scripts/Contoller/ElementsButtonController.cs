using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementsButtonController : MonoBehaviour
{
    [HideInInspector] public int MyElementNum;
    public void GetMyElementNum() 
    { 
        HintsController.MyChosenElement = MyElementNum;
    }    
}
