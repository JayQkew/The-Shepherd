using System;
using System.Collections.Generic;
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

        private void Awake() {
            if (Instance == null) Instance = this;
            else Destroy(this);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void Start() {
            if (TimeManager.Instance.dayCount == 0) {
                SpawnNewSheep(3);
            }

            TimeManager.Instance.dayPhases[^1].onPhaseEnd.AddListener(SpawnNewSheep);
        }

        private void OnDestroy() {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            ClearSheepCount();
            TimeManager.Instance.dayPhases[^1].onPhaseEnd.RemoveListener(SpawnNewSheep);
        }

        public void SpawnSheep() {
            foreach (SheepData sheepData in data.sheepData) {
                Vector3 spawnPos = GetValidSpawnPoint();
                GameObject sheep = Instantiate(data.prefab, spawnPos, Quaternion.identity, parent);
                sheep.GetComponent<Sheep>().sheepData = sheepData;
            }
        }

        public void SpawnNewSheep(int num) {
            int numSheep = num;
            if (data.count + num > data.clampSheep.max) {
                numSheep = data.clampSheep.max - data.count;
            }

            for (int i = 0; i < numSheep; i++) {
                Vector3 spawnPos = GetValidSpawnPoint();
                GameObject sheep = Instantiate(data.prefab, spawnPos, Quaternion.identity, parent);
                data.sheepData.Add(sheep.GetComponent<Sheep>().sheepData);
                data.count++;
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

        public void ClearSheepCount() => data.count = 0;

        public void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode) {
            if (scene.name == "Main Scene") {
                spawnPoint = GameObject.Find("SheepSpawnPoint").transform;
                parent = GameObject.Find("Sheeps").transform;
                SpawnSheep();
            }
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(spawnPoint.position, spawnRadius);
        }
    }
}