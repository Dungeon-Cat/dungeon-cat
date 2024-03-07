using System.Linq;
using Scripts.Components.CommonEntities;
using UnityEngine;

namespace Scripts.Components.Room2
{
    [RequireComponent(typeof(ChestEntity))]
    public class AltarChest : MonoBehaviour
    {
        public ChestEntity chest;
        public AltarEntity altar;

        private void Update()
        {
            if (!chest.IsOpen && altar.data.runes.All(b => b))
            {
                chest.SetOpen(true);
                chest.data.DropContents();
                HermaicAltar.BootsDialogue();
            }
        }
    }
}