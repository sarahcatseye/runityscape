using System;
using System.Collections.Generic;
using Scripts.Game.Defined.Serialized.Buffs;
using Scripts.Model.Buffs;
using Scripts.Model.Characters;
using Scripts.Model.Items;
using Scripts.Model.Spells;
using Scripts.Model.Stats;
using Scripts.Game.Defined.Spells;
using Scripts.Game.Defined.Unserialized.Items;
using Scripts.Game.Defined.Unserialized.Spells;
using Scripts.Game.Defined.Unserialized.Buffs;
using Scripts.Game.Defined.Characters;
using Scripts.Model.Pages;

namespace Scripts.Game.Defined.Serialized.Items {

    public class SharkBait : ConsumableItem {

        public SharkBait() : base("food-chain", 100, TargetType.SELF, "Shark Bait", "Creates a very attractive decoy (for sharks). No villagers were harmed in the making of this product.") {
            this.flags.Remove(Model.Items.Flag.USABLE_OUT_OF_COMBAT);
        }

        public override IList<SpellEffect> GetEffects(Page page, Character caster, Character target) {
            target = caster;
            Func<Character> summonDecoyFunc = () => {
                Character tentacle = OceanNPCs.SharkBaitDecoy();
                return tentacle;
            };
            return new SpellEffect[] {
                new SummonEffect(page.GetSide(target), page, summonDecoyFunc, 1)
            };
        }
    }

    public class FishHook : EquippableItem {

        public FishHook() : base("fishing-hook", EquipType.WEAPON, 50, "Fish Hook", "A used fish hook.") {
            AddFlatStatBonus(StatType.STRENGTH, 5);
            AddFlatStatBonus(StatType.AGILITY, 1);
            AddFlatStatBonus(StatType.VITALITY, -1);
        }

        public override PermanentBuff CreateBuff() {
            return new FishShook();
        }
    }

    public class SharkFin : HealingItem {
        private const int HEALING_AMOUNT = 25;

        public SharkFin() : base("Shark Fin", "shark-fin", "A delicious and illegal fin from a shark.", 100, HEALING_AMOUNT) {
        }
    }

    public class Cleansing : ConsumableItem {
        private const int DAMAGE = 5;

        public Cleansing() : base(
            "hospital-cross",
            20,
            TargetType.ONE_ALLY,
            "Cleansing",
            string.Format("Caster sacrifices {0} {1} to remove all dispellable buffs from a target.", DAMAGE, StatType.HEALTH)) {
        }

        public override IList<SpellEffect> GetEffects(Page page, Character caster, Character target) {
            return new SpellEffect[] {
                new DispelAllBuffs(target.Buffs),
                new AddToModStat(caster.Stats, StatType.HEALTH, -DAMAGE)
            };
        }
    }

    public class VitalityTrinket : SingleStatTrinket {

        public VitalityTrinket() : base(StatType.VITALITY, 50, 4, "Life") {
        }
    }

    public class AgilityTrinket : SingleStatTrinket {

        public AgilityTrinket() : base(StatType.AGILITY, 50, 4, "Swiftness") {
        }
    }

    public class IntellectTrinket : SingleStatTrinket {

        public IntellectTrinket() : base(StatType.INTELLECT, 50, 2, "Smarts") {
        }
    }

    public class StrengthTrinket : SingleStatTrinket {

        public StrengthTrinket() : base(StatType.STRENGTH, 50, 2, "Force") {
        }
    }
}