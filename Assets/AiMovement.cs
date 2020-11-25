using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiMovement : MonoBehaviour
{
    private float rotationSpeed = 2f;
    public GameObject player;
    
    private Quaternion _lookRotation;
    private Vector3 _direction;
    
    public GameObject m_boulet;

    private float shootTimer;
    private float shootColldown = 2f;
    private Grid GridReference;
    private List<Node> path;
    

    private float reactionTime = 1f;
    private AiState currentState;

    private float maxRange;

    private Vector3 lastSeenPos;
    private Vector3 target;
    
    void Awake()
    {
        player = GameObject.Find("Joueur 1");
        shootTimer = shootColldown;
    }

    private void Start()
    {
        path = new List<Node>();
        maxRange = 40f;
        shootTimer = 0f;
        GridReference = GameObject.Find("Obstacles").GetComponent<Grid>();
        lastSeenPos = player.transform.position;
        target = player.transform.position;
        currentState = AiState.SEARCHING;
    }

    // Update is called once per frame
    void Update()
    {
        Reload();
        
        ChooseNextAction();
        switch (currentState)
        {
            case AiState.SEARCHING:
                TravelAlongPath();
                break;
            case AiState.SHOOTING:
                RotateToPoint(target);
                Shoot();
                break;
        }
        
        
    }

    private void ChooseNextAction()
    {
        reactionTime -= Time.deltaTime;
        if (reactionTime < 0)
        {
            reactionTime = 1f;
            if (PlayerInRangeAndSight())
            {
                currentState = AiState.SHOOTING;
                target = player.transform.position;
            }
            else
            {
                currentState = AiState.SEARCHING;
                FindPath(transform.position, lastSeenPos);
            }
        }
    }

    #region PathFinding

    private void TravelAlongPath()
    {
        if (path.Count > 0)
        {
            Node nextNode = path[0];
            Vector3 travelPoint = GridReference.WorldPointFromNode(nextNode);
            var canMove = RotateToPoint(travelPoint);
            if (canMove)
            {
                transform.position += transform.forward * 5 * Time.deltaTime;
            }
            if (Vector3.Distance(travelPoint, transform.position) < 1)
            {
                path.Remove(nextNode);
            }
        }
    }

    private bool RotateToPoint(Vector3 travelPoint)
    {
        _direction = (travelPoint - transform.position).normalized;
        _lookRotation = Quaternion.LookRotation(_direction);
        var temp = transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * rotationSpeed);
        return temp == transform.rotation;
    }

    void FindPath(Vector3 a_StartPos, Vector3 a_TargetPos)
    {
        Node StartNode = GridReference.NodeFromWorldPoint(a_StartPos);
        Node TargetNode = GridReference.NodeFromWorldPoint(a_TargetPos);

        List<Node> OpenList = new List<Node>();
        HashSet<Node> ClosedList = new HashSet<Node>();

        OpenList.Add(StartNode);

        while(OpenList.Count > 0)
        {
            Node CurrentNode = OpenList[0];
            for(int i = 1; i < OpenList.Count; i++)
            {
                if (OpenList[i].FCost < CurrentNode.FCost || OpenList[i].FCost == CurrentNode.FCost && OpenList[i].ihCost < CurrentNode.ihCost)
                {
                    CurrentNode = OpenList[i];
                }
            }
            OpenList.Remove(CurrentNode);
            ClosedList.Add(CurrentNode);

            if (CurrentNode == TargetNode)
            {
                GetFinalPath(StartNode, TargetNode);
            }

            foreach (Node NeighborNode in GridReference.GetNeighboringNodes(CurrentNode))
            {
                if (!NeighborNode.bIsWall || ClosedList.Contains(NeighborNode))
                {
                    continue;//Skip it
                }
                int MoveCost = CurrentNode.igCost + GetManhattenDistance(CurrentNode, NeighborNode);

                if (MoveCost < NeighborNode.igCost || !OpenList.Contains(NeighborNode))
                {
                    NeighborNode.igCost = MoveCost;
                    NeighborNode.ihCost = GetManhattenDistance(NeighborNode, TargetNode);
                    NeighborNode.ParentNode = CurrentNode;

                    if(!OpenList.Contains(NeighborNode))
                    {
                        OpenList.Add(NeighborNode);
                    }
                }
            }

        }
    }



    void GetFinalPath(Node a_StartingNode, Node a_EndNode)
    {
        List<Node> FinalPath = new List<Node>();
        Node CurrentNode = a_EndNode;

        while(CurrentNode != a_StartingNode)
        {
            FinalPath.Add(CurrentNode);
            CurrentNode = CurrentNode.ParentNode;
        }

        FinalPath.Reverse();

        path = FinalPath;
    }

    int GetManhattenDistance(Node a_nodeA, Node a_nodeB)
    {
        int ix = Mathf.Abs(a_nodeA.iGridX - a_nodeB.iGridX);
        int iy = Mathf.Abs(a_nodeA.iGridY - a_nodeB.iGridY);

        return ix + iy;//Return the sum
    }
    #endregion

    #region Shooting

    private bool PlayerInRangeAndSight()
    {
        bool isInRangeAndSight = false;
        RaycastHit hit;
        if(Vector3.Distance(transform.position, player.transform.position) < maxRange )
        {
            if(Physics.Raycast(transform.position, (player.transform.position - transform.position), out hit, maxRange))
            {
                if(hit.transform == player.transform)
                {
                    isInRangeAndSight = true;
                    lastSeenPos = player.transform.position;
                }
            }
        }
        return isInRangeAndSight;
    }

    private void Reload()
    {
        if (shootTimer < shootColldown)
        {
            shootTimer += Time.deltaTime;
        }
    }
    private void Shoot()
    {
        if (shootTimer >= shootColldown)
        {
            m_boulet.GetComponent<physics>().speed = transform.forward*20f;
            Instantiate(m_boulet, 
                transform.position + transform.forward, 
                transform.rotation);
            shootTimer = 0f;
        }
    }
    
    #endregion
    
    private enum AiState
    {
        SEARCHING, SHOOTING
    }
}
