using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static GameSetupStats;
using static NodesController;
using static PlayerMovement;

public class GameController : MonoBehaviour
{
    [Header("GameStat")]
    [SerializeField] private int  CurrentTurnNumber = 0;
    [SerializeField] private int  TurnCapicity;
    [SerializeField] private int  PointCapicity;

    [Header("Core GameStats")]
    [HideInInspector] private bool InfiniteRounds;
    [HideInInspector] private bool InfiniteScore;
    [SerializeField] private int PlayerAmount;
    [SerializeField] public GameState CurrentState;
    [SerializeField] public NodesController CurrentRoute;
    [SerializeField] public static List<GameObject> RollOrder;

    [Header("PlayersPrefabList")]
    [SerializeField] private GameObject[] PlayerPrefabs;
    [SerializeField] private GameObject PlayersParentObject;
    [SerializeField] private List<GameObject> PlayersList = new List<GameObject>();

    [Header("UI")]
    [SerializeField] private GameObject UIVisuals;
    [SerializeField] private GameObject UIInfo;
    [SerializeField] private GameObject UIScore1;
    [SerializeField] private GameObject UIScore2;
    [SerializeField] private GameObject UIScore3;
    [SerializeField] private GameObject UIScore4;
    [SerializeField] private GameObject UIScore5;
    [SerializeField] private GameObject UIRounds;
    [SerializeField] private GameObject UIScoreCap;
    [HideInInspector] private List<GameObject> UIScoreLists = new List<GameObject>();

    [SerializeField] private GameObject UIBigRollButton;
    [SerializeField] private GameObject UIStayOptions;

    [SerializeField] private Text FirstPlace;
    [SerializeField] private Text SecondPlace;
    [SerializeField] private Text ThirdPlace;
    [SerializeField] private Text FourthPlace;
    [SerializeField] private Text FifthPlace;

    [SerializeField] private GameObject PlayerTurnText;
    [SerializeField] private GameObject PlayerTurnButton;

    [Header("GameStatsController")]
    [SerializeField] public GameSetupStats GameStats;

    [Header("Events")]
    [SerializeField] public GameObject PassGOEvent;

