using UnityEngine;
using TMPro;
using System.Collections;

public class BlinkingText : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private AudioSource audioSource;

    [Header("Blink Settings")]
    public float blinkSpeed = 0.5f; // Speed of blinking

    [Header("Audio")]
    public AudioClip dingSound; // Assign in Inspector

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        audioSource = GetComponent<AudioSource>();

        StartCoroutine(BlinkEffect());
    }

    IEnumerator BlinkEffect()
    {
        while (true)
        {
            if (textMesh != null)
            {
                bool isNowVisible = !textMesh.enabled; // Check if text is turning ON

                textMesh.enabled = isNowVisible; // Toggle visibility

                //  Play ding sound ONLY when turning on
                if (isNowVisible && audioSource != null && dingSound != null)
                {
                    audioSource.PlayOneShot(dingSound);
                }
            }
            yield return new WaitForSeconds(blinkSpeed);
        }
    }
}
