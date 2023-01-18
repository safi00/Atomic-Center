using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("GameStat")]
    [SerializeField] private int PlayerAmount;
    [SerializeField] private int RoundCapicity;
    [SerializeField] private int ScoreCapicity;

    [Header("PlayerPrefabList")]
    [SerializeField] private GameObject[] Players;

    [Header("GameStatsController")]
    [SerializeField] public GameSetupStats GameStats;
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
