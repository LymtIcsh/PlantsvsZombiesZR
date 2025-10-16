using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;

public enum EnvironmentType
{
    Day,
    SnowIce,
    Forest,
    Steel,
    Special,
    Phonograph,
    Other,
    Collaboration
}

public struct PlantStruct
{
    public int id;
    public int GetLevel;
    public int Cost;
    public int HP;
    public float CD;
    public EnvironmentType envType;
    public string plantName;
    public string ChineseName;
    public string briefIntroduction;
    public string CompletedIntroduction;

    // 新增字段
    public bool IsPurpleCard;
    public string BasePlantName;

    public PlantStruct(int id, int GetLevel, int Cost, int HP, float CD, EnvironmentType envType, string plantName, string ChineseName, string briefIntroduction, string CompletedIntroduction, bool IsPurpleCard = false, string BasePlantName = null)
    {
        this.id = id;
        this.GetLevel = GetLevel;
        this.Cost = Cost;
        this.HP = HP;
        this.CD = CD;
        this.envType = envType;
        this.plantName = plantName;
        this.ChineseName = ChineseName;
        this.briefIntroduction = briefIntroduction;
        this.CompletedIntroduction = CompletedIntroduction;

        this.IsPurpleCard = IsPurpleCard;
        this.BasePlantName = BasePlantName;
    }
}


public static class PlantStructManager
{
    public static List<PlantStruct> PlantStructDatabase = new List<PlantStruct>();

