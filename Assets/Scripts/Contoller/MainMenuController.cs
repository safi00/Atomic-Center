using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("Main UI Windows")]
    [SerializeField] public GameObject MainMenuWindow;
    [SerializeField] public GameObject GameSetupWindow;
    [SerializeField] public GameObject OptionPanel;

    [Header("Game Setup UI Buttons")]
    [SerializeField] public GameObject StartButton;
    [SerializeField] public GameObject HomeButton;
    [SerializeField] public GameObject NoLimitTurnButton;
    [SerializeField] public GameObject NoLimitPointButton;

    [Header("Game Setup UI Texts")]
    [SerializeField] public GameObject PlayersDisplayText;
    [SerializeField] public GameObject TurnsDisplayText;
    [SerializeField] public GameObject PointsDisplayText;

    [Header("Misc")]
    [SerializeField] public bool isGameSetupReady;
    // Start is called before the first frame update
    void Start()
    {
        OptionPanel.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {        
    }
    public void StartGame()
    {
        Loader.Load(Loader.Scene.Map1);
    }
    public void ShowOptions()
    {
        OptionPanel.SetActive(true);
    }
    public void HideOptions()
    {
        OptionPanel.SetActive(false);
    }
    public void CloseGame()
    {
        Application.Quit();
    }

    public void SetPointToOne()
    {
        GameSetupStats.SetPlayers(1);
    }
    public void SetPointToTwo()
    {
        GameSetupStats.SetPlayers(2);
    }
    public void SetPointToThree()
    {
        GameSetupStats.SetPlayers(3);
    }
    public void SetPointToFour()
    {
        GameSetupStats.SetPlayers(4);
    }
    public void SetPointToFive()
    {
        GameSetupStats.SetPlayers(5);
    }
}
