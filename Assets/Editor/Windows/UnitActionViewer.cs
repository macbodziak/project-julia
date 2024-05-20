using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;


public class UnitActionViewer : EditorWindow
{
    private ListView listView;
    private List<ActionBehaviour> actionList;
    VisualElement root;

    [MenuItem("Window/Project Julia/Unit Action Viewer")]
    public static void ShowExample()
    {
        UnitActionViewer wnd = GetWindow<UnitActionViewer>();
        wnd.titleContent = new GUIContent("Unit Action Viewer");
    }


    public static void OpenWindow()
    {
        GetWindow<UnitActionViewer>().Show();
    }


    private void OnEnable()
    {
        Selection.selectionChanged += OnSelectionChanged;
        // Each editor window contains a root VisualElement object
        root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        VisualElement label = new Label("Unit Action Viewer");
        label.style.fontSize = 18;
        root.Add(label);

        root.Add(MakeHeader());

        actionList = new();
        GameObject selectedGO = Selection.activeGameObject;
        if (selectedGO != null)
        {
            selectedGO.GetComponents<ActionBehaviour>(actionList);
        }


        listView = new ListView(actionList, 30, MakeItem, BindItem);
        root.Add(listView);
    }


    private void OnDisable()
    {
        Selection.selectionChanged -= OnSelectionChanged;
    }


    private void OnSelectionChanged()
    {
        if (Selection.activeObject != null)
        {
            actionList = new();
            GameObject selectedGO = Selection.activeGameObject;
            if (selectedGO != null)
            {
                selectedGO.GetComponents<ActionBehaviour>(actionList);
            }

            root.Remove(listView);
            listView = new ListView(actionList, 30, MakeItem, BindItem);
            root.Add(listView);
        }
        else
        {

        }
    }

    private VisualElement MakeItem()
    {
        var item = new ActionInfoVisualElement();
        var nameLabel = item.Query<Label>(name: "nameLabel");
        var actionPointCostLabel = item.Query<Label>(name: "actionPointCostLabel");
        var cooldownLabel = item.Query<Label>(name: "cooldownLabel");
        var currentCooldownLabel = item.Query<Label>(name: "currentCooldownLabel");
        return item;
    }

    private void BindItem(VisualElement element, int i)
    {
        SerializedObject so = new SerializedObject(actionList[i]);
        SerializedProperty actionDefinitionProperty = so.FindProperty("_actionDefinition");
        SerializedObject actionDefinitionObject = new SerializedObject(actionDefinitionProperty.objectReferenceValue);

        SerializedProperty cooldownProperty = so.FindProperty("_cooldown");

        var nameLabel = element.Q<Label>(name: "nameLabel");
        nameLabel.BindProperty(actionDefinitionObject.FindProperty("m_name"));

        var actionPointCostLabel = element.Q<Label>(name: "actionPointCostLabel");
        actionPointCostLabel.BindProperty(actionDefinitionObject.FindProperty("m_actionPointCost"));

        var powerPointCostLabel = element.Q<Label>(name: "powerPointCostLabel");
        powerPointCostLabel.BindProperty(actionDefinitionObject.FindProperty("m_powerPointCost"));

        var cooldownLabel = element.Q<Label>(name: "cooldownLabel");
        cooldownLabel.BindProperty(actionDefinitionObject.FindProperty("m_cooldown"));

        var currentCooldownLabel = element.Q<Label>(name: "currentCooldownLabel");
        currentCooldownLabel.BindProperty(cooldownProperty);

        var objectRefField = element.Q<ObjectField>(name: "objectRefField");
        objectRefField.BindProperty(actionDefinitionProperty);
    }

    private VisualElement MakeHeader()
    {
        VisualElement header = new VisualElement();
        Label label;

        label = new Label("Action Name");
        label.style.width = 180;
        header.Add(label);

        label = new Label("AP:");
        label.style.width = 50;
        header.Add(label);

        label = new Label("PP:");
        label.style.width = 50;
        header.Add(label);

        label = new Label("Cooldown:");
        label.style.width = 80;
        header.Add(label);

        label = new Label("Object Reference:");
        label.style.width = 240;
        header.Add(label);

        header.style.flexDirection = FlexDirection.Row;
        header.style.paddingTop = 8;
        header.style.paddingBottom = 16;
        return header;
    }

}






public class ActionInfoVisualElement : VisualElement
{
    public ActionInfoVisualElement()
    {
        var root = new VisualElement();

        var nameLabel = new Label("nameLabel");
        nameLabel.name = "nameLabel";
        nameLabel.text = "nameLabel";
        nameLabel.style.width = 180;

        var actionPointCostLabel = new Label("actionPointCostLabel");
        actionPointCostLabel.name = "actionPointCostLabel";
        actionPointCostLabel.style.width = 50;

        var powerPointCostLabel = new Label("powerPointCostLabel");
        powerPointCostLabel.name = "powerPointCostLabel";
        powerPointCostLabel.style.width = 50;

        var cooldownLabel = new Label("cooldownLabel");
        cooldownLabel.name = "cooldownLabel";
        cooldownLabel.style.width = 40;

        var currentCooldownLabel = new Label("currentCooldownLabel");
        currentCooldownLabel.name = "currentCooldownLabel";
        currentCooldownLabel.style.width = 40;

        var objectRefField = new ObjectField();
        objectRefField.name = "objectRefField";
        objectRefField.style.width = 240;

        root.Add(nameLabel);
        root.Add(actionPointCostLabel);
        root.Add(powerPointCostLabel);
        root.Add(cooldownLabel);
        root.Add(currentCooldownLabel);
        root.Add(objectRefField);
        root.style.flexDirection = FlexDirection.Row;

        Add(root);

    }
}