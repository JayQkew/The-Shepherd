using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class StateMachineTemplateGenerator : EditorWindow
{
    private string stateMachineName;
    private string folderName = "StateMachine";

    [SerializeField] public TextAsset stateManagerTemplate;
    [SerializeField] public TextAsset stateTemplate;

    private List<string> states = new List<string> { "State1", "State2", "State3" };

    private string folderPath = "no path selected";
    private string absolutePath;

    private Vector2 scrollPos;

    private string[] stateClasses;

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
            }
            else {
                EditorUtility.DisplayDialog("Invalid Path", "Please select a folder inside the Assets folder.", "OK");
            }
        }

        GUILayout.EndHorizontal();
        EditorGUILayout.LabelField(folderPath);
        GUILayout.EndVertical();
        GUILayout.Space(25);

        GUILayout.Label("States", EditorStyles.label);
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(21.75f * states.Count));
        for (int i = 0; i < states.Count; i++) {
            EditorGUILayout.BeginHorizontal();
            states[i] = EditorGUILayout.TextField($"State {i + 1}", states[i]);
            if (GUILayout.Button("X", GUILayout.Width(20))) {
                states.RemoveAt(i);
                i--;
            }

            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndScrollView();
        if (GUILayout.Button("Add State")) {
            states.Add("");
        }

        GUILayout.Space(25);

        if (GUILayout.Button("Create Folder", GUILayout.Height(50))) {
            GenerateScripts();
        }
    }

    private void GenerateScripts() {
        if (folderPath == "no path selected" || string.IsNullOrWhiteSpace(folderName)) {
            Debug.LogWarning("Please select a valid folder and enter a folder name.");
            return;
        }

        // the path from ../Assets/..
        string newFolderPath = Path.Combine(folderPath, folderName);

        // if the folder path is not valid it doesn't exist -> create the FOLDER
        if (!AssetDatabase.IsValidFolder(newFolderPath)) {
            string parent = Path.GetDirectoryName(newFolderPath)?.Replace("\\", "/");
            string newName = Path.GetFileName(newFolderPath);
            AssetDatabase.CreateFolder(parent, newName);
        }

        // create the Replacement Dictionary and create the string for the StateManager
        Dictionary<string, string> managerReplacements = CreateReplacements();
        string managerContent = ProcessTemplate(stateManagerTemplate.text, managerReplacements);

        // Generate State Manager file
        string managerFileName = $"{CapitalizeFirst(stateMachineName)}StateManager.cs";
        string managerUnityPath = Path.Combine(newFolderPath, managerFileName).Replace("\\", "/");
        string managerSystemPath = Path.Combine(Application.dataPath, managerUnityPath.Substring("Assets/".Length));

        /////////// CREATE STATE MANAGER SCRIPT ///////////
        File.WriteAllText(managerSystemPath, managerContent); 

        // Generate all state files
        foreach (var stateName in states) {
            Dictionary<string, string> stateReplacements = CreateStateReplacements(stateName);
            string stateContent = ProcessTemplate(stateTemplate.text, stateReplacements);

            // The file name should match the class name
            string stateClassName = $"{stateMachineName}{stateName}";
            string stateFileName = $"{CapitalizeFirst(stateClassName)}.cs";

            string stateUnityPath = Path.Combine(newFolderPath, stateFileName).Replace("\\", "/");
            string stateSystemPath = Path.Combine(Application.dataPath, stateUnityPath.Substring("Assets/".Length));

            File.WriteAllText(stateSystemPath, stateContent);
        }

        AssetDatabase.Refresh();
    }

    /// <summary>
    /// Creates a replacement dictionary base on the state
    /// </summary>
    private Dictionary<string, string> CreateStateReplacements(string stateName) {
        return new Dictionary<string, string>
        {
            { "{stateClass}", CapitalizeFirst(stateMachineName) + CapitalizeFirst(stateName) },
            { "{baseState}", CapitalizeFirst(stateMachineName) + "BaseState" },
            { "{stateManagerClass}", CapitalizeFirst(stateMachineName) + "StateManager" },
            { "{stateName}", CapitalizeFirst(stateName) }
        };
    }

    /// <summary>
    /// Creates a replacement dictionary for the StateManager
    /// </summary>
    private Dictionary<string, string> CreateReplacements() {
        Dictionary<string, string> replacements = new Dictionary<string, string>
        {
            { "{stateManagerClass}", CapitalizeFirst(stateMachineName) + "StateManager" },
            { "{baseClass}", CapitalizeFirst(stateMachineName) + "BaseState" },
            { "{firstState}", UncapitalizeFirst(stateMachineName + CapitalizeFirst(states[0])) }
        };

        stateClasses = new string[states.Count];

        string allStateClasses = "";
        for (int i = 0; i < states.Count; i++) {
            stateClasses[i] = CapitalizeFirst(stateMachineName) + CapitalizeFirst(states[i]);
            allStateClasses +=
                $"    public {stateClasses[i]} {UncapitalizeFirst(stateClasses[i])} = new {stateClasses[i]}();\n";
        }

        replacements.Add("{allStateClasses}", allStateClasses.TrimEnd('\n'));

        return replacements;
    }

    /// <summary>
    /// Replaces the keywords with the keywords created with Replacement methods
    /// </summary>
    private string ProcessTemplate(string template, Dictionary<string, string> replacements) {
        string result = template;
        foreach (KeyValuePair<string, string> kvp in replacements) {
            result = result.Replace(kvp.Key, kvp.Value);
        }
        return result;
    }
    
    string CapitalizeFirst(string input) {
        if (string.IsNullOrEmpty(input)) return input;
        return char.ToUpper(input[0]) + input.Substring(1);
    }

    string UncapitalizeFirst(string input) {
        if (string.IsNullOrEmpty(input)) return input;
        return char.ToLower(input[0]) + input.Substring(1);
    }
}