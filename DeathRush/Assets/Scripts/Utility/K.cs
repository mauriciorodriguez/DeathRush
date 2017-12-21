using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class K
{
    // ===== CSV CONFIG =====
    public const string CSV_PATH = "DeathrushConfig"; // Path dentro de resources

    public const string CSV_PASSIVE_SOLDIER = "soldier";
    public const string CSV_PASSIVE_BERSERK = "berserk";
    public const string CSV_PASSIVE_RUNNER = "runner";
    public const string CSV_PASSIVE_TECHNICIAN = "technician";
    public const string CSV_PASSIVE_SUPERSTAR = "superstar";

    public const string CSV_STAT_RESISTANCE = "resistance";
    public const string CSV_STAT_TOP_ACCELERATION = "topAcceleration";
    public const string CSV_STAT_DAMAGE = "damage";
    public const string CSV_STAT_TOP_SPEED = "topSpeed";
    public const string CSV_STAT_TURBO = "turbo";
    public const string CSV_STAT_WEAPON_COST = "weaponCost";

    // ===== PLAYER PREFS =====
    public const string PREFS_PLAYER_COUNTRY = "PlayerCountry";
    public const string PREFS_PLAYER_RACES_COMPLETED = "PlayerRacesCompleted";
    public const string PREFS_PLAYER_RESOURCES = "PlayerResources";
    public const string PREFS_PLAYER_RACER_COUNT = "PlayerRacerCount";
    public const string PREFS_PLAYER_RACER_FOR_HIRE_COUNT = "PlayerRacerForHireCount";
    public const string PREFS_PLAYER_UPGRADES = "PlayerUpgrades";
    public const string PREFS_PLAYER_UPGRADES_COUNT = "PlayerUpgradesCount";
    public const string PREFS_PLAYER_UNLOCKED_WEAPONS = "PlayerUnlockedWeapons";
    public const string PREFS_PLAYER_UNLOCKED_WEAPONS_COUNT = "PlayerUnlockedWeaponsCount";
    public const string PREFS_PLAYER_CAN_HIRE = "PlayerCanHire";
    public const string PREFS_PLAYER_CHAOS = "PlayerChaos";
    public const string PREFS_PLAYER_RACE_FINISHED = "PlayerRaceFinished";
    public const string PREFS_PLAYER_HIRED_RACERS = "PlayerHiredRacers";
    // ===== RACER PREFS =====
    public const string PREFS_RACER_NAME = "RacerName";
    public const string PREFS_RACER_ID = "RacerID";
    public const string PREFS_RACER_COST = "RacerCost";
    public const string PREFS_RACER_POSITION_IN_HIRE = "RacerPositionInHire";
    public const string PREFS_RACER_GENDER = "RacerGender";
    public const string PREFS_RACER_HAIR = "RacerHair";
    public const string PREFS_RACER_HEAD = "RacerHead";
    public const string PREFS_RACER_EXTRA = "RacerExtra";
    public const string PREFS_RACER_TIER_ONE = "RacerTierOne";
    public const string PREFS_RACER_TIER_TWO = "RacerTierTwo";
    public const string PREFS_RACER_TIER_THREE = "RacerTierThree";
    public const string PREFS_RACER_SKIN_COLOR = "RacerSkinColor";
    public const string PREFS_RACER_HAIR_COLOR = "RacerHairColor";
    public const string PREFS_RACER_VEHICLE_TYPE = "RacerVehicleType";
    public const string PREFS_RACER_CLASS = "RacerClass";
    public const string PREFS_RACER_EXP = "RacerExp";
    public const string PREFS_RACER_CURRENT_LIFE = "RacerCurrentLife";
    public const string PREFS_RACER_MAX_LIFE = "RacerMaxLife";
    public const string PREFS_RACER_PRIMARY = "RacerPrimaryWeapon";
    public const string PREFS_RACER_SECONDARY = "RacerSecondaryWeapon";
    public const string PREFS_RACER_GADGET = "RacerGadget";
    public const string PREFS_RACER_USED_SKILLPOINTS = "RacerUsedSkillpoints";
    public const string PREFS_RACER_UNLOCKED_TIER_ONE = "RacerUnlockedTierOne";
    public const string PREFS_RACER_UNLOCKED_TIER_TWO = "RacerUnlockedTierTwo";
    public const string PREFS_RACER_UNLOCKED_TIER_THREE = "RacerUnlockedTierThree";
    public const string PREFS_RACER_NEW_ALLOYS_USED = "RacerNewAlloysUsed";
    public const string PREFS_RACER_UNLOCKED_VEHICLES = "RacerUnlockedVehicles";
    public const string PREFS_RACER_UNLOCKED_VEHICLES_COUNT = "RacerUnlockedVehiclesCount";

    // ===== PLAYER UPGRADES =====
    // ===== subterfuge =====
    public const string UPGRADE_MEDIA_CONTROL = "Media Control";
    public const string UPGRADE_SAW_LAUNCHER = "Saw Launcher";
    public const string UPGRADE_FLAMETHROWER = "Flamethrower";
    public const string UPGRADE_SABOTAGE = "Sabotage";
    public const string UPGRADE_OIL = "Oil";
    public const string UPGRADE_SMOKE = "Smoke";
    public const string UPGRADE_BACKUP_PLAN = "BackUp Plan";
    
    // ===== VEHICLE TYPE =====
    public const string VEHICLE_BUGGY = "Buggy";
    public const string VEHICLE_BIGFOOT = "Bigfoot";
    public const string VEHICLE_TRUCK = "Truck";

    // ===== PLAYER VEHICLE PREFAB PATH =====
    public const string PATH_VEHICLE_BUGGY = "Prefabs/Vehicles/Player/PlayerBuggyPrefab";
    public const string PATH_VEHICLE_BIGFOOT = "Prefabs/Vehicles/Player/PlayerCrusherPrefab";
    public const string PATH_VEHICLE_TRUCK = "Prefabs/Vehicles/Player/PlayerTruckPrefab";
    public const string PATH_VEHICLE_X_T42 = "Prefabs/Vehicles/Player/PlayerX-T42Prefab";

    // ===== FLAG SPRITE PATHS =====
    public const string PATH_FLAG_LOC = "Flag Sprites/New Flags/League of Clans";
    public const string PATH_FLAG_NR = "Flag Sprites/New Flags/Nuclear Republic";
    public const string PATH_FLAG_SOTA = "Flag Sprites/New Flags/Sons of the Apocalypse";
    public const string PATH_FLAG_SF = "Flag Sprites/New Flags/Souther Fighter";
    public const string PATH_FLAG_TNE = "Flag Sprites/New Flags/The New Empire";
    public const string PATH_FLAG_TW = "Flag Sprites/New Flags/The Watchers";

    // ===== WEAPONS PREFAB PATH =====
    public const string PATH_TURRET = "Prefabs/PrefabsWeapons/MachineGun";
    public const string PATH_MISSILE_LAUNCHER = "Prefabs/PrefabsWeapons/MissileLauncherSimple";
    public const string PATH_SAW_LAUNCHER = "Prefabs/PrefabsWeapons/SawbladeLauncher";
    public const string PATH_LASER_BEAM = "Prefabs/PrefabsWeapons/DeathRay";
    public const string PATH_MOLOTOV_LAUNCHER = "Prefabs/PrefabsWeapons/MolotovLauncher";
    public const string PATH_LOCK_LAUNCHER = "Prefabs/PrefabsWeapons/MissileLauncherLock";
    public const string PATH_FREEZE_RAY = "Prefabs/PrefabsWeapons/DoctorFrio";
    public const string PATH_FLAME_THROWER = "Prefabs/PrefabsWeapons/SideBurner";
    public const string PATH_MINES = "Prefabs/PrefabsWeapons/HeGranade";
    public const string PATH_OIL = "Prefabs/PrefabsWeapons/Oil";
    public const string PATH_SHIELD = "Prefabs/PrefabsWeapons/";
    public const string PATH_SMOKE = "Prefabs/PrefabsWeapons/";
    public const string PATH_COMBAT_DRONE = "Prefabs/PrefabsWeapons/CombatDrone";
    public const string PATH_ELECTROMAGNETIC_MINE = "Prefabs/PrefabsWeapons/ECMMine";

    public const string PATH_MOLOTOV = "Prefabs/PrefabsWeapons/Molotov";
    public const string PATH_FIREFLOOR = "Prefabs/PrefabsWeapons/FireFloor";

    // ===== CLASSES SPRITES PATH =====
    public const string PATH_SPRITES_CLASSES = "Sprites Faces/";

    // ===== INPUT =====
    public const string INPUT_HORIZONTAL = "Horizontal";
    public const string INPUT_VERTICAL = "Vertical";
    public const string INPUT_HANDBRAKE = "Handbrake";
    public const string INPUT_NITRO = "Nitro";

    // ===== MESSAGES =====
    public const string OBS_MESSAGE_DESTROYED = "Destroyed";
    public const string OBS_MESSAGE_FINISHED = "Finished";
    public const string OBS_MESSAGE_SPEED = "Speed";
    public const string OBS_MESSAGE_LAPCOUNT = "Laps";

    // ===== LAYERS =====
    public const int LAYER_DEFAULT = 0;
    public const int LAYER_IGNORERAYCAST = 2;
    public const int LAYER_GROUND = 8;
    public const int LAYER_MNINIMAP_PLAYER_CAR = 9;
    public const int LAYER_PLAYER = 10;
    public const int LAYER_NODE = 11;
    public const int LAYER_IA = 12;
    public const int LAYER_ENEMY = 13;
    public const int LAYER_MISSILE = 14;
    public const int LAYER_CHECKPOINT = 15;
    public const int LAYER_OBSTACLE = 16;
    public const int LAYER_RAMP = 17;
    public const int LAYER_SIDEGROUND = 18;
    public const int LAYER_DESTRUCTIBLE = 19;

    // ===== TAG =====
    public const string TAG_PLAYER = "Player";
    public const string TAG_PLAYERCAMERA = "PlayerCamera";
    public const string TAG_MANAGERS = "Managers";
    public const string TAG_VEHICLES = "Vehicles";
    public const string TAG_CHECKPOINTS = "Checkpoints";
    public const string TAG_AMMO_CONTAINER = "AmmoContainer";
    public const string TAG_RACE_CONTINUE_CONTROL = "RaceContinueControl";
    public const string TAG_SPAWN_POINT = "SpawnPoint";
    public const string TAG_WRONGDIRECTION_TEXT = "WrongDirectionText";
    public const string TAG_GLASS_DAMAGE = "GlassDamage";
    public const string TAG_VISUAL_NITRO = "VisualNitro";
    public const string TAG_PORTRAIT_BUILDER = "PortraitBuilder";

    // ===== GROUND CHECK =====
    public const float IS_GROUNDED_RAYCAST_DISTANCE = 2;

    // ===== JEEP CONFIG =====
    public const float JEEP_MAX_SPEED = 100;
    public const float JEEP_MIN_SPEED = 0;
    public const float JEEP_ACCELERATION_RATE = 10;
    public const float JEEP_DECELERATION_RATE = 50;
    public const float JEEP_MAX_STEERING_ANGLE = 30;
    public const float JEEP_STEER_SPEED = 0.8f;
    public const float JEEP_BRAKE = 1.5f;
    public const float JEEP_ROTATION_FORCE = 10000;

    // ===== UI =====
    public const float SPEEDOMETER_MAX_ANGLE = -150;
    public const float SPEEDOMETER_MIN_ANGLE = 70;
    public const float SPEEDOMETER_MAX_SPEED = 180;

    // ===== IA =====
    public const float IA_MAX_HP = 100;
    public const float IA_MAX_SPEED = JEEP_MAX_SPEED - 30;
    public const float IA_TURN_SPEED = 5;
    public const float IA_FALLFORCE = 30000f;

    // ===== MINIMAP =====
    public const float MINIMAP_HEIGHT = 50;

    // ===== GAME PRESETS =====
    public const int MAX_LAPS = 3;
    public const int MAX_RACERS = 5;

    // ===== SCENE CONTAINERS =====
    public const string CONTAINER_VEHICLES_NAME = "VEHICLES";
    public const string CONTAINER_CHECKPOINTS_NAME = "CHECKPOINTS";

    public const float GRAVITY = 9.8f;


    // ====== WEAPONS ======

    public const int PRIMARY_ROCKET = 0;
    public const int PRIMARY_LOCK_ROCKET = 1;
    public const int SIDE_MOLOTOV = 0;
    public const int SIDE_MINIGUN = 1;
    public const int GADGET_HE = 0;
    public const int GADGET_MINE = 1;
    public const int GADGET_ECM = 2;



    // ====== SOUNDS IDS =======
    public const int SOUND_MACHINE_GUN = 0;
    public const int SOUND_MINE_EXPLOSION = 1;
    public const int SOUND_CAR_DESTROY = 2;
    public const int SOUND_MISSILE_HEAVY = 3;
    public const int SOUND_MISSILE = 4;
    public const int SOUND_MOLOTOV_LAUNCH = 5;
    public const int SOUND_MISIL_LAUNCH = 6;

    // ======  PORTRAIT =======
    public static Color[] arrayColorHair = new Color[]
    {
        new Color(0.10980392156f, 0.07843137254f, 0.04705882352f),
        new Color(1, 0.90196078431f, 0.4862745098f),
        new Color(0.30196078431f, 0.26666666666f, 0.20784313725f)
    };



    public static Color[] arrayColorSkin = new Color[]
    {
        //White
        new Color(0.94901960784f, 0.90980392156f, 0.85098039215f),
        new Color(0.91764705882f, 0.81568627451f, 0.68235294117f),
        //Middle tone
        new Color(0.86666666666f, 0.71764705882f, 0.49019607843f),
        new Color (0.61960784313f, 0.45490196078f, 0.24705882352f),
        new Color (0.47843137254f,0.35294117647f, 0.19215686274f),
        //Black
        new Color(0.43529411764f, 0.26274509803f, 0.0431372549f)


    };

    public const float KPH_TO_MPS_MULTIPLIER = 3.6f; // Kilometros por hora a metros por segundo
    public const float TRAIL_WHEEL_START_SPEED = 50f; // Velocidad de inicio de trail
    public const float MPS_TO_MPH_MULTIPLIER = 2.23693629f; //Metros por segundos a metros por hora

    public const float MIN_FORCE_MULTIPLIER = .155f;

    // ======  PILOT =======
    public static bool pilotIsAlive = true;


    // ======  COUNTRY =======
    public static Sprite countryFlag;
}

public enum SCENES_NUMBER : int
{
    ScenesMenu = 0,
    PilotCreation = 1,
    HUB = 2,
    DesertTrack = 3,
    PostGame = 4,
    LoadingScene = 5,
    Garage = 6,
    RushHour = 7,
    NewGame = 8,
    WaterTomb = 9,
    Satellitrack = 10,
    InsideTheCore = 11
}

