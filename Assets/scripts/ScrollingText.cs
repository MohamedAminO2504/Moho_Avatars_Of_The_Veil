using UnityEngine;
using TMPro;

public class ScrollingText : MonoBehaviour
{
    public float speed = 100f;
        public RectTransform text1;
        public RectTransform text2;

        private float textWidth;

        void Start()
        {
            textWidth = text1.rect.width;

            // Positionner le second texte juste à droite du premier
            text2.anchoredPosition = new Vector2(textWidth, 0);
        }

        void Update()
        {
            Vector2 movement = Vector2.left * speed * Time.deltaTime;
            text1.anchoredPosition += movement;
            text2.anchoredPosition += movement;

            // Quand un texte sort complètement à gauche,
            // on le replace à droite de l’autre
            if (text1.anchoredPosition.x <= -textWidth)
            {
                text1.anchoredPosition = new Vector2(text2.anchoredPosition.x + textWidth, 0);
            }

            if (text2.anchoredPosition.x <= -textWidth)
            {
                text2.anchoredPosition = new Vector2(text1.anchoredPosition.x + textWidth, 0);
            }
        }
}