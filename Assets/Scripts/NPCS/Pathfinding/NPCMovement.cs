using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Handles NPCs Movement.
/// </summary>
public class NPCMovement : MonoBehaviour/*, IDataPersistence*/
{
    /// <summary>
    /// Starts from 1 since the Player does not have this script.
    /// </summary>
    public static int Id { get; private set; }

    [Header("-- Nodes --")]
    [SerializeField] private Node targetNode;
    [SerializeField] private List<Node> nodesToTravel = new List<Node>();

    private bool stopMovement;
    public bool TalkingToPlayer { get; private set; }

    [SerializeField] private float speed;
    [SerializeField] private float distanceThreshold = 0.1f;

    private void Update()
    {
        // Move
        if (targetNode == null || nodesToTravel == null || nodesToTravel.Count == 0) { return; }

        if (!stopMovement)
            transform.position = Vector3.MoveTowards(transform.position, nodesToTravel[0].transform.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, nodesToTravel[0].transform.position) < distanceThreshold)
        {
            nodesToTravel.RemoveAt(0);
        }
    }

    public void GoToTargetNode(Node target)
    {
        targetNode = target;

        // The nearest Node of the Character
        Node nearestNode = Utils.Instance.GetNearestNode(transform.position);

        // If nearest node is equals to your target node just move there
        if (nearestNode == targetNode)
        {
            nodesToTravel.Add(target);
            return;
        }

        // The Nodes you must travel in order to reach the given Activity
        nodesToTravel = GetPath(nearestNode, targetNode);
    }

    private List<Node> GetPath(Node startingNode, Node targetNode)
    {
        Utils.Instance.ClearNodeCosts();
        if (startingNode == targetNode) { return null; }

        //print($"{startingNode} --> {targetNode}");
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Calculate H
        List<Node> openNodes = new List<Node>();

        List<Node> visitedNodes = new List<Node>();
        visitedNodes.Add(targetNode);
        targetNode.SetH(0f);

        // For the first neighbors
        foreach (var neighborNode in targetNode.NeighboringNodes)
        {
            // Set Distances
            neighborNode.SetH(Vector3.Distance(targetNode.transform.position, neighborNode.transform.position));
            openNodes.Add(neighborNode);
        }

        while (openNodes.Count > 0)
        {
            foreach (var node in openNodes.ToList())
            {
                foreach (var neighborNode in node.NeighboringNodes)
                {
                    if (visitedNodes.Contains(neighborNode)) { continue; }

                    neighborNode.SetH(Vector3.Distance(node.transform.position, neighborNode.transform.position) + node.H);
                    openNodes.Add(neighborNode);
                }

                openNodes.Remove(node);
                visitedNodes.Add(node);
            }
        }
        //print("finished H");
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // A*
        openNodes.Clear();
        List<Node> closedNodes = new List<Node>();

        openNodes.Add(startingNode);

        while (openNodes.Count > 0)
        {
            var currentNode = openNodes[0];
            foreach (Node node in openNodes)
            {
                if (node.F < currentNode.F)
                    currentNode = node;
            }

            openNodes.Remove(currentNode);
            closedNodes.Add(currentNode);

            if (currentNode == targetNode)
            {
                //print("We found the path");
                break;
            }

            foreach (var neighborNode in currentNode.NeighboringNodes)
            {
                if (closedNodes.Contains(neighborNode)) { continue; }

                if (neighborNode.SetG(Vector3.Distance(currentNode.transform.position, neighborNode.transform.position) + currentNode.G)) ;
                neighborNode.SetConnectionNode(currentNode);

                if (!openNodes.Contains(neighborNode))
                {
                    openNodes.Add(neighborNode);
                }
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Get the Path
        List<Node> pathNodesList = new List<Node>();
        pathNodesList.Add(targetNode);

        var conndectedNode = targetNode.ConnectionNode;
        while (conndectedNode != startingNode)
        {
            pathNodesList.Add(conndectedNode);
            conndectedNode = conndectedNode.ConnectionNode;
        }
        pathNodesList.Add(startingNode);

        pathNodesList.Reverse();

        //StringBuilder sb = new StringBuilder();
        //foreach (var nodeToPrint in pathNodesList)
        //{
        //    sb.Append(nodeToPrint.name.ToString());
        //}
        //print(sb);

        return pathNodesList;
    }

    /// <summary>
    /// Starts or Stops the routine of this NPC
    /// </summary>
    public void ToggleNPCRoutine()
    {
        stopMovement = !stopMovement;
        TalkingToPlayer = !TalkingToPlayer;
    }

    // TODO : Handle save load
//    public void SaveData(GameData data)
//    {
//        if (waypoints == null) { return; }

//        data.allNPCsPositionsList[Id] = (new NPCPositions(Id, this.transform.position, currentWaypoint));
//    }

//    public void LoadData(GameData data)
//    {
//        if (waypoints == null) { return; }

//        if (data.allNPCsPositionsList[Id] == null)
//        {
//            // If you have no Waypoint, go the 1st one
//            currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
//            return;
//        }

//        foreach (NPCPositions NPCPosition in data.allNPCsPositionsList)
//        {
//            if (NPCPosition.characterID == Id)
//            {
//                this.transform.position = NPCPosition.position;

//                // If the currentWaypoint GO is destroyed, find it by name in the Waypoints parent Object.
//                if (NPCPosition.currentWaypoint != null)
//                {
//                    this.currentWaypoint = NPCPosition.currentWaypoint;
//                }
//                else
//                {
//                    this.currentWaypoint = waypoints.transform.Find(NPCPosition.currentWaypointName);
//                }

//                break;
//            }
//        }
//    }
}

[System.Serializable]
public class NPCPositions
{
    public int characterID;
    public Vector3 position;
    public Transform currentWaypoint;
    public string currentWaypointName;

    public NPCPositions(int id, Vector3 pos, Transform currentWaypoint)
    {
        this.characterID = id;
        this.position = pos;
        this.currentWaypoint = currentWaypoint;
        this.currentWaypointName = currentWaypoint.name;
    }
}