    static PlantStructManager()
    {
        #region 草地植物，编号1-100
        // 1. 豌豆射手：基础血量：300，关卡 1
        PlantStructDatabase.Add(new PlantStruct(
            1, 1, 75, 300, 7.5f, EnvironmentType.Day,
            "CommonShooter", "豌豆射手", "射出豌豆",
            "豌豆射手：\n" +
            "植物类型：基础植物\n" +
            "所属环境：草地-白天\n" +
            "消耗阳光：75\n" +
            "基础血量：300\n" +
            "卡牌冷却：7.5秒\n" +
            "植物能力：<color=green>每1.4秒向前方发射一颗豌豆子弹，对敌方单体造成20点普通伤害。</color>"
        ));

        // 2. 向日葵：基础血量：300，关卡 2
        PlantStructDatabase.Add(new PlantStruct(
            2, 2, 50, 300, 7.5f, EnvironmentType.Day,
            "SunFlower", "向日葵", "生产阳光，在初次生产时速度加快",
            "向日葵：\n" +
            "植物类型：基础植物\n" +
            "所属环境：草地-白天\n" +
            "消耗阳光：50\n" +
            "基础血量：300\n" +
            "卡牌冷却：7.5秒，初始无冷却时间\n" +
            "植物能力：<color=green>可以生产阳光。种植5秒后生产一个普通阳光，此后每24秒生产一个普通阳光。</color>\n" +
            "<color=yellow>普通阳光：难度1-2时每个价值50点阳光值，难度3-5时价值25点阳光值。</color>"
        ));

        // 3. 坚果：基础血量：4000，关卡 3
        PlantStructDatabase.Add(new PlantStruct(
            3, 3, 50, 4000, 30f, EnvironmentType.Day,
            "WallNut", "坚果", "防御单位",
            "坚果：\n" +
            "植物类型：基础植物\n" +
            "所属环境：草地-白天\n" +
            "消耗阳光：50\n" +
            "基础血量：4000\n" +
            "卡牌冷却：30秒\n" +
            "植物能力：<color=green>防御植物。</color>"
        ));

        // 4. 窝瓜：基础血量：300，关卡 4
        PlantStructDatabase.Add(new PlantStruct(
            4, 4, 50, 300, 30f, EnvironmentType.Day,
            "Squash", "窝瓜", "一次性伤害植物",
            "窝瓜：\n" +
            "植物类型：基础植物\n" +
            "所属环境：草地-白天\n" +
            "消耗阳光：50\n" +
            "基础血量：300\n" +
            "卡牌冷却：30秒\n" +
            "植物能力：<color=green>对一定范围内敌方群体造成1800点普通伤害。距离尚未翻越植物的撑杆跳僵尸较近时，可能被套路。</color>\n" +
            "<color=yellow>套路：指窝瓜在锁定攻击目标后，攻击目标位置短时间内发生长距离位移，但窝瓜仍向初始锁定位置攻击，从而导致没有攻击到正确目标。</color>"
        ));

        PlantStructDatabase.Add(new PlantStruct(
            5, 6, 100, 1000, 5f, EnvironmentType.Day,
            "Wood", "原始树桩", "没有实际能力",
            "原始树桩：\n" +
            "植物类型：草地-白天（可在白天变种）\n" +
            "所属环境：无\n" +
            "消耗阳光：100\n" +
            "基础血量：1000\n" +
            "卡牌冷却：5秒\n" +
            "植物能力：<color=green>废物</color>\n" +
            "<color=yellow>废物：详见《植物大战僵尸废物版》</color>"
        ));

        PlantStructDatabase.Add(new PlantStruct(
            6, 5, 175, 500, 7.5f, EnvironmentType.Day,
            "Torchwood", "火炬树桩", "将豌豆及寒冰豌豆转化为火焰豌豆子弹",
            "火炬树桩：\n" +
            "植物类型：变种植物\n" +
            "基础植物：原始树桩\n" +
            "所属环境：草地-白天\n" +
            "消耗阳光：175\n" +
            "基础血量：500\n" +
            "卡牌冷却：7.5秒\n" +
            "植物能力：<color=green>强化部分子弹为火焰豌豆子弹，受到敌方可强化的子弹攻击时拥有特殊效果，火焰豌豆子弹可对敌方单体造成30点普通伤害、10点附加普通伤害。</color>\n" +
            "<color=yellow>可强化的子弹：豌豆子弹、寒冰豌豆子弹、敌方豌豆子弹、敌方寒冰豌豆子弹。</color>\n" +
            "<color=yellow>对敌方子弹特殊效果：受到敌方可强化的子弹攻击后，在敌方子弹位置生成一个向前的火焰豌豆子弹。</color>\n" +
            "<color=yellow>树桩的梦想：在该模式中，控制的火炬树桩不会死亡，强化的火焰豌豆子弹可以对敌方单体造成200点普通伤害、10点附加普通伤害。</color>"
        ));

        PlantStructDatabase.Add(new PlantStruct(
            7, 7, 200, 300, 7.5f, EnvironmentType.Day,
            "Repeater", "双发射手", "射出两颗豌豆",
            "双发射手：\n" +
            "植物类型：基础植物\n" +
            "所属环境：草地-白天\n" +
            "消耗阳光：200\n" +
            "基础血量：300\n" +
            "卡牌冷却：7.5秒\n" +
            "植物能力：<color=green>每1.4秒向前方发射两颗豌豆子弹，每颗豌豆子弹可对敌方单体造成20点普通伤害。</color>"
        ));

        PlantStructDatabase.Add(new PlantStruct(
            8, 8, 100, 1000, 20f, EnvironmentType.Day,
            "Spikeweed", "地刺", "每回攻击两次",
            "地刺：\n" +
            "植物类型：基础植物\n" +
            "所属环境：草地-白天\n" +
            "消耗阳光：100\n" +
            "基础血量：1000\n" +
            "卡牌冷却：20秒\n" +
            "植物能力：<color=green>每0.83秒攻击两次，每次攻击对锁定到的敌方群体造成14点二类伤害。</color>\n" +
            "<color=yellow>二类伤害：可以穿透敌方的二类防具。</color>"
        ));

        PlantStructDatabase.Add(new PlantStruct(
            9, 9, 300, 500, 7.5f, EnvironmentType.Day,
            "MelonPult", "西瓜投手", "造成单体与群体伤害",
            "西瓜投手：\n" +
            "植物类型：基础植物\n" +
            "所属环境：草地-白天\n" +
            "消耗阳光：300\n" +
            "基础血量：500\n" +
            "卡牌冷却：7.5秒\n" +
            "植物能力：<color=green>每2.9秒向前方投掷一颗西瓜。每颗西瓜可对敌方单体造成60点二类伤害，对大范围内的敌方群体造成20点二类溅射伤害。</color>\n" +
            "<color=yellow>二类伤害：可以穿透敌方的二类防具。</color>"
        ));

        PlantStructDatabase.Add(new PlantStruct(
            10, 253, 150, 300, 35f, EnvironmentType.Day,
            "CherryBomb", "樱桃炸弹", "对一定范围内敌人造成灰烬效果",
            "樱桃炸弹：\n" +
            "植物类型：基础植物\n" +
            "所属环境：草地-白天\n" +
            "消耗阳光：150\n" +
            "基础血量：300\n" +
            "卡牌冷却：35秒\n" +
            "植物能力：<color=green>种植短暂时间后爆炸，对约以3格为直径的圆内的、不超过自身上下两行的敌人造成灰烬效果。</color>\n" +
            "<color=yellow>灰烬效果：如果敌方本体血量低于1800则直接秒杀，如果高于1800则造成1800点一类伤害。</color>"
        ));

        PlantStructDatabase.Add(new PlantStruct(
            11, 11, 25, 300, 30f, EnvironmentType.Day,
            "PotatoMine", "土豆地雷", "一段时间后出土，对范围内敌方群体造成伤害",
            "土豆地雷：\n" +
            "植物类型：基础植物\n" +
            "所属环境：草地-白天\n" +
            "消耗阳光：25\n" +
            "基础血量：300\n" +
            "卡牌冷却：30秒\n" +
            "植物能力：<color=green>种下13-17秒后出土，在出土状态下碰到敌方目标立刻爆炸，对一定范围内的敌方群体造成1800点二类伤害。会被啃咬，不会受到敌方普通子弹攻击（如豌豆子弹等）</color>\n" +
            "<color=yellow>二类伤害：可以穿透敌方的二类防具。</color>\n" +
            "<color=yellow>狂轰滥炸！：在该模式中，控制的土豆地雷在游戏开始时立刻出土，免疫所有类型伤害，可以无限次爆炸，每次爆炸伤害提高至18000点二类伤害</color>"
        ));

        PlantStructDatabase.Add(new PlantStruct(
            13, 10, 0, 300, 7.5f, EnvironmentType.Day,
            "SmallPuff", "小喷菇", "他不嫌你穷你也别嫌它菜！",
            "小喷菇：\n" +
            "植物类型：基础植物\n" +
            "所属环境：草地-夜晚\n" +
            "消耗阳光：0\n" +
            "基础血量：300\n" +
            "卡牌冷却：7.5秒\n" +
            "植物能力：<color=green>僵尸进入三格范围内时每1.4秒向前方发射一颗孢子，对敌方单体造成20点普通伤害。</color>"
        ));

        PlantStructDatabase.Add(new PlantStruct(
            14, 12, 25, 300, 7.5f, EnvironmentType.Day,
            "SunShroom", "阳光菇", "夜间生产阳光的植物",
            "阳光菇：\n" +
            "植物类型：基础植物\n" +
            "所属环境：草地-夜晚\n" +
            "消耗阳光：0\n" +
            "基础血量：300\n" +
            "卡牌冷却：7.5秒\n" +
            "植物能力：<color=green>可以生产阳光。种植5秒后生产一个阳光，此后每24秒生产一个阳光，80秒后长大。</color>"
        ));

        PlantStructDatabase.Add(new PlantStruct(
            15, 13, 75, 300, 7.5f, EnvironmentType.Day,
            "FumeShroom", "大喷菇", "真群攻穿透攻击，并且无视敌方单位的隐匿效果",
            "大喷菇：\n" +
            "植物类型：基础植物\n" +
            "所属环境：草地-夜晚\n" +
            "消耗阳光：75\n" +
            "基础血量：300\n" +
            "卡牌冷却：7.5秒\n" +
            "植物能力：<color=green>僵尸进入四格范围内每1.4秒向前方喷射孢子，对四格内所有敌人造成20点二类伤害，并且无视敌方单位的隐匿效果。</color>\n"
        ));

        PlantStructDatabase.Add(new PlantStruct(
            16, 253, 125, 8000, 30f, EnvironmentType.Day,
            "TallNut", "高坚果", "可以打断撑杆跳僵尸跳跃的防御单位",
            "高坚果：\n" +
            "植物类型：基础植物\n" +
            "所属环境：草地-白天\n" +
            "消耗阳光：125\n" +
            "基础血量：8000\n" +
            "卡牌冷却：30秒\n" +
            "植物能力：<color=green>防御植物，可以阻止撑杆跳僵尸的翻越，如果高坚果成功拦截撑杆跳僵尸，会使其退后一小步。</color>\n"
        ));

        PlantStructDatabase.Add(new PlantStruct(
            17, 14, 175, 300, 7.5f, EnvironmentType.Day,
            "Chomper", "大嘴花", "吞噬普通的僵尸",
            "大嘴花：\n" +
            "植物类型：基础植物\n" +
            "所属环境：草地-夜晚\n" +
            "消耗阳光：150\n" +
            "基础血量：300\n" +
            "卡牌冷却：7.5秒\n" +
            "植物能力：<color=green>吞噬一般的僵尸，后进入42秒冷却，冷却时间内无法攻击。</color>\n"
        ));

        PlantStructDatabase.Add(new PlantStruct(
            18, 15, 75, 300, 30f, EnvironmentType.Day,
            "HypnoShroom", "魅惑菇", "使一只僵尸为你作战",
            "魅惑菇：\n" +
            "植物类型：基础植物\n" +
            "所属环境：草地-夜晚\n" +
            "消耗阳光：75\n" +
            "基础血量：300\n" +
            "卡牌冷却：30秒\n" +
            "植物能力：<color=green>被啃咬时魅惑可以被魅惑的僵尸。</color>\n" +
            "<color=red>对当前版本魅惑机制的详细补充说明：</color>" +
            "\n在自然版中，我方魅惑僵尸无法阻挡敌方子弹。" +
            "\n魅惑是对僵尸本体的影响，不能影响环境特性。" +
            "\n比如森林僵尸自然死亡（指森林僵尸有可能自杀）会生成森林草丛，这个草丛视作敌对目标，因为是环境生成了草丛，而不是被魅惑的僵尸生成了草丛；但是比如草丛制造者被魅惑，产生的森林草丛就是己方的，因为这个是主动产生，不是因环境而产生的。" +
            "\n再比如森林僵尸自然死亡会产生污染森林值，不会说被魅惑了就生成森林值，因为污染森林值是环境属性，被魅惑的本体无法更改环境。" +
            "\n也就是“环境与产生环境效果的个体无关，与机制有关”，这也是自然版设计的原理之一。"
        ));

        PlantStructDatabase.Add(new PlantStruct(
            19, 16, 125, 300, 50f, EnvironmentType.Day,
            "DoomShroom", "毁灭菇", "造成大规模破坏并留下弹坑。",
            "毁灭菇：\n" +
            "植物类型：基础植物\n" +
            "所属环境：草地-夜晚\n" +
            "消耗阳光：125\n" +
            "基础血量：300\n" +
            "卡牌冷却：50秒\n" +
            "植物能力：<color=green>种下一小段时间后爆炸，对以自身为圆心、直径约6.5格的圆内的所有僵尸造成灰烬效果。</color>"
        ));
        #endregion

        #region 森林植物，编号101-200
        // 以下为森林系植物

        // 104. 森林树桩：基础血量：2000，关卡 51
        PlantStructDatabase.Add(new PlantStruct(
            104, 51, 225, 2000, 7.5f, EnvironmentType.Forest,
            "DiamonWood", "森林树桩", "强化所有豌豆为森林豌豆并消耗生命值，\n强化火焰豌豆时生产阳光，\n森林豌豆可为敌方单体附加并引爆毒素",
            "森林树桩：\n" +
            "植物类型：变种植物\n" +
            "基础植物：原始树桩\n" +
            "所属环境：森林-普通\n" +
            "消耗阳光：225\n" +
            "基础血量：2000\n" +
            "卡牌冷却：5秒\n" +
            "植物能力：<color=green>强化部分子弹为森林豌豆子弹，每次强化子弹扣除150点血量，强化火焰豌豆子弹或受到敌方可强化的子弹攻击时拥有特殊效果，森林豌豆子弹可对敌方单体造成30点普通伤害、附加3层毒伤层数、增加1点森林值。</color>\n" +
            "<color=yellow>可强化的子弹：豌豆子弹、寒冰豌豆子弹、火焰豌豆子弹、敌方豌豆子弹、敌方寒冰豌豆子弹、敌方火焰豌豆子弹。</color>\n" +
            "<color=yellow>对敌方子弹特殊效果：受到敌方可强化的子弹攻击后，在敌方子弹位置生成一个向前的火焰豌豆子弹。</color>\n" +
            "<color=yellow>对火焰豌豆子弹特殊效果：强化敌我双方火焰豌豆子弹时，会生产普通阳光。</color>\n" +
            "<color=yellow>树桩的梦想：在该模式中，控制的森林树桩不会死亡。</color>"
        ));

        // 101. 森林向日葵：基础血量：300，关卡 52
        PlantStructDatabase.Add(new PlantStruct(
            101, 52, 75, 300, 7.5f, EnvironmentType.Forest,
            "ForestSunFlower", "森林向日葵", "治疗并解除周围植物的异常状态。\n在阳光低于1000时周围有森林向日葵时能额外产生阳光\n阳光高于1000时极大提升治疗能力，但是需要消耗阳光",
            "森林向日葵：\n" +
            "植物类型：变种植物\n" +
            "基础植物：向日葵\n" +
            "所属环境：森林-普通\n" +
            "消耗阳光：75\n" +
            "基础血量：300\n" +
            "卡牌冷却：7.5秒\n" +
            "植物能力：<color=green>产生阳光的同时治疗半径为1格的所有植物，并解除他们的异常状态。\n" +
            "阳光小于1000时半径1格之内每有一个森林向日葵额外产生一个阳光\n" +
            "阳光大于1000时提高治疗量到\n（100+植物血量/10），\n" +
            "但此时每治疗一个植物需要（难度三以下：5；\n" +
            "难度三以上：10）点阳光</color>"
        ));

        // 102. 森林坚果：基础血量：6000，关卡 53
        PlantStructDatabase.Add(new PlantStruct(
            102, 53, 150, 6000, 30f, EnvironmentType.Forest,
            "ForestWallNut", "森林坚果", "与治疗植物的好拍档",
            "森林坚果：\n" +
            "植物类型：变种植物\n" +
            "基础植物：坚果\n" +
            "所属环境：森林-普通\n" +
            "消耗阳光：150\n" +
            "基础血量：6000\n" +
            "卡牌冷却：30秒\n" +
            "植物能力：<color=green>受到的治疗量提升100%，被僵尸啃食时为其附加一层毒素。</color>\n"
        ));

        // 103. 森林地刺：基础血量：1000，关卡 54
        PlantStructDatabase.Add(new PlantStruct(
            103, 54, 125, 1000, 7.5f, EnvironmentType.Forest,
            "ForestSpikeweed", "森林地刺", "攻击可以引爆敌方毒素",
            "森林地刺：\n" +
            "植物类型：变种植物\n" +
            "基础植物：地刺\n" +
            "所属环境：森林-普通\n" +
            "消耗阳光：125\n" +
            "基础血量：1000\n" +
            "卡牌冷却：10秒\n" +
            "植物能力：<color=green>每0.83秒攻击两次，每次攻击对锁定到的敌方群体造成14点二类伤害、引爆1次毒伤层数、增加1点森林值。</color>\n" +
            "<color=yellow>二类伤害：可以穿透敌方的二类防具。</color>"
        ));

        // 105. 毒西瓜投手：基础血量：500，关卡 55
        PlantStructDatabase.Add(new PlantStruct(
            105, 55, 450, 500, 7.5f, EnvironmentType.Forest,
            "ForestMelonPult", "毒西瓜投手", "为单体附加与群体附加毒素",
            "毒西瓜投手：\n" +
            "植物类型：变种植物\n" +
            "基础植物：西瓜投手\n" +
            "所属环境：森林-普通\n" +
            "消耗阳光：450\n" +
            "基础血量：500\n" +
            "卡牌冷却：7.5秒\n" +
            "植物能力：<color=green>每2.9秒向前方投掷一颗毒西瓜，每颗毒西瓜可对敌方单体造成60点二类伤害、附加2层毒伤层数、增加2点森林值，对大范围内的敌方群体造成20点二类溅射伤害、附加1层毒伤层数。</color>\n" +
            "<color=yellow>二类伤害：可以穿透敌方的二类防具。</color>"
        ));

        // 106. 橡木弓箭手：基础血量：300，关卡 56
        PlantStructDatabase.Add(new PlantStruct(
            106, 56, 175, 300, 7.5f, EnvironmentType.Forest,
            "OakArcher", "橡木弓箭手", "无视敌人隐匿效果的攻击，并且根据敌人中毒层数提升伤害",
            "橡木弓箭手：\n" +
            "所属环境：森林-普通\n" +
            "消耗阳光：175\n" +
            "基础血量：300\n" +
            "卡牌冷却：7.5秒，初始无冷却时间\n" +
            "植物能力：<color=green>每1.5秒向前发射一发橡木弓箭，无视敌人隐匿效果并造成（50+僵尸中毒层数*10）的伤害</color>\n"
        ));

        // 107. 毒烟小喷菇：基础血量：300，关卡 57
        PlantStructDatabase.Add(new PlantStruct(
            107, 57, 15, 300, 15f, EnvironmentType.Forest,
            "ForestSmallPuff", "毒烟小喷菇", "攻击附加1层毒素，死亡时对本格所有僵尸附加10层毒素",
            "毒烟小喷菇：\n" +
            "植物类型：变种植物\n" +
            "基础植物：小喷菇\n" +
            "所属环境：森林-普通\n" +
            "消耗阳光：15\n" +
            "基础血量：300\n" +
            "卡牌冷却：15秒\n" +
            "植物能力：<color=green>僵尸进入三格范围内每1.4秒向前方发射一颗毒孢子，对敌方单体造成20点普通伤害并附加1层毒素。\n死亡时对本格所有僵尸附加10层毒素。</color>"
        ));

        // 108. 荧光蘑菇：基础血量：300，关卡 57
        PlantStructDatabase.Add(new PlantStruct(
            108, 57, 35, 300, 7.5f, EnvironmentType.Forest,
            "ForestSunShroom", "荧光蘑菇", "周围有其他荧光蘑菇时额外产生阳光，死亡时对本格所有僵尸附加10层毒素",
            "荧光蘑菇：\n" +
            "植物类型：变种植物\n" +
            "基础植物：阳光菇\n" +
            "所属环境：森林-普通\n" +
            "消耗阳光：35\n" +
            "基础血量：300\n" +
            "卡牌冷却：7.5秒\n" +
            "植物能力：<color=green>可以生产阳光。种植5秒后生产一个阳光，此后每24秒生产一个阳光，80秒后长大。\n周围有其他荧光蘑菇时额外产生阳光，死亡时对本格所有僵尸附加10层毒素。</color>"
        ));

        // 109. 毒烟大喷菇：基础血量：300，关卡 58
        PlantStructDatabase.Add(new PlantStruct(
            109, 58, 150, 300, 7.5f, EnvironmentType.Forest,
            "ForestFumeShroom", "毒烟大喷菇", "无视隐匿的真群攻伤害并附加3层毒素，死亡时对本格所有僵尸赋予10层毒素并引爆1次毒伤",
            "毒烟大喷菇：\n" +
            "植物类型：变种植物\n" +
            "基础植物：大喷菇\n" +
            "所属环境：森林-普通\n" +
            "消耗阳光：150\n" +
            "基础血量：300\n" +
            "卡牌冷却：7.5秒\n" +
            "植物能力：<color=green>每1.5秒对前方4格内所有僵尸无视隐匿的真群攻伤害并附加3层毒素。\n死亡后对本格所有僵尸赋加10层毒素并引爆1次毒伤.</color>"
        ));

        // 110. 木壳坚果：基础血量：500，关卡 59
        PlantStructDatabase.Add(new PlantStruct(
            110, 59, 150, 500, 30f, EnvironmentType.Forest,
            "WoodWallNut", "木壳坚果", "因受到攻击而失去生命时获得护甲\n受到攻击时向八个方向发射木刺子弹",
            "木壳坚果：\n" +
            "植物类型：变种植物\n" +
            "基础植物：坚果\n" +
            "所属环境：森林-污染\n" +
            "消耗阳光：150\n" +
            "基础血量：500\n" +
            "卡牌冷却：30秒\n" +
            "植物能力：<color=green>在森林环境中拥有3层坚韧，其他环境2层。\n受到攻击后向八个方向发射木刺子弹,每颗子弹造成5点伤害并附加1层毒素。\n受到的治疗量降低80%。</color>\n" +
            "<color=yellow>坚韧：每当受到物理攻击而失去生命时，每层坚韧可提供50点护甲。</color>"
        ));
        #endregion

        #region 冰雪植物，编号201-300
        // 以下数据为冰雪系植物（不受关卡调整影响）

        // 201. 寒冰射手：基础血量：300，原 GetLevel 保持 242
        PlantStructDatabase.Add(new PlantStruct(
            201, 242, 175, 300, 7.5f, EnvironmentType.SnowIce,
            "SnowPea", "寒冰射手", "射出寒冰豌豆",
            "寒冰射手：\n" +
            "植物类型：变种植物\n" +
            "基础植物：豌豆射手\n" +
            "所属环境：冰雪-普通\n" +
            "消耗阳光：175\n" +
            "基础血量：300\n" +
            "卡牌冷却：7.5秒\n" +
            "植物能力：<color=green>每1.4秒向前方发射一颗寒冰豌豆子弹，每颗寒冰豌豆子弹可对敌方单体造成20点普通伤害、使敌方移速下降50%。</color>"
        ));

        // 202. 寒冰双发射手：基础血量：300，GetLevel 保持 242
        //PlantStructDatabase.Add(new PlantStruct(
        //    202, 242, 350, 300, 7.5f, EnvironmentType.SnowIce,
        //    "SnowPeaRepeater", "寒冰双发射手", "射出两颗寒冰豌豆",
        //    "寒冰双发射手：\n" +
        //    "植物类型：变种植物\n" +
        //    "基础植物：双发射手\n" +
        //    "所属环境：冰雪-普通\n" +
        //    "消耗阳光：350\n" +
        //    "基础血量：300\n" +
        //    "卡牌冷却：7.5秒\n" +
        //    "植物能力：<color=green>每1.4秒向前方发射两颗寒冰豌豆子弹，每颗寒冰豌豆子弹可对敌方单体造成20点普通伤害、使敌方移速下降50%。</color>"
        //));

        // 203. 冰西瓜投手：基础血量：500，GetLevel 保持 242
        PlantStructDatabase.Add(new PlantStruct(
            203, 242, 500, 500, 7.5f, EnvironmentType.SnowIce,
            "WinterMelonPult", "冰西瓜投手", "造成单体与群体伤害并减速",
            "冰西瓜投手：\n" +
            "植物类型：变种植物\n" +
            "基础植物：西瓜投手\n" +
            "所属环境：冰雪-普通\n" +
            "消耗阳光：500\n" +
            "基础血量：500\n" +
            "卡牌冷却：7.5秒\n" +
            "植物能力：<color=green>每2.9秒向前方投掷一颗西瓜。每颗西瓜可对敌方单体造成60点二类伤害，对大范围内的敌方群体造成20点二类溅射伤害并使其移速下降50%。</color>\n" +
            "<color=yellow>二类伤害：可以穿透敌方的二类防具。</color>"
        ));

        // 204. 冰封向日葵：基础血量：100，GetLevel 保持 242
        PlantStructDatabase.Add(new PlantStruct(
            204, 242, 100, 100, 7.5f, EnvironmentType.SnowIce,
            "WinterSunFlower", "冰封向日葵", "生产小阳光后变成向日葵",
            "冰封向日葵：\n" +
            "植物类型：变种植物\n" +
            "基础植物：向日葵\n" +
            "所属环境：冰雪-普通\n" +
            "消耗阳光：100\n" +
            "基础血量：100\n" +
            "卡牌冷却：7.5秒，初始无冷却时间\n" +
            "植物能力：<color=green>可以生产阳光。种植后10秒内每秒产生一个寒冰小阳光，后变为向日葵并恢复所有血量。被啃咬、碾压、铲除后也会变为向日葵。</color>\n" +
            "<color=yellow>寒冰小阳光：难度1-2时每个价值10点阳光值，难度3-5时价值5点阳光值。</color>"
        ));

        #endregion

        #region 钢铁植物，编号301-400
        // 301. 铁刺坚果：基础血量：1000，GetLevel 保持 243
        PlantStructDatabase.Add(new PlantStruct(
            301, 243, 125, 1000, 30f, EnvironmentType.Steel,
            "SteelWallNut", "铁刺坚果", "反伤并击退，死亡后变为坚果",
            "铁刺坚果：\n" +
            "植物类型：变种植物\n" +
            "基础植物：坚果\n" +
            "所属环境：？？？\n" +
            "消耗阳光：125\n" +
            "基础血量：1000\n" +
            "卡牌冷却：30秒\n" +
            "植物能力：<color=green>受到僵尸伤害后，对僵尸造成等于自身受伤值一半的普通伤害并击退僵尸，最高不超过100点伤害，死亡后变为坚果。被啃咬、碾压、铲除后也会变为坚果。</color>"
        ));

        #endregion

        #region 紫卡植物，编号1001-2000
        // 111. 污染毒瓜投手：基础血量：500，保持原 GetLevel（用于特殊模式），不根据前表更新
        PlantStructDatabase.Add(new PlantStruct(
            1001, 60, 200, 500, 50f, EnvironmentType.Special,
            "ForestMelonPult_P", "污染毒西瓜投手", "为单体附加与引爆毒素，群体附加毒素",
            "污染毒瓜投手：\n" +
            "植物类型：变种植物，需要种在毒西瓜投手上\n" +
            "基础植物：毒西瓜投手\n" +
            "所属环境：森林-污染\n" +
            "消耗阳光：200\n" +
            "基础血量：500\n" +
            "卡牌冷却：50秒\n" +
            "植物能力：<color=green>每2.9秒向前方投掷一颗污染毒西瓜，每颗毒西瓜可对敌方单体造成60点二类伤害、附加6层毒伤层数并引爆一次毒素伤害、增加3点森林值，对大范围内的敌方群体造成20点二类溅射伤害、附加1层毒伤层数。</color>\n" +
            "<color=yellow>二类伤害：可以穿透敌方的二类防具。</color>",
            IsPurpleCard: true,
            BasePlantName: "ForestMelonPult"
        ));

        PlantStructDatabase.Add(new PlantStruct(
            1002, 243, 250, 800, 50f, EnvironmentType.Special,
            "GatlingPea", "机枪射手", "一次性发射大量豌豆子弹",
            "机枪射手：\n" +
            "植物类型：基础植物，需要种在双发射手上\n" +
            "基础植物：双发射手\n" +
            "所属环境：草地-白天\n" +
            "消耗阳光：250\n" +
            "基础血量：800\n" +
            "卡牌冷却：50秒\n" +
            "植物能力：<color=green>每1.4秒发射4颗豌豆，每颗豌豆可造成20点伤害。</color>\n",
            IsPurpleCard: true,
            BasePlantName: "Repeater"
        ));

        PlantStructDatabase.Add(new PlantStruct(
            1003, 246, 300, 3000, 30f, EnvironmentType.Special,
            "SymbioticForestTorchWood", "共生森林火炬树桩", "兼具生产与治疗能力的树桩",
            "共生森林火炬树桩：\n" +
            "植物类型：变种植物，需要种在森林树桩上\n" +
            "基础植物：森林树桩\n" +
            "所属环境：森林-完整\n" +
            "消耗阳光：300\n" +
            "基础血量：3000\n" +
            "卡牌冷却：10秒\n" +
            "植物能力：<color=green>\n初始拥有3000点生命值。\n</color>" +
            "<color=green>无法被大部分的僵尸攻击，可以被撑杆跳识别为障碍物并翻越。</color>" +
            "<color=green>\n转化大部分的平射子弹为森林子弹，并使其增加等同于自身最大生命值1%的伤害，前二十次转化时可以生产阳光。</color>" +
            "<color=green>\n在被种植后，如果当前场上存在的共生森林火炬树桩（包括自身）大于等于2株，则使全场植物进入「共鸣」状态，提升坚果类植物植物10%的最大生命值（生命值上限为10万）并立刻恢复所有植物等同于最大生命值20%的血量，此效果可以叠加。</color>",
            IsPurpleCard: true,
            BasePlantName: "DiamonWood"
        ));
        #endregion

        #region 唱片植物，编号2001-3000
        PlantStructDatabase.Add(new PlantStruct(
            2001, 56, 50, 500, 500000000f, EnvironmentType.Phonograph,
            "Phonograph", "故事最初的留声机", "《落在遗世群界的谜语》",
            "故事最初的留声机：\n" +
            "植物类型：留声机\n" +
            "消耗阳光：50\n" +
            "基础血量：500\n" +
            "卡牌冷却：500000000秒\n" +
            "植物能力：<color=green>种下后替代背景音乐，持续播放《落在遗世群界的谜语(Whispers of the Wild Realms)》，死亡后效果消失，每局仅能种植一次</color>"
        ));

        #endregion

        #region 联动植物，编号3001-4000
        PlantStructDatabase.Add(new PlantStruct(
            3001, 201, 350, 800, 30f, EnvironmentType.Collaboration,
            "JalapenoGatlingPea", "辣椒机枪射手", "发射火焰辣椒子弹的加强射手，从名叫嫁接版的异世界而来",
            "辣椒机枪射手：\n" +
            "植物类型：变种植物，需要种在双发射手上\n" +
            "基础植物：双发射手\n" +
            "所属环境：火焰山-昼\n" +
            "消耗阳光：350\n" +
            "基础血量：800\n" +
            "卡牌冷却：30秒\n" +
            "植物能力：<color=green>每1.4秒发射4颗火焰辣椒子弹，每颗子弹可造成40点伤害，其它同火焰豌豆子弹；死亡后在自身所在格产生单格灰烬效果。</color>\n" +
            "联动介绍：<color=red>辣椒机枪射手联动自植物大战僵尸嫁接版。</color>\n",
            IsPurpleCard: true,
            BasePlantName: "Repeater"
        ));

        #endregion
    }

