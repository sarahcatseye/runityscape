﻿using Scripts.Model.Characters;
using Scripts.Model.Spells;
using Scripts.Model.Stats;
using Scripts.Model.Stats.Attributes;
using Scripts.Model.Stats.Resources;
using Scripts.View.Effects;
using Scripts.View.Portraits;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Result {
    public Type Type { get; set; }
    public Func<Character, Character, bool> IsState;
    public Func<Character, Character, bool> IsIndefinite;
    public Func<Character, Character, float> Duration;
    public Func<Character, Character, float> TimePerTick;
    public Action<Spell> React;
    public Action<Spell> Witness;
    public Func<Character, Character, Calculation> Calculation;
    public Action<Character, Character, Calculation> Perform;
    public Action<Character, Character> OnStart;
    public Action<Character, Character> OnEnd;
    public Func<Character, Character, Calculation, string> CreateText;
    public Func<Character, Character, Calculation, string> Sound;
    public Func<Character, Character, Calculation, IList<CharacterEffect>> Sfx;

    public Result(Func<Character, Character, bool> isState = null,
              Func<Character, Character, float> duration = null,
              Func<Character, Character, float> timePerTick = null,
              Action<Spell> react = null,
              Action<Spell> witness = null,
              Func<Character, Character, bool> isIndefinite = null,
              Func<Character, Character, Calculation> calculation = null,
              Action<Character, Character, Calculation> perform = null,
              Action<Character, Character> onStart = null,
              Action<Character, Character> onEnd = null,
              Func<Character, Character, Calculation, string> createText = null,
              Func<Character, Character, Calculation, string> sound = null,
              Func<Character, Character, Calculation, IList<CharacterEffect>> sfx = null) {
        this.IsState = isState ?? ((c, t) => { return false; });
        this.Duration = duration ?? ((c, t) => { return 0; });
        this.TimePerTick = timePerTick ?? ((c, t) => { return 0; });
        this.React = react ?? ((s) => { });
        this.Witness = witness ?? ((s) => { });
        this.IsIndefinite = isIndefinite ?? ((c, t) => { return false; });
        this.Calculation = calculation ?? ((c, t) => { return new Calculation(); });
        this.Perform = perform ?? ((c, t, calc) => { NumericPerform(c, t, calc); });
        this.OnStart = onStart ?? ((c, t) => { });
        this.OnEnd = onEnd ?? ((c, t) => { });
        this.CreateText = createText ?? ((c, t, calc) => { return ""; });
        this.Sound = sound ?? ((c, t, calc) => { return ""; });
        this.Sfx = sfx ?? ((c, t, calc) => {
            return AppendToStandard(c, t, calc, new CharacterEffect[0]);
        });
    }

    public static void NumericPerform(Character caster, Character target, Calculation calculation) {
        foreach (KeyValuePair<AttributeType, PairedValue> pair in calculation.CasterAttributes) {
            caster.AddToAttribute(pair.Key, true, pair.Value.True, true);
            caster.AddToAttribute(pair.Key, false, pair.Value.False, true);
        }
        foreach (KeyValuePair<AttributeType, PairedValue> pair in calculation.TargetAttributes) {
            target.AddToAttribute(pair.Key, true, pair.Value.True, true);
            target.AddToAttribute(pair.Key, false, pair.Value.False, true);
        }
        foreach (KeyValuePair<ResourceType, PairedValue> pair in calculation.CasterResources) {
            caster.AddToResource(pair.Key, true, pair.Value.True, true);
            caster.AddToResource(pair.Key, false, pair.Value.False, true);
        }
        foreach (KeyValuePair<ResourceType, PairedValue> pair in calculation.TargetResources) {
            target.AddToResource(pair.Key, true, pair.Value.True, true);
            target.AddToResource(pair.Key, false, pair.Value.False, true);
        }
    }

    public static void NumericUndo(Character caster, Character target, Calculation calculation) {
        foreach (KeyValuePair<AttributeType, PairedValue> pair in calculation.CasterAttributes) {
            caster.AddToAttribute(pair.Key, true, -pair.Value.True, true);
            caster.AddToAttribute(pair.Key, false, -pair.Value.False, true);
        }
        foreach (KeyValuePair<AttributeType, PairedValue> pair in calculation.TargetAttributes) {
            target.AddToAttribute(pair.Key, true, -pair.Value.True, true);
            target.AddToAttribute(pair.Key, false, -pair.Value.False, true);
        }
        foreach (KeyValuePair<ResourceType, PairedValue> pair in calculation.CasterResources) {
            caster.AddToResource(pair.Key, true, -pair.Value.True, true);
            caster.AddToResource(pair.Key, false, -pair.Value.False, true);
        }
        foreach (KeyValuePair<ResourceType, PairedValue> pair in calculation.TargetResources) {
            target.AddToResource(pair.Key, true, -pair.Value.True, true);
            target.AddToResource(pair.Key, false, -pair.Value.False, true);
        }
    }

    private const float BASE_SHAKE = 3;

    public static ShakeEffect ShakeBasedOnDamage(Character target, Calculation c) {
        float damage = Mathf.Min(c.TargetResources[ResourceType.HEALTH].False, 0);
        float shakePower = 0;
        if (target.GetResourceCount(ResourceType.HEALTH, true) > 0) {
            shakePower = BASE_SHAKE * damage / target.GetResourceCount(ResourceType.HEALTH, true);
        }
        return new ShakeEffect(target.Presenter.PortraitView, shakePower, 0);
    }

    public static IList<ShakeEffect> ShakeBasedOnDamage(Character caster, Character target, Calculation c) {
        return new List<ShakeEffect>() { ShakeBasedOnDamage(caster, c), ShakeBasedOnDamage(target, c) };
    }

    public static IList<HitsplatEffect> StandardSplat(Character caster, Character target, Calculation c) {
        List<HitsplatEffect> splats = new List<HitsplatEffect>();
        PortraitView casterPv = caster.Presenter.PortraitView;
        PortraitView targetPv = target.Presenter.PortraitView;

        foreach (KeyValuePair<AttributeType, PairedValue> pair in c.CasterAttributes) {
            if (pair.Value.False != 0) {
                splats.Add(new HitsplatEffect(casterPv, pair.Key.Color, (pair.Value.False >= 0 ? "+" : "-") + pair.Key.ShortName));
            }
        }
        foreach (KeyValuePair<AttributeType, PairedValue> pair in c.TargetAttributes) {
            if (pair.Value.False != 0) {
                splats.Add(new HitsplatEffect(targetPv, pair.Key.Color, (pair.Value.False >= 0 ? "+" : "-") + pair.Key.ShortName));
            }
        }
        foreach (KeyValuePair<ResourceType, PairedValue> pair in c.CasterResources) {
            if (pair.Value.False != 0) {
                splats.Add(new HitsplatEffect(casterPv, ResourceType.DetermineColor(pair.Key, pair.Value.False), Util.Sign(pair.Value.False)));
            }
        }
        foreach (KeyValuePair<ResourceType, PairedValue> pair in c.TargetResources) {
            if (pair.Value.False != 0) {
                splats.Add(new HitsplatEffect(targetPv, ResourceType.DetermineColor(pair.Key, pair.Value.False), Util.Sign(pair.Value.False)));
            }
        }

        return splats;
    }

    public static IList<CharacterEffect> AppendToStandard(Character caster, Character target, Calculation calc, params CharacterEffect[] effects) {
        IList<HitsplatEffect> splats = StandardSplat(caster, target, calc);
        IList<ShakeEffect> shakes = ShakeBasedOnDamage(caster, target, calc);
        IList<CharacterEffect> defaultEffects = new List<CharacterEffect>();

        foreach (HitsplatEffect h in splats) {
            defaultEffects.Add(h);
        }

        foreach (ShakeEffect s in shakes) {
            defaultEffects.Add(s);
        }

        foreach (CharacterEffect c in effects) {
            defaultEffects.Add(c);
        }
        return defaultEffects;
    }
}