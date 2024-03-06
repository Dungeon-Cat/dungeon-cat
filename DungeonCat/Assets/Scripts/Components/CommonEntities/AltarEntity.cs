using Cainos.PixelArtTopDown_Basic;
using Scripts.Data;
using UnityEngine;

namespace Scripts.Components.CommonEntities
{
    [RequireComponent(typeof(PropsAltar))]
    public class AltarEntity : EntityComponent<AltarData>
    {
        public void SetRune(int index, bool active)
        {
            data.runes[index] = active;
            SyncRunes();
        }

        private void SyncRunes()
        {
            for (var i = 0; i < data.runes.Length; i++)
            {
                GetComponent<PropsAltar>().runes[i].color = data.runes[i] ? Color.white : Color.clear;
            }
        }

        protected override void OnValidateInEditor()
        {
            base.OnValidateInEditor();
            SyncRunes();
        }

        public override void SyncFromData()
        {
            base.SyncFromData();
            SyncRunes();
        }
    }
}