using System.Collections.Generic;

namespace Scripts.Definitions.Items
{
    public class TutorialKey : ItemDef
    {
        public override string Icon => "GoldKey";

        public override IEnumerable<ItemCombination> AddCombinations()
        {
            yield return new ItemCombination(this, 1, nameof(TutorialKeyFragment), nameof(TutorialKeyFragment), nameof(TutorialKeyFragment));
        }
    }
}