using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemScript : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text cost;

    public ShopItem myItem;
    public bool soldOut;
    private bool flashing;
    public void Setup()
    {
        image.sprite = myItem.icon;
        cost.text = $"${myItem.cost}";
    }

    public void Sell()
    {
        soldOut = true;
        cost.text = "Sold!";
        image.color = Color.gray;
    }

    public void ShowInsufficientFunds()
    {
        if (!flashing)
        {   
            StartCoroutine(FlashForSeconds(1f));
        }
    }

    IEnumerator FlashForSeconds(float seconds)
    {
        flashing = true;
        cost.gameObject.GetComponent<Animator>().SetTrigger("Start");
        yield return new WaitForSeconds(seconds);
        cost.gameObject.GetComponent<Animator>().SetTrigger("Stop");
        flashing = false;
    }
}
