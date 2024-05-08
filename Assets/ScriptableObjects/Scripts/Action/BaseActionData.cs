using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Base Action Data", menuName = "Scriptable Objects/Actions/Base Action Data Config", order = 10)]
public class BaseActionData : ScriptableObject
{
    [SerializeField] private string m_name = "no name";
    [SerializeField] private int m_actionPointCost = 1;
    [SerializeField] private float m_duration = 5f;
    [SerializeField] private string m_animationTrigger;
    [SerializeField] private Sprite m_sprite;

    public string Name { get => m_name; protected set => m_name = value; }
    public int ActionPointCost { get => m_actionPointCost; protected set => m_actionPointCost = value; }
    public float Duration { get => m_duration; protected set => m_duration = value; }
    public string AnimationTrigger { get => m_animationTrigger; protected set => m_animationTrigger = value; }
    public Sprite Icon { get => m_sprite; protected set => m_sprite = value; }
}
