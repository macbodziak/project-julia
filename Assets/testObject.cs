using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testObject : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    // [SerializeField] BaseAction baseAction;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        ActionManager.Instance.OnSelectedUnitChanged += HandleSelectedUnitChanged;
        ActionManager.Instance.OnSelectedActionChanged += HandleSelectedActionChanged;
        ActionManager.Instance.OnSelectedTargetsChanged += HandleSelectedTargetsChanged;
        // baseAction.ActionStarted += TestObject_OnActionStarted;
        // baseAction.ActionCompleted += TestObject_OnActionCompleted;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetRandomColor()
    {
        spriteRenderer.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
    }

    public void TestObject_OnActionStarted(object sender, EventArgs e)
    {
        SetRandomColor();
    }

    public void TestObject_OnActionCompleted(object sender, EventArgs e)
    {
        SetRandomColor();
    }

    public void HandleSelectedUnitChanged(object sender, EventArgs e)
    {
        SetRandomColor();
    }

    public void HandleSelectedActionChanged(object sender, EventArgs e)
    {
        SetRandomColor();
    }

    private void OnDestroy()
    {
        ActionManager.Instance.OnSelectedUnitChanged -= HandleSelectedUnitChanged;
        ActionManager.Instance.OnSelectedActionChanged -= HandleSelectedActionChanged;
        ActionManager.Instance.OnSelectedTargetsChanged -= HandleSelectedTargetsChanged;
    }

    public void HandleSelectedTargetsChanged(object sender, EventArgs e)
    {
        SetRandomColor();
    }
}
