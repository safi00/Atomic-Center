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
    public enum Map
    {
        Map1,
        Map2,
    }
    public enum PowerUP
    {
        BonusD6,
        BonusD10,
        StealD6,
        Teleport,
    }
    public enum Debuff
    {
        StolenD6,
        StolenPowerUP,
    }
    public class PlayerStats
    {
        public int playerid { get; set; }
        public string playername { get; set; }
        public int points { get; set; }
        public int location { get; set; }
        public List<Debuff> debuffs { get; set; }
        public List<PowerUP> powerups { get; set; }
    }

    [Header("Game Stats")]
    [SerializeField] public static int  PlayerAmount;
    [SerializeField] public static int  CurrentTurnNumber;
    [SerializeField] public static int  TurnLimit;
    [SerializeField] public static int  PointLimit;
    [SerializeField] public static bool isTurnNoLimit;
    [SerializeField] public static bool isPointNoLimit;
    [SerializeField] public static Map SelectedMap;
    [SerializeField] public static GameState CurrentGameState;

    [Header("Player Stats")]
    [SerializeField] public static List<PlayerStats> PlayersList;

    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {        
    }
    public static void SetPlayerAmount(int AmountPlayers)
    {
        PlayerAmount = AmountPlayers;      
    }
    public static void SetplayerList(List<PlayerStats> SavedList) 
    {
        PlayersList = new List<PlayerStats>();
        for (int i = 0; i < SavedList.Count; i++)
        {
            PlayersList.Add(SavedList[i]);
        }
    }
    public static void SetGameState(GameState GameState)
    {
        CurrentGameState = GameState;
    }
    public static void SetTurnLimit(int AmountTurns)
    {
        TurnLimit = AmountTurns;
    }
    public static void SetPointLimit(int AmountPoints)
    {
        PointLimit = AmountPoints;
    }
    public static void SetMap(int MapNumber)
    {
        switch (MapNumber)
        {
            case 1:
                SelectedMap = Map.Map1;
                break;
            case 2:
                SelectedMap = Map.Map2;
                break;
        }
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
        return PlayerAmount;
    }
    public static List<PlayerStats> GetPlayerList()
    {
        return PlayersList;
    }
    public static Map GetSelectedMap()
    {
        return SelectedMap;
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
    public static GameState GetGameState()
    {
        return CurrentGameState;
    }
    public static void reset()
    {
        PlayerAmount = 0;
        CurrentTurnNumber = 0;
        TurnLimit = 0;
        PointLimit = 0;
        SelectedMap = 0;    
    }
}