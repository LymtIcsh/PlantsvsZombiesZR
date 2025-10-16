using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public struct ZombieStruct
{
    public int id;
    public int Cost;
    public int CD;
    public EnvironmentType envType;

    public string zombieName;
    public string ChineseName;
    public string ZombieIntroduction;

    // 新增血量字段
    public int BaseHP;
    public int Armor1HP;
    public int Armor2HP;

    public int showLevel;

    public ZombieStruct(int id, int Cost, int CD, EnvironmentType envType,
        string zombieName, string ChineseName, string ZombieIntroduction,
        int baseHP = 0, int armor1HP = 0, int armor2HP = 0, int showLevel = -1)
    {
        this.id = id;
        this.Cost = Cost;
        this.CD = CD;
        this.envType = envType;
        this.zombieName = zombieName;
        this.ChineseName = ChineseName;
        this.ZombieIntroduction = ZombieIntroduction;
        this.BaseHP = baseHP;
        this.Armor1HP = armor1HP;
        this.Armor2HP = armor2HP;

        this.showLevel = showLevel;
    }
}

public static class ZombieStructManager
{
    private static List<ZombieStruct> ZombieStructDatabase = new List<ZombieStruct>();

    static ZombieStructManager()
    {
        // 白天环境
        ZombieStructDatabase.Add(new ZombieStruct(1, 25, 0, EnvironmentType.Day,
            "ZombieNormal", "普通僵尸", "耐久：270。\n最平平无奇的僵尸，像你一样。",
            270, 0, 0));

        ZombieStructDatabase.Add(new ZombieStruct(2, 40, 0, EnvironmentType.Day,
            "ConeZombie", "路障僵尸", "耐久：270+370（一类）。\n头上的路障可以防御物理打击。",
            270, 370, 0));

        ZombieStructDatabase.Add(new ZombieStruct(3, 50, 0, EnvironmentType.Day,
            "BucketZombie", "铁桶僵尸", "耐久：270+1100（一类）。\n头上的铁桶可以有效防御物理打击。",
            270, 1100, 0));

        ZombieStructDatabase.Add(new ZombieStruct(4, 50, 0, EnvironmentType.Day,
            "PolevaulterZombie", "撑杆跳僵尸", "耐久：500。\n遇到植物后跳跃（仅限一次），跳跃后移动速度减慢。",
            500, 0, 0));

        ZombieStructDatabase.Add(new ZombieStruct(5, 50, 0, EnvironmentType.Day,
            "ScreenDoorZombie", "铁网门僵尸", "耐久：270+1100（二类）。\n铁门可以有效抵挡直线攻击，无法防御投掷物攻击。",
            270, 0, 1100));

        ZombieStructDatabase.Add(new ZombieStruct(6, 40, 0, EnvironmentType.Day,
            "PeaShooterZ", "豌豆射手僵尸", "耐久：300。\n每两秒向前射出一发豌豆子弹。",
            300, 0, 0));

        ZombieStructDatabase.Add(new ZombieStruct(7, 50, 0, EnvironmentType.Day,
            "FirePeaShooterZ", "火焰豌豆射手僵尸", "耐久：300。\n每1.5秒向前射出一发火焰豌豆子弹。",
            300, 0, 0));

        ZombieStructDatabase.Add(new ZombieStruct(8, 60, 0, EnvironmentType.Day,
            "RepeaterZ", "双发射手僵尸", "耐久：400。\n每1.5秒向前射出两发豌豆子弹。",
            400, 0, 0));

        ZombieStructDatabase.Add(new ZombieStruct(9, 100, 0, EnvironmentType.Day,
            "GatlingPeaZ", "机枪豌豆僵尸", "耐久：300+500（一类）。\n每秒向前方发射四发豌豆子弹。",
            300, 500, 0));

        ZombieStructDatabase.Add(new ZombieStruct(10, 100, 0, EnvironmentType.Day,
            "BucketScreenDoorZombie", "铁桶铁网门僵尸", "耐久：270+900（一类）+1100（二类）。\n手中的铁门可以有效抵挡直线攻击，头上的铁桶可以有效防御物理打击。",
            270, 1100, 1100));

        ZombieStructDatabase.Add(new ZombieStruct(11, 50, 0, EnvironmentType.Day,
            "PaperZombie", "报纸僵尸", "耐久：500+200（一类）。\n手中的报纸可以抵挡直线攻击。\n报纸掉落后发怒，提升移动速度和攻击速度。",
            500, 200, 0));

        ZombieStructDatabase.Add(new ZombieStruct(12, 50, 0, EnvironmentType.Day,
            "SunFlowerZ", "向日葵僵尸", "耐久：300。\n在生成5秒后生成一个黑色阳光，之后每24秒生成一个黑色阳光。\n黑色阳光：难度3及以下时扣除25阳光，难度4及以上时扣除50阳光。",
            300, 0, 0));

        ZombieStructDatabase.Add(new ZombieStruct(13, 10, 0, EnvironmentType.Day,
            "Chicken", "鸡", "耐久：100。",
            100, 0, 0));

        ZombieStructDatabase.Add(new ZombieStruct(14, 75, 0, EnvironmentType.Day,
            "ChickenZombie", "鸡贼僵尸", "耐久：500。\n首次攻击或生命值低于90%时释放五只僵尸鸡。",
            500, 0, 0));

        ZombieStructDatabase.Add(new ZombieStruct(15, 500, 0, EnvironmentType.Day,
            "RepeaterPaperZombie", "双发射手二爷", "耐久：720+300（一类）。\n手中的报纸可以抵挡攻击。\n报纸掉落后发怒，提升移动速度和攻击速度。\n发怒时每1.5秒发射2发子弹，每发射10发子弹就发射一发火爆辣椒子弹。",
            720, 300, 0));

        ZombieStructDatabase.Add(new ZombieStruct(16, 100, 0, EnvironmentType.Day,
            "Ghost", "幽灵", "耐久：300。\n自身具有隐匿，在后半场随机位置产生，不会啃食植物。",
            300, 0, 0));

        ZombieStructDatabase.Add(new ZombieStruct(17, 200, 0, EnvironmentType.Day,
            "BowlingBallZombie", "胖哥哥", "耐久：600。\n每10秒向前滚出一个可在植物间滚动保龄球，造成60点伤害。",
            600, 0, 0));

        ZombieStructDatabase.Add(new ZombieStruct(18, 100, 0, EnvironmentType.Day,
            "FootballZombie", "橄榄球僵尸", "耐久：270+1400（一类）。\n行动更为快速。",
            270, 1400, 0));

        ZombieStructDatabase.Add(new ZombieStruct(19, 25, 0, EnvironmentType.Day,
            "BackupDancer", "伴舞僵尸", "耐久：270。\n由舞王僵尸召唤而来，正常模式下不会单独出现。",
            270, 0, 0));

        // 森林环境
        ZombieStructDatabase.Add(new ZombieStruct(101, 40, 0, EnvironmentType.Forest,
            "ForestZombie", "森林僵尸", "耐久：300。\n森林污染强化：无。\n一段时间后死亡。\n难度4及以上死亡会在原地产生一个僵尸草丛。",
            300, 0, 0));

        ZombieStructDatabase.Add(new ZombieStruct(102, 50, 0, EnvironmentType.Forest,
            "ForestConeZombie", "森林路障僵尸", "耐久：300+600（一类）。\n森林污染强化：无。\n一段时间后死亡。\n难度4及以上会在原地产生一个僵尸草丛。",
            300, 600, 0));

        ZombieStructDatabase.Add(new ZombieStruct(103, 60, 0, EnvironmentType.Forest,
            "ForestBucketZombie", "森林铁桶僵尸", "耐久：300+1000（一类）。\n森林污染强化：无。\n一段时间后死亡。\n难度4及以上会在原地产生一个僵尸草丛。",
            300, 1200, 0));

        ZombieStructDatabase.Add(new ZombieStruct(104, 75, 0, EnvironmentType.Forest,
            "ForestScreenDoorZombie", "森林铁网门僵尸", "耐久：300+1100（二类）。\n森林污染强化：无。\n铁网门每次受到攻击增加2点森林污染值。",
            300, 0, 1200));

        ZombieStructDatabase.Add(new ZombieStruct(105, 50, 0, EnvironmentType.Forest,
            "LeafZombie", "森林草丛", "耐久：500。\n受到普通类型攻击后增加3点污染森林值。\n免疫森林僵尸一段时间死亡的效果。\n无法啃咬、无法移动，会被植物锁定。",
            500, 0, 0));

        ZombieStructDatabase.Add(new ZombieStruct(106, 100, 0, EnvironmentType.Forest,
            "GrassMaker", "草丛制造者", "耐久：1000。\n（最多一次）生命值低于一半时放置一个草丛阻挡攻击，并每秒恢复200血量至全满。",
            1000, 0, 0));

        ZombieStructDatabase.Add(new ZombieStruct(107, 75, 0, EnvironmentType.Forest,
            "ForestPolevaulterZombie", "森林撑杆僵尸", "耐久：500。\n遇到植物后跳跃（仅限一次），跳跃后移动速度减慢。\n接触到森林灌木丛时进入狂暴状态。",
            600, 0, 0));

        ZombieStructDatabase.Add(new ZombieStruct(108, 100, 0, EnvironmentType.Forest,
            "ForestHiddenZombie", "森林隐匿僵尸", "耐久：300。\n自身具有隐匿效果，每20秒在自身位置随机生成一个森林僵尸。",
            400, 0, 0));

        ZombieStructDatabase.Add(new ZombieStruct(109, 300, 0, EnvironmentType.Forest,
            "HunterZombie", "狙击手僵尸", "耐久：500+500（一类）。\n每8秒狙杀（秒杀）场上血量最少的植物，当存在多株血量相同植物时则锁定最后种植的植物。",
            500, 500, 0));

        ZombieStructDatabase.Add(new ZombieStruct(110, 300, 0, EnvironmentType.Forest,
            "FireGunZombie", "纵火僵尸", "耐久：500+900（一类）。\n自身无视眼前的植物，当有植物处于自身四格内一格外打开火焰喷射器，每0.5秒对该范围内所有植物造成5次20点伤害。",
            500, 900, 0));

        ZombieStructDatabase.Add(new ZombieStruct(111, 150, 0, EnvironmentType.Forest,
            "ForestZombie_P", "污染森林僵尸", "耐久：300。\n拥有三层坚韧。\n坚韧：每次因为攻击失去生命时获得坚韧层数*50的护甲（视为一类防具）。",
            300, 0, 0));

        ZombieStructDatabase.Add(new ZombieStruct(112, 0, 0, EnvironmentType.Forest,
            "Bushes", "森林灌木丛", "在受到直射子弹攻击时，只会受到森林豌豆的伤害（可被其他类型伤害攻击）。" +
            "\n每经过一段时间根据当前关卡出现的僵尸随机生成一个僵尸。" +
            "\n在森林环境中，每隔一段时间在场上随机出现森林灌木丛。" +
            "\n进入森林灌木丛的僵尸拥有「隐匿」效果，不会被大多数植物锁定和大多数子弹攻击。",
            300, 0, 0));

        // 雪地环境
        ZombieStructDatabase.Add(new ZombieStruct(201, 75, 0, EnvironmentType.SnowIce,
            "IceBlockZombie", "冰壳僵尸", "耐久：300+1800（一类）。\n身上有冰块时免疫冰冻效果，但会受到巨大的火焰伤害。",
            300, 1800, 0));

        ZombieStructDatabase.Add(new ZombieStruct(202, 50, 0, EnvironmentType.SnowIce,
            "IceShield", "冰块盾牌", "耐久：1800。\n会受到巨大的火焰伤害。",
            1800, 0, 0));

        ZombieStructDatabase.Add(new ZombieStruct(203, 150, 0, EnvironmentType.SnowIce,
            "YetiZombie", "“北极熊”", "耐久：1500。\n疯狂的冲锋。",
            1500, 0, 0));


        ///钢铁环境
        ZombieStructDatabase.Add(new ZombieStruct(301, 50, 0, EnvironmentType.Steel,
            "SteelZombie", "钢铁僵尸", "耐久：540。\n坚硬的打工人。",
            540, 0, 0));

        ZombieStructDatabase.Add(new ZombieStruct(302, 80, 0, EnvironmentType.Steel,
            "SteelConeZombie", "钢铁路障僵尸", "耐久：540+740（一类）。\n更加坚硬的路障。",
            540, 740, 0));

        ZombieStructDatabase.Add(new ZombieStruct(303, 100, 0, EnvironmentType.Steel,
            "SteelBucketZombie", "钢铁铁桶僵尸", "耐久：540+2200（一类）。\n钢铁般的铁桶，似乎还要去掉一个“般”。",
            540, 2200, 0));


        //联动僵尸
        ZombieStructDatabase.Add(new ZombieStruct(3001, 175, 0, EnvironmentType.Collaboration,
            "TreePeopleZombie", "树人僵尸", "耐久：1500。\n杂交版联动僵尸\n在非啃食状态下持续拥有「隐匿」效果，啃食时失去「隐匿」效果，结束啃食时获得「隐匿」效果。\n移动与啃咬速度更快。",
            1500, 0, 0,
            showLevel: 202));
    }

