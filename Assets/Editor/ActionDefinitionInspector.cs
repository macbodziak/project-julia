// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEditor;
// using UnityEditor.UIElements;
// using UnityEngine.UIElements;

// [CustomEditor(typeof(ActionDefinition))]
// public class ActionDefinitionInspector : Editor
// {
//     public override VisualElement CreateInspectorGUI()
//     {
//         // Create a new VisualElement to be the root of our Inspector UI.
//         VisualElement myInspector = new VisualElement();

//         var serializedObject = new SerializedObject(target);
//         var name = serializedObject.FindProperty("m_name");
//         var m_actionPointCost = serializedObject.FindProperty("m_actionPointCost");
//         var m_duration = serializedObject.FindProperty("m_duration");
//         var m_animationTrigger = serializedObject.FindProperty("m_animationTrigger");
//         var m_sprite = serializedObject.FindProperty("m_sprite");
//         var m_targetingMode = serializedObject.FindProperty("m_targetingMode");
//         var m_numberOfTargets = serializedObject.FindProperty("m_numberOfTargets");

//         var m_numberOfTargetsField = new IntegerField();
//         m_numberOfTargetsField.style.display = DisplayStyle.None;
//         // Add a simple label.
//         myInspector.Add(new Label("This is a custom Inspector"));
//         myInspector.Add(new PropertyField(name));
//         myInspector.Add(new PropertyField(m_actionPointCost));
//         myInspector.Add(new PropertyField(m_duration));
//         myInspector.Add(new PropertyField(m_animationTrigger));
//         myInspector.Add(new PropertyField(m_sprite));
//         myInspector.Add(new PropertyField(m_targetingMode));
//         // myInspector.Add(new PropertyField(m_numberOfTargets));
//         if (m_numberOfTargets.enumValueIndex == (int)TargetingModeType.MultipleEnemyTargets)
//         {
//             m_numberOfTargetsField.style.display = DisplayStyle.Flex; // Show the field
//             myInspector.Q<Label>("Conditional Label:").parent.Insert(1, m_numberOfTargetsField); // Insert after the label
//             m_numberOfTargetsField.BindProperty(m_numberOfTargets);
//         }


//         // Return the finished Inspector UI.
//         return myInspector;
//     }
// }
