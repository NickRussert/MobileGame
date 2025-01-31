using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class BlinkingText : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private Image image;
    public float blinkSpeed = 0.5f; // Speed of blinking

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        image = GetComponent<Image>();

        StartCoroutine(BlinkEffect());
    }

    IEnumerator BlinkEffect()
    {
        while (true)
        {
            if (textMesh != null) textMesh.enabled = !textMesh.enabled;
            if (image != null) image.enabled = !image.enabled;

            yield return new WaitForSeconds(blinkSpeed);
        }
    }
}
