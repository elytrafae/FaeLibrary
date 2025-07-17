using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace FaeLibrary.API {
    public class FaeLootUtils : ModSystem {

        private static Dictionary<int, List<IItemDropRule>> NPCLootList = new();
        private static Dictionary<int, List<IItemDropRule>> ItemLootList = new();

        private static List<Tuple<Predicate<NPC>, IItemDropRule>> NPCPredicatedLootList = new();
        private static List<Tuple<Predicate<Item>, IItemDropRule>> ItemPredicatedLootList = new();

        public sealed override void Unload() {
            NPCLootList.Clear();
            ItemLootList.Clear();
            NPCPredicatedLootList.Clear();
            ItemPredicatedLootList.Clear();
        }

        // Publically available methods:
        public static void AddLootToItem(int itemID, IItemDropRule rule) {
            if (!ItemLootList.TryGetValue(itemID, out List<IItemDropRule> ruleList)) {
                ruleList = [];
                ItemLootList.Add(itemID, ruleList);
            }
            ruleList.Add(rule);
        }

        public static void AddLootToNPC(int npcID, IItemDropRule rule) {
            if (!NPCLootList.TryGetValue(npcID, out List<IItemDropRule> ruleList)) {
                ruleList = [];
                NPCLootList.Add(npcID, ruleList);
            }
            ruleList.Add(rule);
        }

        public static void AddLootToBossAndBag(int npcID, int itemID, IItemDropRule rule) {
            LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
            notExpertRule.OnSuccess(rule);

            AddLootToNPC(npcID, notExpertRule);
            AddLootToItem(itemID, rule);
        }

        public static void AddLootToNPC(Predicate<NPC> predicate, IItemDropRule rule) {
            NPCPredicatedLootList.Add(new(predicate, rule));
        }

        public static void AddLootToItem(Predicate<Item> predicate, IItemDropRule rule) {
            ItemPredicatedLootList.Add(new(predicate, rule));
        }
        ///////////////////////////////////////

        private static void IterateID(int id, ILoot loot, Dictionary<int, List<IItemDropRule>> dict) {
            if (dict.TryGetValue(id, out List<IItemDropRule> list)) {
                foreach (IItemDropRule rule in list) {
                    loot.Add(rule);
                }
            }
        }

        private static void IteratePredicates<T>(T subject, ILoot loot, List<Tuple<Predicate<T>, IItemDropRule>> list) where T : Entity {
            foreach (Tuple<Predicate<T>, IItemDropRule> t in list) {
                if (t.Item1.Invoke(subject)) {
                    loot.Add(t.Item2);
                }
            }
        }

        internal static void IterateNPCLoot(NPC npc, NPCLoot loot) {
            IterateID(npc.type, loot, NPCLootList);
            IteratePredicates(npc, loot, NPCPredicatedLootList);
        }

        internal static void IterateItemLoot(Item item, ItemLoot loot) {
            IterateID(item.type, loot, ItemLootList);
            IteratePredicates(item, loot, ItemPredicatedLootList);
        }

    }

    internal class FaeLootNPC : GlobalNPC {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot) {
            FaeLootUtils.IterateNPCLoot(npc, npcLoot);
        }
    }

    internal class FaeLootItem : GlobalItem {
        public override void ModifyItemLoot(Item item, ItemLoot itemLoot) {
            FaeLootUtils.IterateItemLoot(item, itemLoot);
        }
    }
}
