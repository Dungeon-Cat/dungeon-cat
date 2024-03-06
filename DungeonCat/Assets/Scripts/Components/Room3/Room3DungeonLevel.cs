using Scripts.Components.CommonEntities;
using Scripts.Data;
using UnityEngine;

namespace Scripts.Components.Room3
{
    public class Room3DungeonLevel : DungeonLevel
    {
        [HideInInspector]
        public bool switchState;
        
        public Switch[] switches;
        public ToggleWall[] blueWalls;
        public ToggleWall[] redWalls;

        public void Awake()
        {
            UpdateSwitch();
        }
        public void OnSwitchChange()
        {
            switchState = !switchState; 
            UpdateSwitch();
        }

        private void UpdateSwitch()
        {
            foreach (var _switch in switches)
            {
                _switch.SetOpen(switchState);
            }

            foreach (var wall in blueWalls)
            {
                wall.SetOpen(!switchState); 
            }

            foreach (var wall in redWalls)
            {
                wall.SetOpen(switchState);
            }
        }
    }
}