using UnityEditor;

[CustomEditor(typeof(SOCardData))]
public class EditorCardGUI : Editor
{
    public CardType cardAttributesToDisplay;

    public override void OnInspectorGUI()
    {   
        SerializedProperty CardTypeProperty = serializedObject.FindProperty("cardType");
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cardType"));

        EditorGUILayout.Space();

        switch (CardTypeProperty.enumValueIndex)
        {
            case (int)CardType.Utility:
                DisplayUtilityCardAttributes();
                break;

            case (int)CardType.Event:
                DisplayEventCardAttributes();
                break;

        }
        serializedObject.ApplyModifiedProperties();
    }

    private void DisplayUtilityCardAttributes()
    {
        DisplayCardBaseAttributes();

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("utilityType"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("equipmentType"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("utilityPoints"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("isMandatory"));
    }

    private void DisplayEventCardAttributes()
    {
        DisplayCardBaseAttributes();

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("maxDangerPoints"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("maxPlayNumber"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("eventType"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("eventEffect"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("enactAtStart")); 
        EditorGUILayout.PropertyField(serializedObject.FindProperty("enactAtEnd"));
    }

    private void DisplayCardBaseAttributes()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cardName"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cardDescription"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cardForeground"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cardBackground"));
    }
}