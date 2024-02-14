using Scripts.Data;
using UnityEngine;
namespace Scripts.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class DoorEntity : EntityComponent<DoorData>
    {
        public SpriteRenderer spriteRenderer;
        
        public Sprite openSprite;

        public Sprite closedSprite;

        public bool IsOpen => data.isOpen;

        public void SetOpen(bool isOpen)
        {
            data.isOpen = isOpen;
            
            spriteRenderer.sprite = isOpen ? openSprite : closedSprite;
        }

        public override void SyncFromData()
        {
            base.SyncFromData();
            SetOpen(data.isOpen);
        }

        protected override void OnValidateInEditor()
        {
            SetOpen(data.isOpen);
            base.OnValidateInEditor();
        }
    }
}