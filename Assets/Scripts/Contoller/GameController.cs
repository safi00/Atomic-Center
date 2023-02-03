using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameSetupStats;
using static NodesController;
using static PlayerMovement;
public class GameController : MonoBehaviour
{
    [Header("GameStat")]
    [SerializeField] private int  CurrentTurnNumber;
    [SerializeField] private int  TurnCapicity;
    [SerializeField] private int  PointCapicity;

    [Header("Core GameStats")]
    [HideInInspector] private bool InfiniteRounds;
    [HideInInspector] private bool InfiniteScore;
    [SerializeField] private int PlayerAmount;
    [SerializeField] public GameState CurrentState;
    [SerializeField] public NodesController CurrentRoute;
    [SerializeField] public static List<GameObject> RollOrder;
    [SerializeField] private Difficulty GameDifficulty;

    [Header("PlayersPrefabList")]
    [SerializeField] private GameObject[] PlayerPrefabs;
    [SerializeField] private GameObject PlayersParentObject;
    [SerializeField] private List<GameObject> PlayersList = new List<GameObject>();

    [Header("Camera")]
    [SerializeField] public GameObject RollOrderCamera;
    [SerializeField] public GameObject PlayerCamera;
    [SerializeField] public GameObject MiniMap;

    [Header("UI")]
    [SerializeField] private Text UIGameInfo;
    [SerializeField] private GameObject UIVisuals;
    [SerializeField] private GameObject UIInfo;
    [SerializeField] private GameObject UIScore1;
    [SerializeField] private GameObject UIScore2;
    [SerializeField] private GameObject UIScore3;
    [SerializeField] private GameObject UIScore4;
    [SerializeField] private GameObject UIScore5;
    [SerializeField] private GameObject UIRounds;
    [SerializeField] private GameObject UIScoreCap;
    [SerializeField] private List<GameObject> UIScoreLists = new List<GameObject>();

    [SerializeField] private GameObject UIBigRollButton;
    [SerializeField] private GameObject UIStayOptions;

    [SerializeField] private GameObject UIStatScreen;
    [SerializeField] private Text FirstPlace;
    [SerializeField] private Text SecondPlace;
    [SerializeField] private Text ThirdPlace;
    [SerializeField] private Text FourthPlace;
    [SerializeField] private Text FifthPlace;

    [SerializeField] private GameObject PlayerTurnUI;
    [SerializeField] private GameObject PlayerTurnText;
    [SerializeField] private GameObject PlayerTurnClickText;
    [SerializeField] private GameObject PlayerTurnButton;

    [Header("Game Controllers")]
    [SerializeField] public GameSetupStats GameStats;
    [SerializeField] public HintsController ElementHintsController;
    [SerializeField] public QuestionController QuestionsController;
    [SerializeField] public DynamicController DynamicController;

    [Header("Events")]
    [SerializeField] public GameObject PointUPEvent;
    [SerializeField] public GameObject ResetDataEvent;
    [SerializeField] public GameObject LogEvent;
    [SerializeField] public GameObject EventTriggerEvent;
    [SerializeField] public GameObject EndTurnEvent;
    [HideInInspector] public static string LogString;

    [Header("Misc")]
    [SerializeField] private List<PlayerHierarchy> PlayerHierarchies;
    [HideInInspector] private bool isLogtextUpdated;
    [HideInInspector] private static List<int> RollOrderRolls = new List<int>();
    [HideInInspector] private static int IndexRoll = 0;
    [HideInInspector] private static bool isRollOrderDone = false;
    [HideInInspector] public static NodeEventType NodeEventType;
    [HideInInspector] public static bool didTheGameSetup;

