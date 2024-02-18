using BepInEx;
using BepInEx.Configuration;
using CreatureManager;
using HarmonyLib;
using ItemManager;
using LocalizationManager;
using ServerSync;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;



namespace MemeHeim
{
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    public class MemeHeim : BaseUnityPlugin
    {
        private const string ModName = "MemeHeim";
        private const string ModVersion = "0.0.6";
        private const string ModGUID = "org.bepinex.plugins.memeheim";

        private static readonly ConfigSync configSync = new(ModName) { DisplayName = ModName, CurrentVersion = ModVersion, MinimumRequiredVersion = ModVersion };

        private static ConfigEntry<Toggle> serverConfigLocked = null!;

        private ConfigEntry<T> config<T>(string group, string name, T value, ConfigDescription description, bool synchronizedSetting = true)
        {
            ConfigEntry<T> configEntry = Config.Bind(group, name, value, description);

            SyncedConfigEntry<T> syncedConfigEntry = configSync.AddConfigEntry(configEntry);
            syncedConfigEntry.SynchronizedConfig = synchronizedSetting;

            return configEntry;
        }

        private ConfigEntry<T> config<T>(string group, string name, T value, string description, bool synchronizedSetting = true) => config(group, name, value, new ConfigDescription(description), synchronizedSetting);

        private enum Toggle
        {
            On = 1,
            Off = 0
        }


        public void Awake()
        {
            Localizer.Load();

            GameObject fx_meme_death = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "fx_meme_death");

            GameObject sfx_dora_alerted = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "sfx_dora_alerted"); 
            GameObject sfx_dora_death = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "sfx_dora_death");
            GameObject sfx_dora_idle = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "sfx_dora_idle");
            GameObject sfx_dora_swing = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "sfx_dora_swing");
            
            GameObject sfx_spongebob_alerted = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "sfx_spongebob_alerted");
            GameObject sfx_spongebob_attack = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "sfx_spongebob_attack");
            GameObject sfx_spongebob_death = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "sfx_spongebob_death");
            GameObject sfx_spongebob_hit = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "sfx_spongebob_hit");
            GameObject sfx_spongebob_idle = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "sfx_spongebob_idle");
            GameObject Spongebob_Attack = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "Spongebob_Attack");
            GameObject vfx_spongesparks = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "vfx_spongesparks");

            GameObject sfx_thomas_alert = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "sfx_thomas_alert");
            GameObject sfx_thomas_attack = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "sfx_thomas_attack");
            GameObject thomas_attack = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "thomas_attack");
            GameObject thomas_attack_aoe = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "thomas_attack_aoe");
            GameObject vfx_thomas_spray = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "vfx_thomas_spray");
            GameObject sfx_thomas_idle = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "sfx_thomas_idle");

            GameObject sfx_shrek_elite_alerted = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "sfx_shrek_elite_alerted");
            GameObject sfx_shrek_elite_attack = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "sfx_shrek_elite_attack");
            GameObject sfx_shrek_elite_death = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "sfx_shrek_elite_death");
            GameObject sfx_shrek_elite_idle = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "sfx_shrek_elite_idle");
            GameObject sfx_shrek_hit = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "sfx_shrek_hit");
            GameObject Shrek_attack = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "Shrek_attack");
            GameObject vfx_shrek_hit = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "vfx_shrek_hit");

            GameObject sfx_pepe_alerted = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "sfx_pepe_alerted");
            GameObject sfx_pepe_attack = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "sfx_pepe_attack");
            GameObject sfx_pepe_death = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "sfx_pepe_death");
            GameObject sfx_pepe_hit = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "sfx_pepe_hit");
            GameObject Pepe_Attack = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "Pepe_Attack");
            GameObject Pepe_Projectile = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "Pepe_Projectile");
            GameObject Pepe_Explosion = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "Pepe_Explosion");

            GameObject big_smoke_bat = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "big_smoke_bat");
            GameObject Big_Smoke_Bomb = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "Big_Smoke_Bomb");
            GameObject BigSmoke_Projectile = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "BigSmoke_Projectile");
            GameObject sfx_bigsmoke_alerted = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "sfx_bigsmoke_alerted");
            GameObject sfx_bigsmoke_attack = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "sfx_bigsmoke_attack");
            GameObject sfx_bigsmoke_death = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "sfx_bigsmoke_death");
            GameObject sfx_bigsmoke_hit = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "sfx_bigsmoke_hit");
            GameObject sfx_bigsmoke_idle = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "sfx_bigsmoke_idle");
            GameObject sfx_bigsmoke_glass = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "sfx_bigsmoke_glass");

            GameObject fx_knuckles_Fire_hit = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "fx_knuckles_Fire_hit");
            GameObject knuckles_fire_aoe = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "knuckles_fire_aoe");
            GameObject knuckles_fireball_projectile = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "knuckles_fireball_projectile");
            GameObject Ugandan_Fireball = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "Ugandan_Fireball");
            GameObject Ugandan_Punch = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "Ugandan_Punch");
            GameObject sfx_knuckles_alerted = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "sfx_knuckles_alerted");
            GameObject sfx_knuckles_attack = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "sfx_knuckles_attack");
            GameObject sfx_knuckles_death = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "sfx_knuckles_death");
            GameObject sfx_knuckles_hit = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "sfx_knuckles_hit");
            GameObject sfx_knuckles_idle = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "sfx_knuckles_idle");

            GameObject sfx_ricardo_despawn = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "sfx_ricardo_despawn");
            GameObject vfx_ricardo_despawn = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "vfx_ricardo_despawn");
            GameObject Ricardo = ItemManager.PrefabManager.RegisterPrefab("doradestroyer", "Ricardo");
            MemeSpawnSystem.CustomSpawnerManager.RegisterSpawner(new MemeSpawnSystem.CustomSpawnerConfig
            {
                Name = "Ricardo",
                Prefab = Ricardo,
                Biome = Heightmap.Biome.Meadows,
                BiomeArea = Heightmap.BiomeArea.Median,
                SpawnChance = 1,
                MaxSpawned = 1,
                SpawnInterval = 6000,
                SpawnDistance = 10,
                SpawnRadiusMin = 35,
                SpawnRadiusMax = 35,
                RequiredGlobalKey = "defeated_gdking",
                RequiredEnvironments = new List<Heightmap.BiomeArea>(),
                GroupSizeMin = 1,
                GroupSizeMax = 1,
                GroupRadius = 3,
                SpawnAtNight = true,
                SpawnAtDay = false,
                MinAltitude = 0,
                MaxAltitude = 1000,
                MinTilt = 0,
                MaxTilt = 35,
                InForest = true,
                OutsideForest = false,
                MinOceanDepth = 0,
                MaxOceanDepth = 0,
                HuntPlayer = false,
                GroundOffset = 0.5f,
                MaxLevel = 1,
                MinLevel = 1,
                LevelUpMinCenterDistance = 0,
                OverrideLevelupChance = -1,
                Foldout = false
            });


            Item Meme_Mt_Dew = new("doradestroyer", "Meme_Mt_Dew");
            Item Meme_Snacks = new("doradestroyer", "Meme_Snacks");

            Item Thomas_Joint = new("doradestroyer", "Thomas_Joint");
            Thomas_Joint.Crafting.Add(CraftingTable.Workbench, 2);
            Thomas_Joint.RequiredItems.Add("Wood", 5);
            Thomas_Joint.RequiredItems.Add("Resin", 2);
            Thomas_Joint.CraftAmount = 1;

            Creature Dora_The_Destroyer = new("doradestroyer", "Dora_The_Destroyer")            //add creature
			{
				Biome = Heightmap.Biome.BlackForest,
                CanBeTamed = true,
                FoodItems = "Thomas_Joint, Meme_Mt_Dew, Meme_Snacks",
                FedDuration = 600,
                TamingTime = 1200,
                CreatureFaction = Character.Faction.ForestMonsters,
                CanSpawn = true,
                SpawnChance = 15,
                GroupSize = new Range(1, 2),
                CheckSpawnInterval = 1600,
                SpecificSpawnTime = SpawnTime.Day,
                Maximum = 1
            };
            Dora_The_Destroyer.Drops["Amber"].Amount = new Range(1, 2);
            Dora_The_Destroyer.Drops["Amber"].DropChance = 100f;
            Dora_The_Destroyer.Drops["Ruby"].Amount = new Range(1, 2);
            Dora_The_Destroyer.Drops["Ruby"].DropChance = 100f;
            Dora_The_Destroyer.Drops["Coins"].Amount = new Range(10, 20);
            Dora_The_Destroyer.Drops["Coins"].DropChance = 100f;

            Creature Thomas_The_Hate_Engine = new("doradestroyer", "Thomas_The_Hate_Engine")            //add creature
            {
                Biome = Heightmap.Biome.Plains,
                CanBeTamed = true,
                FoodItems = "Thomas_Joint, Meme_Mt_Dew, Meme_Snacks",
                FedDuration = 600,
                TamingTime = 1200,
                CreatureFaction = Character.Faction.PlainsMonsters,
                CanSpawn = true,
                SpawnChance = 25,
                GroupSize = new Range(1, 2),
                CheckSpawnInterval = 1600,
                SpecificSpawnTime = SpawnTime.Always,
                Maximum = 1

            };
            Dora_The_Destroyer.Drops["Amber"].Amount = new Range(1, 2);
            Dora_The_Destroyer.Drops["Amber"].DropChance = 100f;
            Dora_The_Destroyer.Drops["Ruby"].Amount = new Range(1, 2);
            Dora_The_Destroyer.Drops["Ruby"].DropChance = 100f;
            Dora_The_Destroyer.Drops["Coins"].Amount = new Range(50, 80);
            Dora_The_Destroyer.Drops["Coins"].DropChance = 100f;

            Creature SpongeBob_Angry_Pants = new("doradestroyer", "SpongeBob_Angry_Pants")            //add creature
            {
                Biome = Heightmap.Biome.Meadows,
                CanBeTamed = true,
                FoodItems = "Thomas_Joint, Meme_Mt_Dew, Meme_Snacks",
                FedDuration = 600,
                TamingTime = 1200,
                CreatureFaction = Character.Faction.ForestMonsters,
                CanSpawn = true,
                SpawnChance = 25,
                GroupSize = new Range(1, 4),
                CheckSpawnInterval = 1200,
                SpecificSpawnTime = SpawnTime.Day,
                Maximum = 4
            };
            Dora_The_Destroyer.Drops["Amber"].Amount = new Range(1, 2);
            Dora_The_Destroyer.Drops["Amber"].DropChance = 50f;
            Dora_The_Destroyer.Drops["Ruby"].Amount = new Range(1, 2);
            Dora_The_Destroyer.Drops["Ruby"].DropChance = 50f;
            Dora_The_Destroyer.Drops["Coins"].Amount = new Range(5, 10);
            Dora_The_Destroyer.Drops["Coins"].DropChance = 100f;

            Creature Shrek_Elite = new("doradestroyer", "Shrek_Elite")            //add creature
            {
                Biome = Heightmap.Biome.Swamp,
                CanBeTamed = true,
                FoodItems = "Thomas_Joint, Meme_Mt_Dew, Meme_Snacks",
                FedDuration = 600,
                TamingTime = 1200,
                CreatureFaction = Character.Faction.ForestMonsters,
                CanSpawn = true,
                SpawnChance = 15,
                GroupSize = new Range(1, 2),
                CheckSpawnInterval = 1600,
                SpecificSpawnTime = SpawnTime.Always,
                Maximum = 1
            };
            Dora_The_Destroyer.Drops["Amber"].Amount = new Range(1, 2);
            Dora_The_Destroyer.Drops["Amber"].DropChance = 100f;
            Dora_The_Destroyer.Drops["Ruby"].Amount = new Range(1, 2);
            Dora_The_Destroyer.Drops["Ruby"].DropChance = 100f;
            Dora_The_Destroyer.Drops["Coins"].Amount = new Range(20, 30);
            Dora_The_Destroyer.Drops["Coins"].DropChance = 100f;

            Creature Evil_Pepe = new("doradestroyer", "Evil_Pepe")            //add creature
            {
                Biome = Heightmap.Biome.Mountain,
                CanBeTamed = true,
                FoodItems = "ShocklateSmoothie, Thomas_Joint, Meme_Mt_Dew, Meme_Snacks",
                FedDuration = 600,
                TamingTime = 1200,
                CreatureFaction = Character.Faction.MountainMonsters,
                CanSpawn = true,
                SpawnChance = 15,
                GroupSize = new Range(1, 2),
                CheckSpawnInterval = 1200,
                SpecificSpawnTime = SpawnTime.Always,
                Maximum = 1
            };
            Dora_The_Destroyer.Drops["Amber"].Amount = new Range(1, 2);
            Dora_The_Destroyer.Drops["Amber"].DropChance = 100f;
            Dora_The_Destroyer.Drops["Ruby"].Amount = new Range(1, 2);
            Dora_The_Destroyer.Drops["Ruby"].DropChance = 100f;
            Dora_The_Destroyer.Drops["Coins"].Amount = new Range(30, 40);
            Dora_The_Destroyer.Drops["Coins"].DropChance = 100f;

            Creature Big_Smoke = new("doradestroyer", "Big_Smoke")            //add creature
            {
                Biome = Heightmap.Biome.Mistlands,
                CanBeTamed = true,
                FoodItems = "Thomas_Joint, Meme_Mt_Dew, Meme_Snacks",
                FedDuration = 600,
                TamingTime = 1200,
                CreatureFaction = Character.Faction.MistlandsMonsters,
                CanSpawn = true,
                SpawnChance = 15,
                GroupSize = new Range(1, 2),
                CheckSpawnInterval = 1600,
                SpecificSpawnTime = SpawnTime.Always,
                Maximum = 1
            };
            Dora_The_Destroyer.Drops["Amber"].Amount = new Range(2, 3);
            Dora_The_Destroyer.Drops["Amber"].DropChance = 100f;
            Dora_The_Destroyer.Drops["Ruby"].Amount = new Range(2, 3);
            Dora_The_Destroyer.Drops["Ruby"].DropChance = 10f;
            Dora_The_Destroyer.Drops["Coins"].Amount = new Range(50, 60);
            Dora_The_Destroyer.Drops["Coins"].DropChance = 100f;

            Creature Ugandan_Knuckles = new("doradestroyer", "Ugandan_Knuckles")            //add creature
            {
                Biome = Heightmap.Biome.Mistlands,
                CanBeTamed = true,
                FoodItems = "Thomas_Joint, Meme_Mt_Dew, Meme_Snacks",
                FedDuration = 600,
                TamingTime = 1200,
                CreatureFaction = Character.Faction.Dverger,
                CanSpawn = true,
                SpawnChance = 15,
                GroupSize = new Range(3, 4),
                CheckSpawnInterval = 1600,
                SpecificSpawnTime = SpawnTime.Always,
                Maximum = 4
            };
            Dora_The_Destroyer.Drops["Amber"].Amount = new Range(2, 3);
            Dora_The_Destroyer.Drops["Amber"].DropChance = 100f;
            Dora_The_Destroyer.Drops["Ruby"].Amount = new Range(2, 3);
            Dora_The_Destroyer.Drops["Ruby"].DropChance = 10f;
            Dora_The_Destroyer.Drops["Coins"].Amount = new Range(50, 60);
            Dora_The_Destroyer.Drops["Coins"].DropChance = 100f;

            Assembly assembly = Assembly.GetExecutingAssembly();
            Harmony harmony = new(ModGUID);
            harmony.PatchAll(assembly);
        }
    }



    [HarmonyPatch(typeof(MonsterAI), nameof(MonsterAI.Start))]
            static class MonsterAI_Start_Patch
        {
            static void Postfix(MonsterAI __instance)
            {
                if (Player.m_localPlayer && __instance.gameObject.name.Contains("Ugandan_Knuckles") && __instance.m_nview.IsOwner())
                {
                    __instance.ResetPatrolPoint();
                    __instance.SetFollowTarget(Player.m_localPlayer.gameObject);
                }


            }

        }
    }		
