using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static PlayerMovement;
using static UnityEngine.GraphicsBuffer;

public class GameController : MonoBehaviour
{
    [Header("GameStat")]
    [SerializeField] private int  TurnCapicity;
    [SerializeField] private int  PointCapicity;

    [Header("Core GameStats")]
    [SerializeField] private int  PlayerAmount;
    [SerializeField] private bool InfiniteRounds;
    [SerializeField] private bool InfiniteScore;
    [SerializeField] public NodesController CurrentRoute;

    [Header("PlayersPrefabList")]
    [SerializeField] private GameObject[] PlayerPrefabs;
    [SerializeField] private GameObject PlayersParentObject;
    [SerializeField] private List<GameObject> Players = new List<GameObject>();

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
        TurnCapicity  = GameSetupStats.GetTurnLimit();
        PointCapicity = GameSetupStats.GetPointLimit();
        PlayerAmount  = GameSetupStats.GetPlayerAmount();
        InfiniteRounds= GameSetupStats.GetTurnLimitBool();
        InfiniteScore = GameSetupStats.GetPointLimitBool();
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

            //Player.GetComponent<PlayerMovement>().PlayerHier = PlayerHeirSetter(i);
            //Player.GetComponent<PlayerMovement>().PlayerName = GameSetupStats.GetPlayerList()[i].playername;
            //Player.GetComponent<PlayerMovement>().CurrentRoute = CurrentRoute;

            Players.Add(Player);
            Player.transform.parent = PlayersParentObject.transform;
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
