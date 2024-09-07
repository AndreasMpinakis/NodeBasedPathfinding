using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Utils : MonoBehaviour
{
    #region Singleton

    public static Utils Instance;

    private void Awake() // called zero
    {
        if (Instance == null)
        {
            //First run, set the instance
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            //Instance is not the same as the one we have, destroy old one, and reset to newest one
            Destroy(Instance.gameObject);
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    #endregion Singleton

    #region Fields ----------------------------------------------------------------

    [SerializeField] private Transform waypointsSystem;

    #region Nodes -----------------------------------------------------------------

    [SerializeField] private Node quarry;
    public Node GetQuarry() => quarry;

    [SerializeField] private Node temple;
    public Node GetTemple() => temple;

    [SerializeField] private Node tavern;
    public Node GetTavern() => tavern;

    [SerializeField] private Node elodinsManor;
    public Node GetElodinsManor() => elodinsManor;

    [SerializeField] private Node market;
    public Node GetMarket() => market;

    [SerializeField] private Node hospital;
    public Node GetHospital() => hospital;

    [SerializeField] private Node blacksmith;
    public Node GetBlacksmith() => blacksmith;

    [SerializeField] private Node woodcuttersLodge;
    public Node GetWoodcuttersLodge() => woodcuttersLodge;

    [SerializeField] private Node woodcuttersWoodDeposit;
    public Node GetWoodcuttersWoodDeposit() => woodcuttersWoodDeposit;
    
    [SerializeField] private Node woodcuttersChoppingPoint;
    public Node GetWoodcuttersChoppingPoint() => woodcuttersChoppingPoint;

    [SerializeField] private Node farms;
    public Node GetFarms() => farms;

    #endregion Nodes --------------------------------------------------------------

    #region Other Points Of Interest ----------------------------------------------

    [SerializeField] private Node square;
    public Node GetSquare() => square;

    [SerializeField] private Node forestClearing;
    public Node GetForestClearing() => forestClearing;

    [SerializeField] private Node lake;
    public Node GetLake() => lake;

    #endregion Other Points Of Interest -------------------------------------------

    #endregion Fields -------------------------------------------------------------


    #region Methods

    public Node GetNearestNode(Vector3 charactersPosition)
    {
        float minDistance = Vector3.Distance(waypointsSystem.GetChild(0).position, charactersPosition);
        Transform closestNode = waypointsSystem.GetChild(0);

        float currentDistance = 0f;
        for (int i = 1; i < waypointsSystem.childCount; i++)
        {
            var node = waypointsSystem.GetChild(i);
            currentDistance = Vector3.Distance(node.position, charactersPosition);
            if (currentDistance < minDistance)
            {
                minDistance = currentDistance;
                closestNode = node;
            }
        }

        return closestNode.GetComponent<Node>();
    }

    public void ClearNodeCosts()
    {
        foreach (Transform t in waypointsSystem.transform)
        {
            t.GetComponent<Node>().ClearCosts();
        }
    }

    #endregion Methods

}
