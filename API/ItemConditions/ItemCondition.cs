using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace FaeLibrary.API.ItemConditions {
    public record class ItemCondition(LocalizedText Text, Func<Item, bool> Predicate) {
        public ItemCondition(string LocalizationKey, Func<Item, bool> Predicate) : this(Language.GetOrRegister(LocalizationKey), Predicate) { }
        public ItemCondition(Mod mod, string name, Func<Item, bool> Predicate) : this(mod.GetLocalization($"ItemConditions.{name}"), Predicate) { }
        public bool IsMet(Item item) => Predicate(item);

        public static LocalizedText QuickName(string name) => Language.GetOrRegister($"Mods.{nameof(FaeLibrary)}.ItemConditions.{name}");

        public static readonly ItemCondition Any = new(QuickName(nameof(Any)), (item) => true);
        public static readonly ItemCondition IsWeapon = new(QuickName(nameof(IsWeapon)), (Item item) => item.damage > 0 && item.useStyle != ItemUseStyleID.None);
        public static readonly ItemCondition IsMeleeWeapon = new(QuickName(nameof(IsMeleeWeapon)), (Item item) => item.DamageType.CountsAsClass(DamageClass.Melee) && IsWeapon.IsMet(item));
        public static readonly ItemCondition IsWhipWeapon = new(QuickName(nameof(IsWhipWeapon)), (Item item) => item.DamageType.CountsAsClass(DamageClass.SummonMeleeSpeed) && IsWeapon.IsMet(item));
        public static readonly ItemCondition IsRangedWeapon = new(QuickName(nameof(IsRangedWeapon)), (Item item) => item.DamageType.CountsAsClass(DamageClass.Ranged) && IsWeapon.IsMet(item));
        public static readonly ItemCondition IsMagicWeapon = new(QuickName(nameof(IsMagicWeapon)), (Item item) => item.DamageType.CountsAsClass(DamageClass.Magic) && IsWeapon.IsMet(item));
        public static readonly ItemCondition IsSummonerWeapon = new(QuickName(nameof(IsSummonerWeapon)), (Item item) => item.DamageType.CountsAsClass(DamageClass.Summon) && IsWeapon.IsMet(item));
        public static readonly ItemCondition IsMinionWeapon = new(QuickName(nameof(IsMinionWeapon)), (Item item) => item.DamageType.CountsAsClass(DamageClass.Summon) && !item.sentry && item.shoot > ProjectileID.None && !ProjectileID.Sets.IsAWhip[item.shoot]);
        public static readonly ItemCondition IsSentryWeapon = new(QuickName(nameof(IsSentryWeapon)), (Item item) => item.sentry);
        public static readonly ItemCondition IsAccessory = new(QuickName(nameof(IsAccessory)), (Item item) => item.accessory);

        // Logical
        public static ItemCondition GrammaticalAnd(ItemCondition cond1, ItemCondition cond2) => new(QuickName(nameof(GrammaticalAnd)).WithFormatArgs(cond1.Text, cond2.Text), (item) => cond1.IsMet(item) || cond2.IsMet(item));
        public static ItemCondition GrammaticalAnd3(ItemCondition cond1, ItemCondition cond2, ItemCondition cond3) => new(QuickName(nameof(GrammaticalAnd3)).WithFormatArgs(cond1.Text, cond2.Text, cond3.Text), (item) => cond1.IsMet(item) || cond2.IsMet(item) || cond3.IsMet(item));
        public static ItemCondition Not(ItemCondition cond) => new(QuickName(nameof(Not)).WithFormatArgs(cond.Text), (item) => !cond.IsMet(item));
        public static ItemCondition ThisButNotThis(ItemCondition trueCond, ItemCondition falseCond) => new(QuickName(nameof(ThisButNotThis)).WithFormatArgs(trueCond.Text, falseCond.Text), (item) => trueCond.IsMet(item) && !falseCond.IsMet(item));

    }
}
