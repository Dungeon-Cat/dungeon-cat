using System;
using UnityEngine;

namespace Scripts.Components.Room3
{
    public class Yarn : MonoBehaviour
    {
        public Collider2D yarnBowlCollider2d;
        
        public Room3DungeonLevel dungeonLevel;
        
        public void Update()
        {
            if (IsAtGoal())
            {
                // GetComponent<Collider2D>().isTrigger = true;
                dungeonLevel.OnGoalReached();
            }
        }

        public bool IsAtGoal()
        {
            return GetComponent<Collider2D>().Distance(yarnBowlCollider2d).distance < 0.1f;
        }
    }
}