using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color buildColor;
    public Color buildFailColor;
    private Color startColor;

    private Renderer rend;
    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;
    public Vector3 turretOffset;

    BuildManager buildManager;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + turretOffset;
    }

    public void SellTurret()
    {
        buildManager.playerData.money += turretBlueprint.GetSellAmount();
        Destroy(turret);
        turretBlueprint = null;
        isUpgraded = false;

        GameObject effect = Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (!buildManager.CanBuild)
        {
            return;
        }
        if (buildManager.CanAfford)
        {
            rend.material.color = buildColor;
        }
        else
        {
            rend.material.color = buildFailColor;
        }
        

    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        

        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
        {
            return;
        }

        BuildTurret(buildManager.GetTurretToBuild());
    }

    void BuildTurret(TurretBlueprint blueprint)
    {
        if (buildManager.playerData.money < blueprint.cost)
        {
            Debug.Log("Insufficient funds");
            return;
        }
        buildManager.playerData.money -= blueprint.cost;

        GameObject _turret = Instantiate(blueprint.prefab, GetBuildPosition(), transform.rotation);
        turret = _turret;

        turretBlueprint = blueprint;

        GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
    }

    public void UpgradeTurret()
    {
        if (buildManager.playerData.money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Insufficient funds");
            return;
        }
        buildManager.playerData.money -= turretBlueprint.upgradeCost;

        Destroy(turret);
        GameObject _turret = Instantiate(turretBlueprint.upgradePrefab, GetBuildPosition(), transform.rotation);
        turret = _turret;
        GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
        isUpgraded = true;
    }
}
