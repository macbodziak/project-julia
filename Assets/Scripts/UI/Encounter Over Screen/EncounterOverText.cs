using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EncounterOverText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textGUI;
    [SerializeField] private string victoryText = "Victory";
    [SerializeField] private string defeatText = "Defeat";
    [SerializeField] private float speed = 2.0f;
    public bool IsTransitioning { get; private set; }

    public void Show(bool playerWon)
    {
        if (playerWon)
        {
            textGUI.text = victoryText;
        }
        else
        {
            textGUI.text = defeatText;
        }
        StartCoroutine(FadeIn());
    }

    public void Hide()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {
        IsTransitioning = true;
        float startPositionY = -75f;
        float targetPositionY = 0f;
        float timer = 0f;
        Vector3 currentPosition = textGUI.rectTransform.localPosition;
        Color currentColor = textGUI.color;
        while (timer < 1f)
        {
            timer += Time.deltaTime * speed;
            currentPosition.y = Mathf.SmoothStep(startPositionY, targetPositionY, timer);
            currentColor.a = Mathf.SmoothStep(0, 1, timer);

            textGUI.rectTransform.localPosition = currentPosition;
            textGUI.color = currentColor;
            yield return null;
        }
        IsTransitioning = false;
        yield return null;
    }

    private IEnumerator FadeOut()
    {
        IsTransitioning = true;
        float startPositionY = 0f;
        float targetPositionY = 75f;
        float timer = 0f;
        Vector3 currentPosition = textGUI.rectTransform.localPosition;
        Color currentColor = textGUI.color;
        while (timer < 1f)
        {
            timer += Time.deltaTime * speed;
            currentPosition.y = Mathf.SmoothStep(startPositionY, targetPositionY, timer);
            currentColor.a = Mathf.SmoothStep(1, 0, timer);

            textGUI.rectTransform.localPosition = currentPosition;
            textGUI.color = currentColor;
            yield return null;
        }
        IsTransitioning = false;
        yield return null;
    }
}


