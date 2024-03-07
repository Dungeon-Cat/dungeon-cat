using Scripts.Components.CommonEntities;
using UnityEngine;

namespace Scripts.Components.Room2
{
    [RequireComponent(typeof(AltarEntity))]
    public class ChestAltar: MonoBehaviour
    {
        private AltarEntity altar;
        public PressurePlateAltar[] pressurePlates;
        
        private void Start()
        {
            altar = GetComponent<AltarEntity>();
        }

        private void Update()
        {
            for (var i = 0; i < pressurePlates.Length; i++)
            {
                var active =  pressurePlates[i].Active;
                if (altar.data.runes[i] != active)
                {
                    altar.SetRune(i, active);
                }
            }
        }

    }
}