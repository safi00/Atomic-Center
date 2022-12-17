using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] public GameObject mainMenuWindow;
    [SerializeField] public GameObject optionPanel;

    // Start is called before the first frame update
    void Start()
    {
        optionPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {        
    }
    public void StartGame()
    {
        Loader.Load(Loader.Scene.GameScene);
    }
    public void ShowOptions()
    {
        optionPanel.SetActive(true);
    }
    public void HideOptions()
    {
        optionPanel.SetActive(false);
    }
    public void CloseGame()
    {
        Application.Quit();
    }
}
