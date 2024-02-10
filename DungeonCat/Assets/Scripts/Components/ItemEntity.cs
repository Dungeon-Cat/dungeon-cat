using Scripts.Data;
using Scripts.Definitions;
using UnityEngine;

namespace Scripts.Components
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
            Debug.Log("Happening!");
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