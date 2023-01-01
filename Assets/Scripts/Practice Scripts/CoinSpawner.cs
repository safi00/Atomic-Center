using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    // Update is called once per frame
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
            MoveCoin();
        }
    }

    private void MoveCoin()
    {
        int randomNM1 = ((Random.Range(0, 8) * 5) + 5);
        int randomNM2 = ((Random.Range(0, 8) * 5) + 5);
        transform.TransformVector(new Vector3(randomNM1, 0.9f, randomNM2));
    }
}
