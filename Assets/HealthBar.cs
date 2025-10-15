using TMPro;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public RectTransform rect;
    public TextMeshProUGUI healthText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHealth(float current, float max)
    {
        float scale = Mathf.Clamp(current, 0, Mathf.Infinity) / Mathf.Clamp(max, 1, Mathf.Infinity);
        //rect.sizeDelta = new Vector2(scale, 0);
        rect.transform.localScale = new Vector3(scale, rect.transform.localScale.y, rect.transform.localScale.z);

        if(healthText)
        {
            healthText.text = (int)current + " / " + (int)max;
        }
    }
}
