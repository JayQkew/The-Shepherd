using System;
using Climate;
using TimeSystem;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

namespace Environment
{
    [Serializable]
    public class TerrainSnow
    {
        public Terrain terrain;
        public Gradient grassTint;
        [SerializeField] private bool fadeToSnow;
        [SerializeField] private Timer fadeTimer;
        private TerrainData terrainData;
        private float[,,] alphamaps;
        private int alphaMapWidth;
        private int alphaMapHeight;
        private int numLayers;

        private readonly int layer1Index = 0;
        private readonly int layer2Index = 1;

        public void Init() {
            terrainData = terrain.terrainData;
            alphaMapWidth = terrainData.alphamapWidth;
            alphaMapHeight = terrainData.alphamapHeight;
            numLayers = terrainData.alphamapLayers;
            
            fadeTimer.currTime = fadeTimer.maxTime;
        }

        public void Update() {
            fadeTimer.Update();
            if (!fadeTimer.IsFinished) {
                float t = fadeToSnow ? fadeTimer.Progress : 1 - fadeTimer.Progress;
                ApplyFade(t);
                ApplyGrassTint(t);
                ApplySnowMat(t * 2);
            }
        }

        public void FadeToSnow() {
            fadeToSnow = true;
            fadeTimer.Reset();
        }

        public void FadeToGrass() {
            fadeToSnow = false;
            fadeTimer.Reset();
        }

        public void ApplyToZero() {
            ApplySnowMat(0);
            ApplyGrassTint(0);
            ApplyFade(0);
        }

        public void ApplyFade(float t) {
            alphamaps = terrainData.GetAlphamaps(0, 0, alphaMapWidth, alphaMapHeight);

            float layer1Weight = 1f - t;
            float layer2Weight = t;

            for (int y = 0; y < alphaMapHeight; y++) {
                for (int x = 0; x < alphaMapWidth; x++) {
                    for (int i = 0; i < numLayers; i++) {
                        alphamaps[x, y, i] = 0f;
                    }

                    alphamaps[x, y, layer1Index] = layer1Weight;
                    alphamaps[x, y, layer2Index] = layer2Weight;
                }
            }

            terrainData.SetAlphamaps(0, 0, alphamaps);

            ApplyGrassTint(t);
        }

        public void ApplyGrassTint(float t) {
            terrainData.wavingGrassTint = grassTint.Evaluate(t);
        }


        public void ApplySnowMat(float v) {
            Shader.SetGlobalFloat("_SnowOpacity", v);
        }
    }
}