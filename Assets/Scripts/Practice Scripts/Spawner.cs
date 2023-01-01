using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public GameObject[] random;
    public bool spawnSwitch;
    // Start is called before the first frame update
    void Start()
    {
        spawnSwitch = true;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(collectSpawner());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            spawnSwitch = !spawnSwitch;
        }
    }
    IEnumerator collectSpawner() 
    {
        while(spawnSwitch)
        {
            int randomNM = Random.Range(0, random.Length);
            int randomNM1 = Random.Range(0, 20);
            int randomNM2 = Random.Range(0, 20);
            GameObject g = Instantiate(random[randomNM], new Vector3(randomNM1, 0.5f, randomNM2), transform.rotation);

            yield return new WaitForSeconds(1f);
        }
    }
}
