using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingDisplay : MonoBehaviour
{
    [Header("Loading Screen Text")]
    [SerializeField] public Text LoadingText;
    [SerializeField] public WaitForSeconds Delay = new WaitForSeconds(0.5f);
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DynamicText());
    }
    // Update is called once per frame
    void Update()
    {
    }
    private void LateUpdate()
    {
    }
    IEnumerator DynamicText()
    {
        while (true)
        {
            for (int i = 0; i <= 3; i++)
            {
                switch (i)
                {
                    case 0:
                        LoadingText.text = "loading";
                        break;
                    case 1:
                        LoadingText.text = "loading.";
                        break;
                    case 2:
                        LoadingText.text = "loading..";
                        break;
                    case 3:
                        LoadingText.text = "loading...";
                        break; 
                }
                yield return Delay;
            }
        }

    }
}
