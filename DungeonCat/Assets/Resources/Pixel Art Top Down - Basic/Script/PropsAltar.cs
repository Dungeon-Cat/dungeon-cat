﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//when something get into the alta, make the runes glow
namespace Cainos.PixelArtTopDown_Basic
{

    public class PropsAltar : MonoBehaviour
    {
        public bool differentRuneColors;
        public List<SpriteRenderer> runes;
        public float lerpSpeed;

        public Color curColor;
        public Color targetColor;

        private void OnTriggerEnter2D(Collider2D other)
        {
            // targetColor = new Color(1, 1, 1, 1);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            // targetColor = new Color(1, 1, 1, 0);
        }

        private void Update()
        {
            if (differentRuneColors) return;
            
            curColor = Color.Lerp(curColor, targetColor, lerpSpeed * Time.deltaTime);

            foreach (var r in runes)
            {
                r.color = curColor;
            }
        }
    }
}
