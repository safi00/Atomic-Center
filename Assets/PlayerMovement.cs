using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("MovementStat")]
    [SerializeField] private int  RoutePosition;
    [SerializeField] public  int  RouteSteps;
    [SerializeField] private bool isMoving;
    [SerializeField] private bool isRolled;

    [Header("Route")]
    [SerializeField] public NodesController CurrentRoute;

    [Header("MISC")]
    [SerializeField] public WaitForSeconds TimeDelay = new WaitForSeconds(0.15f);

    // Start is called before the first frame update
    void Start()
    {        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && !isMoving)
        {
            RouteSteps = Random.Range(1,7);
            Debug.Log("Rolled: " + RouteSteps);
            isRolled = true;
            if (isRolled)
            {
                StartCoroutine(Move());
                isRolled= false;
            }
        }
    }

    IEnumerator Move()
    {
        if (isMoving) 
        {
            yield break;
        }
        isMoving = true;
        while (RouteSteps > 0) 
        {
            Vector3 NextNode = CurrentRoute.ChildNodeList[RoutePosition + 1].position;
            while (MoveToNextNode(NextNode))
            {
                yield return null;
            }
            yield return TimeDelay;
            RouteSteps--;
            RoutePosition++;
        }
        isMoving = false;
    }
    private bool MoveToNextNode(Vector3 Destination) 
    {
        return Destination != (transform.position = Vector3.MoveTowards(transform.position, Destination, 2f * Time.deltaTime));
    }
}
