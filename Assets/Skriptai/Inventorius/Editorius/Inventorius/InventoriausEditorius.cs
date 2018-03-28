using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(Inventorius))]
public class InventoriausEditorius : Editor
{
    private SerializedProperty daiktoPaveiksliukioSavybe;
    private SerializedProperty daiktuSavybes;
    private bool[] rodytiDaiktuLangelius = new bool[Inventorius.daiktuKiekis];

    private const string inventoriausSavybeDaiktoPaveiksliukoPavadinimas = "daiktuPaveiksleliai";
    private const string inventoriausSavybeDaiktuPavadinimai = "daiktai";


    private void OnEnable()
    {
        daiktoPaveiksliukioSavybe = serializedObject.FindProperty(inventoriausSavybeDaiktoPaveiksliukoPavadinimas);
        daiktuSavybes = serializedObject.FindProperty(inventoriausSavybeDaiktuPavadinimai);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update(); //Atnaujina invwentorius

        for (int i = 0; i < Inventorius.daiktuKiekis; i++)
        {
            DaiktuLangeliuGUI(i);
        }

        serializedObject.ApplyModifiedProperties();

    }

    private void DaiktuLangeliuGUI(int indeksas)
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUI.indentLevel++;

        rodytiDaiktuLangelius[indeksas] = EditorGUILayout.Foldout(rodytiDaiktuLangelius[indeksas], "Daikto Langelis " + indeksas);

        if (rodytiDaiktuLangelius[indeksas])
        {
            EditorGUILayout.PropertyField(daiktoPaveiksliukioSavybe.GetArrayElementAtIndex(indeksas));
            EditorGUILayout.PropertyField(daiktuSavybes.GetArrayElementAtIndex(indeksas));
        }
        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();
    }
}
