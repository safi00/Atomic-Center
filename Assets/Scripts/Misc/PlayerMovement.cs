using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("PlayerStats")]
    [SerializeField] public PlayerHierarchy PlayerHier;
    [SerializeField] public string PlayerName;

    [Header("MovementStats")]
    [SerializeField] private int   MaxSteps;
    [SerializeField] private float MovementSpeed;

    [Header("Hidden MovementStats")]
    [HideInInspector] public  int  RouteSteps;
    [HideInInspector] private int  RoutePosition;
    [HideInInspector] private bool isMoving;
    [HideInInspector] private bool isPlayerOrderRolled;

    [Header("Route")]
    [SerializeField] public NodesController CurrentRoute;

    [Header("MISC")]
    [HideInInspector] public WaitForSeconds TimeDelay = new WaitForSeconds(0.25f);

    // Start is called before the first frame update
    void Start()
    {        

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && !isMoving)
        {
            switch (isPlayerOrderRolled) 
            {
                case true:
                    Roll();
                    break;
                case false:
                    RollHeir();
                    break;
            }
        }
    }
    private void RollHeir()
    {
        RouteSteps = Random.Range(1, (10 + 1));
        StartCoroutine(DecideTurnOrder());
    }
    private void Roll()
    {
        RouteSteps = Random.Range(1, (MaxSteps + 1));
        Debug.Log("Rolled: " + RouteSteps);
        StartCoroutine(Move());        
    }
    IEnumerator DecideTurnOrder()
    {
        if (isPlayerOrderRolled)
        {
            yield break;
        }
        isPlayerOrderRolled = true;
        if (CurrentRoute != null)
        {
            while (RouteSteps > 0)
            {
                RoutePosition++;
                RoutePosition %= CurrentRoute.ChildNodeList.Count;

                Vector3 NextNode = CurrentRoute.ChildNodeList[RoutePosition].position;
                while (MoveToNextNode(NextNode))
                {
                    yield return null;
                }
                yield return TimeDelay;
                RouteSteps--;
            }
        }
        else
        {
            Debug.Log("No Route");
        }
        isMoving = false;
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

                Vector3 NextNode = CurrentRoute.ChildNodeList[RoutePosition].position;
                while (MoveToNextNode(NextNode))
                {
                    yield return null;
                }
                yield return TimeDelay;
                RouteSteps--;
            }
        }
        else
        {
            Debug.Log("No Route");
        }
        isMoving = false;
    }
    private bool MoveToNextNode(Vector3 Destination) 
    {
        return Destination != (transform.position = Vector3.MoveTowards(transform.position, Destination, MovementSpeed * Time.deltaTime));
    }
    public void SetPlayerHierarchy(PlayerHierarchy PHeir) 
    {
        PlayerHier = PHeir;
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
