using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SelectedVisual : MonoBehaviour
{
    [SerializeField] SpriteRenderer m_SpriteRenderer;
    [SerializeField] bool isVisible;

    void Awake()
    {
        isVisible = false;
        SetSelectedVisual(false);
    }

    public void ShowSelectedVisual()
    {
        isVisible = true;
        SetSelectedVisual(true);
    }

    public void HideSelectedVisual()
    {
        isVisible = false;
        SetSelectedVisual(false);
    }

    public void SetSelectedVisual(bool b)
    {
        isVisible = b;
        m_SpriteRenderer.enabled = b;
    }

    public void Toggle()
    {
        isVisible = !isVisible;
        SetSelectedVisual(isVisible);
    }

}
