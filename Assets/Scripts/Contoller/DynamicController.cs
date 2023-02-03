using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameSetupStats;

public class DynamicController : MonoBehaviour
{
    [Header("Big UIs")]
    [SerializeField] public GameObject VisualsUI;
    [SerializeField] public GameObject DisplayPowerUI;

    [SerializeField] public Text PlayerButton1Text;
    [SerializeField] public Text PlayerButton2Text;
    [SerializeField] public Text PlayerButton3Text;
    [SerializeField] public Text PlayerButton4Text;

    [Header("Buttons")]
    [SerializeField] public GameObject PlusThreeButton;
    [SerializeField] public GameObject PlusDSixButton;
    [SerializeField] public GameObject MinusThreeButton;
    [SerializeField] public GameObject MinusDSixButton;

    [SerializeField] public GameObject PlayerButtonList;

    [SerializeField] public GameObject PlayerButton1;
    [SerializeField] public GameObject PlayerButton2;
    [SerializeField] public GameObject PlayerButton3;
    [SerializeField] public GameObject PlayerButton4;

    [SerializeField] public GameObject LockInButton;

    [Header("Event")]
    [SerializeField] public GameObject EndTurnEvent;

    [Header("Player Stats")]
    [HideInInspector] public List<PlayerMovement> AllPlayers;
    [HideInInspector] public PlayerMovement CurrentPlayer;

    [SerializeField] public List<PlayerMovement> AllPlayersExclCurrentPlayer;
    [HideInInspector] public PlayerMovement Player1;
    [HideInInspector] public PlayerMovement Player2;
    [HideInInspector] public PlayerMovement Player3;
    [HideInInspector] public PlayerMovement Player4;

    [Header("Misc")]
    [SerializeField] public int ButtonAvailable = 0;
    [SerializeField] public PlayerMovement TheChosenPlayer;
    [SerializeField] public BuffsAndDebuffs TheChosenDynamic;

