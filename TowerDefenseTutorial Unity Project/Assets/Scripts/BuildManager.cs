using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public PlayerData playerData;
    public WaveData waveData;
    public GameObject buildEffect;
    public GameObject sellEffect;
    public NodeUI nodeUI;

    private void Awake()
    {
        playerData.money = waveData.startingMoney;
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in Scene!");
        }
        instance = this;
    }

    private TurretBlueprint turretToBuild;
    private Node selectedNode;





    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool CanAfford { get { return playerData.money >= turretToBuild.cost; } }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
       DeselectNode();
    }

    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }
            
        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }
}
