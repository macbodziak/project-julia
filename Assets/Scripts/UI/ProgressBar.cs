using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PercentageBar : MonoBehaviour
{
    [SerializeField] private Image bar;
    [SerializeField] private float value;
    [SerializeField] private TextMeshProUGUI textMesh;

    [SerializeField] private bool showText = false;

    public float Value
    {
        get => value;
        set
        {
            this.value = Mathf.Clamp(value, 0f, 1f);
            bar.rectTransform.localScale = new Vector3(this.value, 1f, 1f);
        }
    }

    public string Text
    {
        get { return textMesh.text; }
        set
        {
            textMesh.text = value;
        }
    }


    public bool ShowText
    {
        get => showText;
        set
        {
            showText = value;
            textMesh.gameObject.SetActive(showText);
        }
    }


    public void SetProgress(int current, int max)
    {
        Value = (float)current / (float)max;
    }


    public void SetProgress(float current, float max)
    {
        Value = current / max;
    }

}
