using System.Collections.Generic;

namespace Scripts.Definitions.Items
{
    /// <summary>
    /// Key that is made from combining Tutorial Key Fragments
    /// </summary>
    public class TutorialKey : ItemDef
    {
        public override string Icon => "Images/Items/gold_key";

        public override IEnumerable<ItemCombination> AddCombinations()
        {
            yield return new ItemCombination(this, 1, nameof(TutorialKeyFragment1), nameof(TutorialKeyFragment2));
        }
    }
}