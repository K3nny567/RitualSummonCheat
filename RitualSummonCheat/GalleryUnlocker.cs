using System.Collections.Generic;

namespace RitualSummonCheat
{
    class GalleryUnlocker
    {
        private List<string> CGList = new List<string>
        {
            { "Goblin_1" }, { "Goblin_2" }, { "Goblin_3" },
            { "Priest_1" }, { "Priest_2" },
            { "Slug_1" }, { "Slug_2" }, { "Slug_3" },
            { "NPC_1" },  { "NPC_2" },
            { "Man_1" },
            { "BossGoblin_1" }, { "BossGoblin_2" },
            { "CultFamale_1" }, { "CultFamale_2" },
            { "CultMale_1" }, { "CultMale_2" },
            { "Lux_1" },  { "Lux_2" },
            { "BossMagician_1" }, { "BossMagician_2" },
            { "Tentacle_1" },  { "Tentacle_2" },
            { "BossSuccubus_1" },  { "BossSuccubus_2" },
            { "NPC_3" },  { "NPC_4" },  { "NPC_5" }
        };
        public GalleryUnlocker()
        {
            List<KeyValuePair<string, bool>> ListCG = BundleManager.Instance.GetCGList();

            for (int i = 0; i < CGList.Count; i++)
            {
                if (ListCG[i].Key == CGList[i] && !ListCG[i].Value)
                {
                    BundleManager.Instance.UnlockCG(CGList[i]);
                }
            }

        }
    }
}
