using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


[CustomEditor(typeof(StatusEffectController))]
public class StatusEffectControllerInspector : Editor
{
    IntegerField statusEffectDuration;
    ObjectField statusEffectTypePicker;

    public override VisualElement CreateInspectorGUI()
    {

        var root = new VisualElement();

        root.Add(DrawStatusEffects());
        root.Add(DrawAddStatusEffectBox());


        return root;
    }


    private VisualElement DrawAddStatusEffectBox()
    {
        Box box = new Box();

        statusEffectTypePicker = new ObjectField("status effect");
        statusEffectTypePicker.objectType = typeof(StatusEffect);
        box.Add(statusEffectTypePicker);

        statusEffectDuration = new IntegerField();
        statusEffectDuration.label = "duration";
        statusEffectDuration.value = 1;
        statusEffectDuration.RegisterValueChangedCallback(evt =>
        {
            if (evt.newValue < 1)
            {
                statusEffectDuration.value = 1;
            }
        });
        box.Add(statusEffectDuration);

        var addStatusEffectBtn = new Button();
        addStatusEffectBtn.text = "Add Status Effect";
        addStatusEffectBtn.clicked += AddStatusEffect;

        box.Add(addStatusEffectBtn);

        var clearAllBtn = new Button();
        clearAllBtn.text = "Clear All";
        clearAllBtn.clicked += Clear;

        box.Add(clearAllBtn);

        box.style.paddingTop = 2;
        box.style.paddingBottom = 2;

        return box;
    }

    private void AddStatusEffect()
    {
        Debug.Log("clicked AddStatusEffect");
        if ((StatusEffect)statusEffectTypePicker.value == null)
        {
            return;
        }
        StatusEffectController statusEffectController = target as StatusEffectController;
        statusEffectController.ReceiveStatusEffect((StatusEffect)statusEffectTypePicker.value, statusEffectDuration.value);
    }


    private void RemoveStatusEffect(StatusEffect statusEffect)
    {
        Debug.Log("clicked X");
        StatusEffectController statusEffectController = target as StatusEffectController;
        statusEffectController.RemoveStatusEffect(statusEffect);

    }

    private void Clear()
    {
        StatusEffectController statusEffectController = target as StatusEffectController;
        statusEffectController.Clear();
    }


    private void DrawStatusEffectsList(SerializedProperty listProperty, VisualElement parent, ref int count)
    {
        for (int i = 0; i < listProperty.arraySize; i++)
        {
            count++;
            SerializedObject statusEffectBehaviourObject = new SerializedObject(listProperty.GetArrayElementAtIndex(i).objectReferenceValue);
            StatusEffectInfoVisualElement element = new StatusEffectInfoVisualElement(statusEffectBehaviourObject);
            Button cancelBtn = element.Q<Button>("cancelBtn");
            StatusEffect se = statusEffectBehaviourObject.FindProperty("_statusEffect").objectReferenceValue as StatusEffect;
            cancelBtn.clicked += () => { RemoveStatusEffect(se); };
            parent.Add(element);
        }
    }


    private VisualElement DrawStatusEffects()
    {
        VisualElement root = new VisualElement();
        Foldout foldout = new Foldout();

        int count = 0;
        SerializedProperty listProperty = serializedObject.FindProperty("earlyStatusEffectsBehaviours");
        DrawStatusEffectsList(listProperty, foldout, ref count);

        listProperty = serializedObject.FindProperty("statusEffectsBehaviours");
        DrawStatusEffectsList(listProperty, foldout, ref count);

        foldout.text = "status effects (" + count + ")";
        root.Add(foldout);
        return root;
    }

}

