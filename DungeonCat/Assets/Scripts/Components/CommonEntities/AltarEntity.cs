using System.Linq;
using Cainos.PixelArtTopDown_Basic;
using Scripts.Data;
using UnityEngine;

namespace Scripts.Components.CommonEntities
{
    [RequireComponent(typeof(PropsAltar))]
    public class AltarEntity : EntityComponent<AltarData>
    {
        public bool FullyActive => data.runes.All(b => b);
        
        public void SetRune(int index, bool active)
        {
            data.runes[index] = active;
        }

        private void Update()
        {
            SyncRunes();
        }

        private void SyncRunes()
        {
            var altar = GetComponent<PropsAltar>();
            for (var i = 0; i < data.runes.Length; i++)
            {
                altar.runes[i].color = data.runes[i] ? Color.white : Color.clear;
            }
        }

        protected override void OnValidateInEditor()
        {
            base.OnValidateInEditor();
            SyncRunes();
        }
    }
}