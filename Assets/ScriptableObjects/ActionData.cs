using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ActionData", menuName = "Scriptable Objects/Action Data Config", order = 1)]
public class ActionData : ScriptableObject
{
    [SerializeField] private string m_name = "no name";
    [SerializeField] private int m_actionPointCost = 1;
    [SerializeField] private float m_duration = 5f;
    [SerializeField] private string m_animationTrigger;
    [SerializeField] private Sprite m_sprite;

    public string Name { get => m_name; private set => m_name = value; }
    public int ActionPointCost { get => m_actionPointCost; private set => m_actionPointCost = value; }
    public float Duration { get => m_duration; private set => m_duration = value; }
    public string AnimationTrigger { get => m_animationTrigger; private set => m_animationTrigger = value; }
    public Sprite Icon { get => m_sprite; private set => m_sprite = value; }
}
