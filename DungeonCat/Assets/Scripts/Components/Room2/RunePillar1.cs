using UnityEngine;

namespace Scripts.Components.Room2
{
    public class RunePillar1 : MonoBehaviour
    {
        public GameObject glow;
        public PressurePlateAltar altar;

        private void Update()
        {
            glow.SetActive(altar.Active);
        }
    }
}