using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int rewardGold = 25;
    [SerializeField] int penaltyGold = 25;

    Bank bank;

    // Start is called before the first frame update
    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    public void GiveGold()
    {
        bank.Deposit(rewardGold);
    }

    public void StealGold()
    {
        bank.Withdraw(penaltyGold);
    }
}
