using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarWallet : MonoBehaviour
{
    private int money;
    [SerializeField] private TextMeshProUGUI txtMoney; 
    private void OnEnable()
    {
        HousePlacedObject.OnTaskDelivered += HousePlacedObject_OnTaskDelivered;
    }

    private void OnDisable()
    {
        HousePlacedObject.OnTaskDelivered -= HousePlacedObject_OnTaskDelivered;
    }

    private void HousePlacedObject_OnTaskDelivered(object sender, HousePlacedObject e)
    {
        Task t = e.GetTask();
        AddMoney(t.Money);

        
    }

    public bool HasMoney(int amount)
    {
        return amount <= money;
       
    }

    public void SpendMoney(int amount)
    {
        if(!HasMoney(amount))
        {
            return;
        }

        money -= amount;
        txtMoney.text=money.ToString();
    }

    public void AddMoney(int amount)
    {
        money += amount;
        txtMoney.text = money.ToString();
    }

}
