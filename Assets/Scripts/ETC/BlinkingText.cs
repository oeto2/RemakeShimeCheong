using TMPro;
using UnityEngine;
using System.Collections;

public class BlinkingText : MonoBehaviour
{
    public float blinkTime = 1f;
    private TextMeshProUGUI textComponent;

    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        StartCoroutine(BlinkText());
    }

    IEnumerator BlinkText()
    {
        while (true)
        {
            for (float t = 0; t <= 1; t += Time.deltaTime / blinkTime)
            {
                textComponent.color = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, Mathf.Lerp(1, 0, t));
                yield return null;
            }

            for (float t = 0; t <= 1; t += Time.deltaTime / blinkTime)
            {
                textComponent.color = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, Mathf.Lerp(0, 1, t));
                yield return null;
            }
        }
    }
}