    // 以下方法保持不变...
    public static ZombieStruct GetZombieStructById(int ZombieStructId)
    {
        return ZombieStructDatabase.FirstOrDefault(ZombieStruct => ZombieStruct.id == ZombieStructId);
    }

    public static ZombieStruct GetZombieStructByCost(int Cost)
    {
        return ZombieStructDatabase.FirstOrDefault(ZombieStruct => ZombieStruct.Cost == Cost);
    }

    public static List<ZombieStruct> GetZombieStructByEnvironment(EnvironmentType envType)
    {
        return ZombieStructDatabase.Where(ZombieStruct => ZombieStruct.envType == envType).ToList();
    }

    public static List<ZombieStruct> GetZombieStructByLevel(int level)
    {
        return ZombieStructDatabase.Where(ZombieStruct => ZombieStruct.showLevel == level).ToList();
    }

    public static List<ZombieStruct> MatchZombieStruct(string field, object value)
    {
        List<ZombieStruct> matchedZombieStructs = new List<ZombieStruct>();

        switch (field.ToLower())
        {
            case "id":
                if (value is int ZombieStructId)
                {
                    matchedZombieStructs.Add(GetZombieStructById(ZombieStructId));
                }
                break;
            case "envtype":
                if (value is EnvironmentType envType)
                {
                    matchedZombieStructs = GetZombieStructByEnvironment(envType);
                }
                break;
            default:
                Debug.LogWarning("无法识别的字段：" + field);
                break;
        }

        return matchedZombieStructs;
    }

    public static int GetDataBaseLength()
    {
        return ZombieStructDatabase.Count;
    }
}