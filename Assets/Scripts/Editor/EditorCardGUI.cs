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

        DisplayCardBaseAttributes();

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
        EditorGUILayout.Space();

        SerializedProperty UtilityTypeProperty = serializedObject.FindProperty("utilityType");

        EditorGUILayout.PropertyField(serializedObject.FindProperty("utilityType"));

        if (UtilityTypeProperty.intValue == 0)
            return;

        if (UtilityTypeProperty.intValue == 1)
            EditorGUILayout.PropertyField(serializedObject.FindProperty("equipmentType"));

        if (UtilityTypeProperty.intValue > 1)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("utilityPoints"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("isMandatory"));
        }

        SerializedProperty CardEffectProperty = serializedObject.FindProperty("cardEffects");
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cardEffects"));
    }

    private void DisplayEventCardAttributes()
    {
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("maxDangerPoints"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("maxPlayNumber"));

        SerializedProperty CardEffectProperty = serializedObject.FindProperty("cardEffects");
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cardEffects"));
    }

    private void DisplayCardBaseAttributes()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cardName"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cardDescription"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cardForeground"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cardBackground"));

        EditorGUILayout.Space();

        serializedObject.ApplyModifiedProperties();
    }
}