using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "SceneSpawnData", menuName = "Scene/Spawn Data")]
    public class SceneSpawnData : ScriptableObject
    {
        [SerializeField] private List<SceneSpawnConfig> sceneConfigs = new();

        public SpawnPoint GetSpawnPoint(string currentScene, string previousScene) {
            // Find the config for the current scene
            SceneSpawnConfig config = sceneConfigs.Find(c => c.sceneName == currentScene);
            
            if (config == null) {
                return null;
            }

            // If we have a previous scene, try to find a specific spawn point for that transition
            if (!string.IsNullOrEmpty(previousScene)) {
                SpawnPoint fromSpecificScene = config.spawnPoints.Find(
                    sp => sp.fromScene == previousScene
                );
                
                if (fromSpecificScene != null) {
                    return fromSpecificScene;
                }
            }

            // Fall back to default spawn point (fromScene = "")
            SpawnPoint defaultSpawn = config.spawnPoints.Find(sp => string.IsNullOrEmpty(sp.fromScene));
            return defaultSpawn;
        }
    }

    [Serializable]
    public class SceneSpawnConfig
    {
        [Tooltip("The name of the scene these spawn points are for")]
        public string sceneName;
        
        [Tooltip("List of spawn points for this scene. Use empty 'fromScene' for default spawn.")]
        public List<SpawnPoint> spawnPoints = new();
    }

    [Serializable]
    public class SpawnPoint
    {
        [Tooltip("Leave empty for default spawn. Otherwise, specify which scene the player is coming FROM")]
        public string fromScene = "";
        
        [Tooltip("Where the player spawns")]
        public Vector3 position;
        
        [Tooltip("Y-axis rotation in degrees")]
        public float rotationY = 0f;
    }
}