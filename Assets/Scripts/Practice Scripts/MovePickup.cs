using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePickup : MonoBehaviour
{
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            moveCoin(true);
        }
    }

    private void updatePos()
    {

    }

    private void moveCoin(bool eaten)
    {

        if (eaten)
        {
         //   int randomNM1 = ((Random.Range(0, 8) * 5) + 5);
         //   int randomNM2 = ((Random.Range(0, 8) * 5) + 5);
        }
        else
        {
            
        }
        int randomNM1 = ((Random.Range(0, 8) * 5) + 5);
        int randomNM2 = ((Random.Range(0, 8) * 5) + 5);
        //rb.gameObject.transform.SetPositionAndRotation(new Vector3(randomNM1, 0.9f, randomNM2), rotation);
    }   
}
