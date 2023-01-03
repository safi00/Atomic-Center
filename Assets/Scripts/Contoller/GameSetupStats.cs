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
    [SerializeField] public static int  Players;
    [SerializeField] public static int  CurrentTurnNumber;
    [SerializeField] public static int  TurnLimit;
    [SerializeField] public static int  PointLimit;
    [SerializeField] public static bool isTurnNoLimit;
    [SerializeField] public static bool isPointNoLimit;
    [SerializeField] public static GameState CurrentGameState;

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
    public static void NoTurnLimitSwitch()
    {
        isTurnNoLimit = !isTurnNoLimit;
    }
    public static void NoPointLimitSwitch()
    {
        isPointNoLimit = !isPointNoLimit;
    }
    public static void TurnOnNoTurnLimitSwitch()
    {
        isTurnNoLimit = true;
    }
    public static void TurnOnNoPointLimitSwitch()
    {
        isPointNoLimit = true;
    }
    public static int GetPlayerAmount()
    {
        return Players;
    }
    public static int GetCurrentTurnNumber()
    {
        return CurrentTurnNumber;
    }
    public static int GetTurnLimit()
    {
        return TurnLimit;
    }
    public static int GetPointLimit()
    {
        return PointLimit;
    }
    public static bool GetTurnLimitBool()
    {
        return isTurnNoLimit;
    }
    public static bool GetPointLimitBool()
    {
        return isPointNoLimit;
    }
    public static void SetGameState(GameState GameState)
    {
        CurrentGameState = GameState;
    }
    public static void CheckGameState()
    {
        if (true)
        {

        }
    }
    public static GameState GetGameState()
    {
        return CurrentGameState;
    }
}