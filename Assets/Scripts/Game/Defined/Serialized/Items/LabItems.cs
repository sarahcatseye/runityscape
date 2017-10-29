using Scripts.Game.Defined.Serialized.Buffs;
using Scripts.Game.Defined.Unserialized.Items;
using Scripts.Model.Buffs;
using Scripts.Model.Items;
using Scripts.Model.Stats;

namespace Scripts.Game.Defined.Serialized.Items {
    //Let's make some final boss items!

    public class EvilCloneArmor : EquippableItem {

        public EvilCloneArmor() : base("chain-mail", EquipType.ARMOR, 1, "Evil clone armor", "Your evil clone's armor, pulsing with firey energy") {

        }

        public override PermanentBuff CreateBuff() {
            return new FlamingArmor();
        }
    }

    public class EvilCloneTrinket : EquippableItem {
        private const int STAT_AMOUNT = 10;

        public EvilCloneTrinket()
            : base(
                  Util.GetSprite("gem-pendant"),
                  EquipType.TRINKET,
                  1,
                  "Your evil clone's trinket",
                  "A magical trinket providing a host of buffs") {
            AddFlatStatBonus(StatType.STRENGTH, STAT_AMOUNT);
            AddFlatStatBonus(StatType.AGILITY, STAT_AMOUNT);
            AddFlatStatBonus(StatType.VITALITY, STAT_AMOUNT);
            AddFlatStatBonus(StatType.INTELLECT, STAT_AMOUNT);
        }

        public override PermanentBuff CreateBuff() {
            return new RegenerateLotsOfMana();
        }
    }

    public class EvilFriendTrinket : EquippableItem {
        private const int STAT_AMOUNT = 10;

        public EvilFriendTrinket()
            : base(
                  Util.GetSprite("gem-pendant"),
                  EquipType.TRINKET,
                  1,
                  "Your friend's evil clone's trinket",
                  "A magical trinket providing a host of buffs") {
            AddFlatStatBonus(StatType.STRENGTH, STAT_AMOUNT);
            AddFlatStatBonus(StatType.AGILITY, STAT_AMOUNT);
            AddFlatStatBonus(StatType.VITALITY, STAT_AMOUNT);
            AddFlatStatBonus(StatType.INTELLECT, STAT_AMOUNT);
        }

        public override PermanentBuff CreateBuff() {
            return new RegenerateLotsOfSkill();
        }
    }
}