    public static PlantStruct GetPlantStructById(int PlantStructId)
    {
        return PlantStructDatabase.FirstOrDefault(PlantStruct => PlantStruct.id == PlantStructId);
    }
    public static PlantStruct GetPlantStructByName(string PlantStructName)
    {
        return PlantStructDatabase.FirstOrDefault(PlantStruct => PlantStruct.plantName == PlantStructName);
    }
    public static PlantStruct GetPlantStructByGetLevel(int GetLevel)
    {
        return PlantStructDatabase.FirstOrDefault(PlantStruct => PlantStruct.GetLevel == GetLevel);
    }

    public static List<PlantStruct> GetPlantStructsByGetLevel(int GetLevel)
    {
        return PlantStructDatabase.Where(ps => ps.GetLevel == GetLevel).ToList();
    }

    public static List<PlantStruct> GetPlantStructByEnvironment(EnvironmentType envType)
    {
        return PlantStructDatabase.Where(PlantStruct => PlantStruct.envType == envType).ToList();
    }

    public static List<PlantStruct> MatchPlantStruct(string field, object value)
    {
        List<PlantStruct> matchedPlantStructs = new List<PlantStruct>();

        switch (field.ToLower())
        {
            case "id":
                if (value is int PlantStructId)
                {
                    matchedPlantStructs.Add(GetPlantStructById(PlantStructId));
                }
                break;
            case "envtype":
                if (value is EnvironmentType envType)
                {
                    matchedPlantStructs = GetPlantStructByEnvironment(envType);
                }
                break;
            default:
                Debug.LogWarning("无法识别的字段：" + field);
                break;
        }

        return matchedPlantStructs;
    }

    public static int GetDataBaseLength()
    {
        return PlantStructDatabase.Count;
    }
}
