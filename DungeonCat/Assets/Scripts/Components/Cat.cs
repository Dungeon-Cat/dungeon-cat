using Scripts.Data;
using UnityEngine;
namespace Scripts.Components
{
    public class Cat : EntityComponent<CatData>
    {
        protected override void Start()
        {
            base.Start();
            InputManager.Actions.Player.DropItem.performed += _ => data.DropAllItems();
        }

        public void Meow()
        {
            Debug.Log("Meow");
        }
    }
}