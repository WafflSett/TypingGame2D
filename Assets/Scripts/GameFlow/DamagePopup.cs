using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    private TMP_Text textMesh;
    private float lifeTime = 0.25f;
    private const float MAX_LIFETIME = 0.25f;
    private Color textColor;

    public static DamagePopup Create(Vector3 position, string damage, bool isCritical = false)
    {
        GameObject popupObject = Instantiate(Resources.Load("Prefabs/DamageNumber", typeof(GameObject)) as GameObject, position, Quaternion.identity);
        DamagePopup damagePopup = popupObject.GetComponent<DamagePopup>();
        damagePopup.Setup(damage, isCritical);
        return damagePopup;
    }

    private void Awake()
    {
        textMesh = gameObject.GetComponent<TMP_Text>();
    }

    public void Setup(string damageAmount, bool isCrit)
    {
        textMesh.text = damageAmount.ToString();
        if (isCrit)
        {
            textMesh.fontSize = 6;
        }
        else
        {
            textMesh.fontSize = 4;
        }
        textColor = textMesh.color;
    }


    private void Update()
    {
        float moveYSpeed = 2f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;
        lifeTime -= Time.deltaTime;

        if (lifeTime > MAX_LIFETIME * .5f)
        {
            transform.localScale += Vector3.one * 0.4f *  Time.deltaTime;
        }
        else
        {
            transform.localScale -= Vector3.one * Time.deltaTime;
        }

        if (lifeTime < 0)
        {
            float disappearSpeed = 10f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a <=0)
            {
                Destroy(gameObject);
            }
        }
    }

}
