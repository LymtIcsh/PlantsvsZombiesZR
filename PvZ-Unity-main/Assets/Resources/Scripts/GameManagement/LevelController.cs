using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    protected virtual void Start()
    {
        switch (BeginManagement.level)
        {
            case 1:
                SetAchievement.SetAchievementCompleted("�ο�ʼ�ĵط���");
                break;
            default:
                break;
        }
    }

    private void Level300()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 300,
            levelName = "���Թؿ�",
            levelEnviornment = "Steel",
            mapSuffix = "_Steel",
            rowCount = 5,
            landRowCount = 5,
            isDay = true,
            plantingManagementSuffix = "_Steel",
            backgroundSuffix = "_Day",
            canSelectCardsInFirstPlaythrough = true,
            DontFallSun = true,
            LevelType = levelType.FaithHill,
            GloveHaveNoCD = true,

            zombieInitPosY = new List<float> { -2.1f, -1.175f, -0.25f, 0.675f, 1.6f },
            plantCards = new List<string>
            {
                "TallNut",
                "MelonPult",
                "Chomper",
                "PotatoMine"
            }
        };
    }

    #region ð��ģʽ
    // �ؿ� 1 �ĳ�ʼ���߼�
    private void Level1()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 1,   // LevelNumber
            levelName = "1 - 1",   // �ؿ���
            levelEnviornment = "Day",
            mapSuffix = "_Unsodded", // ��ͼͼƬ��׺
            rowCount = 1,       // �ܹ�����
            landRowCount = 1,   // ����½��
            isDay = true,       // �Ƿ����
            plantingManagementSuffix = "_Sod1row",   // ��Ӧ����ֲ���������׺
            backgroundSuffix = "_Day",   // ��Ӧ�������ֺ�׺
            canSelectCardsInFirstPlaythrough = false,
            zombieInitPosY = new List<float> { -0.151f, },
            plantCards = new List<string>
            {
                "CommonShooter",
            }
        };
    }

    // �ؿ� 2 �ĳ�ʼ���߼�
    private void Level2()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 2,   // LevelNumber
            levelName = "1 - 2",   // �ؿ���
            levelEnviornment = "Day",
            mapSuffix = "_Unsodded", // ��ͼͼƬ��׺
            rowCount = 3,       // �ܹ�����
            landRowCount = 3,   // ����½��
            isDay = true,       // �Ƿ����
            plantingManagementSuffix = "_Sod3row",   // ��Ӧ����ֲ���������׺
            backgroundSuffix = "_Day",   // ��Ӧ�������ֺ�׺
            canSelectCardsInFirstPlaythrough = false,
            zombieInitPosY = new List<float> { -1.25f, -0.35f, 0.7f },
            plantCards = new List<string>
            {
                "CommonShooter",
                "SunFlower"
            }
        };
    }
    public virtual void init()
    {
        int level = BeginManagement.level;
        string methodName = "Level" + level;

        var method = GetType().GetMethod(methodName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

        if (method != null)
        {
            method.Invoke(this, null);
        }
        else
        {
            Debug.LogWarning($"Undefined level: {level}");
        }
    }

    private void Level3()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 3,   // LevelNumber
            levelName = "1 - 3",   // �ؿ���
            levelEnviornment = "Day",
            mapSuffix = "_Day",
            rowCount = 5,
            landRowCount = 5,
            isDay = true,
            plantingManagementSuffix = "_OriginalLawn",
            backgroundSuffix = "_Day",
            canSelectCardsInFirstPlaythrough = false,
            zombieInitPosY = new List<float> { -2.3f, -1.25f, -0.35f, 0.7f, 1.7f },
            plantCards = new List<string>
        {
            "CommonShooter",
            "SunFlower",
            "WallNut"
        }
        };
    }

    // Level 4 initialization
    private void Level4()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 4,
            levelName = "1 - 4",
            levelEnviornment = "Day",
            mapSuffix = "_Day",
            rowCount = 5,
            landRowCount = 5,
            isDay = true,
            plantingManagementSuffix = "_OriginalLawn",
            backgroundSuffix = "_Day",
            canSelectCardsInFirstPlaythrough = false,
            zombieInitPosY = new List<float> { -2.3f, -1.25f, -0.35f, 0.7f, 1.7f },
            plantCards = new List<string>
        {
            "CommonShooter",
            "SunFlower",
            "WallNut",
            "Squash"
        }
        };
    }

    // Level 5 initialization
    private void Level5()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 5,
            levelName = "1 - 5",
            levelEnviornment = "Day",
            mapSuffix = "_Day",
            rowCount = 5,
            landRowCount = 5,
            isDay = true,
            plantingManagementSuffix = "_OriginalLawn",
            backgroundSuffix = "_Day",
            canSelectCardsInFirstPlaythrough = false,
            zombieInitPosY = new List<float> { -2.3f, -1.25f, -0.35f, 0.7f, 1.7f },
            plantCards = new List<string>
        {
            "CommonShooter",
            "SunFlower",
            "WallNut",
            "Squash",
            "Torchwood"
        }
        };
    }

    // Level 6 initialization
    private void Level6()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 6,
            levelName = "1 - 6",
            levelEnviornment = "Day",
            mapSuffix = "_Day",
            rowCount = 5,
            landRowCount = 5,
            isDay = true,
            plantingManagementSuffix = "_OriginalLawn",
            backgroundSuffix = "_Day",
            canSelectCardsInFirstPlaythrough = false,
            zombieInitPosY = new List<float> { -2.3f, -1.25f, -0.35f, 0.7f, 1.7f },
            plantCards = new List<string>
        {
            "CommonShooter",
            "SunFlower",
            "WallNut",
            "Squash",
            "Torchwood",
            "Wood"
        }
        };
    }

    // Level 7 initialization
    private void Level7()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 7,
            levelName = "1 - 7",
            levelEnviornment = "Day",
            mapSuffix = "_Day",
            rowCount = 5,
            landRowCount = 5,
            isDay = true,
            plantingManagementSuffix = "_OriginalLawn",
            backgroundSuffix = "_Day",
            canSelectCardsInFirstPlaythrough = false,
            zombieInitPosY = new List<float> { -2.3f, -1.25f, -0.35f, 0.7f, 1.7f },
            plantCards = new List<string>
        {
            "CommonShooter",
            "SunFlower",
            "WallNut",
            "Squash",
            "Torchwood",
            "Wood",
            "Repeater"
        }
        };
    }

    // Level 8 initialization
    private void Level8()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 8,
            levelName = "1 - 8",
            levelEnviornment = "Day",
            mapSuffix = "_Day",
            rowCount = 5,
            landRowCount = 5,
            isDay = true,
            plantingManagementSuffix = "_OriginalLawn",
            backgroundSuffix = "_Day",
            canSelectCardsInFirstPlaythrough = false,
            zombieInitPosY = new List<float> { -2.3f, -1.25f, -0.35f, 0.7f, 1.7f },
            plantCards = new List<string>
        {
            "CommonShooter",
            "SunFlower",
            "WallNut",
            "Squash",
            "Torchwood",
            "Wood",
            "Repeater",
            "Spikeweed"
        }
        };
    }

    // Level 9 initialization
    private void Level9()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 9,
            levelName = "1 - 9",
            levelEnviornment = "Day",
            mapSuffix = "_Day",
            rowCount = 5,
            landRowCount = 5,
            isDay = true,
            plantingManagementSuffix = "_OriginalLawn",
            backgroundSuffix = "_Day",
            canSelectCardsInFirstPlaythrough = false,
            zombieInitPosY = new List<float> { -2.3f, -1.25f, -0.35f, 0.7f, 1.7f },
            plantCards = new List<string>
        {
            "CommonShooter",
            "SunFlower",
            "WallNut",
            "Squash",
            "Torchwood",
            "Wood",
            "Repeater",
            "Spikeweed",
            "MelonPult"
        }
        };
    }

    // Level 10 initialization
    private void Level10()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 10,
            levelName = "2 - 1",
            levelEnviornment = "Day",
            mapSuffix = "_Night",
            rowCount = 5,
            landRowCount = 5,
            isDay = false,
            plantingManagementSuffix = "_OriginalLawn",
            backgroundSuffix = "_Night",
            canSelectCardsInFirstPlaythrough = false,
            zombieInitPosY = new List<float> { -2.3f, -1.25f, -0.35f, 0.7f, 1.7f },
            plantCards = new List<string>
        {
            "CommonShooter",
            "SunFlower",
            "WallNut",
            "Squash",
            "Torchwood",
            "Wood",
            "Repeater",
            "Spikeweed",
            "MelonPult",
            "SmallPuff"
        }
        };
    }

    // Level 11 initialization
    private void Level11()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 11,
            levelName = "2 - 2",
            levelEnviornment = "Day",
            mapSuffix = "_Night",
            rowCount = 5,
            landRowCount = 5,
            isDay = false,
            plantingManagementSuffix = "_OriginalLawn",
            backgroundSuffix = "_Night",
            canSelectCardsInFirstPlaythrough = false,
            zombieInitPosY = new List<float> { -2.3f, -1.25f, -0.35f, 0.7f, 1.7f },
            plantCards = new List<string>
        {
            "CommonShooter",
            "SunFlower",
            "WallNut",
            "Squash",
            "Torchwood",
            "Wood",
            "Repeater",
            "Spikeweed",
            "MelonPult",
            "SmallPuff",
            "PotatoMine"
        }
        };
    }

    // Level 12 initialization
    private void Level12()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 12,
            levelName = "2 - 3",
            levelEnviornment = "Day",
            mapSuffix = "_Night",
            rowCount = 5,
            landRowCount = 5,
            isDay = false,
            plantingManagementSuffix = "_OriginalLawn",
            backgroundSuffix = "_Night",
            canSelectCardsInFirstPlaythrough = false,
            zombieInitPosY = new List<float> { -2.3f, -1.25f, -0.35f, 0.7f, 1.7f },
            plantCards = new List<string>
        {
            "CommonShooter",
            "SunFlower",
            "WallNut",
            "Squash",
            "Torchwood",
            "Wood",
            "Repeater",
            "Spikeweed",
            "MelonPult",
            "SmallPuff",
            "PotatoMine",
            "SunShroom"
        }
        };
    }

    // Level 13 initialization
    private void Level13()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 13,
            levelName = "2 - 4",
            levelEnviornment = "Day",
            mapSuffix = "_Night",
            rowCount = 5,
            landRowCount = 5,
            isDay = false,
            plantingManagementSuffix = "_OriginalLawn",
            backgroundSuffix = "_Night",
            canSelectCardsInFirstPlaythrough = false,
            zombieInitPosY = new List<float> { -2.3f, -1.25f, -0.35f, 0.7f, 1.7f },
            plantCards = new List<string>
        {
            "CommonShooter",
            "SunFlower",
            "WallNut",
            "Squash",
            "Torchwood",
            "Wood",
            "Repeater",
            "Spikeweed",
            "MelonPult",
            "SmallPuff",
            "PotatoMine",
            "SunShroom",
            "FumeShroom"
        }
        };
    }

    private void Level14()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 14,
            levelName = "2 - 5",
            levelEnviornment = "Day",
            mapSuffix = "_Night",
            rowCount = 5,
            landRowCount = 5,
            isDay = false,
            plantingManagementSuffix = "_OriginalLawn",
            backgroundSuffix = "_Night",
            canSelectCardsInFirstPlaythrough = false,
            zombieInitPosY = new List<float> { -2.3f, -1.25f, -0.35f, 0.7f, 1.7f },
            plantCards = new List<string>
        {
            "CommonShooter",
            "SunFlower",
            "WallNut",
            "Squash",
            "Torchwood",
            "Wood",
            "Repeater",
            "Spikeweed",
            "MelonPult",
            "SmallPuff",
            "PotatoMine",
            "SunShroom",
            "FumeShroom",
            "Chomper"
        }
        };
    }

    private void Level15()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 15,
            levelName = "2 - 6",
            levelEnviornment = "Day",
            mapSuffix = "_Night",
            rowCount = 5,
            landRowCount = 5,
            isDay = false,
            plantingManagementSuffix = "_OriginalLawn",
            backgroundSuffix = "_Night",
            canSelectCardsInFirstPlaythrough = false,
            TheSizeofNeck = 0.88f,
            zombieInitPosY = new List<float> { -2.3f, -1.25f, -0.35f, 0.7f, 1.7f },
            plantCards = new List<string>
            {
            "CommonShooter",
            "SunFlower",
            "WallNut",
            "Squash",
            "Torchwood",
            "Wood",
            "Repeater",
            "Spikeweed",
            "MelonPult",
            "SmallPuff",
            "PotatoMine",
            "SunShroom",
            "FumeShroom",
            "Chomper",
            "HypnoShroom"
            }
        };
    }

    private void Level16()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 16,
            levelName = "2 - 7",
            levelEnviornment = "Day",
            mapSuffix = "_Night",
            rowCount = 5,
            landRowCount = 5,
            isDay = false,
            plantingManagementSuffix = "_OriginalLawn",
            backgroundSuffix = "_Night",
            canSelectCardsInFirstPlaythrough = false,
            TheSizeofNeck = 0.86f,
            zombieInitPosY = new List<float> { -2.3f, -1.25f, -0.35f, 0.7f, 1.7f },
            plantCards = new List<string>
            {
            "CommonShooter",
            "SunFlower",
            "WallNut",
            "Squash",
            "Torchwood",
            "Wood",
            "Repeater",
            "Spikeweed",
            "MelonPult",
            "SmallPuff",
            "PotatoMine",
            "SunShroom",
            "FumeShroom",
            "Chomper",
            "HypnoShroom",
            "DoomShroom"
            }
        };
    }



    #endregion

    #region ����ģʽ
    // Level 51 initialization
    private void Level51()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 51,
            levelName = "ɭ�� - 1 - 1",
            levelEnviornment = "Forest",
            mapSuffix = "_Forest",
            rowCount = 5,
            landRowCount = 5,
            isDay = true,
            plantingManagementSuffix = "_Forest",
            backgroundSuffix = "_ForestDay",
            StartSunNumber = 8000,
            canSelectCardsInFirstPlaythrough = false,
            EnablesForestBushGeneration = false,
            zombieInitPosY = new List<float> { -1.976f, -0.986f, -0.276f, 0.7f, 1.574f },
            plantCards = new List<string>
        {
            "CommonShooter",
            "Repeater",
            "Repeater",
            "Squash",
            "Torchwood",
            "Torchwood",
            "Torchwood",
            "Wood",
            "Wood",
            "DiamonWood",
            "DiamonWood",
            "PotatoMine",
        }
        };
    }

    // Level 52 initialization
    private void Level52()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 52,
            levelName = "ɭ�� - 1 - 2",
            levelEnviornment = "Forest",
            mapSuffix = "_Forest",
            rowCount = 5,
            landRowCount = 5,
            isDay = true,
            plantingManagementSuffix = "_Forest",
            backgroundSuffix = "_ForestDay",
            canSelectCardsInFirstPlaythrough = false,
            EnablesForestBushGeneration = false,
            zombieInitPosY = new List<float> { -1.976f, -0.986f, -0.276f, 0.7f, 1.574f },
            plantCards = new List<string>
        {
            "SunFlower",
            "ForestSunFlower",
            "CommonShooter",
            "Repeater",
            "Squash",
            "PotatoMine",
            "Torchwood",
            "Wood",
            "DiamonWood",
            
        }
        };
    }

    // Level 53 initialization
    private void Level53()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 53,
            levelName = "ɭ�� - 1 - 3",
            levelEnviornment = "Forest",
            mapSuffix = "_Forest",
            rowCount = 5,
            landRowCount = 5,
            isDay = true,
            plantingManagementSuffix = "_Forest",
            backgroundSuffix = "_ForestDay",
            canSelectCardsInFirstPlaythrough = false,
            EnablesForestBushGeneration = false,
            zombieInitPosY = new List<float> { -1.976f, -0.986f, -0.276f, 0.7f, 1.574f },
            plantCards = new List<string>
        {
            "SunFlower",
            "ForestSunFlower",
            "CommonShooter",
            "WallNut",
            "ForestWallNut",
            "Repeater",
            "Squash",
            "PotatoMine",
            "Torchwood",
            "Wood",
            "DiamonWood",
            
        }
        };
    }

    // Level 54 initialization
    private void Level54()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 54,
            levelName = "ɭ�� - 1 - 4",
            levelEnviornment = "Forest",
            mapSuffix = "_Forest",
            rowCount = 5,
            landRowCount = 5,
            isDay = true,
            plantingManagementSuffix = "_Forest",
            backgroundSuffix = "_ForestDay",
            canSelectCardsInFirstPlaythrough = false,
            EnablesForestBushGeneration = false,
            zombieInitPosY = new List<float> { -1.976f, -0.986f, -0.276f, 0.7f, 1.574f },
            plantCards = new List<string>
        {
            "SunFlower",
            "ForestSunFlower",
            "CommonShooter",
            "WallNut",
            "ForestWallNut",
            "Repeater",
            "Squash",
            "PotatoMine",
            "Torchwood",
            "Wood",
            "DiamonWood",
            "Spikeweed",
            "ForestSpikeweed"
        }
        };
    }

    // Level 55 initialization
    private void Level55()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 55,
            levelName = "ɭ�� - 1 - 5",
            levelEnviornment = "Forest",
            mapSuffix = "_Forest",
            rowCount = 5,
            landRowCount = 5,
            isDay = true,
            plantingManagementSuffix = "_Forest",
            backgroundSuffix = "_ForestDay",
            canSelectCardsInFirstPlaythrough = false,
            zombieInitPosY = new List<float> { -1.976f, -0.986f, -0.276f, 0.7f, 1.574f },
            plantCards = new List<string>
        {
            "SunFlower",
            "ForestSunFlower",
            "CommonShooter",
            "WallNut",
            "ForestWallNut",
            "Repeater",
            "Squash",
            "PotatoMine",
            "Torchwood",
            "Wood",
            "DiamonWood",
            "Spikeweed",
            "ForestSpikeweed",
            "MelonPult",
            "ForestMelonPult"
        }
        };
    }

    // Level 56 initialization
    private void Level56()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 56,
            levelName = "ɭ�� - 1 - 6",
            levelEnviornment = "Forest",
            mapSuffix = "_Forest",
            rowCount = 5,
            landRowCount = 5,
            isDay = true,
            plantingManagementSuffix = "_Forest",
            backgroundSuffix = "_ForestDay",
            canSelectCardsInFirstPlaythrough = false,
            TheSizeofNeck = 0.82f,
            zombieInitPosY = new List<float> { -1.976f, -0.986f, -0.276f, 0.7f, 1.574f },
            plantCards = new List<string>
        {
            "SunFlower",
            "ForestSunFlower",
            "CommonShooter",
            "WallNut",
            "ForestWallNut",
            "Repeater",
            "Squash",
            "PotatoMine",
            "Torchwood",
            "Wood",
            "DiamonWood",
            "Spikeweed",
            "ForestSpikeweed",
            "MelonPult",
            "ForestMelonPult",
            "OakArcher"
        }
        };
    }

    // Level 57 initialization
    private void Level57()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 57,
            levelName = "ɭ�� - 2 - 1",
            levelEnviornment = "Forest",
            mapSuffix = "_ForestNight",
            rowCount = 5,
            landRowCount = 5,
            isDay = false,
            plantingManagementSuffix = "_Forest",
            backgroundSuffix = "_ForestNight",
            canSelectCardsInFirstPlaythrough = false,
            TheSizeofNeck = 0.82f,
            zombieInitPosY = new List<float> { -1.976f, -0.986f, -0.276f, 0.7f, 1.574f },
            plantCards = new List<string>
        {
            "CommonShooter",
            "WallNut",
            "ForestWallNut",
            "Repeater",
            "Squash",
            "PotatoMine",
            "Torchwood",
            "Wood",
            "DiamonWood",
            "Spikeweed",
            "ForestSpikeweed",
            "OakArcher",
            "SmallPuff",
            "ForestSmallPuff",
            "SunShroom",
            "ForestSunShroom"
        }
        };
    }

    // Level 58 initialization
    private void Level58()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 58,
            levelName = "ɭ�� - 2 - 2",
            levelEnviornment = "Forest",
            mapSuffix = "_ForestNight",
            rowCount = 5,
            landRowCount = 5,
            isDay = false,
            plantingManagementSuffix = "_Forest",
            backgroundSuffix = "_ForestNight",
            canSelectCardsInFirstPlaythrough = false,
            zombieInitPosY = new List<float> { -1.976f, -0.986f, -0.276f, 0.7f, 1.574f },
            plantCards = new List<string>
        {

            "WallNut",
            "ForestWallNut",
            "Squash",
            "PotatoMine",
            "Spikeweed",
            "ForestSpikeweed",
            "OakArcher",
            "SmallPuff",
            "ForestSmallPuff",
            "SunShroom",
            "ForestSunShroom",
            "FumeShroom",
            "ForestFumeShroom"
        }
        };
    }

    // Level 59 initialization
    private void Level59()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 59,
            levelName = "ɭ�� - 3 - 1",
            levelEnviornment = "Forest",
            mapSuffix = "_Forest_P",
            rowCount = 5,
            landRowCount = 5,
            isDay = true,
            plantingManagementSuffix = "_Forest",
            backgroundSuffix = "_ForestBattle",
            canSelectCardsInFirstPlaythrough = false,
            TheSizeofNeck = 0.82f,
            zombieInitPosY = new List<float> { -1.976f, -0.986f, -0.276f, 0.7f, 1.574f },
            plantCards = new List<string>
        {
            "SunFlower",
            "ForestSunFlower",
            "WallNut",
            "ForestWallNut",
            "Squash",
            "PotatoMine",
            "Spikeweed",
            "ForestSpikeweed",
            "OakArcher",
            "SmallPuff",
            "ForestSmallPuff",
            "SunShroom",
            "ForestSunShroom",
            "FumeShroom",
            "ForestFumeShroom",
            "WoodWallNut"
        }
        };
    }

    // Level 60 initialization
    private void Level60()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 60,
            levelName = "ɭ�� - 3 - 2",
            levelEnviornment = "Forest",
            mapSuffix = "_Forest_P",
            rowCount = 5,
            landRowCount = 5,
            isDay = true,
            plantingManagementSuffix = "_Forest",
            backgroundSuffix = "_ForestBattle",
            canSelectCardsInFirstPlaythrough = false,
            zombieInitPosY = new List<float> { -1.976f, -0.986f, -0.276f, 0.7f, 1.574f },
            plantCards = new List<string>
        {
            "CommonShooter",
            "Squash",
            "Torchwood",
            "Wood",
            "Repeater",
            "Torchwood",
            "DiamonWood",
            "DiamonWood",
            "SunFlower",
            "ForestSunFlower",
            "WallNut",
            "ForestWallNut",
            "Spikeweed",
            "ForestSpikeweed",
            "MelonPult",
            "ForestMelonPult",
            "OakArcher",
            "SmallPuff",
            "ForestSmallPuff",
            "SunShroom",
            "ForestSunFlowers",
            "FumeShroom",
            "ForestFumeShroom",
            "WoodWallNut"
        }
        };
    }

    //��������
    private void Level81()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 81,
            levelName = "���� - 1 - 1",
            levelEnviornment = "Steel",
            mapSuffix = "_Steel",
            rowCount = 5,
            landRowCount = 5,
            isDay = true,
            plantingManagementSuffix = "_Steel",
            backgroundSuffix = "_Day",
            canSelectCardsInFirstPlaythrough = true,
            DontFallSun = true,
            LevelType = levelType.FaithHill,
            GloveHaveNoCD = true,

            zombieInitPosY = new List<float> { -2.1f, -1.175f, -0.25f, 0.675f, 1.6f },
            plantCards = new List<string>
            {
                "TallNut",
                "MelonPult",
                "Chomper",
                "PotatoMine"
            }
        };
    }

    #endregion

    #region С��Ϸģʽ

    private void Level242()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 242,
            levelName = "��������ɽ������",
            levelEnviornment = "Day",
            mapSuffix = "_Day",
            rowCount = 5,
            landRowCount = 5,
            isDay = true,
            plantingManagementSuffix = "_OriginalLawn",
            backgroundSuffix = "_Day",
            canSelectCardsInFirstPlaythrough = false,
            disableCardSelection = true,
            DontFallSun = true,
            LevelType = levelType.FaithHill,
            ConveyorGame = true,
            GloveHaveNoCD = true,

            zombieInitPosY = new List<float> { -2.3f, -1.25f, -0.35f, 0.7f, 1.7f },
            plantCards = new List<string>
            {
                "TallNut",
                "MelonPult",
                "Chomper",
                "PotatoMine"
            }
        };
    }

    private void Level243()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 243,
            levelName = "��ɽ����",
            levelEnviornment = "Day",
            mapSuffix = "_Day",
            rowCount = 5,
            landRowCount = 5,
            isDay = true,
            plantingManagementSuffix = "_OriginalLawn",
            backgroundSuffix = "_Day",
            canSelectCardsInFirstPlaythrough = true,
            LevelType = levelType.FaithHill,
            GloveHaveNoCD = true,
            zombieInitPosY = new List<float> { -2.3f, -1.25f, -0.35f, 0.7f, 1.7f },
            plantCards = new List<string>
            {
            }
        };
    }

    private void Level244()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 244,
            levelName = "��׮�����롤һ",
            levelEnviornment = "Day",
            mapSuffix = "_Day",
            rowCount = 5,
            landRowCount = 5,
            isDay = true,
            plantingManagementSuffix = "_OriginalLawn",
            backgroundSuffix = "_Day",
            canSelectCardsInFirstPlaythrough = false,
            disableCardSelection = true,
            DontFallSun = true,
            LevelType = levelType.TheDreamOfWood,
            
            zombieInitPosY = new List<float> { -2.3f, -1.25f, -0.35f, 0.7f, 1.7f },
            plantCards = new List<string>
            {
            }
        };
    }

    private void Level245()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 245,
            levelName = "��׮�����롤��",
            levelEnviornment = "Day",
            mapSuffix = "_Day",
            rowCount = 5,
            landRowCount = 5,
            isDay = true,
            plantingManagementSuffix = "_OriginalLawn",
            backgroundSuffix = "_Day",
            canSelectCardsInFirstPlaythrough = false,
            disableCardSelection = true,
            DontFallSun = true,
            LevelType = levelType.TheDreamOfWood,

            zombieInitPosY = new List<float> { -2.3f, -1.25f, -0.35f, 0.7f, 1.7f },
            plantCards = new List<string>
            {
            }
        };
    }

    private void Level246()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 246,
            levelName = "��׮�����롤��",
            levelEnviornment = "Day",
            mapSuffix = "_Day",
            rowCount = 5,
            landRowCount = 5,
            isDay = true,
            plantingManagementSuffix = "_OriginalLawn",
            backgroundSuffix = "_Day",
            canSelectCardsInFirstPlaythrough = false,
            disableCardSelection = true,
            DontFallSun = true,
            LevelType = levelType.TheDreamOfWood,

            zombieInitPosY = new List<float> { -2.3f, -1.25f, -0.35f, 0.7f, 1.7f },
            plantCards = new List<string>
            {
            }
        };
    }

    private void Level247()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 247,
            levelName = "��׮������-����ģʽ",
            levelEnviornment = "Day",
            mapSuffix = "_Day",
            rowCount = 5,
            landRowCount = 5,
            isDay = true,
            plantingManagementSuffix = "_OriginalLawn",
            backgroundSuffix = "_Day",
            canSelectCardsInFirstPlaythrough = false,
            disableCardSelection = true,
            DontFallSun = true,
            LevelType = levelType.TheDreamOfWood,
            StartSunNumber = 20090505,
            zombieInitPosY = new List<float> { -2.3f, -1.25f, -0.35f, 0.7f, 1.7f },
            plantCards = new List<string>
            {
            }
        };
    }

    private void Level248()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 248,
            levelName = "�����ľ��ϣ�",
            levelEnviornment = "Day",
            mapSuffix = "_Day",
            rowCount = 5,
            landRowCount = 5,
            isDay = true,
            plantingManagementSuffix = "_OriginalLawn",
            backgroundSuffix = "_Day",
            canSelectCardsInFirstPlaythrough = false,
            disableCardSelection = true,
            DontFallSun = true,
            LevelType = levelType.FaithHill,
            ConveyorGame = true,
            GloveHaveNoCD = true,
            zombieInitPosY = new List<float> { -2.3f, -1.25f, -0.35f, 0.7f, 1.7f },
            plantCards = new List<string>
            {
                "Chomper",
                "WallNut",
                "FumeShroom",
                "GatlingPea",
                "Repeater",
                "HypnoShroom"
            }
        };
    }

    private void Level253()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 253,
            levelName = "�����ը��һ",
            levelEnviornment = "Day",
            mapSuffix = "_Day",
            rowCount = 5,
            landRowCount = 5,
            isDay = true,
            plantingManagementSuffix = "_OriginalLawn",
            backgroundSuffix = "_Day",
            canSelectCardsInFirstPlaythrough = false,
            disableCardSelection = true,
            DontFallSun = true,
            LevelType = levelType.TheDreamOfPotatoMine,
            StartSunNumber = 0,
            zombieInitPosY = new List<float> { -2.3f, -1.25f, -0.35f, 0.7f, 1.7f },
            plantCards = new List<string>
            {
            }
        };
    }

    private void Level201()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 201,
            levelName = "<color=red>�޽�����</color>",
            levelEnviornment = "Forest",
            mapSuffix = "_Forest",
            rowCount = 5,
            landRowCount = 5,
            isDay = true,
            plantingManagementSuffix = "_Forest",
            backgroundSuffix = "_ForestDay",
            canSelectCardsInFirstPlaythrough = false,
            StartSunNumber = 300,
            LevelType = levelType.None,
            zombieInitPosY = new List<float> { -1.976f, -0.986f, -0.276f, 0.7f, 1.574f },
            plantCards = new List<string>
        {
            "SunShroom",
            "ForestSunShroom",
            "WallNut",
            "ForestWallNut",
            "Repeater",
            "JalapenoGatlingPea",
            "JalapenoGatlingPea",
            "Torchwood",
            "Wood",
            "DiamonWood",
            "Spikeweed",
            "ForestSpikeweed",
            "SymbioticForestTorchWood",
            "CherryBomb",
            "FumeShroom"
        }
        };
    }

    private void Level202()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 202,
            levelName = "<color=red>�ӽ�����</color>",
            levelEnviornment = "Forest",
            mapSuffix = "_Forest",
            rowCount = 5,
            landRowCount = 5,
            isDay = true,
            plantingManagementSuffix = "_Forest",
            backgroundSuffix = "_ForestDay",
            canSelectCardsInFirstPlaythrough = false,
            StartSunNumber = 500,
            LevelType = levelType.None,
            zombieInitPosY = new List<float> { -1.976f, -0.986f, -0.276f, 0.7f, 1.574f },
            plantCards = new List<string>
        {
            "SunShroom",
            "ForestSunShroom",
            "WallNut",
            "ForestWallNut",
            "Repeater",
            "JalapenoGatlingPea",
            "JalapenoGatlingPea",
            "Torchwood",
            "Wood",
            "DiamonWood",
            "Spikeweed",
            "ForestSpikeweed",
            "SymbioticForestTorchWood",
            "CherryBomb",
            "FumeShroom"
        }
        };
    }
    #endregion

    public virtual void activate()
    {
        // �����߼���������Ҫʵ�֣�
    }
}