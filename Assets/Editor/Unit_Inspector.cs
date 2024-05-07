using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

// [CustomEditor(typeof(Unit))]
public class Unit_Inspector : Editor
{
    public VisualTreeAsset m_InspectorXML;
    public override VisualElement CreateInspectorGUI()
    {

        // Create a new VisualElement to be the root of our Inspector UI.
        VisualElement myInspector = new VisualElement();

        // Add a simple label.
        myInspector.Add(new Label("This is a custom Inspector"));

        m_InspectorXML.CloneTree(myInspector);

        // Return the finished Inspector UI.
        return myInspector;
    }
}
