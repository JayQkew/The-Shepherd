using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class StateMachineTemplateGenerator : EditorWindow
{
    private string stateMachineName;
    private string folderName = "StateMachine";
    private List<string> stateNames = new List<string> { "State1", "State2", "State3" };
    
    [SerializeField] public TextAsset stateManagerTemplate;
    [SerializeField] public TextAsset stateTemplate;
    
    private string folderPath = "no path selected";
    private string absolutePath;
    
    private Vector2 scrollPos;

    [MenuItem("Tools/State Machine Template")]
    public static void ShowWindow() {
        GetWindow<StateMachineTemplateGenerator>();
    }

    private void OnGUI() {
        GUILayout.Label("Generate State Machine Template", EditorStyles.boldLabel);
        GUILayout.Space(10);

        stateMachineName = EditorGUILayout.TextField("Name", stateMachineName);
        folderName = EditorGUILayout.TextField("Folder Name", folderName);
        GUILayout.Space(10);
        
                
        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Folder Path");
        if (GUILayout.Button("Get Path")) {
            absolutePath = EditorUtility.OpenFolderPanel("Select folder", Application.dataPath, "");
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
            //GenerateScripts();
            Debug.Log(stateManagerTemplate.text);
            Debug.Log(stateTemplate.text);
        }
    }

    private void GenerateScripts()
    {
        if (folderPath == "no path selected" || string.IsNullOrWhiteSpace(folderName))
        {
            Debug.LogWarning("Please select a valid folder and enter a folder name.");
            return;
        }

        string newFolderPath = Path.Combine(folderPath, folderName);

        if (!AssetDatabase.IsValidFolder(newFolderPath))
        {
            string parent = Path.GetDirectoryName(newFolderPath).Replace("\\", "/");
            string newName = Path.GetFileName(newFolderPath);
            AssetDatabase.CreateFolder(parent, newName);
        }

        foreach (var name in stateNames)
        {
            string unityPath = Path.Combine(newFolderPath, $"{name}.cs").Replace("\\", "/");              
            string systemPath = Path.Combine(Application.dataPath, unityPath.Substring("Assets/".Length));

            string scriptContent = GetTemplate(name);
            File.WriteAllText(systemPath, scriptContent);
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