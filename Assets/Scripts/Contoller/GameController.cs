using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
