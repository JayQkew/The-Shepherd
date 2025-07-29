using System.IO;
using UnityEditor;
using UnityEngine;

public class StateMachineTemplateGenerator : EditorWindow
{
    private string folderName = "NewScripts";
    private string[] templateNames = { "Player", "Enemy", "Manager" };

    [MenuItem("Tools/ScriptTemplateGenerator")]
    public static void ShowWindow() {
        GetWindow<StateMachineTemplateGenerator>("Script Generator");
    }

    private void OnGUI() {
        GUILayout.Label("Generate Script Folder", EditorStyles.boldLabel);
        folderName = EditorGUILayout.TextField("Folder Name", folderName);

        if (GUILayout.Button("Create Folder")) {
            GenerateScripts();
        }
    }

    private void GenerateScripts() {
        string path = Path.Combine("Assets", folderName);
        if (!AssetDatabase.IsValidFolder(path)) {
            AssetDatabase.CreateFolder("Assets", folderName);
        }

        foreach (var name in templateNames) {
            string scriptPath = Path.Combine(path, $"{name}.cs");
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
