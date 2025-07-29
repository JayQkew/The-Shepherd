using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class StateMachineTemplateGenerator : EditorWindow
{
    private string folderName = "StateMachine";
    private List<string> stateNames = new List<string> { "State1", "State2", "State3" };
    private string folderPath = "no path selected";
    
    private Vector2 scrollPos;

    [MenuItem("Tools/State Machine Template")]
    public static void ShowWindow() {
        GetWindow<StateMachineTemplateGenerator>();
    }

    private void OnGUI() {
        GUILayout.Label("Generate State Machine Template", EditorStyles.boldLabel);
        GUILayout.Space(10);

        folderName = EditorGUILayout.TextField("Folder Name", folderName);
        GUILayout.Space(10);
        
                
        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Folder Path");
        if (GUILayout.Button("Get Path")) {
            string absolutePath = EditorUtility.OpenFolderPanel("Select folder", Application.dataPath, "");
            if (!string.IsNullOrEmpty(absolutePath) && absolutePath.StartsWith(Application.dataPath)) {
                folderPath = "Assets" + absolutePath.Substring(Application.dataPath.Length);
            } else {
                EditorUtility.DisplayDialog("Invalid Path", "Please select a folder inside the Assets folder.", "OK");
            }
        }
        GUILayout.EndHorizontal();
        EditorGUILayout.LabelField(folderPath);
        GUILayout.EndVertical();
        GUILayout.Space(25);

        GUILayout.Label("States", EditorStyles.label);
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(21.75f * stateNames.Count));
        for (int i = 0; i < stateNames.Count; i++) {
            EditorGUILayout.BeginHorizontal();
            stateNames[i] = EditorGUILayout.TextField($"State {i + 1}", stateNames[i]);

            if (GUILayout.Button("X", GUILayout.Width(20))) {
                stateNames.RemoveAt(i);
                i--;
            }

            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndScrollView();
        
        if (GUILayout.Button("Add State")) {
            stateNames.Add("");
        }
        
        GUILayout.Space(25);

        if (GUILayout.Button("Create Folder", GUILayout.Height(50))) {
            GenerateScripts();
        }
    }

    private void GenerateScripts() {
        if (folderPath == "no path selected" || folderName == "") {
            return;
        }

        if (Directory.Exists(folderPath) && !AssetDatabase.IsValidFolder(folderPath)) {
            AssetDatabase.CreateFolder(folderPath, folderName);
        }

        foreach (var name in stateNames) {
            string scriptPath = Path.Combine(folderPath, $"{name}.cs");
            string scriptContent = GetTemplate(name);
            File.WriteAllText(scriptPath, scriptContent);
        }

        AssetDatabase.Refresh();
    }

    private string GetTemplate(string className) {
        return
            $@"using UnityEngine;
public class {className} : MonoBehaviour
{{
    void Start(){{
        Debug.Log(""Hello World"");
    }}
}}";
    }
}