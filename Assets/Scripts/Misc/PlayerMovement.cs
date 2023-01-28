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
    [SerializeField] public List<Debuff> PlayerDebuffList;
    [SerializeField] public List<PowerUP> PlayerPowerUPList;

    [Header("MovementStats")]
    [SerializeField] private int   MaxSteps;
    [SerializeField] private float MovementSpeed;

    [Header("Hidden MovementStats")]
    [HideInInspector] public  int  RouteSteps;
    [HideInInspector] public  int  RoutePosition;
    [HideInInspector] private bool isMoving;
    [HideInInspector] public  bool isPlayerOrderRolled;

    [Header("Route")]
    [SerializeField] public NodesController CurrentRoute;

    [Header("MISC")]
    [HideInInspector] public WaitForSeconds TimeDelay = new WaitForSeconds(0.25f);
    [HideInInspector] public int TempRoll;
    [HideInInspector] private Animator Anim;
    [HideInInspector] private Coroutine LookCoroutine;
    [SerializeField] public Transform TargetToLookAt;
    [SerializeField] public float Speed = 3.5f;
    [SerializeField] public bool ongoingPrompt;
    [SerializeField] public IEvent PassGO;

    // Start is called before the first frame update
    void Start()
    {
        Anim = gameObject.GetComponentInChildren<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        WalkingAnimation();
    }
    public void RollHeir()
    {
        TempRoll = Random.Range(1, (10 + 1));
        GameController.AddRollOrder(TempRoll);
        Debug.Log(PlayerHier + " Rolled: " + TempRoll + " for roll order");
        isPlayerOrderRolled = true;
    }
    public void Roll()
    {
        RouteSteps = Random.Range(1, (MaxSteps + 1));
        Debug.Log(PlayerHier + " Rolled: " + RouteSteps);
        StartCoroutine(Move());        
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
    public enum PlayerHierarchy
    {
        Player1,
        Player2,
        Player3,
        Player4,
        Player5,
    }
}
