using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("Main UI Windows")]
    [SerializeField] private GameObject MainMenuWindow;
    [SerializeField] private GameObject GameSetupWindow;
    [SerializeField] private GameObject OptionPanel;
    [SerializeField] private GameObject TurnPointUIWindow;
    [SerializeField] private GameObject CustomUIWindow;
    [SerializeField] private GameObject MapPlayerUIWindow;

    [Header("Game Setup UI Buttons")]
    [SerializeField] private GameObject NextButton;
    [SerializeField] private GameObject HomeButton;
    [SerializeField] private GameObject CustomTurnButton;
    [SerializeField] private GameObject CustomPointButton;
    [SerializeField] private GameObject NoLimitTurnButton;
    [SerializeField] private GameObject NoLimitPointButton;

    [Header("Game Setup UI Texts")]
    [SerializeField] private Text PlayersDisplayText;
    [SerializeField] private Text TurnsDisplayText;
    [SerializeField] private Text PointsDisplayText;

    [Header("Misc")]
    [SerializeField] private bool isGameSetupReady;
    [SerializeField] private bool NoLimitCheck;
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
    public void BackToMainMenu()
    {
        GameSetupWindow.SetActive(false);
        MainMenuWindow.SetActive(true);
        GameSetupStats.reset();
    }
    public void ToGameSetupUI()
    {
        MainMenuWindow.SetActive(false);
        GameSetupWindow.SetActive(true);
    }
    public void ToMapSelectUI()
    {
        TurnPointUIWindow.SetActive(false);
        MapPlayerUIWindow.SetActive(true);
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
                PlayersDisplayText.text = "-";
                break;
            case > 0:
                PlayersDisplayText.text = "" + GameSetupStats.GetPlayerAmount();
                break;
        }

        switch (GameSetupStats.GetTurnLimit())
        {
            case < 1:
                TurnsDisplayText.text = "-";
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
                PointsDisplayText.text = "-";
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
                NextButton.GetComponent<Button>().interactable = true;
                break;
            case false:
                NextButton.GetComponent<Button>().interactable = false;
                break;
        }
    }
    public void SetPlayersToOne()
    {
        GameSetupStats.SetPlayerAmount(1);
    }
    public void SetPlayersToTwo()
    {
        GameSetupStats.SetPlayerAmount(2);
    }
    public void SetPlayersToThree()
    {
        GameSetupStats.SetPlayerAmount(3);
    }
    public void SetPlayersToFour()
    {
        GameSetupStats.SetPlayerAmount(4);
    }
    public void SetPlayersToFive()
    {
        GameSetupStats.SetPlayerAmount(5);
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
        else
        {
            isGameSetupReady = false;
        }
    }
}
