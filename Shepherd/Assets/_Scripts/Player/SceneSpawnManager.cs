using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class SceneSpawnManager : MonoBehaviour
    {
        public static SceneSpawnManager Instance { get; private set; }
        
        [SerializeField] private SceneSpawnData spawnData;
        [SerializeField] private Transform playerTransform;
        
        private string previousSceneName;

        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            
            if (playerTransform == null) {
                playerTransform = transform;
            }
        }

        private void OnEnable() {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable() {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode) {
            // Find the appropriate spawn point for this scene
            playerTransform = FindFirstObjectByType<Movement>().transform;
            SpawnPoint spawnPoint = spawnData.GetSpawnPoint(scene.name, previousSceneName);
            
            if (spawnPoint != null) {
                playerTransform.position = spawnPoint.position;
                playerTransform.rotation = Quaternion.Euler(0, spawnPoint.rotationY, 0);
            }
            else {
                Debug.LogWarning($"No spawn point found for scene '{scene.name}' from '{previousSceneName}'");
            }
            
            // Update previous scene for next transition
            previousSceneName = scene.name;
        }
    }
}