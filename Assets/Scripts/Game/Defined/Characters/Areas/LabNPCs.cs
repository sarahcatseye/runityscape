﻿using Scripts.Game.Defined.Serialized.Buffs;
using Scripts.Game.Defined.Serialized.Items;
using Scripts.Game.Defined.Serialized.Spells;
using Scripts.Game.Defined.Serialized.Statistics;
using Scripts.Game.Defined.Unserialized.Buffs;
using Scripts.Game.Defined.Unserialized.Spells;
using Scripts.Game.Serialized;
using Scripts.Game.Serialized.Brains;
using Scripts.Game.Shopkeeper;
using Scripts.Model.Characters;
using Scripts.Model.Pages;

namespace Scripts.Game.Defined.Characters {

    public static class LabNPCs {

        public static Trainer Trainer(Page previous, Party party) {
            return new Trainer(
                    previous,
                    party,
                    Ruins.Villager(),
                    new PurchasedSpell(200, new Revive()),
                    new PurchasedSpell(200, new Inspire()),
                    new PurchasedSpell(500, new MagicMissile()),
                    new PurchasedSpell(500, new SelfHeal())
                );
        }

        public static Shop Shop(Page previous, Flags flags, Party party) {
            return new Shop(
                previous,
                "Gift Shop",
                flags,
                party,
                .7f,
                1,
                Ruins.Villager()
                );
        }

        public static InventoryMaster LabMaster(Page previous, Party party) {
            return new InventoryMaster(
                previous,
                party,
                Ruins.Villager(),
                8,
                10,
                500
                );
        }

        public static class Ruins {

            public static Character Villager() {
                return CharacterUtil.StandardEnemy(
                    new Stats(20, 8, 5, 1, 50),
                    new Look("Spectre",
                             "villager lab",
                             "A not-so-innocent villager.",
                             Breed.SPIRIT),
                    new Attacker()
                    )
                    .AddMoney(50);
            }

            public static Character Enforcer() {
                return CharacterUtil.StandardEnemy(
                    new Stats(12, 12, 8, 5, 75),
                    new Look("Enforcer",
                             "knight lab",
                             "Augmented knight.",
                             Breed.SPIRIT),
                    new LabKnight()
                    )
                    .AddSpells(new CrushingBlow())
                    .AddMoney(50);
            }

            public static Character Darkener() {
                return CharacterUtil.StandardEnemy(
                    new Stats(12, 5, 20, 15, 50),
                    new Look("Darkener", "illusionist lab", "Powerful illusionist.", Breed.SPIRIT),
                    new Illusionist()
                    )
                    .AddSpells(new Blackout())
                    .AddMoney(50);
            }

            public static Character BigKnightA() {
                return BigKnight("Perse", "big-knight-a");
            }

            public static Character BigKnightB() {
                return BigKnight("Verance", "big-knight-b");
            }

            public static Character Mage() {
                return CharacterUtil.StandardEnemy(
                        new Stats(12, 4, 20, 20, 80),
                        new Look("Warlock",
                                 "wizard lab",
                                 "Magical madman.",
                                 Breed.SPIRIT),
                        new Warlock()
                    ).AddSpells(new Inferno())
                    .AddBuff(new UnholyInsight())
                    .AddMoney(75);
            }

            public static Character Cleric() {
                return CharacterUtil.StandardEnemy(
                        new Stats(12, 4, 20, 20, 40),
                        new Look("Cultist",
                                 "white-mage lab",
                                 "Healer from Hell.",
                                 Breed.SPIRIT),
                        new Cleric()
                    ).AddSpells(new SetupDefend(), new PlayerHeal())
                    .AddBuff(new UnholyInsight())
                    .AddMoney(75);
            }

            private static Character BigKnight(string name, string sprite) {
                return CharacterUtil.StandardEnemy(
                    new Stats(15, 15, 10, 10, 120),
                    new Look(
                        name,
                        sprite,
                        "One of a pair of knights known for their determination.",
                        Breed.SPIRIT
                        ),
                    new LabBigKnight()
                    ).AddFlags(Flag.PERSISTS_AFTER_DEFEAT)
                    .AddBuff(new StandardCountdown())
                    .AddSpells(new UnholyRevival())
                    .AddMoney(100);
            }
        }

        public static class Ocean {

            public static Character Shark() {
                return CharacterUtil.StandardEnemy(
                    new Stats(5, 5, 6, 8, 60),
                    new Look(
                        "Razor Shark",
                        "shark lab",
                        "Shark who needs lotion.",
                        Breed.FISH
                        ),
                    new Attacker())
                    .AddBuff(new RougherSkin())
                    .AddItem(new Money(), Util.RandomRange(50, 100))
                    .AddMoney(150);
            }

