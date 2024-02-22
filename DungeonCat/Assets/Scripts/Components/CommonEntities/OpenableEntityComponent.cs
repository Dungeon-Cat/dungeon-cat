using Scripts.Data;
using UnityEngine;

namespace Scripts.Components.CommonEntities
{
    /// <summary>
    /// Entity that has an open and closed state with different main sprites
    /// </summary>
    public abstract class OpenableEntityComponent<T>  : EntityComponent<T> where T : OpenableEntityData
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