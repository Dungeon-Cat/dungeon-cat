using Scripts.Data;
using Scripts.Definitions;
using Scripts.Utility;
using UnityEngine;

namespace Scripts.Components.CommonEntities
{
    /// <summary>
    /// Entity that represents an inventory item and exists in worldspace
    /// </summary>
    public class ItemEntity : EntityComponent<ItemEntityData>
    {
        public SpriteRenderer spriteRenderer;

        public override void SyncFromData()
        {
            base.SyncFromData();
            LoadSprite();
        }

        public void LoadSprite()
        {
            var icon = data.item.GetItemDef().Icon;

            var sprite = Resources.Load<Sprite>(icon);

            spriteRenderer.sprite = sprite;
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.HasComponent(out Cat cat))
            {
                cat.data.TryPickupItem(data);
            }
        }

        protected override void OnValidateInEditor()
        {
            base.OnValidateInEditor();
            ItemRegistry.Initialize();
            LoadSprite();
        }


    }
}