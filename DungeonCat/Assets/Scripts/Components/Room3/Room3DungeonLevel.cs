using System.Collections.Generic;
using Cainos.PixelArtTopDown_Basic;
using Scripts.Components.CommonEntities;
using UnityEngine;

namespace Scripts.Components.Room3
{
    /// <summary>
    /// Script for root object of Room 3
    /// </summary>
    public class Room3DungeonLevel : DungeonLevel
    {
        public bool SwitchState
        {
            get => data.flags.GetValueOrDefault(nameof(SwitchState), false);
            private set => data.flags[nameof(SwitchState)] = value;
        }
        
        public Switch[] switches;
        public ToggleWall[] blueWalls;
        public ToggleWall[] redWalls;

        public MovableEntity yarn;
        public GameObject yarnStart;

        public DoorEntity exit;

        public override void Start()
        {
            base.Start();
            UpdateSwitch();
        }

        public void OnSwitchChange()
        {
            SwitchState = !SwitchState; 
            UpdateSwitch();
        }

        private void UpdateSwitch()
        {
            foreach (var _switch in switches)
            {
                _switch.SetOpen(SwitchState);
            }

            foreach (var wall in blueWalls)
            {
                wall.SetOpen(!SwitchState); 
            }

            foreach (var wall in redWalls)
            {
                wall.SetOpen(SwitchState);
            }
            
            UnityState.Instance.ScanPathfinding();
        }

        public void OnYarnStatueInteract()
        {
            yarn.GetComponent<Rigidbody2D>().position = yarnStart.transform.position;
            yarn.collider2d.isTrigger = false;
        }

        public void OnGoalReached()
        {
            exit.SetOpen(true);
            exit.collider2d.isTrigger = false;
        }
    }
}