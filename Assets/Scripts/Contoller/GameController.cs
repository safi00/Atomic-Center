using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameSetupStats;
using static PlayerMovement;

public class GameController : MonoBehaviour
{
    [Header("GameStat")]
    [SerializeField] private int  TurnCapicity;
    [SerializeField] private int  PointCapicity;

    [Header("Core GameStats")]
    [SerializeField] private int  PlayerAmount;
    [SerializeField] private bool InfiniteRounds;
    [SerializeField] private bool InfiniteScore;
    [SerializeField] public GameState CurrentState;
    [SerializeField] public NodesController CurrentRoute;
    [SerializeField] private List<PlayerHierarchy> RollOrder;

    [Header("PlayersPrefabList")]
    [SerializeField] private GameObject[] PlayerPrefabs;
    [SerializeField] private GameObject PlayersParentObject;
    [SerializeField] private List<GameObject> Players = new List<GameObject>();

    [Header("UI")]
    [SerializeField] private GameObject UIScores;
    [SerializeField] private GameObject UIScore1;
    [SerializeField] private GameObject UIScore2;
    [SerializeField] private GameObject UIScore3;
    [SerializeField] private GameObject UIScore4;
    [SerializeField] private GameObject UIScore5;
    [HideInInspector] private List<GameObject> UIScoreLists = new List<GameObject>();

    [Header("GameStatsController")]
    [SerializeField] public GameSetupStats GameStats;
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
        TurnOffScoreUI();
        SetupUIList();
        SetupBaseRollOrder();
        TurnCapicity  = GameSetupStats.GetTurnLimit();
        PointCapicity = GameSetupStats.GetPointLimit();
        PlayerAmount  = GameSetupStats.GetPlayerAmount();
        InfiniteRounds= GameSetupStats.GetTurnLimitBool();
        InfiniteScore = GameSetupStats.GetPointLimitBool();
        SetGameState(GameState.Rolling);
        MakePlayers(PlayerAmount);
    }
    private void MakePlayers(int amount) 
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject Player = Instantiate(PlayerPrefabs[i], PlayerPostion(i), PlayerPrefabs[i].transform.rotation);
            PlayerMovement PlayerScript = Player.GetComponent<PlayerMovement>();

            PlayerScript.PlayerHier = PlayerHeirSetter(i);
            PlayerScript.PlayerName = GameSetupStats.GetPlayerList()[i].playername;
            PlayerScript.CurrentRoute = CurrentRoute;
            PlayerScript.isPlayerOrderRolled = false;

            Players.Add(Player);
            Player.transform.parent = PlayersParentObject.transform; 
            SetupScores(PlayerAmount);
        }
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
    private void SetupScores(int PlayerAmount)
    {
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
    private void SetupBaseRollOrder()
    {
        RollOrder = new List<PlayerHierarchy>() {
            PlayerHierarchy.Player1,
            PlayerHierarchy.Player2,
            PlayerHierarchy.Player3,
            PlayerHierarchy.Player4,
            PlayerHierarchy.Player5, 
        };
    }
    public List<int> SortRollOrderList(List<int> list)
    {
        // Sorting using a single loop
        for (int j = 0; j < PlayerAmount - 1; j++)
        {
            // Checking the condition for two
            // simultaneous elements of the array
            if (list[j] < list[j + 1])
            {
                // Swapping the elements.
                int temp = list[j];
                list[j] = list[j + 1];
                list[j + 1] = temp;

                // updating the value of j = -1
                // so after getting updated for j++
                // in the loop it becomes 0 and
                // the loop begins from the start.
                j = -1;
            }
        }
        return list;
    }
    public static void CheckTurnLimit()
    {
        bool isThereTurnNoLimit = GameSetupStats.GetTurnLimitBool();
        if (!isThereTurnNoLimit)
        {
            GameSetupStats.SetGameState(GameSetupStats.GameState.GameOver);
        }
    }
    public static void CheckPointLimit()
    {
        bool isTherePointNoLimit = GameSetupStats.GetPointLimitBool();
        if (!isTherePointNoLimit)
        {

        }
    }
}
