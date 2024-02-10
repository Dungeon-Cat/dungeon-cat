using System;
using System.Collections.Generic;
using Scripts.Data;
using UnityEngine;
namespace Scripts.Components
{
    public class Cat : EntityComponent<CatData>
    {
        public void Meow()
        {
            Debug.Log("Meow");
            Interaction interaction = Interaction.Basic("cat", new []{"meow", "nyan", "mrao"});
            UnityState.Instance.dialogue.StartInteraction(interaction);
        }
    }
}