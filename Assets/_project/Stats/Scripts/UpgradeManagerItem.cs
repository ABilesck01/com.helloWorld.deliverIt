using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeManagerItem : MonoBehaviour
{
    [System.Serializable]
    public struct UpgradeManagerItemView
    {
        public GameObject screen;
        public TextMeshProUGUI txtName;
        public TextMeshProUGUI txtCost;
    }

    [SerializeField] private Transform visualPoint;
    [SerializeField] private UpgradeManagerItemView view;

    private Upgrade myUpgrade;

    public void AsignUpgrade(Upgrade upgrade)
    {
        myUpgrade = upgrade;
        Instantiate(upgrade.visual, visualPoint);
        view.txtName.text = upgrade.upgradeName;
        view.txtCost.text = upgrade.cost.ToString();
    }

    public void ShowScreen()
    {
        view.screen.SetActive(true);
    }

    public void HideScreen()
    {
        view.screen.SetActive(false);
    }

    public void BuyUpgrade(OnWorldCursor cursor)
    {
        Debug.Log("buy upgrade " + myUpgrade.name);
        StatAsset asset = cursor.GetCarController().GetStatAsset();
        asset.UnlockUpgrade(myUpgrade);
        Destroy(visualPoint.GetChild(0).gameObject);
        myUpgrade = null;
    }

}
