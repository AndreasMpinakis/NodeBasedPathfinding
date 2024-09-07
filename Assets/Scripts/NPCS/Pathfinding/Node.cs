using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Node : MonoBehaviour
{
    public static int Id { get; private set; }

    [Header("--- Fields ---")]
    public bool isEndNode;

    [SerializeField] private float g;
    public float G
    {
        get { return g; }
        set { g = value; }
    }

    [SerializeField] private float h;
    public float H
    {
        get { return h; }
        set { h = value; }
    }

    public float F => G + H;

    [SerializeField] private List<Node> _neighboringNodes;
    public List<Node> NeighboringNodes
    {
        get { return _neighboringNodes; }
        private set { _neighboringNodes = value; }
    }

    [SerializeField] private Node _connectionNode;
    public Node ConnectionNode
    {
        get { return _connectionNode; }
        private set { _connectionNode = value; }
    }

    public void SetConnectionNode(Node node)
    {
        ConnectionNode = node;
    }

    [Header("--- Gizmos ---")]
    [SerializeField] private Color waypointColor = Color.blue;
    [SerializeField] private Color lineColor = Color.green;

    /// <summary>
    /// Sets the new G and returns True if the new F value is less than the previous.
    /// </summary>
    /// <param name="g"></param>
    /// <returns></returns>
    public bool SetG(float g)
    {
        var oldF = F;
        G = g;

        if (F < oldF)
        {
            return true;
        }
        return false;
    }

    public void SetH(float h)
    {
        if (H != 0 && h > H) { return; }
        H = h;
    }

    public void ClearCosts()
    {
        G = 0;
        H = 0;
    }

    private void Start()
    {
        Id = transform.GetSiblingIndex();
        GetComponentInChildren<TextMeshProUGUI>().SetText(Id.ToString());

        if (_neighboringNodes.Count != 0)
        {
            foreach (Node node in NeighboringNodes)
            {
                node.AddNeighboringNode(this);
            }
        }

        if (Id != 0)
        {
            var node = transform.parent.GetChild(transform.GetSiblingIndex() - 1).GetComponent<Node>();
            if (!node.isEndNode)
                AddNeighboringNode(transform.parent.GetChild(transform.GetSiblingIndex() - 1).GetComponent<Node>());
        }

        if (isEndNode) { return; }

        if (transform.GetSiblingIndex() < transform.parent.childCount - 1)
            AddNeighboringNode(transform.parent.GetChild(transform.GetSiblingIndex() + 1).GetComponent<Node>());
    }

    public void AddNeighboringNode(Node node)
    {
        if (!_neighboringNodes.Contains(node))
        {
            _neighboringNodes.Add(node);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = waypointColor;
        Gizmos.DrawCube(transform.position, new Vector3(.1f, .1f, .1f));

        Gizmos.color = lineColor;

        foreach (Node node in _neighboringNodes)
        {
            Gizmos.DrawLine(transform.position, node.transform.position);
        }
    }
}
