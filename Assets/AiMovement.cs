﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiMovement : MonoBehaviour
{
    private float rotationSpeed = 4f;
    public GameObject player;
    
    private Quaternion _lookRotation;
    private Vector3 _direction;
    
    public GameObject m_boulet;

    private float shootTimer;
    public float shootColldown = 2f;
    private bool shotReady = true;
    private Grid GridReference;
    private List<Node> path;
    
    void Awake()
    {
        player = GameObject.Find("Joueur 1");
        shootTimer = shootColldown;
    }

    private void Start()
    {
        path = new List<Node>();

        GridReference = GameObject.Find("Obstacles").GetComponent<Grid>();
        FindPath(transform.position, player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        FindPath(transform.position, player.transform.position);
        TravelAlongPath();
    }

    private void TravelAlongPath()
    {
        if (path.Count > 0)
        {
            Node nextNode = path[0];
            Vector3 travelPoint = GridReference.WorldPointFromNode(nextNode);
            RotateToPoint(travelPoint);
            transform.position += transform.forward * 5 * Time.deltaTime;
            if (Vector3.Distance(travelPoint, transform.position) < 1)
            {
                path.Remove(nextNode);
            }
        }
    }

    private void RotateToPoint(Vector3 travelPoint)
    {
        _direction = (travelPoint - transform.position).normalized;
        _lookRotation = Quaternion.LookRotation(_direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * rotationSpeed);
    }

    private void Shoot()
    {
        m_boulet.GetComponent<physics>().speed = transform.forward*20f;
        Instantiate(m_boulet, 
            transform.position + transform.forward, 
            transform.rotation);
        shotReady = false;
        shootTimer = 0;
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
}
