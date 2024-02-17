using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

namespace MemeSpawnSystem;

public class CustomSpawnerConfig
{
    public string Name { get; set; }
    public GameObject Prefab { get; set; }
    public Heightmap.Biome Biome { get; set; }
    public Heightmap.BiomeArea BiomeArea { get; set; }
    public float SpawnChance { get; set; }
    public int MaxSpawned { get; set; }

    public float SpawnInterval { get; set; }
    public float SpawnDistance { get; set; }

    public float SpawnRadiusMin { get; set; }

    public float SpawnRadiusMax { get; set; }

    public string RequiredGlobalKey { get; set; }

    public List<Heightmap.BiomeArea> RequiredEnvironments { get; set; }

    public int GroupSizeMin { get; set; }
    public int GroupSizeMax { get; set; }
    public float GroupRadius { get; set; }
    public bool SpawnAtNight { get; set; }
    public bool SpawnAtDay { get; set; }
    public float MinAltitude { get; set; }
    public float MaxAltitude { get; set; }
    public float MinTilt { get; set; }
    public float MaxTilt { get; set; }
    public bool InForest { get; set; }
    public bool OutsideForest { get; set; }
    public float MinOceanDepth { get; set; }
    public float MaxOceanDepth { get; set; }
    public bool HuntPlayer { get; set; }
    public float GroundOffset { get; set; }
    public int MaxLevel { get; set; }
    public int MinLevel { get; set; }
    public float LevelUpMinCenterDistance { get; set; }
    public float OverrideLevelupChance { get; set; }
    public bool Foldout { get; set; }
}

public static class CustomSpawnerManager
{
    private static List<CustomSpawnerConfig> customSpawners = new List<CustomSpawnerConfig>();

    public static void RegisterSpawner(CustomSpawnerConfig config)
    {
        customSpawners.Add(config);
    }

    internal static void AddCustomSpawnersToGame(SpawnSystemList spawnSystemList)
    {
        foreach (CustomSpawnerConfig? config in customSpawners)
        {
            SpawnSystem.SpawnData spawnData = new SpawnSystem.SpawnData
            {
                m_name = config.Name,
                m_enabled = true,
                m_prefab = config.Prefab,
                m_biome = config.Biome,
                m_biomeArea = config.BiomeArea,
                m_maxSpawned = config.MaxSpawned,
                m_spawnInterval = config.SpawnInterval,
                m_spawnChance = config.SpawnChance,
                m_spawnDistance = config.SpawnDistance,
                m_spawnRadiusMin = config.SpawnRadiusMin,
                m_spawnRadiusMax = config.SpawnRadiusMax,
                m_requiredGlobalKey = config.RequiredGlobalKey,
                m_requiredEnvironments = new List<string>(),
                m_groupSizeMin = config.GroupSizeMin,
                m_groupSizeMax = config.GroupSizeMax,
                m_groupRadius = config.GroupRadius,
                m_spawnAtNight = config.SpawnAtNight,
                m_spawnAtDay = config.SpawnAtDay,
                m_minAltitude = config.MinAltitude,
                m_maxAltitude = config.MaxAltitude,
                m_minTilt = config.MinTilt,
                m_maxTilt = config.MaxTilt,
                m_inForest = config.InForest,
                m_outsideForest = config.OutsideForest,
                m_minOceanDepth = config.MinOceanDepth,
                m_maxOceanDepth = config.MaxOceanDepth,
                m_huntPlayer = config.HuntPlayer,
                m_groundOffset = config.GroundOffset,
                m_maxLevel = config.MaxLevel,
                m_minLevel = config.MinLevel,
                m_levelUpMinCenterDistance = config.LevelUpMinCenterDistance,
                m_overrideLevelupChance = config.OverrideLevelupChance,
                m_foldout = config.Foldout
            };

            spawnSystemList.m_spawners.Add(spawnData);
        }
    }
}

[HarmonyPatch(typeof(SpawnSystem), nameof(SpawnSystem.Awake))]
static class SpawnSystemAwakePatch
{
    static void Postfix(SpawnSystem __instance)
    {
        foreach (SpawnSystem? spawnSystem in SpawnSystem.m_instances)
        {
            SpawnSystemList? customSpawnSystemList = spawnSystem.m_spawnLists.Find(s => s.name == "CustomSpawnerList");
            if (customSpawnSystemList == null)
            {
                customSpawnSystemList = new GameObject("CustomSpawnerList").AddComponent<SpawnSystemList>();
                spawnSystem.m_spawnLists.Add(customSpawnSystemList);
            }

            CustomSpawnerManager.AddCustomSpawnersToGame(customSpawnSystemList);
        }
    }
}