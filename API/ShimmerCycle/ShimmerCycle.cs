using FaeLibrary.Implementation;
using Microsoft.Build.Utilities;
using rail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace FaeLibrary.API.ShimmerCycle
{
    public abstract class ShimmerCycle : ModType {   

        protected sealed override void Register() {
            ShimmerCycleActivationSystem.ShimmerCycles.Add(this);
        }

        public sealed override void SetupContent() {
            Conditions = new List<Condition>();
            SetStaticDefaults();
            BeforeItems = new List<int>[BaseItems.Length];
            AfterItems = new List<int>[BaseItems.Length];
            DisabledBaseItems = new bool[BaseItems.Length];
            for (int i = 0; i < BaseItems.Length; i++) {
                BeforeItems[i] = new();
                AfterItems[i] = new();
                DisabledBaseItems[i] = false;
                if (ShimmerCycleActivationSystem.WhichShimmerCycleThisItemBelongsTo.ContainsKey(BaseItems[i])) {
                    throw new UsageException("Shimmer Cycle " + Name + " has item with ID " + BaseItems[i] + " (Name \"" + ContentSamples.ItemsByType[BaseItems[i]].Name + "\") that is already in a different Shimmer Cycle!");
                } else {
                    ShimmerCycleActivationSystem.WhichShimmerCycleThisItemBelongsTo.Add(BaseItems[i], this);
                }
            }

        }

        internal abstract int[] BaseItems { get; } // I sadly cannot make this private :/
        public List<Condition> Conditions { get; private set; }
        private List<int>[] BeforeItems { get; set; }
        private List<int>[] AfterItems { get; set; }
        private bool[] DisabledBaseItems { get; set; }

        public void DisableBaseItem(int type)
        {
            for (int i = 0; i < BaseItems.Length; i++)
            {
                if (BaseItems[i] == type)
                {
                    DisabledBaseItems[i] = true;
                    ShimmerCycleActivationSystem.WhichShimmerCycleThisItemBelongsTo.Remove(type);
                }
            }
        }

        public bool TryInsertBefore(int baseItem, int newItem)
        {
            for (int i = 0; i < BaseItems.Length; i++)
            {
                if (BaseItems[i] == baseItem)
                {
                    if (!ShimmerCycleActivationSystem.WhichShimmerCycleThisItemBelongsTo.ContainsKey(newItem))
                    {
                        BeforeItems[i].Add(newItem);
                        ShimmerCycleActivationSystem.WhichShimmerCycleThisItemBelongsTo.Add(newItem, this);
                        return true; // Successful insert
                    }
                    return false; // The item is already part of a shimmer cycle
                }
            }
            return false; //Invalid base item given
        }

        public bool TryInsertAfter(int baseItem, int newItem)
        {
            for (int i = 0; i < BaseItems.Length; i++)
            {
                if (BaseItems[i] == baseItem)
                {
                    if (!ShimmerCycleActivationSystem.WhichShimmerCycleThisItemBelongsTo.ContainsKey(newItem))
                    {
                        AfterItems[i].Add(newItem);
                        ShimmerCycleActivationSystem.WhichShimmerCycleThisItemBelongsTo.Add(newItem, this);
                        return true; // Successful insert
                    }
                    return false; // The item is already part of a shimmer cycle
                }
            }
            return false; //Invalid base item given
        }

        public void ProcessShimmerCycle()
        {
            int? firstItem = null;
            int? lastItem = null;
            int previousItem = -1;
            int currentItem;
            for (int i = 0; i < BaseItems.Length; i++)
            {
                for (int j = 0; j < BeforeItems[i].Count; j++)
                {
                    currentItem = BeforeItems[i][j];
                    ProcessShimmerCycleSnippet(ref firstItem, ref lastItem, ref previousItem, ref currentItem);
                }
                if (!DisabledBaseItems[i])
                {
                    currentItem = BaseItems[i];
                    ProcessShimmerCycleSnippet(ref firstItem, ref lastItem, ref previousItem, ref currentItem);
                }
                for (int j = 0; j < AfterItems[i].Count; j++)
                {
                    currentItem = AfterItems[i][j];
                    ProcessShimmerCycleSnippet(ref firstItem, ref lastItem, ref previousItem, ref currentItem);
                }
            }

            if (firstItem.HasValue)
            {
                ItemID.Sets.ShimmerTransformToItem[lastItem.Value] = firstItem.Value;
            }
        }

        private void ProcessShimmerCycleSnippet(ref int? firstItem, ref int? lastItem, ref int previousItem, ref int currentItem)
        {
            if (firstItem.HasValue)
            {
                ItemID.Sets.ShimmerTransformToItem[previousItem] = currentItem;
            }
            else
            {
                firstItem = currentItem;
            }
            previousItem = currentItem;
            lastItem = currentItem;
        }

        public static ShimmerCycle GetInstance<T>() where T : ShimmerCycle {
            foreach (ShimmerCycle cycle in ShimmerCycleActivationSystem.ShimmerCycles) {
                if (cycle is T) {
                    return cycle;
                }
            }
            return null;
        }

    }
}
