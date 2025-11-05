using System;
using System.Collections.Generic;
using Creatures;
using Creatures.Sheep;
using TimeSystem;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace HerdingSystem
{
    [Serializable]
    public class SheepSpawn : MonoBehaviour
    {
        public static SheepSpawn Instance { get; private set; }
        [SerializeField] private SheepSpawnData data;
        [SerializeField] private Transform parent;
        public Transform spawnPoint;
        public float spawnRadius;
        private string lastLoadedScene = "";

        private void Awake() {
            Instance = this;
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void Start() {
            TimeManager.Instance.dayPhases[^1].onPhaseEnd.AddListener(SpawnNewSheep);
        }

        private void OnDestroy() {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            TimeManager.Instance.dayPhases[^1].onPhaseEnd.RemoveListener(SpawnNewSheep);
        }

        public void SpawnSheep() {
            foreach (SheepData sheepData in data.sheepData) {
                Vector3 spawnPos = GetValidSpawnPoint();
                GameObject sheep = Instantiate(data.prefab, spawnPos, Quaternion.identity, parent);
                sheep.GetComponent<Sheep>().Init(sheepData);
            }
        }

        public void SpawnNewSheep(int num) {
            int numSheep = num;
            if (data.sheepData.Count + num > data.clampSheep.max) {
                numSheep = data.clampSheep.max - data.sheepData.Count;
            }

            for (int i = 0; i < numSheep; i++) {
                Vector3 spawnPos = GetValidSpawnPoint();
                GameObject sheep = Instantiate(data.prefab, spawnPos, Quaternion.identity, parent);
                Sheep sheepComponent = sheep.GetComponent<Sheep>();
                sheepComponent.Init();  // Create new data
                data.sheepData.Add(sheepComponent.sheepData);
            }
        }

        public void SpawnNewSheep() => SpawnNewSheep(1);

        private Vector3 GetValidSpawnPoint() {
            for (int i = 0; i < 10; i++) {
                // 10 attempts
                Vector3 randomOffset = Random.insideUnitCircle * spawnRadius;
                randomOffset.y = 0;
                Vector3 candidate = spawnPoint.position + randomOffset;

                bool blocked = Physics.CheckSphere(candidate, spawnRadius);
                if (!blocked) return candidate;
            }

            return spawnPoint.position;
        }

        public void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode) {
            Debug.Log($"OnSceneLoaded - Scene: {scene.name}, SheepSpawn Instance: {Instance != null}, Data count: {data.sheepData.Count}");
            if (scene.name == "Main Scene") {
                if (lastLoadedScene == scene.name) {
                    return;
                }
        
                lastLoadedScene = scene.name;
                spawnPoint = GameObject.Find("SheepSpawnPoint").transform;
                parent = GameObject.Find("Sheeps").transform;
        
                if (data.sheepData.Count == 0) {
                    SpawnNewSheep(3);
                } else {
                    SpawnSheep();
                }
            }
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(spawnPoint.position, spawnRadius);
        }
    }
}