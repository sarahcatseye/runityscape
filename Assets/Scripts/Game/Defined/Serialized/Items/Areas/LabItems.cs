using Scripts.Game.Defined.Serialized.Buffs;
using Scripts.Game.Defined.Unserialized.Items;
using Scripts.Model.Buffs;
using Scripts.Model.Items;
using Scripts.Model.Stats;

namespace Scripts.Game.Defined.Serialized.Items {

    // Shop items
    public class HealingPotion : HealingItem {
        public const string NAME = "Hyper Potion";

        public HealingPotion() : base(NAME, "potion-ball", "Smells like apples and fish.", 50, 25, false) {
        }
    }

    public class LifeGem : HealingItem {

        public LifeGem() : base("Life Gem", "ubisoft-sun", string.Format("Made of concentrated {0}s.", HealingPotion.NAME), 100, 50, true) {
        }
    }

    // Final boss items

    public class EvilCloneArmor : EquippableItem {

        public EvilCloneArmor() : base("chain-mail", EquipType.ARMOR, 1, "Infernal Platebody", "A powerful armor pulsing with firey energy.") {
        }

        public override PermanentBuff CreateBuff() {
            return new FlamingArmor();
        }
    }

    public class EvilCloneTrinket : EquippableItem {
        private const int STAT_AMOUNT = 5;

        public EvilCloneTrinket()
            : base(
                  Util.GetSprite("gem-pendant"),
                  EquipType.TRINKET,
                  1,
                  "Healer's Vow",
                  "A pendant that greatly boosts magical power.") {
            AddFlatStatBonus(StatType.AGILITY, STAT_AMOUNT);
            AddFlatStatBonus(StatType.INTELLECT, STAT_AMOUNT);
        }

        public override PermanentBuff CreateBuff() {
            return new HighManaRegeneration();
        }
    }

    public class EvilFriendTrinket : EquippableItem {
        private const int STAT_AMOUNT = 10;

        public EvilFriendTrinket()
            : base(
                  Util.GetSprite("gem-pendant"),
                  EquipType.TRINKET,
                  1,
                  "Knight's Vow",
                  "A pendant that greatly boosts physical power.") {
            AddFlatStatBonus(StatType.STRENGTH, STAT_AMOUNT);
            AddFlatStatBonus(StatType.AGILITY, STAT_AMOUNT);
            AddFlatStatBonus(StatType.VITALITY, STAT_AMOUNT);
        }

        public override PermanentBuff CreateBuff() {
            return new SkillRegeneration();
        }
    }
}