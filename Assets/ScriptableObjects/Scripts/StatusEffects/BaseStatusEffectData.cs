using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Base Status Effect Data", menuName = "Scriptable Objects/Status Effects/Base Status Effect Data Config", order = 10)]
public class BaseStatusEffectData : ScriptableObject
{
    [SerializeField] private string m_name = "no name";
    [SerializeField] private int m_duration = 1;
    [SerializeField] private Sprite m_sprite;

    public string Name { get => m_name; protected set => m_name = value; }
    public int Duration { get => m_duration; protected set => m_duration = value; }
    public Sprite Icon { get => m_sprite; protected set => m_sprite = value; }
}
