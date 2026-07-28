using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TooltipScript : MonoBehaviour
{
    private static TooltipScript instance;
    
    [SerializeField] private TMP_Text text;
    [SerializeField] private RectTransform background;

    private string tooltipText;

    public static bool isTooltipShown = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        hideTooltip();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;
    }


    private void showTooltip(string ttipText)
    {
        gameObject.SetActive(true);
        isTooltipShown = true;
        tooltipText = ttipText;
        text.text = tooltipText;
        float padding = 15f;
        Vector2 bgSize = new Vector2(text.preferredWidth+padding*2, text.preferredHeight+padding);
        background.sizeDelta = bgSize;
    }

    private void hideTooltip()
    {
        gameObject.SetActive(false);
        isTooltipShown = false;
    }

    public static void ShowTooltip(string text)
    {
        instance.showTooltip(text);
    }

    public static void HideTooltip()
    {
        instance.hideTooltip();
    }

    public static string GetActiveText()
    {
        return instance.tooltipText;
    }
}
