using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Metadata;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text balanceText;

    [SerializeField]
    private GameObject slotsParent;

    [SerializeField]
    private GameObject itemPrefab;

    private int balance;
    public int Balance
    {
        get { return balance; }
        set { balance = value; balanceText.text = $"Balance: ${value}"; }
    }

    private ShopItem[] allItems;
    private List<ShopItem> availableItems;
    private int maxShopSlots = 5;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Balance = GameManager.instance.Balance;
        allItems = Resources.LoadAll<ShopItem>("Items");
        availableItems = allItems.ToList();
        RefreshItems();
    }

    private void Update()
    {
        CheckForTooltips();
    }

    private void CheckForTooltips()
    {
        var children = slotsParent.GetComponentsInChildren<RectTransform>();
        Vector2 mouse = Input.mousePosition;
        bool shouldTooltipShow = false;
        foreach (var item in children)
        {
            if (item.gameObject.Equals(slotsParent)) continue;
            if (RectTransformUtility.RectangleContainsScreenPoint(item.GetComponent<RectTransform>(), mouse))
            {
                ItemScript itemScript = item.GetComponent<ItemScript>();
                if (itemScript == null) continue;
                string title = itemScript.myItem.title;
                if (!TooltipScript.isTooltipShown || title != TooltipScript.GetActiveText())
                {
                    TooltipScript.ShowTooltip(title);
                }
                shouldTooltipShow = true;
                if (Input.GetMouseButton(0) && !itemScript.soldOut)
                {
                    if (Balance >= itemScript.myItem.cost)
                    {
                        itemScript.Sell();
                        Balance -= itemScript.myItem.cost;
                        availableItems.Remove(itemScript.myItem);
                    }
                    else
                    {
                        itemScript.ShowInsufficientFunds();
                    }
                }

                break;
            }
        }

        if (!shouldTooltipShow)
        {
            TooltipScript.HideTooltip();
        }
    }

    public void RefreshItems()
    {
        var children = slotsParent.GetComponentsInChildren<RectTransform>();
        foreach (var item in children)
        {
            if (item.gameObject.Equals(slotsParent)) continue;
            Destroy(item.gameObject);
        }
        ShopItem[] selectedItems = selectItems();

        foreach (var item in selectedItems)
        {
            GameObject newItem = Instantiate(itemPrefab, slotsParent.transform.position, Quaternion.identity);
            newItem.transform.SetParent(slotsParent.transform);
            ItemScript itemscript = newItem.GetComponent<ItemScript>();
            itemscript.myItem = item;
            itemscript.Setup();
        }

    }

    private ShopItem[] selectItems()
    {
        List<ShopItem> selected = new List<ShopItem>();

        int shopSlots = Mathf.Min(maxShopSlots, availableItems.Count);

        for (int i = 0; i < shopSlots; i++)
        {
            int index = Random.Range(0, availableItems.Count - 1);
            if (selected.Contains(availableItems[index]))
            {
                i--;
                continue;
            }
            selected.Add(availableItems[index]);
        }

        return selected.ToArray();
    }
}
