using UnityEditor;
using UnityEngine;

public class DaiktoKurimoLangas : EditorWindow
{
    private Daiktas daiktas;
    private const string vietaSaugojimo = "Assets/Daiktai/Duomenys";
    [MenuItem("SukurtiLangai/Inventoriaus/Daikto Generatorius")]
    public static void RodytiLanga()
    {
        GetWindow<DaiktoKurimoLangas>("Daikto Kurimas");
    }

    private void OnEnable()
    {
        daiktas = CreateInstance("Daiktas") as Daiktas;
    }

    private void OnGUI()
    {
        //Daikto pavadinimas
        daiktas.pavadinimas = EditorGUILayout.TextField("Pavadinimas:", daiktas.pavadinimas);
        EditorGUILayout.Space();
        //Daikto aprasymas
        daiktas.aprasymas = EditorGUILayout.TextField("Aprasymas:", daiktas.aprasymas);
        EditorGUILayout.Space();
        //Daikto laukelio numeris
        daiktas.laukelioNr = (Daiktas.LaukelisPriklausimo)EditorGUILayout.EnumPopup("Daikto tipas:", daiktas.laukelioNr);
        EditorGUILayout.Space();
        //Daikto paveiksliukas
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Daikto Paveiksliukas:");
        daiktas.daiktoPaveiksliukas = (Sprite)EditorGUILayout.ObjectField(daiktas.daiktoPaveiksliukas, typeof(Sprite), allowSceneObjects: true);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        //Daikto patvarumas (reagavimas su kitais daiktais)
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Daikto rigidbody:");
        daiktas.patavarumas = (Rigidbody)EditorGUILayout.ObjectField(daiktas.patavarumas, typeof(Rigidbody), allowSceneObjects: true);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        if (GUILayout.Button("Sukurti Daikta"))
        {
            if (daiktas.pavadinimas.Length != 0)
            {
                if (AssetDatabase.IsValidFolder(vietaSaugojimo))
                {
                    AssetDatabase.CreateAsset(daiktas, vietaSaugojimo + "/" + daiktas.pavadinimas + ".asset");
                }
                else
                {
                    Debug.LogWarning(string.Format("Vieta -> {0} neegzistuoja", vietaSaugojimo));
                }
            }
            else
            {
                Debug.LogWarning("Daikto pavadinimas neįrašytas");
            }
        }
    }
}
