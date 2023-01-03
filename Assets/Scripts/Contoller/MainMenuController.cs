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
    [SerializeField] public GameObject CustomUIWindow;

    [Header("Game Setup UI Buttons")]
    [SerializeField] public GameObject StartButton;
    [SerializeField] public GameObject HomeButton;
    [SerializeField] public GameObject CustomTurnButton;
    [SerializeField] public GameObject CustomPointButton;
    [SerializeField] public GameObject NoLimitTurnButton;
    [SerializeField] public GameObject NoLimitPointButton;

    [Header("Game Setup UI Texts")]
    [SerializeField] public Text PlayersDisplayText;
    [SerializeField] public Text TurnsDisplayText;
    [SerializeField] public Text PointsDisplayText;

    [Header("Misc")]
    [SerializeField] public bool isGameSetupReady;
    [SerializeField] public bool NoLimitCheck;
    // Start is called before the first frame update
    void Start()
    {
        isGameSetupReady = false;
        OptionPanel.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        TextCheck();
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

    public void TextCheck()
    {
        ButtonCheck();
        switch (GameSetupStats.GetPlayerAmount())
        {
            case < 1:
                break;
            case > 0:
                PlayersDisplayText.text = "" + GameSetupStats.GetPlayerAmount();
                break;
        }

        switch (GameSetupStats.GetTurnLimit())
        {
            case < 1:
                break;
            case < 100:
                TurnsDisplayText.text = "" + GameSetupStats.GetTurnLimit();
                NoLimitPointButton.GetComponent<Button>().interactable = true;
                break;
            case > 99:
                TurnsDisplayText.text = "NO LIMIT";
                NoLimitPointButton.GetComponent<Button>().interactable = false;
                break;
        }

        switch (GameSetupStats.GetPointLimit())
        {
            case < 1:
                break;
            case < 100:
                PointsDisplayText.text = "" + GameSetupStats.GetPointLimit();
                NoLimitTurnButton.GetComponent<Button>().interactable = true;
                break;
            case > 99:
                PointsDisplayText.text = "NO LIMIT";
                NoLimitTurnButton.GetComponent<Button>().interactable = false;
                break;
        }

        switch (isGameSetupReady)
        {
            case true:
                StartButton.GetComponent<Button>().interactable = true;
                break;
            case false:
                StartButton.GetComponent<Button>().interactable = false;
                break;
        }
    }
    public void SetPlayersToOne()
    {
        GameSetupStats.SetPlayers(1);
    }
    public void SetPlayersToTwo()
    {
        GameSetupStats.SetPlayers(2);
    }
    public void SetPlayersToThree()
    {
        GameSetupStats.SetPlayers(3);
    }
    public void SetPlayersToFour()
    {
        GameSetupStats.SetPlayers(4);
    }
    public void SetPlayersToFive()
    {
        GameSetupStats.SetPlayers(5);
    }
    public void SetTurnsToTen()
    {
        GameSetupStats.SetTurnLimit(10);
    }
    public void SetTurnsToTwentyFive()
    {
        GameSetupStats.SetTurnLimit(25);
    }
    public void SetTurnsToFifty()
    {
        GameSetupStats.SetTurnLimit(50);
    }
    public void SetTurnsToCustom()
    {
        Customwindow.SetupTheCustom(1);
        CustomUIWindow.SetActive(true);
    }
    public void SetTurnsToUnlimited()
    {
        NoLimitPointButton.GetComponent<Button>().interactable = false;
        GameSetupStats.TurnOnNoTurnLimitSwitch();
        GameSetupStats.SetTurnLimit(1000);
    }
    public void SetPointsToTen()
    {
        GameSetupStats.SetPointLimit(10);
    }
    public void SetPointsToTwentyfive()
    {
        GameSetupStats.SetPointLimit(25);
    }
    public void SetPointsToFifty()
    {
        GameSetupStats.SetPointLimit(50);
    }
    public void SetPointsToCustom()
    {
        Customwindow.SetupTheCustom(2);
        CustomUIWindow.SetActive(true);
    }
    public void SetPointsToUnlimited()
    {
        NoLimitTurnButton.GetComponent<Button>().interactable = false;
        GameSetupStats.TurnOnNoPointLimitSwitch();
        GameSetupStats.SetPointLimit(1000);
    }
    public void ButtonCheck()
    {
        if (GameSetupStats.GetPlayerAmount() > 0 && GameSetupStats.GetTurnLimit() > 0 && GameSetupStats.GetPointLimit() > 0)
        {
            isGameSetupReady = true;
        }
    }
}