    // Start is called before the first frame update
    void Start()
    {
        //Setup();
    }
    // Update is called once per frame
    void Update()
    {
        if (!didTheGameSetup)
        {
            Setup();
        }
    }
    /// <summary>
    /// This method Sets everything in line for the controller to start working
    /// </summary>
    private void Setup() 
    {
        didTheGameSetup= true;
        TurnCapicity  = GameSetupStats.GetTurnLimit();
        PointCapicity = GameSetupStats.GetPointLimit();
        PlayerAmount  = GameSetupStats.GetPlayerAmount();
        InfiniteRounds= GameSetupStats.GetTurnLimitBool();
        InfiniteScore = GameSetupStats.GetPointLimitBool();
        GameDifficulty= GameSetupStats.GetSelectedGameDifficulty();
        ElementHintsController.ChosenDiffuculty = GameDifficulty;
        CurrentTurnNumber = 0;
        LogString = "Roll for Turn Order";
        switch (InfiniteRounds)
        {
            case  true:
                UIRounds.transform.GetChild(0).transform.GetComponent<Text>().text = "Round: " + String.Format("{0:00}", CurrentTurnNumber);
                break;
            case false:
                UIRounds.transform.GetChild(0).transform.GetComponent<Text>().text = "Round: " + String.Format("{0:00}", CurrentTurnNumber) + "/" + String.Format("{0:00}", PointCapicity);
                break;
        }
        UIScoreCap.transform.GetChild(0).transform.GetComponent<Text>().text = String.Format("{0:00}", TurnCapicity);

        UpdateGameInfoLog();
        SetGameState(GameState.RollOrder);
        SetupUIList();
        MakePlayers(PlayerAmount); 
        DynamicFiller();
    }
    /// <summary>
    /// This method makes player and adds their stats, script and scores to the scoreboard and shares it with the other scripts
    /// </summary>
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
            PlayerScript.CurrentElementIndex = -1;

            PlayerScript.PassGO = PointUPEvent.GetComponent<IEvent>();
            PlayerScript.Log = LogEvent.GetComponent<IEvent>();
            PlayerScript.Trigger = EventTriggerEvent.GetComponent<IEvent>();

            InitilizingPlayerDataLists(PlayerScript, GameSetupStats.GetPlayerList()[i].BuffsAndDebuffsList);

            PlayersList.Add(Player);
            Player.transform.parent = PlayersParentObject.transform;

