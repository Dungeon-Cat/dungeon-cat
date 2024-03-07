using System.Linq;
using Scripts.Components.CommonEntities;
using UnityEngine;

namespace Scripts.Components.Room2
{
    public class RunePillar2 : MonoBehaviour
    {
        public GameObject glow;
        public AltarEntity altar;

        private void Update()
        {
            glow.SetActive(altar.data.runes.All(b => b));
        }
    }
}