            public static Character Siren() {
                return CharacterUtil.StandardEnemy(
                        new Stats(6, 4, 10, 10, 40),
                        new Look(
                            "Enthraller",
                            "siren lab",
                            "Sings a mean tune.",
                            Breed.FISH
                        ),
                        new Siren()
                    ).AddSpells(Game.Serialized.Brains.Siren.DEBUFF_LIST)
                    .AddMoney(150);
            }

            public static Character Tentacle() {
                Character c = CharacterUtil.StandardEnemy(
                        new Stats(7, 3, 25, 1, 20),
                        new Look(
                            "Lasher",
                            "tentacle lab",
                            "Tentacle belonging to a Leviathan.",
                            Breed.FISH
                            ),
                        new Attacker()
                    )
                    .AddItem(new OctopusLeg());
                if (Util.IsChance(.50)) {
                    c.AddBuff(new OnlyAffectedByHero());
                } else {
                    c.AddBuff(new OnlyAffectedByPartner());
                }
                return c;
            }

            public static Character Kraken() {
                return CharacterUtil.StandardEnemy(
                        new Stats(8, 10, 10, 20, 200),
                        new Look(
                            "Leviathan",
                            "kraken lab",
                            "Even bigger squid thing.",
                            Breed.FISH
                            ),
                        new Kraken()
                    )
                    .AddSpells(new SpawnLashers())
                    .AddSpells(new CrushingBlow())
                    .AddBuff(new StandardCountdown())
                    .AddStats(new Skill())
                    .AddMoney(200);
            }

            public static Character Elemental() {
                return CharacterUtil.StandardEnemy(
                    new Stats(9, 5, 20, 15, 20),
                    new Look(
                        "Undine",
                        "elemental lab",
                        "Sea elemental.",
                        Breed.FISH
                        ),
                    new Elemental())
                    .AddStats(new Mana())
                    .AddSpells(new WaterboltSingle(), new WaterboltMulti())
                    .AddBuff(new UnholyInsight())
                    .AddMoney(150);
            }

            public static Character DreadSinger() {
                return CharacterUtil.StandardEnemy(
                        new Stats(10, 5, 20, 20, 25),
                        new Look(
                            "Hellhound",
                            "shuck lab",
                            "Cursed lab canine.",
                            Breed.BEAST
                            ),
                        new DreadSinger())
                        .AddSpells(new NullifyHealing())
                        .AddSpells(new CastDelayedEternalDeath())
                        .AddItem(new Cleansing(), 1)
                        .AddMoney(150);
            }

            public static Character Swarm() {
                return CharacterUtil.StandardEnemy(
                    new Stats(2, 1, 5, 2, 15),
                    new Look(
                        "Myriad",
                        "swarm lab",
                        "Questionable member of the sea that travels in schools.",
                        Breed.FISH
                        ),
                    new Swarm())
                    .AddMoney(20);
            }
        }

        public static class Final {

            private static Character PlayerClone(Stats stats, Look look, Brain brain) {
                return CharacterUtil.StandardEnemy(stats, look, brain)
                    .RemoveFlags(Flag.DROPS_ITEMS, Flag.GIVES_EXPERIENCE)
                    .AddFlags(Flag.PERSISTS_AFTER_DEFEAT);
            }

            public static Character HeroClone() {
                return PlayerClone(
                    new Stats(15, 5, 20, 20, 150),
                    new Look("Memory H", "player evil", "A corrupted memory in a familiar form.", Breed.PROGRAMMER),
                    new Hero()
                    )
                    .AddStats(new Mana())
                    .AddSpells(
                        new Revive(),
                        new PlayerHeal(),
                        new SetupDefend(),
                        new MagicMissile())
                    .AddEquip(new EvilCloneTrinket(), 1)
                    .AddEquip(new EvilCloneArmor(), 1);
            }

            public static Character PartnerClone() {
                return PlayerClone(
                    new Stats(15, 10, 20, 10, 150),
                    new Look("Memory P", "partner evil", "A corrupted memory in a familiar form.", Breed.HUMAN),
                    new Partner()
                    )
                    .AddStats(new Skill())
                    .AddSpells(
                        new QuickAttack(),
                        new SelfHeal(),
                        new Inspire(),
                        new SetupDefend(),
                        new CrushingBlow(),
                        new Multistrike())
                    .AddEquip(new EvilFriendTrinket(), 1);
            }
        }
    }
}