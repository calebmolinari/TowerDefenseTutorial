using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    private Node target;
    public GameObject UI;
    public Button upgradeButton;
    public TextMeshProUGUI upgradeCost;
    public TextMeshProUGUI upgradeText;
    public TextMeshProUGUI sellCostText;

    public void SetTarget(Node node)
    {
        UI.SetActive(true);
        target = node;
        transform.position = target.GetBuildPosition();
        sellCostText.text = "$" + target.turretBlueprint.GetSellAmount();

        if (!target.isUpgraded)
        {
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost;
            upgradeButton.interactable = true;
        } else
        {
            upgradeCost.text = "MAX";
            upgradeButton.interactable = false;
        }
        
        
    }

    public void Hide()
    {
        UI.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }
}
