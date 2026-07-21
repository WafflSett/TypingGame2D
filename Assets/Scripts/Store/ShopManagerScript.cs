using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    public TMP_Text balanceText;

    private int balance;
    public int Balance
    {
        get { return balance; }
        set { balance = value; balanceText.text = $"Balance: ${Balance}"; }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Balance = GameManager.instance.Balance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
