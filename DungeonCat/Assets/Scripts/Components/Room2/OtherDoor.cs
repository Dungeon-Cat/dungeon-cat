using Scripts.Components.CommonEntities;
using UnityEngine;

namespace Scripts.Components.Room2
{
    public class OtherDoor : MonoBehaviour
    {
        public DoorEntity door;
        public PressurePlateAltar altar1;
        public AltarEntity altar2;

        private void Update()
        {
            door.SetOpen(altar1.Active && altar2.FullyActive);
        }
    }
}