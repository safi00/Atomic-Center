using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameSetupStats;

public class PlayerMovement : MonoBehaviour
{
    [Header("PlayerStats")]
    [SerializeField] public PlayerHierarchy PlayerHier;
    [SerializeField] public string PlayerName;
    [SerializeField] public int PlayerScore;
    [SerializeField] public int PlayerLocation;
    [SerializeField] public List<BuffsAndDebuffs> PlayerBuffsAndDebuffsList;

    [Header("MovementStats")]
    [SerializeField] private int MinSteps;
    [SerializeField] private int MaxSteps;
    [SerializeField] private int DiceChanger;
    [SerializeField] private float MovementSpeed;

    [Header("Hint Stats")]
    [SerializeField] public int CurrentElementIndex;
    [SerializeField] public int AmountofHints;

    [Header("Hidden MovementStats")]
    [HideInInspector] public  int  RouteSteps;
    [HideInInspector] public  int  RoutePosition;
    [HideInInspector] private bool isMoving;
    [HideInInspector] public  bool isPlayerOrderRolled;

    [Header("Route")]
    [SerializeField] public NodesController CurrentRoute;

    [Header("Events")]
    [HideInInspector] public IEvent PassGO;
    [HideInInspector] public IEvent Log;
    [HideInInspector] public IEvent Trigger;

    [Header("MISC")]
    [HideInInspector] public WaitForSeconds TimeDelay = new WaitForSeconds(0.25f);
    [HideInInspector] public int TempRoll;
    [HideInInspector] private Animator Anim;
    [HideInInspector] private Coroutine LookCoroutine;
    [SerializeField] public Transform TargetToLookAt;
    [SerializeField] public float Speed = 5f;
    [HideInInspector] public static bool didCharacterSetup;

    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (!didCharacterSetup)
        {
            Anim = gameObject.GetComponentInChildren<Animator>();
            MinSteps = 1;
        }
        WalkingAnimation();
    }
    public void RollHeir()
    {
        TempRoll = Random.Range(1, (10 + 1));
        GameController.AddRollOrder(TempRoll);
        GameController.LogString = PlayerName + " Rolled: " + TempRoll + " for roll order";
        LogEvent();
        isPlayerOrderRolled = true;
    }
    public void Roll()
    {
        DiceChanger = BuffsAndDebuffsCalculator();
        RouteSteps = (Random.Range(MinSteps, (MaxSteps + 1)) + DiceChanger);
        if (RouteSteps <= 0)
        {
            RouteSteps = 0;
        }
        if (DiceChanger == 0)
        {
            GameController.LogString = PlayerName + " Rolled: " + RouteSteps;
        }
        else
        {
            GameController.LogString = PlayerName + " Rolled: " + RouteSteps + "  ( With " + DiceChanger + " )";
        }
        LogEvent(); 

        CurrentElementIndex = -1;
        AmountofHints = 0;

        StartCoroutine(Move());
    }
    public void Stay()
    {
        GameController.LogString = PlayerName + " Stayed";
        LogEvent();

        PlayerLocation = RoutePosition;
        GameController.NodeEventType = CurrentRoute.ChildNodeTypeList[RoutePosition];
        EventTriggerEvent();
    }
    private int BuffsAndDebuffsCalculator()
    {
        int DiceChangerInt = 0;
        int BnDListCount = PlayerBuffsAndDebuffsList.Count;
        if (BnDListCount > 0)
        {
            for (int i = 0; i < BnDListCount; i++)
            {
                switch (PlayerBuffsAndDebuffsList[i])
                {
                    case BuffsAndDebuffs.MinusD6:
                        DiceChangerInt = DiceChangerInt - Random.Range(1, (6 + 1));
                        break;
                    case BuffsAndDebuffs.Minus3:
                        DiceChangerInt = DiceChangerInt - 3;
                        break;
                    case BuffsAndDebuffs.PlusD6:
                        DiceChangerInt = DiceChangerInt + Random.Range(1, (6 + 1));
                        break;
                    case BuffsAndDebuffs.Plus3:
                        DiceChangerInt = DiceChangerInt + 3;
                        break;
                }
            }
        }
        PlayerBuffsAndDebuffsList.Clear();
        return DiceChangerInt;
    }
    IEnumerator Move()
    {
        if (isMoving) 
        {
            yield break;
        }
        isMoving = true;
        if (CurrentRoute != null)
        {
            while (RouteSteps > 0)
            {
                RoutePosition++;
                RoutePosition %= CurrentRoute.ChildNodeList.Count;

                Transform NextNodeTransform = CurrentRoute.ChildNodeList[RoutePosition].transform;
                TargetToLookAt = NextNodeTransform;

                CheckIfPassedStart();

                while (MoveToNextNode(NextNodeTransform.position))
                {
                    yield return null;
                }
                yield return TimeDelay;
                RouteSteps--;
            }
            PlayerLocation = RoutePosition;
            GameController.NodeEventType = CurrentRoute.ChildNodeTypeList[RoutePosition];
            EventTriggerEvent();
        }
        else
        {
            Debug.Log("No Route");
        }
        isMoving = false;
    }
    private void CheckIfPassedStart()
    {
        if (RoutePosition == 0)
        {
            PointUPEvent();
        }
    }
    private void WalkingAnimation()
    {
        if (isMoving)
        {
            Anim.SetInteger("AnimationPar", 1);
        }
        else
        {
            Anim.SetInteger("AnimationPar", 0);
        }
    }
    public void StartRotating()
    {
        if (LookCoroutine != null)
        {
            StopCoroutine(LookCoroutine);
        }
        LookCoroutine = StartCoroutine(LookAt());
    }
    private IEnumerator LookAt()
    {
        Quaternion lookRotation = Quaternion.LookRotation(TargetToLookAt.position - transform.position);

        float time = 0;

        Quaternion initialRotation = transform.rotation;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(initialRotation, lookRotation, time);

            time += Time.deltaTime * Speed;

            yield return null;
        }
    }
    private bool MoveToNextNode(Vector3 Destination)
    {
        StartRotating();
        return Destination != (transform.position = Vector3.MoveTowards(transform.position, Destination, MovementSpeed * Time.deltaTime));
    }
    public void SetPlayerHierarchy(PlayerHierarchy PHeir) 
    {
        PlayerHier = PHeir;
    }
    private void PointUPEvent()
    {
        if (PassGO != null)
        {
            PassGO.playEvent("PointUP");
        }
    }
    private void EventTriggerEvent()
    {
        if (Trigger != null)
        {
            Trigger.playEvent("EventTriggered");
        }
    }
    private void LogEvent()
    {
        if (Log != null)
        {
            Log.playEvent("Log");
        }
    }
    public enum PlayerHierarchy
    {
        Player1,
        Player2,
        Player3,
        Player4,
        Player5,
    }
}
