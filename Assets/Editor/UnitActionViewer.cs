using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class UnitActionViewer : EditorWindow
{
    [MenuItem("Window/Project Julia/Unit Action Viewer")]
    public static void ShowExample()
    {
        UnitActionViewer wnd = GetWindow<UnitActionViewer>();
        wnd.titleContent = new GUIContent("Unit Action Viewer");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        VisualElement label = new Label("Unit Action Viewer");
        label.style.fontSize = 18;
        root.Add(label);

        TwoPaneSplitView splitView = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Horizontal);
        rootVisualElement.Add(splitView);



        var leftPane = new ListView();
        leftPane.style.minWidth = 220;
        // Initialize the list view with all sprites' names
        // leftPane.makeItem = () => new Label();
        // leftPane.bindItem = (item, index) => { (item as Label).text = allObjects[index].name; };
        // leftPane.itemsSource = allObjects;

        var rightPane = new VisualElement();




        splitView.Add(leftPane);
        splitView.Add(rightPane);

    }

    private void OnSelectionChange()
    {
        Debug.Log("now selected: " + Selection.activeGameObject);
    }
}
