using Scripts.Data;
using UnityEngine;
namespace Scripts.Components
{
    public class Cat : EntityComponent<CatData>
    {
        
        
        public void Meow()
        {
            Debug.Log("Meow");
        }
    }
}