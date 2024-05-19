using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor.Rendering;

[CustomPropertyDrawer(typeof(StatusEffectBehaviour))]
public class StatusEffectBehaviourPropertyDrawer : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        VisualElement root = new VisualElement();

        var foldout = new UnityEngine.UIElements.Foldout();
        foldout.text = "status effect";

        foldout.Add(new PropertyField(property));
        SerializedObject obj = new SerializedObject(property.objectReferenceValue);
        StatusEffectBehaviour seb = property.objectReferenceValue as StatusEffectBehaviour;
        SerializedProperty StatusEffectProp = obj.FindProperty("_statusEffect");
        SerializedProperty remainingDurationProp = obj.FindProperty("remainingDuration");

        Label label = new Label(seb.Name + "  Cooldown: <color=#ff5555>" + remainingDurationProp.intValue + "</color>");
        foldout.Add(label);
        root.Add(foldout);
        return root;
    }
}
