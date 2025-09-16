using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace HerdingSystem
{
    /// <summary>
    /// A Herding Ticket will determine how many missions there will be per creature
    /// Each Creature will get a Herding ticket based on what number the day is
    /// </summary>
    [CreateAssetMenu(fileName = "New Herding Ticket", menuName = "Herding System/Herding Ticket")]
    public class HerdingTicket : ScriptableObject
    {
        public TicketDifficulty difficulty;

        [Tooltip("Each weight is a mission")]
        public List<float> weights = new List<float>();


        public void Normalize() {
            float total = 0;
            foreach (float w in weights) {
                total += w;
            }

            if (total <= 0f) return;

            for (int i = 0; i < weights.Count; i++) {
                weights[i] /= total;
            }
        }
    }

    public enum TicketDifficulty
    {
        Easy,
        Medium,
        Hard
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(HerdingTicket))]
    public class HerdingTicketEditor : Editor
    {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            HerdingTicket ticket = (HerdingTicket)target;

            if (GUILayout.Button("Normalize Weights")) {
                ticket.Normalize();
                EditorUtility.SetDirty(ticket);
            }
        }
    }
#endif
}