    [Header("Misc")]
    [SerializeField] private List<PlayerHierarchy> PlayerHierarchies;
    [HideInInspector] private static List<int> RollOrderRolls = new List<int>();
    [HideInInspector] private static int IndexRoll = 0;
    [HideInInspector] private static bool isBigRollButtonVisable;
    [HideInInspector] private static bool isRollOrderDone = false;

    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void Setup() 
    {
        isBigRollButtonVisable = true;
        TurnCapicity  = GameSetupStats.GetTurnLimit();
        PointCapicity = GameSetupStats.GetPointLimit();
        PlayerAmount  = GameSetupStats.GetPlayerAmount();
        InfiniteRounds= GameSetupStats.GetTurnLimitBool();
        InfiniteScore = GameSetupStats.GetPointLimitBool();

        UIRounds.transform.GetChild(0).transform.GetComponent<Text>().text = "Round: " + String.Format("{0:00}", CurrentTurnNumber) + "/" + String.Format("{0:00}", PointCapicity);
        UIScoreCap.transform.GetChild(0).transform.GetComponent<Text>().text = String.Format("{0:00}", TurnCapicity);

        SetGameState(GameState.Instructions);
        SetupUIList();
        MakePlayers(PlayerAmount);
    }
    private void MakePlayers(int amount)
    {
        MakePlayerHeirList();
        RollOrder = new List<GameObject>() {};
        for (int i = 0; i < amount; i++)
        {
            GameObject Player = Instantiate(PlayerPrefabs[i], PlayerPostion(i), PlayerPrefabs[i].transform.rotation);
            PlayerMovement PlayerScript = Player.GetComponent<PlayerMovement>();

            PlayerScript.PlayerHier = PlayerHeirSetter(i);
            PlayerScript.CurrentRoute = CurrentRoute;
            PlayerScript.isPlayerOrderRolled = false;

            PlayerScript.PlayerName = GameSetupStats.GetPlayerList()[i].playername;
            PlayerScript.PlayerScore = GameSetupStats.GetPlayerList()[i].playerscore;
            PlayerScript.PlayerLocation = GameSetupStats.GetPlayerList()[i].playerlocation;
            PlayerScript.ongoingPrompt = false;
            PlayerScript.PassGO = PassGOEvent.GetComponent<IEvent>();

            InitilizingPlayerDataLists(PlayerScript, GameSetupStats.GetPlayerList()[i].debuffs, GameSetupStats.GetPlayerList()[i].powerups);

            PlayersList.Add(Player);
            Player.transform.parent = PlayersParentObject.transform;

            RollOrder.Add(Player);
        }
        SetupScores(PlayerAmount);
        UpdateScores();
    }
    private PlayerHierarchy PlayerHeirSetter(int amount)
    {
        PlayerHierarchy ReturnPos = PlayerHierarchy.Player1;
        switch (amount)
        {
            case 0:
                ReturnPos = PlayerHierarchy.Player1;
                break;
            case 1:
                ReturnPos = PlayerHierarchy.Player2;
                break;
            case 2:
                ReturnPos = PlayerHierarchy.Player3;
                break;
            case 3:
                ReturnPos = PlayerHierarchy.Player4;
                break;
            case 4:
                ReturnPos = PlayerHierarchy.Player5;
                break;
        }
        return ReturnPos;
    }
    private void InitilizingPlayerDataLists(PlayerMovement PlayerScript, List<Debuff> PlayerDebuffList, List<PowerUP> PlayerPowerUPList)
    {
        int AmountOfDebuffs  = PlayerDebuffList.Count;
        int AmountOfPowerUPs = PlayerPowerUPList.Count;

        PlayerScript.PlayerDebuffList = new List<Debuff>();
        PlayerScript.PlayerPowerUPList = new List<PowerUP>();
        if (AmountOfDebuffs > 0)
        {
            for (int i = 0; i < AmountOfDebuffs; i++)
            {
                PlayerScript.PlayerDebuffList.Add(PlayerDebuffList[i]);
            }
        }
        if (AmountOfPowerUPs > 0)
        {
            for (int i = 0; i < AmountOfPowerUPs; i++)
            {
                PlayerScript.PlayerPowerUPList.Add(PlayerPowerUPList[i]);
            }
        }
    }
    private Vector3 PlayerPostion(int amount) 
    {
        Vector3 ReturnPos = new Vector3();
        switch (amount)
        {
            case 0:
                ReturnPos = new Vector3(-0.6f, 0, 0.6f);
                break;
            case 1:
                ReturnPos = new Vector3(0.2f, 0, 0.8f);
                break;
            case 2:
                ReturnPos = new Vector3(0, 0, 0);
                break;
            case 3:
                ReturnPos = new Vector3(-0.6f, 0, -0.6f);
                break;
            case 4:
                ReturnPos = new Vector3(0.2f, 0, 0.8f);
                break;
        }
        return ReturnPos;
    }
    public void PlayerRoll()
    {
        PlayerMovement Player = GetCurrentTurnPlayerObject().GetComponent<PlayerMovement>();
        switch (Player.isPlayerOrderRolled)
        {
            case true:
                Player.Roll();
                break;
            case false:
                Player.RollHeir();
                NextTurn();
                break;
        }
    }
    private void UpdateScores()
    {
        for (int i = 0; i < PlayerAmount; i++)
        {
            UIScoreLists[i].transform.GetChild(0).transform.GetComponent<Text>().text = "" + String.Format("{0:00}", GetPlayerScript(i).PlayerScore);
        }
        if (true)
        {

        }
    }
    private void SetupScores(int PlayerAmount)
    {
        TurnOffScoreUI();
        for (int i = 0; i < PlayerAmount; i++)
        {
            UIScoreLists[i].SetActive(true);
        }
    }
    private void TurnOffScoreUI()
    {
        for (int i = 0; i < UIScoreLists.Count; i++)
        {
            UIScoreLists[i].SetActive(false);
        }
    }
    private void SetupUIList()
    {
        UIScoreLists.Add(UIScore1);
        UIScoreLists.Add(UIScore2);
        UIScoreLists.Add(UIScore3);
        UIScoreLists.Add(UIScore4);
        UIScoreLists.Add(UIScore5);
    }
    private void SetGameState(GameState GameState)
    {
        GameSetupStats.SetGameState(GameState);
        CurrentGameState = GameSetupStats.GetGameState();
    }
    private void MakePlayerHeirList()
    {
        PlayerHierarchies = new List<PlayerHierarchy>() {
            PlayerHierarchy.Player1,
            PlayerHierarchy.Player2,
            PlayerHierarchy.Player3,
            PlayerHierarchy.Player4,
            PlayerHierarchy.Player5,
        };
    }
    public void SortRollOrderList()
    {
        for (int i = 0; i < PlayerAmount - 1; i++)
        {
            if (RollOrderRolls[i] < RollOrderRolls[i + 1])
            {
                int Temp1 = RollOrderRolls[i];
                RollOrderRolls[i] = RollOrderRolls[i + 1];
                RollOrderRolls[i + 1] = Temp1;

                GameObject Temp2 = RollOrder[i];
                RollOrder[i] = RollOrder[i + 1];
                RollOrder[i + 1] = Temp2;

                i = -1;
            }
        }
    }
    public static void AddRollOrder(int Roll) 
    {
        RollOrderRolls.Add(Roll);
    }
    public static GameObject GetCurrentTurnPlayerObject()
    {
        return RollOrder[IndexRoll];
    }
    public static PlayerMovement GetCurrentTurnPlayerScript()
    {
        return GetCurrentTurnPlayerObject().GetComponent<PlayerMovement>();
    }
    private PlayerMovement GetPlayerScript(int PlayerNumber)
    {
        return PlayersList[PlayerNumber].GetComponent<PlayerMovement>();
    }
    public static int GetCurrentTurn()
    {
        return (IndexRoll + 1);
    }
    public void NextTurn()
    {
        if (IndexRoll >= (RollOrder.Count - 1))
        {
            IndexRoll = 0;
        }
        else
        {
            IndexRoll++;
        }
        if (isRollOrderDone)
        {
            TurnOnVisuals();
            PlayerTurnText.SetActive(true);
            PlayerTurnText.transform.GetComponent<Text>().text = "Player " + (IndexRoll + 1) + "'s Turn";
            PlayerTurnButton.SetActive(true);
        }
    }
    private void UpdateRollButtons()
    {
        if (GetCurrentTurnPlayerScript().ongoingPrompt)
        {
            if (isBigRollButtonVisable)
            {
                FlipTheRollButton();
            }
        }
        else
        {
            if (!isBigRollButtonVisable)
            {
                FlipTheRollButton();
            }
        }
    }
    private void EventTypeTrigger(NodeEventType eventType)
    {
        switch (eventType)
        {
            case NodeEventType.Green:
                TriggerGreenNodeEvent();
                break;
            case NodeEventType.White:
                TriggerWhiteNodeEvent();
                break;
            case NodeEventType.Purple:
                TriggerPurpleNodeEvent();
                break;
            case NodeEventType.Aquamarine:
                TriggerAquamarineNodeEvent();
                break;
            case NodeEventType.Red:
                TriggerRedNodeEvent();
                break;
        }
    }
    private void TriggerGreenNodeEvent()
    {

    }
    private void TriggerWhiteNodeEvent()
    {

    }
    private void TriggerPurpleNodeEvent()
    {

    }
    private void TriggerAquamarineNodeEvent()
    {

    }
    private void TriggerRedNodeEvent()
    {

    }
    private void FlipTheRollButton()
    {
        isBigRollButtonVisable = !isBigRollButtonVisable;
        if (isBigRollButtonVisable)
        {
            UIBigRollButton.SetActive(true);
            UIStayOptions.SetActive(false);
        }
        else
        {
            UIBigRollButton.SetActive(false);
            UIStayOptions.SetActive(true);
        }
    }
    private void FillInStats()
    {
        SetGameState(GameState.Stats);

        List<String> PlayerNameRanking = new List<String>();
        List<int> PlayerScoreRanking = new List<int>();

        for (int i = 0; i < PlayerAmount; i++)
        {
            PlayerNameRanking.Add(GetPlayerScript(i).PlayerName);
            PlayerScoreRanking.Add(GetPlayerScript(i).PlayerScore);
        }
        for (int i = 0; i < PlayerAmount - 1; i++)
        {
            if (GetPlayerScript(i).PlayerScore < GetPlayerScript(i + 1).PlayerScore)
            {
                int Temp1 = PlayerScoreRanking[i];
                PlayerScoreRanking[i] = PlayerScoreRanking[i + 1];
                PlayerScoreRanking[i + 1] = Temp1;

                string Temp2 = PlayerNameRanking[i];
                PlayerNameRanking[i] = PlayerNameRanking[i + 1];
                PlayerNameRanking[i + 1] = Temp2;

                i = -1;
            }
        }
        FirstPlace.text = "winner is: " + PlayerNameRanking[0] + " " + PlayerScoreRanking[0];
        switch (PlayerAmount)
        {
            case 2:
                SecondPlace.text = PlayerNameRanking[1] + " " + PlayerScoreRanking[1];
                break;
            case 3:
                ThirdPlace.text  = PlayerNameRanking[2] + " " + PlayerScoreRanking[2];
                break;
            case 4:
                FourthPlace.text = PlayerNameRanking[3] + " " + PlayerScoreRanking[3];
                break;
            case 5:
                FifthPlace.text  = PlayerNameRanking[4] + " " + PlayerScoreRanking[4];
                break;
        }
    }
    private void CheckTurnLimit()
    {
        if (!InfiniteRounds)
        {
            for (int i = 0; i < PlayerAmount; i++)
            {

            }
        }
    }
    private void CheckPointLimit()
    {
        if (!InfiniteScore)
        {
            for (int i = 0; i < PlayerAmount; i++)
            {
                int TempPlayerScore = GetPlayerScript(i).PlayerScore;
                if (TempPlayerScore >= PointCapicity)
                {

                }
            }
        }
    }
    public void TurnOnVisuals()
    {
        UIVisuals.SetActive(true);
    }
    public void TurnOffVisuals()
    {
        UIVisuals.SetActive(false);
        PlayerTurnText.SetActive(false);
        PlayerTurnButton.SetActive(false);
    }
    private void PlayerPointUP()
    {
        GetCurrentTurnPlayerScript().PlayerScore++;
        UpdateScores();
    }
    /// <summary>
    /// This methods are here to subcribe to events
    /// so when a Qcoin gets collected all the other scripts know to tun a method
    /// </summary>
    private void OnEnable()
    {
        PointUP.OnPointUP += PlayerPointUP;
    }
    private void OnDisable()
    {
        PointUP.OnPointUP -= PlayerPointUP;
    }
}
