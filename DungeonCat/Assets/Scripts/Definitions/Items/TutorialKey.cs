using System.Collections.Generic;

namespace Scripts.Definitions.Items
{
    public class TutorialKey : ItemDef
    {
        public override string Icon => "Images/Items/gold_key";

        public override IEnumerable<ItemCombination> AddCombinations()
        {
            yield return new ItemCombination(this, 1, nameof(TutorialKeyFragment1), nameof(TutorialKeyFragment2));
        }
    }
}