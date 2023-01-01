using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttoncollision : MonoBehaviour
{
    public GameObject[] collectables;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < collectables.Length; i++)
            {
                collectables[i].SetActive(true);
            }
        }
    }
}
