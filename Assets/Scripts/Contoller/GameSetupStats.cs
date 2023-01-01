using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetupStats : MonoBehaviour
{
    public enum GameState
    {
        Setup,
        Playing,
        Paused,
        GameOver,
        Stats,
    }
    public enum PowerUP
    {
        BonusD6,
        BonusD10,
        StealD6,
        Teleport,
    }
    public class Player
    {
        public int playerid { get; set; }
        public string playername { get; set; }
        public int points { get; set; }
        public int location { get; set; }
        public List<string> debuffs { get; set; }
        public List<PowerUP> powerups { get; set; }
    }

    [Header("Game Stats")]
    [SerializeField] public static GameState CurrentGameState;
    [SerializeField] public static int  Players;
    [SerializeField] public static int  TurnLimit;
    [SerializeField] public static int  PointLimit;
    [SerializeField] public static bool isTurnNoLimit;
    [SerializeField] public static bool isPointNoLimit;

    [Header("Player Stats")]
    [SerializeField] public static List<Player> PlayersList;


    // Start is called before the first frame update
    void Start()
    {        
    }
    // Update is called once per frame
    void Update()
    {        
    }

    public static void SetPlayers(int AmountPlayers)
    {
        Players = AmountPlayers;
    }
    public static void SetTurnLimit(int AmountTurns)
    {
        TurnLimit = AmountTurns;
    }
    public static void SetPointLimit(int AmountPoints)
    {
        PointLimit = AmountPoints;
    }
    public static void SetTurnsUnlimit(bool isItUnlimit)
    {
        isTurnNoLimit = isItUnlimit;
    }
    public static void SetPointsUnlimit(bool isItUnlimit)
    {
        isPointNoLimit = isItUnlimit;
    }
    public static void CheckPointLimit()
    {
        if (!isTurnNoLimit)
        {

        }
    }
    public static void CheckTurnLimit()
    {
        if (!isPointNoLimit)
        {
            
        }
    }
    public static void CheckGameState()
    {
    }
}