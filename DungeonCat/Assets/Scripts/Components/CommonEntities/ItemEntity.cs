using Scripts.Data;
using Scripts.Definitions;
using Scripts.Utility;
using UnityEngine;

namespace Scripts.Components.CommonEntities
{
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
            //Execute unsafe code here...
            ItemRegistry.Initialize();
            LoadSprite();
        }


    }
}