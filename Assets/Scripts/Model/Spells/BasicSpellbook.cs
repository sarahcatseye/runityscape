﻿using Scripts.Model.Characters;
using UnityEngine;

namespace Scripts.Model.Spells {

    /// <summary>
    /// A simple spellbook abstraction.
    /// Most spellbooks can't target dead people and don't have cooldowns.
    /// So this implementation pre-overides these methods.
    /// </summary>
    public abstract class BasicSpellbook : SpellBook {
        private readonly bool isCastableOnDead;

        /// <summary>
        /// Normal priority constructor
        /// </summary>
        /// <param name="spellName">Name of the spell</param>
        /// <param name="sprite">Sprite of the spell</param>
        /// <param name="target">What kind of targets this spell can be used on</param>
        /// <param name="spell">What type of spell is this</param>
        public BasicSpellbook(string spellName, Sprite sprite, TargetType target, SpellType spell) : this(spellName, sprite, target, spell, PriorityType.NORMAL) { }

        /// <summary>
        /// Priority constructor
        /// </summary>
        /// <param name="spellName">Name of the spell</param>
        /// <param name="sprite">Sprite of the spell</param>
        /// <param name="target">What kind of targets this spell can be used on</param>
        /// <param name="spell">What type of spell is this</param>
        /// <param name="priority">Spell's priority</param>
        public BasicSpellbook(string spellName, Sprite sprite, TargetType target, SpellType spell, PriorityType priority) : base(spellName, sprite, target, spell, priority, "Perform") {
            this.isCastableOnDead = false;
        }

        protected sealed override bool IsMeetOtherCastRequirements(Character caster, Character target) {
            return (isCastableOnDead || target.Stats.State == Characters.State.ALIVE);
        }
    }
}