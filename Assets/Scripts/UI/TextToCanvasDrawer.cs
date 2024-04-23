using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
// Summary
// Utility component
// This component draws Text to UI Elements such as a Canvas
// Should be attached to a UI object and be referenced from another object,
// that calls its methods
public class TextToCanvasDrawer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textPrefab;

    public TextMeshProUGUI DisplayTextAtGameObject(string text, GameObject gameObject, Vector2 offset)
    {

        Vector3 screenPosition = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        return DisplayTextAtScreenPosition(text, screenPosition, offset);
    }

    public TextMeshProUGUI DisplayTextAtScreenPosition(string text, Vector2 screenPosition, Vector2 offset)
    {
        Vector2 localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)transform, screenPosition, null, out localPosition);

        localPosition += offset;
        return DisplayText(text, localPosition);
    }

    public TextMeshProUGUI DisplayTextAtScreenPosition(string text, Vector2 screenPosition)
    {
        Vector2 localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)transform, screenPosition, null, out localPosition);

        return DisplayText(text, localPosition);
    }

    public TextMeshProUGUI DisplayTextAtScreenPosition(string text, Vector2 screenPosition, Vector2 offset, float destroyDelay)
    {
        Vector2 localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)transform, screenPosition, null, out localPosition);

        localPosition += offset;
        return DisplayText(text, localPosition, destroyDelay);
    }

    public TextMeshProUGUI DisplayRaisingTextAtScreenPosition(string text, Vector2 screenPosition, Vector2 offset, float speed = 50f, float destroyDelay = 1f)
    {
        Vector2 localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)transform, screenPosition, null, out localPosition);

        localPosition += offset;
        TextMeshProUGUI returnText = DisplayText(text, localPosition);
        IEnumerator coroutine = RaiseText(returnText, speed, destroyDelay);
        StartCoroutine(coroutine);
        return returnText;
    }

    public TextMeshProUGUI DisplayRaisingTextAtGameObject(string text, GameObject gameObject, Vector2 offset, float speed = 50f, float destroyDelay = 1f)
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        return DisplayRaisingTextAtScreenPosition(text, screenPosition, offset, speed, destroyDelay);
    }

    public TextMeshProUGUI DisplayText(string text, Vector2 position)
    {
        TextMeshProUGUI dislayedText = Instantiate<TextMeshProUGUI>(textPrefab, transform);
        dislayedText.rectTransform.localPosition = position;
        dislayedText.text = text;

        return dislayedText;
    }

    public TextMeshProUGUI DisplayText(string text, Vector2 position, float destroyDelay)
    {
        TextMeshProUGUI dislayedText = Instantiate<TextMeshProUGUI>(textPrefab, transform);
        dislayedText.rectTransform.localPosition = position;
        dislayedText.text = text;
        Destroy(dislayedText, destroyDelay);
        return dislayedText;
    }

    private IEnumerator RaiseText(TextMeshProUGUI text, float speed, float destroyDelay)
    {
        float startTime = 0;
        Vector3 newPosition;
        while (startTime < destroyDelay)
        {
            startTime += Time.deltaTime;
            newPosition = text.transform.localPosition;
            newPosition.y += speed * Time.deltaTime;
            text.transform.localPosition = newPosition;
            yield return null;
        }
        Destroy(text.gameObject);
    }
}