            RollOrder.Add(Player);
        }
        SetupScores(PlayerAmount);
        UpdateScores();
    }

    /// <summary>
    /// This method gives each player their ranking in player order but not turn order
    /// </summary>
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
    /// <summary>
    /// This method gives each player stats ife they were to have any given before the game
    /// </summary>
    private void InitilizingPlayerDataLists(PlayerMovement PlayerScript, List<BuffsAndDebuffs> PlayerPowerUPList)
    {
        int AmountOfBuffsAndDebuffs = PlayerPowerUPList.Count;

        PlayerScript.PlayerBuffsAndDebuffsList = new List<BuffsAndDebuffs>();        
        if (AmountOfBuffsAndDebuffs > 0)
        {
            for (int i = 0; i < AmountOfBuffsAndDebuffs; i++)
            {
                PlayerScript.PlayerBuffsAndDebuffsList.Add(PlayerPowerUPList[i]);
            }
        }
    }

    /// <summary>
    /// This method gives each player their unique positions
    /// </summary>
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

    /// <summary>
    /// This method is a global roll for all players but its distinguished by their player position
    /// </summary>
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
    /// <summary>
    /// This method is a global stay for all players but its distinguished by their player position
    /// </summary>
    public void PlayerStay()
    {
        PlayerMovement Player = GetCurrentTurnPlayerObject().GetComponent<PlayerMovement>();
        Player.Stay();
    }
    /// <summary>
    /// This method is a logger that is called it also updates the scores with it
    /// </summary>
    private void UpdateGameInfoLog()
    {
        UIGameInfo.text = LogString;
        if (isRollOrderDone)
        {
            UpdateScores();
        }
    }
    /// <summary>
    /// This method updates the scores and rounds depending on variables like if the rounds are infninite it wont show the 999 cap
    /// </summary>
    private void UpdateScores()
    {
        for (int i = 0; i < PlayerAmount; i++)
        {
            UIScoreLists[i].transform.GetChild(0).transform.GetComponent<Text>().text = "" + String.Format("{0:00}", GetPlayerScript(i).PlayerScore);
        }

        switch (InfiniteRounds)
        {
            case true:
                UIRounds.transform.GetChild(0).transform.GetComponent<Text>().text = "Round: " + String.Format("{0:00}", CurrentTurnNumber);
                break;
            case false:
                UIRounds.transform.GetChild(0).transform.GetComponent<Text>().text = "Round: " + String.Format("{0:00}", CurrentTurnNumber) + "/" + String.Format("{0:00}", PointCapicity);
                break;
        }
    }
    /// <summary>
    /// This method turns off the other scores that arent beign used that game
    /// </summary>
    private void SetupScores(int PlayerAmount)
    {
        TurnOffScoreUI();
        for (int i = 0; i < PlayerAmount; i++)
        {
            UIScoreLists[i].SetActive(true);
        }
    }
    /// <summary>
    /// This method turns off the scores
    /// </summary>
    private void TurnOffScoreUI()
    {
        for (int i = 0; i < UIScoreLists.Count; i++)
        {
            UIScoreLists[i].SetActive(false);
        }
    }

    /// <summary>
    /// This method adds each player score line to a list
    /// </summary>
    private void SetupUIList()
    {
        UIScoreLists.Add(UIScore1);
        UIScoreLists.Add(UIScore2);
        UIScoreLists.Add(UIScore3);
        UIScoreLists.Add(UIScore4);
        UIScoreLists.Add(UIScore5);
    }
    /// <summary>
    /// This method updates the camera to a top down view
    /// </summary>
    private void CameraUpdate() 
    {
        RollOrderCamera.SetActive(false);
        MiniMap.SetActive(true);
    }
    /// <summary>
    /// This method is to set the game stat for when the game is ending or setting up
    /// </summary>
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

    /// <summary>
    /// This method is a list sorter it sort lists from big to small need for roll order or 
    /// to show stats of who had the highest scor
    /// </summary>
    public void SortRollOrderList()
    {
        for (int i = 0; i < (PlayerAmount - 1); i++)
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
    /// <summary>
    /// This method adds each player early roll to decide turn order
    /// </summary>
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
    public static string GetCurrentTurnPlayerName()
    {
        return GetCurrentTurnPlayerObject().GetComponent<PlayerMovement>().PlayerName;
    }
    private PlayerMovement GetPlayerScript(int PlayerNumber)
    {
        return PlayersList[PlayerNumber].GetComponent<PlayerMovement>();
    }
    public static int GetCurrentTurn()
    {
        return (IndexRoll + 1);
    }

    /// <summary> 
    /// This method ends the current turn and passing the turn to the next player
    /// while checking if the game should end and or update the scores
    /// </summary>
    public void NextTurn()
    {
        if (!isRollOrderDone)
        {
            CheckIfRollOrderIsFinished();
        }
        if (IndexRoll >= (RollOrder.Count - 1))
        {
            CurrentTurnNumber++;
            IndexRoll = 0;
        }
        else
        {
            IndexRoll++;
        }
        if (isRollOrderDone)
        {
            CheckTurnLimit();
            CheckPointLimit();
            UpdateScores();
            if (CurrentGameState == GameState.RollOrder)
            {
                SetGameState(GameState.Playing);
            }
            if (CurrentGameState == GameState.Playing)
            {
                UpdateRollButtons();
                TurnOnVisuals();
                TurnOnTurnUI();
                CameraUpdate();
            }
        }
    }
    /// <summary>
    /// This method checks if each player hass rolled for turn order
    /// </summary>
    private void CheckIfRollOrderIsFinished()
    {
        if (RollOrderRolls.Count == PlayerAmount)
        {
            isRollOrderDone = true;
            SortRollOrderList();
        }
    }

    /// <summary>
    /// This method is for ui, its for swapping back and for and adding the stay option
    /// </summary>
    private void UpdateRollButtons()
    {
        if (GetCurrentTurnPlayerScript().CurrentElementIndex > -1)
        {
            TurnOnStayButton();
        }
        else
        {
            TurnOnBigRollButton();
        }
    }

    /// <summary>
    /// This methods checks what event the player triggered and what its should do depending on the event
    /// </summary>
    public void EventTypeTrigger()
    {
        NodeEventType eventType = NodeEventType;
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
        GetCurrentTurnPlayerScript().PlayerScore++;
        NextTurn();
    }
    private void TriggerWhiteNodeEvent()
    {
        ElementHintsController.CurrentGuessingPlayer = GetCurrentTurnPlayerScript();
        ElementHintsController.HintPOP();
        Debug.Log("White");
    }
    private void TriggerPurpleNodeEvent()
    {
        QuestionsController.CurrentGuessingPlayer = GetCurrentTurnPlayerScript();
        QuestionsController.QuetionPOP();
        Debug.Log("Purple");
    }
    private void TriggerAquamarineNodeEvent()
    {
        DynamicController.CurrentPlayer = GetCurrentTurnPlayerScript();
        DynamicController.DynamicPOP();
        Debug.Log("Aquamarine");
    }
    private void TriggerRedNodeEvent()
    {
        for (int i = 0; i < PlayersList.Count; i++)
        {
            PlayersList[i].GetComponent<PlayerMovement>().PlayerBuffsAndDebuffsList.Add(BuffsAndDebuffs.Minus3);
        }
        Debug.Log("Red");
    }

    /// <summary>
    /// These methods are for ui
    /// </summary>
    private void TurnOnStayButton()
    {
        UIStayOptions.SetActive(true);
        UIBigRollButton.SetActive(false);
    }
    private void TurnOnBigRollButton()
    {
        UIStayOptions.SetActive(false);
        UIBigRollButton.SetActive(true);
    }
    private void PlayerPointUP()
    {
        GetCurrentTurnPlayerScript().PlayerScore++;
        UpdateScores();
    }
    /// <summary>
    /// This method fills in the stats at the end
    /// showing who was first and adding buttons
    /// </summary>
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
        if (PlayerAmount >= 2)
        {
            for (int i = 0; i < (PlayerAmount - 1); i++)
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
        }
        switch (PlayerAmount)
        {
            case <=1:
                FirstPlace.text = "winner is: " + PlayerNameRanking[0] + ", Score " + PlayerScoreRanking[0];
                SecondPlace.text = String.Empty;
                ThirdPlace.text = String.Empty;
                FourthPlace.text = String.Empty;
                FifthPlace.text = String.Empty;
                break;
            case   2:
                FirstPlace.text = "winner is: " + PlayerNameRanking[0] + ", Score " + PlayerScoreRanking[0];
                SecondPlace.text = PlayerNameRanking[1] + ", Score " + PlayerScoreRanking[1];
                ThirdPlace.text = String.Empty;
                FourthPlace.text = String.Empty;
                FifthPlace.text = String.Empty;
                break;
            case   3:
                FirstPlace.text = "winner is: " + PlayerNameRanking[0] + ", Score " + PlayerScoreRanking[0];
                SecondPlace.text = PlayerNameRanking[1] + ", Score " + PlayerScoreRanking[1];
                ThirdPlace.text  = PlayerNameRanking[2] + ", Score " + PlayerScoreRanking[2];
                FourthPlace.text = String.Empty;
                FifthPlace.text = String.Empty;
                break;
            case   4:
                FirstPlace.text = "winner is: " + PlayerNameRanking[0] + ", Score " + PlayerScoreRanking[0];
                SecondPlace.text = PlayerNameRanking[1] + ", Score " + PlayerScoreRanking[1];
                ThirdPlace.text = PlayerNameRanking[2] + ", Score " + PlayerScoreRanking[2];
                FourthPlace.text = PlayerNameRanking[3] + ", Score " + PlayerScoreRanking[3];
                FifthPlace.text = String.Empty;
                break;
            case >=5:
                FirstPlace.text = "winner is: " + PlayerNameRanking[0] + ", Score " + PlayerScoreRanking[0];
                SecondPlace.text = PlayerNameRanking[1] + ", Score " + PlayerScoreRanking[1];
                ThirdPlace.text = PlayerNameRanking[2] + ", Score " + PlayerScoreRanking[2];
                FourthPlace.text = PlayerNameRanking[3] + ", Score " + PlayerScoreRanking[3];
                FifthPlace.text  = PlayerNameRanking[4] + ", Score " + PlayerScoreRanking[4];
                break;
        }
    }

    /// <summary>
    /// This method checks if the game hass reached its turn limit
    /// </summary>
    private void CheckTurnLimit()
    {
        if (!InfiniteRounds)
        {
            if (CurrentTurnNumber > TurnCapicity)
            {
                CurrentTurnNumber = TurnCapicity;
                EndTheGame();
            }
        }
    }
    /// <summary>
    /// This method checks if the game hass reached its score limit
    /// </summary>
    private void CheckPointLimit()
    {
        if (!InfiniteScore)
        {
            for (int i = 0; i < PlayerAmount; i++)
            {
                int TempPlayerScore = GetPlayerScript(i).PlayerScore;
                if (TempPlayerScore >= PointCapicity)
                {
                    EndTheGame();
                }
            }
        }
    }
    private void TurnOnStatStatScreenUI()
    {
        UIStatScreen.SetActive(true);
    }

    /// <summary>
    /// This method passes the players to thee Dynamic script
    /// </summary>
    private void DynamicFiller()
    {
        DynamicController.AllPlayers = new List<PlayerMovement>();
        for (int i = 0; i < PlayersList.Count; i++)
        {
            DynamicController.AllPlayers.Add(PlayersList[i].GetComponent<PlayerMovement>());
        }
    }

    /// <summary>
    /// These methods are for ui
    /// </summary>
    public void TurnOnVisuals()
    {
        UIVisuals.SetActive(true);
    }
    public void TurnOnTurnUI()
    {
        PlayerTurnText.SetActive(true);
        PlayerTurnText.transform.GetComponent<Text>().text = GetCurrentTurnPlayerName() + "'s Turn";
        PlayerTurnButton.SetActive(true);
        PlayerTurnUI.SetActive(true);
    }
    public void TurnOffVisuals()
    {
        UIVisuals.SetActive(false);

        PlayerTurnUI.SetActive(false);
        PlayerTurnText.SetActive(false);
        PlayerTurnButton.SetActive(false);
    }
    private void EndTheGame()
    {
        TurnOnVisuals();
        TurnOnStatStatScreenUI();
        FillInStats();
    }
    private void ResetData()
    {
        GameSetupStats.SetGameState(GameState.Setup);
        PlayersList.Clear();
        isRollOrderDone = false;   
    }
    private void ResetEvent()
    {
        IEvent resetEvent = ResetDataEvent.GetComponent<IEvent>();
        if (resetEvent != null)
        {
            resetEvent.playEvent("Reset");
        }
    }
    public void ReturnToMainMenu()
    {
        ResetEvent();
        GameSetupStats.reset();
        Loader.Load(Loader.Scene.MainMenu);
    }
    public void ExitAplication()
    {
        Application.Quit();
    }
    /// <summary>
    /// This methods are here to subcribe to events
    /// so when a Qcoin gets collected all the other scripts know to tun a method
    /// </summary>
    private void OnEnable()
    {
        EndTurn.OnTurnEnd += NextTurn;
        TriggerEvent.OnEventTriggered += EventTypeTrigger;
        Log.OnLog += UpdateGameInfoLog;
        Reset.OnReset += ResetData;
        PointUP.OnPointUP += PlayerPointUP;
    }
    private void OnDisable()
    {
        EndTurn.OnTurnEnd -= NextTurn;
        TriggerEvent.OnEventTriggered -= EventTypeTrigger;
        Log.OnLog -= UpdateGameInfoLog;
        Reset.OnReset -= ResetData;
        PointUP.OnPointUP -= PlayerPointUP;
    }
}
