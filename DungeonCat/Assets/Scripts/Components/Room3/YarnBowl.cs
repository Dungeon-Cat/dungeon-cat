﻿using Cainos.PixelArtTopDown_Basic;
using UnityEngine;

namespace Scripts.Components.Room3
{
    /// <summary>
    /// Yarn Bowl in Room 3.
    /// Turns on when Yarn is at goal
    /// </summary>
    public class YarnBowl : MonoBehaviour
    {
        public Yarn yarn;

        public PropsAltar altar;
        
        private void Update()
        {
            if (yarn.IsAtGoal())
            {
                yarn.transform.position = altar.transform.position + new Vector3(0, 7,0 );
                altar.targetColor = Color.white;
            }
            else
            {
                altar.targetColor = Color.clear;
            }
        }
    }
}