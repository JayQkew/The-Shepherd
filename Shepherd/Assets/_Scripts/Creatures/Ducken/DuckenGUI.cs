using System;
using UnityEngine;

namespace Creatures.Ducken
{
    public class DuckenGUI : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite duckenSprite;
        [SerializeField] private Sprite chickenSprite;
        [SerializeField] private Sprite duckSprite;

        public void ChangeSprite(Form duckenForm) {
            switch (duckenForm) {
                case Form.Ducken:
                    spriteRenderer.sprite = duckenSprite;
                    break;
                case Form.Chicken:
                    spriteRenderer.sprite = chickenSprite;
                    break;
                case Form.Duck:
                    spriteRenderer.sprite = duckSprite;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(duckenForm), duckenForm, null);
            }
        }
    }
}
