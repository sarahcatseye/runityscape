﻿using UnityEngine;
using System.Collections;
using System;

public class Petrify : SpellFactory {
    public Petrify() : base("Petrify", "", SpellType.OFFENSE, TargetType.SINGLE_ENEMY, abbreviation: "PFY", color: Color.magenta, isSelfTargetable: true) { }

    public override Hit CreateHit() {
        return new Hit(
            isState: (c, t, o) => true,
            isIndefinite: (c, t, o) => true,
            duration: (c, t, o) => 20f,
            timePerTick: (c, t, o) => 10f,
            onStart:
            (c, t, o) => t.IsCharging = false,
            onEnd:
            (c, t, o) => t.IsCharging = true
            );
    }
}