    // Start is called before the first frame update
    void Start()
    {        
    }
    // Update is called once per frame
    void Update()
    {
    }
    private void Setup()
    {
        ButtonAvailable = 0;
        LockInButton.SetActive(false);
        ActivateAllPlayerButtons();
        FillInPlayerNames();
        DeactivateAllPlayerButtons();
    }
    public void DynamicPOP()
    {
        ActivateAllDynamicButtons();
        VisualsUI.SetActive(true);
        DisplayPowerUI.SetActive(true);
        Setup();
    }
    private void ActivateAllDynamicButtons()
    {
        PlusThreeButton.GetComponent<Button>().interactable = true;
        PlusDSixButton.GetComponent<Button>().interactable = true;
        switch (AllPlayers.Count)
        {
            case <=1:
                MinusThreeButton.GetComponent<Button>().interactable = false;
                MinusDSixButton.GetComponent<Button>().interactable = false;
                break;
            case >=2:
                MinusThreeButton.GetComponent<Button>().interactable = true;
                MinusDSixButton.GetComponent<Button>().interactable = true;
                break;
        }
    }
    private void ActivateAllPlayerButtons()
    {
        switch (AllPlayers.Count)
        {
            case <=1:
                break;
            case 2:
                PlayerButton1.SetActive(true);
                PlayerButton1.GetComponent<Button>().interactable = true;
                break;
            case 3:
                PlayerButton1.SetActive(true);
                PlayerButton2.SetActive(true);
                PlayerButton1.GetComponent<Button>().interactable = true;
                PlayerButton2.GetComponent<Button>().interactable = true;
                break;
            case 4:
                PlayerButton1.SetActive(true);
                PlayerButton2.SetActive(true);
                PlayerButton3.SetActive(true);
                PlayerButton1.GetComponent<Button>().interactable = true;
                PlayerButton2.GetComponent<Button>().interactable = true;
                PlayerButton3.GetComponent<Button>().interactable = true;
                break;
            case >=5:
                PlayerButton1.SetActive(true);
                PlayerButton2.SetActive(true);
                PlayerButton3.SetActive(true);
                PlayerButton4.SetActive(true);
                PlayerButton1.GetComponent<Button>().interactable = true;
                PlayerButton2.GetComponent<Button>().interactable = true;
                PlayerButton3.GetComponent<Button>().interactable = true;
                PlayerButton4.GetComponent<Button>().interactable = true;
                break;
        }
    }
    private void DeactivateAllPlayerButtons()
    {
        switch (AllPlayers.Count)
        {
            case <= 1:
                PlayerButton1.SetActive(false);
                PlayerButton2.SetActive(false);
                PlayerButton3.SetActive(false);
                PlayerButton4.SetActive(false);
                break;
            case 2:
                PlayerButton1.SetActive(true);
                PlayerButton1.GetComponent<Button>().interactable = false;

                PlayerButton2.SetActive(false);
                PlayerButton3.SetActive(false);
                PlayerButton4.SetActive(false);
                break;
            case 3:
                PlayerButton1.SetActive(true);
                PlayerButton2.SetActive(true);

                PlayerButton1.GetComponent<Button>().interactable = false;
                PlayerButton2.GetComponent<Button>().interactable = false;

                PlayerButton3.SetActive(false);
                PlayerButton4.SetActive(false);
                break;
            case 4:
                PlayerButton1.SetActive(true);
                PlayerButton2.SetActive(true);
                PlayerButton3.SetActive(true);

                PlayerButton1.GetComponent<Button>().interactable = false;
                PlayerButton2.GetComponent<Button>().interactable = false;
                PlayerButton3.GetComponent<Button>().interactable = false;

                PlayerButton4.SetActive(false);
                break;
            case >= 5:
                PlayerButton1.SetActive(true);
                PlayerButton2.SetActive(true);
                PlayerButton3.SetActive(true);
                PlayerButton4.SetActive(true);

                PlayerButton1.GetComponent<Button>().interactable = false;
                PlayerButton2.GetComponent<Button>().interactable = false;
                PlayerButton3.GetComponent<Button>().interactable = false;
                PlayerButton4.GetComponent<Button>().interactable = false;
                break;
        }
    }
    private void FillInPlayerNames()
    {
        AllPlayersExclCurrentPlayer = new List<PlayerMovement>();
        for (int i = 0; i < AllPlayers.Count; i++)
        {
            if (AllPlayers[i].PlayerHier != CurrentPlayer.PlayerHier)
            {
                AllPlayersExclCurrentPlayer.Add(AllPlayers[i]);
            }
        }
        switch (AllPlayersExclCurrentPlayer.Count)
        {
            case <=0:
                break;
            case   1:
                Player1 = AllPlayersExclCurrentPlayer[0];
                PlayerButton1Text.text = AllPlayersExclCurrentPlayer[0].PlayerName;
                break;
            case   2:
                Player1 = AllPlayersExclCurrentPlayer[0];
                Player2 = AllPlayersExclCurrentPlayer[1];
                PlayerButton1Text.text = AllPlayersExclCurrentPlayer[0].PlayerName;
                PlayerButton2Text.text = AllPlayersExclCurrentPlayer[1].PlayerName;
                break;
            case   3:
                Player1 = AllPlayersExclCurrentPlayer[0];
                Player2 = AllPlayersExclCurrentPlayer[1];
                Player3 = AllPlayersExclCurrentPlayer[2];
                PlayerButton1Text.text = AllPlayersExclCurrentPlayer[0].PlayerName;
                PlayerButton2Text.text = AllPlayersExclCurrentPlayer[1].PlayerName;
                PlayerButton3Text.text = AllPlayersExclCurrentPlayer[2].PlayerName;
                break;
            case >=4:
                Player1 = AllPlayersExclCurrentPlayer[0];
                Player2 = AllPlayersExclCurrentPlayer[1];
                Player3 = AllPlayersExclCurrentPlayer[2];
                Player4 = AllPlayersExclCurrentPlayer[3];
                PlayerButton1Text.text = AllPlayersExclCurrentPlayer[0].PlayerName;
                PlayerButton2Text.text = AllPlayersExclCurrentPlayer[1].PlayerName;
                PlayerButton3Text.text = AllPlayersExclCurrentPlayer[2].PlayerName;
                PlayerButton4Text.text = AllPlayersExclCurrentPlayer[3].PlayerName;
                break;
        }
    }
    public void PlusThree()
    {
        DeactivateAllPlayerButtons();
        ActivateAllDynamicButtons();
        PlusThreeButton.GetComponent<Button>().interactable = false;

        TheChosenPlayer = CurrentPlayer;
        TheChosenDynamic = BuffsAndDebuffs.Plus3;
        ButtonAvailable = 2;

        CheckLockInButton();
    }
    public void PlusDSix()
    {
        DeactivateAllPlayerButtons();
        ActivateAllDynamicButtons();
        PlusDSixButton.GetComponent<Button>().interactable = false;

        TheChosenPlayer = CurrentPlayer;
        TheChosenDynamic = BuffsAndDebuffs.PlusD6;
        ButtonAvailable = 2;

        CheckLockInButton();
    }
    public void MinusThree()
    {
        ActivateAllPlayerButtons();
        ActivateAllDynamicButtons();
        MinusThreeButton.GetComponent<Button>().interactable = false;

        TheChosenDynamic = BuffsAndDebuffs.Minus3;
        ButtonAvailable = 1;

        CheckLockInButton();
    }
    public void MinusDSix()
    {
        ActivateAllPlayerButtons();
        ActivateAllDynamicButtons();
        MinusDSixButton.GetComponent<Button>().interactable = false;

        TheChosenDynamic = BuffsAndDebuffs.MinusD6;
        ButtonAvailable = 1;

        CheckLockInButton();
    }
    private void TurnEndEvent()
    {
        IEvent events = EndTurnEvent.GetComponent<IEvent>();
        if (events != null)
        {
            events.playEvent("EndTurn");
        }
    }
    public void PlayerButtons1()
    {
        ActivateAllPlayerButtons();
        PlayerButton1.GetComponent<Button>().interactable = false;

        TheChosenPlayer = Player1;
        ButtonAvailable++;

        CheckLockInButton();
    }
    public void PlayerButtons2()
    {
        ActivateAllPlayerButtons();
        PlayerButton2.GetComponent<Button>().interactable = false;

        TheChosenPlayer = Player2;
        ButtonAvailable++;

        CheckLockInButton();
    }
    public void PlayerButtons3()
    {
        ActivateAllPlayerButtons();
        PlayerButton3.GetComponent<Button>().interactable = false;

        TheChosenPlayer = Player3;
        ButtonAvailable++;

        CheckLockInButton();
    }
    public void PlayerButtons4()
    {
        ActivateAllPlayerButtons();
        PlayerButton4.GetComponent<Button>().interactable = false;

        TheChosenPlayer = Player4;
        ButtonAvailable++;

        CheckLockInButton();
    }
    private void CheckLockInButton()
    {
        if (ButtonAvailable >= 2)
        {
            LockInButton.SetActive(true);
        }
        else
        {
            LockInButton.SetActive(false);
        }
    }
    private void TurnOffVisualsUI()
    {
        VisualsUI.SetActive(false);
        DisplayPowerUI.SetActive(false);
        DeactivateAllPlayerButtons();
        ActivateAllDynamicButtons();
    }
    public void LockIn()
    {
        TheChosenPlayer.PlayerBuffsAndDebuffsList.Add(TheChosenDynamic);
        ButtonAvailable = 0;
        TurnOffVisualsUI();
        TurnEndEvent();
    }
}
