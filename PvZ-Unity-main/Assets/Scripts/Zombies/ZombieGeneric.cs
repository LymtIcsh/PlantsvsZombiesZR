using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Zombie: MonoBehaviour
{
    #region 定义

    /// <summary>
    /// 可以啃咬 - 标识僵尸是否可以啃咬植物
    /// </summary>
    [FormerlySerializedAs("可以啃咬")] public bool CanBite = true;

    protected bool armIsDrop = false;
    protected bool headIsDrop = false;
    protected bool level1ArmorIsDrop = false;
    protected bool level2ArmorIsDrop = false;

    /// <summary>
    /// 铁门类僵尸 - 标识是否为铁门类僵尸
    /// </summary>
    [FormerlySerializedAs("铁门类僵尸")] public bool IsIronDoorZombie = false;

    public int zombieID;

    /// <summary>
    /// 基础僵尸类型 - 0为普通僵尸，1为路障，2为铁桶
    /// </summary>
    [FormerlySerializedAs("基础僵尸类型")] public int BaseZombieType;

    //[HideInInspector]
    public int pos_row;   //位于第几行

    //生命相关
    /// <summary>
    /// 血量 - 当前血量
    /// </summary>
    [FormerlySerializedAs("血量")] [HideInInspector]
    public int Health;

    /// <summary>
    /// 最大血量 - 最大血量值
    /// </summary>
    [FormerlySerializedAs("最大血量")] [HideInInspector]
    public int MaxHealth;

    /// <summary>
    /// 一类防具
    /// </summary>
    [HideInInspector]
    public int level1ArmorHealth;
    [HideInInspector]
    public int level1ArmorMaxHealth;



    /// <summary>
    /// 二类防具
    /// </summary>
    [HideInInspector]
    public int level2ArmorHealth;
    [HideInInspector]
    public int level2ArmorMaxHealth;

    /// <summary>
    /// 一类防具半破损已经切换 - 标识一类防具半破损状态是否已切换
    /// </summary>
    protected bool Level1ArmorHalfDamagedSwitched = false;

    /// <summary>
    /// 一类防具完全破损已经切换 - 标识一类防具完全破损状态是否已切换
    /// </summary>
    protected bool Level1ArmorFullyDamagedSwitched = false;

    /// <summary>
    /// 二类半破损已经切换 - 标识二类防具半破损状态是否已切换
    /// </summary>
    [FormerlySerializedAs("二类半破损已经切换")] [HideInInspector]
    public bool Level2ArmorHalfDamagedSwitched = false;

    /// <summary>
    /// 二类完全破损已经切换 - 标识二类防具完全破损状态是否已切换
    /// </summary>
    [FormerlySerializedAs("二类完全破损已经切换")] [HideInInspector]
    public bool Level2ArmorFullyDamagedSwitched = false;

    [HideInInspector]
    public bool alive = true;

    //攻击相关
    /// <summary>
    /// 攻击力 - 僵尸的攻击力
    /// </summary>
    [FormerlySerializedAs("攻击力")] [HideInInspector]
    public int AttackPower;

    protected Plant attackPlant;   //当前所攻击植物的Plant组件
    protected Zombie attackZombie;

    protected Animator myAnimator;   //动画组件


    [HideInInspector]
    public int audioIndex = 1;

    //static int orderOffset = 0;

    [HideInInspector]
    public Material highlightMaterial; // 在Inspector中指定的高光材质

    [HideInInspector]
    public const float highlightDuration = 0.06f; // 高光效果的持续时间（秒）

    [HideInInspector]
    public Renderer[] allRenderers;    // 所有 Renderer 组件

    /// <summary>
    /// 减速时间 - 减速效果的持续时间
    /// </summary>
    private float _decelerationTime = 5f;

    /// <summary>
    /// 减速协程 - 用来存储减速协程的引用
    /// </summary>
    private Coroutine _decelerationCoroutine;

    /// <summary>
    /// 中毒效果中 - 标识是否正在中毒效果中
    /// </summary>
    private bool _isPoisoned = false;

    /// <summary>
    /// 冰冻效果中 - 标识是否正在冰冻效果中
    /// </summary>
    private bool _isFrozen = false;


    public Sprite coneBroken1;//路障三个
    public Sprite coneBroken2;
    public Sprite coneBroken3;
    public GameObject coneDrop;

    public Sprite bucketBroken1; //铁桶三个
    public Sprite bucketBroken2;
    public Sprite bucketBroken3;
    public GameObject bucketDrop;

    public Sprite fullHead;

    public Sprite brokenArm;//受伤的手臂图片

    public GameObject zombieHeadDrops;
    public GameObject zombieArmDrops;


    public bool dontHaveDropHead;

    /// <summary>
    /// 高亮 - 高亮效果协程
    /// </summary>
    public Coroutine _highlightCoroutine;

    public bool isEating;

    public ZombieForestSlider zombieForestSlider;

    /// <summary>
    /// 正在啃咬目标 - 当前正在啃咬的目标对象
    /// </summary>
    [FormerlySerializedAs("正在啃咬目标")] public GameObject CurrentBiteTarget;

    //[HideInInspector]
    public bool dying = false;

    public Sprite[] DoorSprite;//铁门图片

    /// <summary>
    /// 铁门僵尸啃咬显示的 - 铁门僵尸啃咬时显示的对象数组
    /// </summary>
    private GameObject[] _ironDoorZombieBiteDisplay;

    /// <summary>
    /// 铁门僵尸行走显示的 - 铁门僵尸行走时显示的对象数组
    /// </summary>
    private GameObject[] _ironDoorZombieWalkDisplay;

    /// <summary>
    /// 铁门僵尸无铁门行走时显示的 - 铁门僵尸无铁门行走时显示的对象数组
    /// </summary>
    private GameObject[] _ironDoorZombieWalkWithoutDoorDisplay;

    /// <summary>
    /// 铁门 - 铁门对象数组
    /// </summary>
    private GameObject[] _ironDoors;

    /// <summary>
    /// 铁门断臂时强制不显示 - 铁门断臂时强制不显示的对象数组
    /// </summary>
    private GameObject[] _ironDoorArmBrokenForceHide;

    /// <summary>
    /// 铁门断臂时强制显示 - 铁门断臂时强制显示的对象数组
    /// </summary>
    private GameObject[] _ironDoorArmBrokenForceShow;

    /// <summary>
    /// 铁门全部 - 所有铁门相关对象数组
    /// </summary>
    private GameObject[] _ironDoorAll;

    /// <summary>
    /// 狂暴速度乘区 - 狂暴状态的速度倍率
    /// </summary>
    private float _furiousSpeedZone = 1f;

    /// <summary>
    /// 减速速度乘区 - 减速状态的速度倍率
    /// </summary>
    private float _decelerationSpeedZone = 1f;

    /// <summary>
    /// 随机速度乘区 - 随机速度倍率
    /// </summary>
    private float _randomSpeedZone = 1f;

    /// <summary>
    /// 关卡特殊乘区 - 关卡特殊速度倍率
    /// </summary>
    private float _levelSpecialZone = 1f;

    /// <summary>
    /// 环境速度乘区 - 环境速度倍率（私有字段）
    /// </summary>
    [SerializeField]
    private float _environmentSpeedZone = 1f;

    /// <summary>
    /// 环境速度乘区 - 环境速度倍率（用于覆写）
    /// </summary>
    protected virtual float EnvironmentSpeedZone
    {
        get => _environmentSpeedZone;
        set => _environmentSpeedZone = value;
    }

    /// <summary>
    /// 血量显示 - 血量显示文本组件
    /// </summary>
    private TMP_Text _healthDisplay;

    /// <summary>
    /// debuff - 僵尸的负面效果
    /// </summary>
    public ZombieDebuff debuff;

    /// <summary>
    /// buff - 僵尸的增益效果
    /// </summary>
    public ZombieBuff buff;

    public bool newZombie = false;

    private bool hasFaded = false;

    private bool isEntrance = false;
    #endregion

    #region 开始时执行
    protected virtual void Awake()
    {
        //highlightMaterial = Resources.Load<Material>("Shader/highLight");
        myAnimator = gameObject.GetComponent<Animator>();
        allRenderers = GetComponentsInChildren<Renderer>(true);

        if (GameManagement.instance != null && GameManagement.instance.IsGameing && ZombieManagement.instance != null)
        {
            ZombieManagement.instance.GetComponent<ZombieManagement>().addZombieNumAll(gameObject);
        }
        else
        {
            InitializeDisplayAnimation();
        }

        TMP_Text loadHealthObject = Resources.Load<TMP_Text>("Prefabs/Effects/血量显示/普通僵尸血量显示");
        _healthDisplay = Instantiate(loadHealthObject, transform.position, Quaternion.identity, transform);
        _healthDisplay.gameObject.SetActive(false);

        _randomSpeedZone = Random.Range(0.8f, 1.2f);
        LoadAnimationSpeed();
        //foreach (Renderer renderer in allRenderers)
        //{
        //    if (renderer != null && renderer.material != highlightMaterial)
        //    {
        //        renderer.material = highlightMaterial;
        //    }
        //}

        GetComponent<Collider2D>().enabled = false;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _healthDisplay.gameObject.SetActive(true);
        Vector3 parentScale = transform.localScale;
        float safeX = parentScale.x != 0 ? parentScale.x : 1f;
        float safeY = parentScale.y != 0 ? parentScale.y : 1f;
        float safeZ = parentScale.z != 0 ? parentScale.z : 1f;
        _healthDisplay.transform.localScale = new Vector3(
            0.05f / safeX,
            0.05f / safeY,
            0.05f / safeZ
        );
        _healthDisplay.gameObject.SetActive(false);
        _healthDisplay.gameObject.SetActive(true);
        _healthDisplay.gameObject.SetActive(GameManagement.isShowHp);

        InitializeIronDoor();

        zombieForestSlider = GameManagement.instance.zombieForestSlider;

        InitializeGameAnimation();

        Health = ZombieStructManager.GetZombieStructById(zombieID).BaseHP;
        level1ArmorHealth = ZombieStructManager.GetZombieStructById(zombieID).Armor1HP;
        level2ArmorHealth = ZombieStructManager.GetZombieStructById(zombieID).Armor2HP;
        level1ArmorMaxHealth = level1ArmorHealth;
        level2ArmorMaxHealth = level2ArmorHealth;

        AttackPower = 50;

        switch (BaseZombieType)
        {
            case 1: loadCone(1); break;
            case 2: loadBucket(1); break;
            default: break;
        }
        if(IsIronDoorZombie)
        {
            List<GameObject> combinedList = new List<GameObject>();
            AddUnique(_ironDoorZombieBiteDisplay, combinedList);
            AddUnique(_ironDoorZombieWalkDisplay, combinedList);
            AddUnique(_ironDoorZombieWalkWithoutDoorDisplay, combinedList);
            AddUnique(_ironDoors, combinedList);
            AddUnique(_ironDoorArmBrokenForceHide, combinedList);
            AddUnique(_ironDoorArmBrokenForceShow, combinedList);
            _ironDoorAll = combinedList.ToArray();
            IronDoorWalkDisplayLogic();

        }


        if(GameManagement.levelData.LevelType == levelType.TheDreamOfPotatoMine)
        {
            _levelSpecialZone *= 1.5f * GameManagement.GameDifficult;
        }
        //添加随机速度增幅

        LoadAnimationSpeed();


        activate();


        MaxHealth = Health;

        InvokeRepeating("CalculatePoisonDamage", 0, 2f);

        LoadHealthText();
    }


    protected virtual void activate()
    {
        Invoke("doAfterStartSomeTimes", Random.Range(15, 80));
        gameObject.SetActive(true);
    }

    protected virtual void doAfterStartSomeTimes()//用于森林僵尸10 - 30秒后生成草丛并杀死自己
    {
        
    }

    private void OnEnable()
    {
        if (gameObject != null)
        {
            ZombieManagement.allZombies.Add(gameObject.GetComponent<Zombie>());
        }
    }
    private void OnDisable()
    {
        try
        {
            if (ZombieManagement.instance != null && ZombieManagement.instance.isActiveAndEnabled && this != null && gameObject != null && !GameManagement.instance.IsGameing) 
            {
                ZombieManagement.instance.minusZombieNumAll(gameObject);
            }

            if (this != null)
            {
                var zombieComp = GetComponent<Zombie>();
                if (zombieComp != null)
                {
                    ZombieManagement.allZombies.Remove(zombieComp);
                }
            }
        }
        catch
        {

        }
        
    }



    #endregion

    protected virtual void Update()
    {
        if(!isEntrance && transform.position.x <= 5.3f)
        {
            isEntrance = true;
            GetComponent<Collider2D>().enabled = true;
        }
    }

    #region 啃咬与攻击
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(CanBite && !dying)
        {
            if (!debuff.Charmed
            && collision.tag == "Plant"
            && myAnimator.GetBool("Attack") == false
            )
            {
                attackPlant = collision.GetComponent<Plant>();
                attackZombie = collision.GetComponent<Zombie>();
                if(attackPlant != null && attackPlant.row == pos_row && attackPlant._plantType != PlantType.GroundHuggingPlants && attackPlant.CanSubjectAttack)
                {

                }
                else if(attackZombie != null && attackZombie.pos_row == pos_row && attackZombie.debuff.Charmed != debuff.Charmed)
                {

                }
                else
                {
                    return;
                }

                CurrentBiteTarget = collision.gameObject;
                myAnimator.SetBool("Walk", false);
                myAnimator.SetBool("Attack", true);
                isEating = true;

                if (IsIronDoorZombie)
                {
                    IronDoorBiteDisplayLogic();
                }
            }
            else if (collision.tag == "Zombie"
            && collision.GetComponent<Zombie>().pos_row == pos_row
            && collision.GetComponent<Zombie>().debuff.Charmed != debuff.Charmed
            && myAnimator.GetBool("Attack") == false
            )
            {
                CurrentBiteTarget = collision.gameObject;
                myAnimator.SetBool("Walk", false);
                myAnimator.SetBool("Attack", true);
                isEating = true;
                attackZombie = collision.GetComponent<Zombie>();

                if (IsIronDoorZombie)
                {
                    IronDoorBiteDisplayLogic();
                }

            }
        }

        if (!debuff.Charmed && collision.tag == "GameOverLine" && !dying && alive)
        {
            GameManagement.instance.GetComponent<GameManagement>().gameOver();
        }
        else if(collision.tag == "GameOverLine")
        {
            Destroy(gameObject);
            ZombieManagement.instance.minusZombieNumAll(gameObject);
        }

        if (collision.tag == "ZombieDisappearLine")
        {

            if (ZombieManagement.instance != null && gameObject != null)
            {
                ZombieManagement.instance.minusZombieNumAll(gameObject);
                Destroy(gameObject);
            }
        }

    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == CurrentBiteTarget)
        {
            myAnimator.SetBool("Attack", false);
            myAnimator.SetBool("Walk", true);
            isEating = false;
            CurrentBiteTarget = null;
            attackPlant = null;
            attackZombie = null;

            GetComponent<Collider2D>().enabled = false;
            GetComponent<Collider2D>().enabled = true;

            if (IsIronDoorZombie)
            {
                if (!level2ArmorIsDrop)
                {
                    IronDoorWalkDisplayLogic();
                }
                else
                {
                    IronDoorWalkWithoutDoorDisplayLogic();
                }
            }
        }
        //else if (collision.tag == "界外死亡")
        //{
        //    die();
        //}
    }

    //protected virtual void OnTriggerStay2D(Collider2D collision)
    //{
    //    ////if (可以啃咬 &&
    //    ////    !dying &&
    //    ////    !isEating && collision.tag == "Plant" && collision.GetComponent<Plant>().row == pos_row && collision.GetComponent<Plant>().plantType == 0 && collision.GetComponent<Plant>().canBeAttacked)
    //    ////{
    //    ////    myAnimator.SetBool("Walk", false);
    //    ////    myAnimator.SetBool("Attack", true);
    //    ////    isEating = true;
    //    ////    plant = collision.GetComponent<Plant>();
    //    ////    正在啃咬目标 = collision.gameObject;
    //    ////    啃咬目标方法 = 正在啃咬目标.GetComponent<Plant>();
    //    ////    if (isDoorZombie)
    //    ////    {
    //    ////        铁门啃咬显示逻辑();
    //    ////    }
    //    ////}
    //    //if (isEating && plant.row != pos_row)
    //    //{
    //    //    myAnimator.SetBool("Attack", false);
    //    //    myAnimator.SetBool("Walk", true);
    //    //    isEating = false;
    //    //    正在啃咬目标 = null;
    //    //    plant = null;
    //    //    if (isDoorZombie)
    //    //    {
    //    //        if (!level2ArmorIsDrop)
    //    //        {
    //    //            铁门行走显示逻辑();
    //    //        }
    //    //        else
    //    //        {
    //    //            铁门无门行走显示逻辑();
    //    //        }
    //    //    }
    //    //}
    //}
    public virtual void attack()
    {
        if (attackPlant != null && !dying)
        {
            attackPlant.beAttacked(AttackPower, "beEated", gameObject);
        }
        else if(attackZombie != null && !dying)
        {
            attackZombie.beAttacked(AttackPower, 1, -1);
        }
    }
    #endregion

    #region 受击
    public virtual void beAttacked(int hurt, int BulletType ,int AttackedMusicType)//AttackedMusicType为2时是地刺攻击，仅播放本体受伤音效，为-1时不播放音效
    {
        if(AttackedMusicType != -1)
        {
            if (AttackedMusicType == 1)
            {
                playAudioOfBeingAttacked(BulletType);
            }
            else if (AttackedMusicType == 2)
            {
                playAudioOfBodyBeingAttacked();
            }
        }
        

        TriggerHighlight(BulletType);

        if (BulletType == 0) // 本体伤害，扣除本体血量
        {
            HandleBodyDamage(hurt); // 调用本体受伤的处理方法
        }
        else if (BulletType == 1 || BulletType==3) // 一类伤害
        {
            if (level2ArmorHealth > 0) // 如果二类防具有血量
            {
                // 二类防具先扣血
                int level2Damage = Mathf.Min(hurt, level2ArmorHealth);
                level2ArmorHealth -= level2Damage;
                hurt -= level2Damage; // 剩余伤害继续处理
                HandleLevel2ArmorDamage(level2Damage); // 调用二类防具受伤处理

                if (hurt > 0) // 如果仍有伤害未被抵消
                {
                    // 一类防具扣血
                    int level1Damage = Mathf.Min(hurt, level1ArmorHealth);
                    level1ArmorHealth -= level1Damage;
                    hurt -= level1Damage; // 剩余伤害继续处理
                    HandleLevel1ArmorDamage(level1Damage); // 调用一类防具受伤处理

                    if (hurt > 0) // 如果仍有伤害未被抵消
                    {
                        HandleBodyDamage(hurt); // 继续扣除本体血量
                    }
                }
            }
            else // 二类防具没有血量，直接扣一类防具和本体
            {
                // 一类防具先扣血
                int level1Damage = Mathf.Min(hurt, level1ArmorHealth);
                level1ArmorHealth -= level1Damage;
                hurt -= level1Damage; // 剩余伤害继续处理
                HandleLevel1ArmorDamage(level1Damage); // 调用一类防具受伤处理

                if (hurt > 0) // 如果仍有伤害未被抵消
                {
                    HandleBodyDamage(hurt); // 继续扣除本体血量
                }
            }
        }
        else if (BulletType == 2) // 二类伤害
        {
            if (level1ArmorHealth > 0) // 如果一类防具有血量
            {
                // 先扣除一类防具的血量
                int level1Damage = Mathf.Min(hurt, level1ArmorHealth);
                level1ArmorHealth -= level1Damage;
                hurt -= level1Damage; // 剩余伤害继续处理
                HandleLevel1ArmorDamage(level1Damage); // 调用一类防具受伤处理
            }

            if (hurt > 0) // 如果一类防具已经没有血量，继续扣除本体血量
            {
                HandleBodyDamage(hurt); // 继续扣除本体血量
            }
        }
        loadArmorStatus();
    }

    // 一类防具受到伤害时调用的处理方法
    protected virtual void HandleLevel1ArmorDamage(int hurt)
    {
        // 这里可以添加一类防具受伤后的逻辑，例如播放一类防具的损坏动画、音效等
        //Debug.Log($"一类防具受到 {hurt} 点伤害");
    }

    // 二类防具受到伤害时调用的处理方法
    protected virtual void HandleLevel2ArmorDamage(int hurt)
    {
        // 这里可以添加二类防具受伤后的逻辑，例如播放二类防具的损坏动画、音效等
        //Debug.Log($"二类防具受到 {hurt} 点伤害");
    }

    // 本体受到伤害时调用的处理方法
    protected virtual void HandleBodyDamage(int hurt)
    {
        // 这里可以处理本体受伤后的逻辑，例如扣除血量、播放受伤音效、动画等
        Health -= hurt;
        //Debug.Log($"本体受到 {hurt} 点伤害，剩余血量: {bloodVolume}");
        // 可以在这里添加本体受伤后的其他逻辑，例如播放受伤动画、音效等
    }
    //被灼伤，被火焰攻击时调用
    public virtual void beBurned(int damage)
    {
        beAttacked(damage, 1, 1);
        SwitchFrozenState(false);
    }
    public virtual void beSquashed()
    {
        if (buff.Stealth) return;
        beAttacked(1800, 1, -1);
        if (Health <= 0 && !alive)
        {
            ZombieManagement.instance.minusZombieNumAll(gameObject);
            Destroy(gameObject);
        }
    }

    public virtual void beChompered()
    {
        ZombieManagement.instance.minusZombieNumAll(gameObject);
        Destroy(gameObject);
    }

    /// <summary>
    /// 计算坚韧 - 计算并应用坚韧效果
    /// </summary>
    public void CalculateToughness(){
        level1ArmorHealth += buff.Toughness * 50;
    }

    public void beAshAttacked()
    {
        if(Health <= 1800)
        {
            Vector3 offset = new Vector3(0.137f, -0.088f, 0f); // z轴保持不变
            GameObject charredZombie = Instantiate(
                Resources.Load<GameObject>("Prefabs/Zombies/Zombie_Other/Zombie_charred"),
                gameObject.transform.position + offset,
                Quaternion.identity
            );
            ZombieManagement.instance.minusZombieNumAll(gameObject);
            Destroy(gameObject);
            charredZombie.GetComponent<SortingGroup>().sortingLayerName = GetComponent<SortingGroup>().sortingLayerName;
        }
        else
        {
            beAttacked(1800, 1, -1);
        }
    }

    #endregion

    #region 音效播放
    // 播放被攻击音效
    public virtual void playAudioOfBeingAttacked(int bulletType)
    {
        if (IsIronDoorZombie && !level2ArmorIsDrop && bulletType == 1)
        {
            // 使用 AudioManager 播放音效
            AudioManager.Instance.PlaySoundEffect(11); // 对应 shieldhit 音效
        }
        else
        {
            if (!level1ArmorIsDrop)
            {
                switch (BaseZombieType)
                {
                    case 0:
                        // 使用 AudioManager 播放不同的音效
                        AudioManager.Instance.PlaySoundEffect(audioIndex == 1 ? 2 : 3); // 对应 bodyhit1 或 bodyhit2
                        break;
                    case 1:
                        AudioManager.Instance.PlaySoundEffect(audioIndex == 1 ? 9 : 10); // 对应 conehit1 或 conehit2
                        break;
                    case 2:
                        AudioManager.Instance.PlaySoundEffect(audioIndex == 1 ? 11 : 12); // 对应 shieldhit1 或 shieldhit2
                        break;
                }
            }
            else
            {
                // 使用 AudioManager 播放默认的音效
                AudioManager.Instance.PlaySoundEffect(audioIndex == 1 ? 2 : 3); // 对应 bodyhit1 或 bodyhit2
            }
        }

        // 切换音效索引
        audioIndex = (audioIndex == 1) ? 2 : 1;
    }

    // 播放身体受伤音效
    public virtual void playAudioOfBodyBeingAttacked() // 地刺攻击使用，播放身体受伤
    {
        // 使用 AudioManager 播放音效
        AudioManager.Instance.PlaySoundEffect(audioIndex == 1 ? 2 : 3); // 对应 bodyhit1 或 bodyhit2

        // 切换音效索引
        audioIndex = (audioIndex == 1) ? 2 : 1;
    }

    // 播放僵尸啃咬的音效
    public virtual void PlayEatAudio()
    {
        // 播放随机的啃咬音效 (chomp1 或 chomp2)
        AudioManager.Instance.PlaySoundEffect(Random.Range(13, 15)); // 对应 chomp1 或 chomp2
    }

    public virtual void fallDown()
    {
        AudioManager.Instance.PlaySoundEffect(20);
    }
    #endregion

    #region 中毒、冰冻等状态切换
    private void SwitchMaterialState(int _state)
    {
        switch(_state)
        {
            case 0:
                foreach (Renderer renderer in allRenderers)
                {
                    if (renderer != null && renderer.gameObject.name != "Shadow")
                    {
                        renderer.material.SetColor("_BlendColor", new Color(1.0f, 1.0f, 1.0f, 1f));
                    }
                }
                break;
            case 1:
                foreach (Renderer renderer in allRenderers)
                {
                    if (renderer != null && renderer.gameObject.name != "Shadow")
                    {
                        renderer.material.SetColor("_BlendColor", new Color(0.0f, 0.279f, 1.0f, 1f));
                    }
                }
                break;
            case 2:
                foreach (Renderer renderer in allRenderers)
                {
                    if (renderer != null && renderer.gameObject.name != "Shadow")
                    {
                        renderer.material.SetColor("_BlendColor", new Color(0.0f, 0.279f, 1.0f, 1f));
                    }
                }
                break;
            case 3:
                foreach (Renderer renderer in allRenderers)
                {
                    if (renderer != null && renderer.gameObject.name != "Shadow")
                    {
                        renderer.material.SetColor("_BlendColor", new Color(0.0f, 0.647f, 0.165f, 1f));
                    }
                }
                break;
            case 4:
                foreach (Renderer renderer in allRenderers)
                {
                    if (renderer != null && renderer.gameObject.name != "Shadow")
                    {
                        renderer.material.SetColor("_BlendColor", new Color(1.0f, 0.0f, 0.787f, 1f));
                    }
                }
                break;
            case 5:
                foreach (Renderer renderer in allRenderers)
                {
                    if (renderer != null && renderer.gameObject.name != "Shadow")
                    {
                        renderer.material.SetColor("_BlendColor", new Color(0.729f, 0.053f, 0.0f, 1f));
                    }
                }
                break;
        }
    }

    /// <summary>
    /// 切换冰冻状态 - 切换僵尸的冰冻状态
    /// </summary>
    /// <param name="start">开始 - true为开始冰冻，false为解除冰冻</param>
    private void SwitchFrozenState(bool start)
    {
        if (_isFrozen == start) return; // 如果当前状态已经是目标状态，则直接返回
        if(start)
        {
            SwitchPoisonedState(false);
        }
        _isFrozen = start;
        _decelerationSpeedZone = start ? 0.5f : 1f;  // 使用三元运算符来简化赋值
        SwitchMaterialState(start ? 1 : 0);
        LoadAnimationSpeed();
    }

    /// <summary>
    /// 附加减速 - 为僵尸附加减速效果
    /// </summary>
    public virtual void ApplyDeceleration()
    {
        if (_decelerationCoroutine == null)
        {
            debuff.Deceleration = _decelerationTime;
            _decelerationCoroutine = StartCoroutine(DecelerationEffectCoroutine(debuff.Deceleration));
        }
        else
        {
            debuff.Deceleration = _decelerationTime;
            StopCoroutine(_decelerationCoroutine);
            _decelerationCoroutine = StartCoroutine(DecelerationEffectCoroutine(debuff.Deceleration));
        }
    }

    /// <summary>
    /// 减速效果协程 - 减速效果的协程
    /// </summary>
    /// <param name="decelerationTime">减速时间 - 减速持续时间</param>
    private IEnumerator DecelerationEffectCoroutine(float decelerationTime)
    {
        SwitchFrozenState(true);
        float elapsedTime = 0f;
        while (elapsedTime < decelerationTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        RemoveDecelerationState();
    }

    /// <summary>
    /// 解除减速状态 - 解除僵尸的减速状态
    /// </summary>
    protected void RemoveDecelerationState()
    {
        SwitchFrozenState(false);
    }

    /// <summary>
    /// 切换中毒状态 - 切换僵尸的中毒状态
    /// </summary>
    /// <param name="start">开始 - true为开始中毒，false为解除中毒</param>
    private void SwitchPoisonedState(bool start)
    {
        if (_isPoisoned == start) return;
        if(start)
        {
            SwitchFrozenState(false);
            SwitchFuriousState(false);
        }
        if(!start)
        {
            debuff.Poison = 0;
        }
        _isPoisoned = start;


        SwitchMaterialState(start ? 3 : 0);

        LoadAnimationSpeed();

        LoadHealthText();
    }

    /// <summary>
    /// 附加中毒 - 为僵尸附加中毒效果
    /// </summary>
    /// <param name="addPoisonLayers">附加中毒层数 - 要添加的中毒层数</param>
    public void ApplyPoison(int addPoisonLayers)
    {
        debuff.Poison += addPoisonLayers;

        SwitchFuriousState(false);



        for(int i = 0;i < addPoisonLayers;i++)
        {
            Invoke("DecreasePoisonLayer", 6f);
        }
        if(debuff.Poison > 60)
        {
            SetAchievement.SetAchievementCompleted("我不是毒神");
        }
        if (debuff.Poison > 0 && !_isPoisoned)
        {
            SwitchPoisonedState(true);
            _isPoisoned = true;
        }

        LoadHealthText();
    }
    /// <summary>
    /// 引爆毒伤 - 引爆中毒伤害
    /// </summary>
    /// <param name="detonationCount">引爆次数 - 引爆的次数</param>
    public void DetonatePoisonDamage(int detonationCount)
    {
        if (debuff.Poison > 0 && detonationCount > 0)
        {
            beAttacked(debuff.Poison * MaxHealth / 100 * detonationCount, 3, -1);
        }
        else
        {
            return;
        }
    }

    /// <summary>
    /// DecreasePoisonLayer - 减少中毒层数
    /// </summary>
    public void DecreasePoisonLayer()
    {
        debuff.Poison -= 1;
        if (debuff.Poison < 0)
            debuff.Poison = 0;

        if (debuff.Poison == 0 && _isPoisoned)
        {
            SwitchPoisonedState(false);
        }

        LoadHealthText();
    }

    /// <summary>
    /// DecreasePoisonLayer - 减少指定层数的中毒
    /// </summary>
    /// <param name="decreaseLayers">减少的层数 - 要减少的中毒层数</param>
    public void DecreasePoisonLayer(int decreaseLayers)
    {
        debuff.Poison -= decreaseLayers;
        if (debuff.Poison < 0)
            debuff.Poison = 0;

        if (debuff.Poison == 0 && _isPoisoned)
        {
            SwitchPoisonedState(false);
        }

        LoadHealthText();
    }

    /// <summary>
    /// 计算毒伤 - 计算并应用中毒伤害
    /// </summary>
    public void CalculatePoisonDamage()
    {
        if(debuff.Poison > 0)
        {
            beAttacked(debuff.Poison * 3, 1,-1);
        }
        else
        {
            return;
        }
    }

    /// <summary>
    /// 切换狂暴状态 - 切换僵尸的狂暴状态
    /// </summary>
    /// <param name="on">开 - true为开启狂暴，false为关闭狂暴</param>
    public void SwitchFuriousState(bool on)
    {

        if(debuff.Furious == on)
        {
            return;
        }
        else
        {
            if(on)
            {
                if (on)
                {
                    SwitchFrozenState(false);
                    SwitchPoisonedState(false);
                }
                SwitchMaterialState(5);
                debuff.Furious = true;
                _furiousSpeedZone = 3f;
            }
            else
            {
                SwitchMaterialState(0);
                debuff.Furious = false;
                _furiousSpeedZone = 1f;
            }

        }
        LoadAnimationSpeed();

    }

    /// <summary>
    /// 切换魅惑状态 - 切换僵尸的魅惑状态（魅惑只能进入，无法退出）
    /// </summary>
    public void SwitchCharmedState()
    {
        if (debuff.Charmed)
            return;
        debuff.Charmed = true;
        transform.Rotate(0f, 180f, 0f);
        _healthDisplay.transform.Rotate(0f, 180f, 0f);
        SwitchPoisonedState(false);
        SwitchFrozenState(false);
        SwitchFuriousState(false);
        SwitchMaterialState(4);
        ZombieManagement.instance.minusZombieNumAll(gameObject);
        gameObject.tag = "Plant";
        //myAnimator.SetBool("Attack", false);
        //myAnimator.SetBool("Walk", true);
        //isEating = false;
        //正在啃咬目标 = null;
        //attackPlant = null;
        //if (铁门类僵尸)
        //{
        //    if (!level2ArmorIsDrop)
        //    {
        //        铁门行走显示逻辑();
        //    }
        //    else
        //    {
        //        铁门无门行走显示逻辑();
        //    }
        //}
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Collider2D>().enabled = true;
    }

    /// <summary>
    /// 展示动画初始化 - 初始化展示模式的动画
    /// </summary>
    public void InitializeDisplayAnimation()
    {
        if (newZombie)
        {
            string[] walkAnimations = { "anim_idle", "anim_idle2" };
            string randomAnim = walkAnimations[Random.Range(0, walkAnimations.Length)];

            if (HasAnimation(randomAnim))
            {
                myAnimator.Play(randomAnim);
            }
            else
            {
                Debug.LogWarning($"动画 '{randomAnim}' 不存在，无法播放");
            }
        }
    }

    /// <summary>
    /// 游戏动画初始化 - 初始化游戏模式的动画
    /// </summary>
    public void InitializeGameAnimation()
    {
        if (!newZombie)
        {
            if (myAnimator.HasParameter("Walk"))
                myAnimator.SetBool("Walk", true);

            if (myAnimator.HasParameter("Idle"))
                myAnimator.SetBool("Idle", false);
        }
        else
        {
            string[] walkAnimations = { "anim_walk", "anim_walk2" };
            string randomAnim = walkAnimations[Random.Range(0, walkAnimations.Length)];

            if (HasAnimation(randomAnim))
            {
                myAnimator.Play(randomAnim);
            }
            else
            {
                Debug.LogWarning($"动画 '{randomAnim}' 不存在，无法播放");
            }
        }
    }

    private bool HasAnimation(string animationName)
    {
        if (myAnimator == null || myAnimator.runtimeAnimatorController == null)
            return false;

        foreach (var clip in myAnimator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == animationName)
                return true;
        }

        return false;
    }



    /// <summary>
    /// 加载动画速度 - 根据各种速度倍率加载动画速度
    /// </summary>
    private void LoadAnimationSpeed()
    {
        if(myAnimator != null)
        {
            myAnimator.speed = _furiousSpeedZone * _decelerationSpeedZone * _randomSpeedZone * _levelSpecialZone * EnvironmentSpeedZone;
        }
    }

    #endregion

    #region 加载不同状态（铁门独立在铁门代码中）
    protected virtual void hideHead()
    {
        if(!dying)
        {
            dying = true;

            StartCoroutine(DyingHealthDeduction());

            AudioManager.Instance.PlaySoundEffect(59);
            if(!newZombie)
            {
                Transform createPosition = FindInChildren(transform, "Zombie_head");
                Transform hidePosition = FindInChildren(transform, "Zombie_jaw");
                Transform showPosition = FindInChildren(transform, "Zombie_neck_0");
                if (createPosition != null)
                {
                    createPosition.gameObject.SetActive(false);
                }
                if (hidePosition != null)
                {
                    SpriteRenderer shouldBeHide2 = hidePosition.GetComponent<SpriteRenderer>();
                    shouldBeHide2.enabled = false;
                }
                if (showPosition != null)
                {
                    showPosition.gameObject.SetActive(true);
                }
                if (!dontHaveDropHead && !GameManagement.isPerformance)
                {
                    GameObject gameObject = Instantiate(zombieHeadDrops, createPosition.position, Quaternion.identity);

                    gameObject.GetComponent<ParticleSystemRenderer>().sortingLayerName = GetComponent<SortingGroup>().sortingLayerName;
                }
            }
            else
            {
                Transform createPosition = FindInChildren(transform, "ZOMBIE_HEAD");
                Transform hidePosition = FindInChildren(transform, "ZOMBIE_JAW");
                Transform hidePosition2 = FindInChildren(transform, "ZOMBIE_HAIR");
                createPosition.gameObject.SetActive(false);
                hidePosition.gameObject.SetActive(false);
                hidePosition2.gameObject.SetActive(false);

                if (!dontHaveDropHead && !GameManagement.isPerformance)
                {
                    GameObject go = DynamicObjectPoolManager.Instance.GetFromPool(PoolType.ZombieNormalHeadDrop);

                    if(fullHead != null)
                    {
                        go.GetComponent<ParticleSystem>().textureSheetAnimation.RemoveSprite(0);
                        go.GetComponent<ParticleSystem>().textureSheetAnimation.AddSprite(fullHead);
                    }

                    go.GetComponent<ParticleSystemRenderer>().sortingLayerName = GetComponent<SortingGroup>().sortingLayerName;


                    go.transform.position = createPosition.transform.position;
                    go.transform.rotation = Quaternion.identity;

                }
            }
            
            
        }
    }
    protected Transform FindInChildren(Transform parent,string beFind)
    {
        foreach (Transform child in parent.GetComponentsInChildren<Transform>(true)) // 第二个参数设置为true，查找所有子物体（包括未激活的）
        {
            if (child.name == beFind)
            {
                return child;
            }
        }
        return null; // 没有找到名为子物体
    }
    public void loadCone(int loadType)//根据不同伤害加载路障
    {
        if(!newZombie)
        {
            // 查找名为 "Cone" 的所有子物体（包括未激活的）
            Transform coneTransform = FindInChildren(transform, "Cone");

            if (coneTransform != null)
            {
                // 获取该子物体的SpriteRenderer组件
                SpriteRenderer coneSpriteRenderer = coneTransform.GetComponent<SpriteRenderer>();

                if (coneSpriteRenderer != null)
                {
                    switch (loadType)
                    {
                        case 1: coneTransform.gameObject.SetActive(true); coneSpriteRenderer.sprite = coneBroken1; break;
                        case 2: coneSpriteRenderer.sprite = coneBroken2; break;
                        case 3: coneSpriteRenderer.sprite = coneBroken3; break;
                        case 0:
                            if (!GameManagement.isPerformance)
                            {
                                GameObject shatterEffect = Instantiate(coneDrop, coneTransform.position, Quaternion.identity);
                                shatterEffect.GetComponent<ParticleSystemRenderer>().sortingLayerName = GetComponent<SortingGroup>().sortingLayerName;
                            }

                            coneSpriteRenderer.enabled = false;
                            coneTransform.gameObject.SetActive(false);

                            break;
                        default: break;
                    }

                }
            }
        }
        else
        {
            // 查找名为 "Cone" 的所有子物体（包括未激活的）
            Transform coneTransform = FindInChildren(transform, "ZOMBIE_CONE1");

            if (coneTransform != null)
            {
                // 获取该子物体的SpriteRenderer组件
                SpriteRenderer coneSpriteRenderer = coneTransform.GetComponent<SpriteRenderer>();

                if (coneSpriteRenderer != null)
                {
                    switch (loadType)
                    {
                        case 1: coneTransform.gameObject.SetActive(true); coneSpriteRenderer.sprite = coneBroken1; break;
                        case 2: coneSpriteRenderer.sprite = coneBroken2; break;
                        case 3: coneSpriteRenderer.sprite = coneBroken3; break;
                        case 0:
                            if (!GameManagement.isPerformance)
                            {
                                GameObject go = DynamicObjectPoolManager.Instance.GetFromPool(PoolType.ZombiePendantDrop);
                                go.GetComponent<ParticleSystem>().textureSheetAnimation.RemoveSprite(0);
                                go.GetComponent<ParticleSystem>().textureSheetAnimation.AddSprite(coneSpriteRenderer.sprite);
                                go.transform.position = coneTransform.position;
                                go.transform.rotation = Quaternion.identity;

                                go.GetComponent<ParticleSystemRenderer>().sortingLayerName = GetComponent<SortingGroup>().sortingLayerName;
                            }

                            coneTransform.gameObject.SetActive(false);

                            break;
                        default: break;
                    }

                }
            }
        }
        
    }
    public virtual void loadBucket(int loadType)//根据不同伤害加铁桶
    {
        if(!newZombie)
        {
            // 查找名为 "Cone" 的所有子物体（包括未激活的）
            Transform bucketTransform = FindInChildren(transform, "Bucket");

            if (bucketTransform != null)
            {
                // 获取该子物体的SpriteRenderer组件
                SpriteRenderer bucketSpriteRenderer = bucketTransform.GetComponent<SpriteRenderer>();

                if (bucketSpriteRenderer != null)
                {
                    switch (loadType)
                    {
                        case 1: bucketTransform.gameObject.SetActive(true); bucketSpriteRenderer.sprite = bucketBroken1; break;
                        case 2: bucketSpriteRenderer.sprite = bucketBroken2; break;
                        case 3: bucketSpriteRenderer.sprite = bucketBroken3; break;
                        case 0:
                            if (!GameManagement.isPerformance)
                            {
                                GameObject shatterEffect = Instantiate(bucketDrop, bucketTransform.position, Quaternion.identity);
                                shatterEffect.GetComponent<ParticleSystemRenderer>().sortingLayerName = GetComponent<SortingGroup>().sortingLayerName;
                            }


                            bucketTransform.gameObject.SetActive(false);
                            bucketSpriteRenderer.enabled = false;
                            break;
                        default: break;
                    }

                }
            }
        }
        else
        {
            // 查找名为 "Cone" 的所有子物体（包括未激活的）
            Transform bucketTransform = FindInChildren(transform, "ZOMBIE_BUCKET1");

            if (bucketTransform != null)
            {
                // 获取该子物体的SpriteRenderer组件
                SpriteRenderer bucketSpriteRenderer = bucketTransform.GetComponent<SpriteRenderer>();

                if (bucketSpriteRenderer != null)
                {
                    switch (loadType)
                    {
                        case 1: bucketTransform.gameObject.SetActive(true); bucketSpriteRenderer.sprite = bucketBroken1; break;
                        case 2: bucketSpriteRenderer.sprite = bucketBroken2; break;
                        case 3: bucketSpriteRenderer.sprite = bucketBroken3; break;
                        case 0:
                            if (!GameManagement.isPerformance)
                            {
                                GameObject go = DynamicObjectPoolManager.Instance.GetFromPool(PoolType.ZombiePendantDrop);
                                go.GetComponent<ParticleSystem>().textureSheetAnimation.RemoveSprite(0);
                                go.GetComponent<ParticleSystem>().textureSheetAnimation.AddSprite(bucketSpriteRenderer.sprite);
                                go.transform.position = bucketTransform.position;
                                go.transform.rotation = Quaternion.identity;
                                go.GetComponent<ParticleSystemRenderer>().sortingLayerName = GetComponent<SortingGroup>().sortingLayerName;
                            }
                            bucketTransform.gameObject.SetActive(false);

                            break;
                        default: break;
                    }

                }
            }
        }

        
    }

    protected virtual void dropArm()//手臂掉落
    {
        if (!armIsDrop)
        {
            armIsDrop = true;
            AudioManager.Instance.PlaySoundEffect(59);
            if (!newZombie)
            {
                // 查找名为 "Cone" 的所有子物体（包括未激活的）
                Transform createPosition = FindInChildren(transform, "CreateDropArmPosition");
                Transform shouldBeHide1 = FindInChildren(transform, "Zombie_outerarm_lower");
                Transform shouldBeHide2 = FindInChildren(transform, "Zombie_outerarm_hand");

                Transform shouldBeExchange = FindInChildren(transform, "Zombie_outerarm_upper");
                if (shouldBeExchange != null)
                {

                    shouldBeExchange.GetComponent<SpriteRenderer>().sprite = brokenArm;
                    shouldBeHide1.GetComponent<SpriteRenderer>().enabled = false;
                    shouldBeHide2.GetComponent<SpriteRenderer>().enabled = false;
                    if (!GameManagement.isPerformance)
                    {
                        GameObject gameObject = Instantiate(zombieArmDrops, createPosition.position, Quaternion.identity);
                        gameObject.GetComponent<ParticleSystemRenderer>().sortingLayerName = GetComponent<SortingGroup>().sortingLayerName;
                    }
                }
            }
            else
            {
                Transform shouldBeHide1 = FindInChildren(transform, "ZOMBIE_OUTERARM_LOWER");
                Transform shouldBeHide2 = FindInChildren(transform, "ZOMBIE_OUTERARM_HAND");
                Transform shouldBeHide3 = FindInChildren(transform, "ZOMBIE_OUTERARM_HAND2");
                Transform shouldBeExchange = FindInChildren(transform, "ZOMBIE_OUTERARM_UPPER");
                if (shouldBeExchange != null)
                {

                    shouldBeExchange.GetComponent<SpriteRenderer>().sprite = brokenArm;
                    shouldBeHide1.gameObject.SetActive(false);
                    shouldBeHide2.gameObject.SetActive(false);
                    shouldBeHide3.gameObject.SetActive(false);
                    shouldBeHide1.GetComponent<SpriteRenderer>().enabled = false;
                    shouldBeHide2.GetComponent <SpriteRenderer>().enabled = false;
                    shouldBeHide3.GetComponent<SpriteRenderer>().enabled = false;
                    if (!dontHaveDropHead && !GameManagement.isPerformance)
                    {
                        GameObject go = DynamicObjectPoolManager.Instance.GetFromPool(PoolType.ZombieNormalArmDrop);
                        go.GetComponent<ParticleSystemRenderer>().sortingLayerName = GetComponent<SortingGroup>().sortingLayerName;

                        go.transform.position = shouldBeHide1.transform.position;
                        go.transform.rotation = Quaternion.identity;
                    }
                }
            }
        }

    }
    virtual public void loadArmorStatus()
    {
        switch (BaseZombieType)
        {
            case 1:
                if (!level1ArmorIsDrop)
                {
                    if (!Level1ArmorHalfDamagedSwitched)
                    {

                        if (level1ArmorHealth <= level1ArmorMaxHealth / 3 * 2)
                        {
                            Level1ArmorHalfDamagedSwitched = true;
                            loadCone(2);
                        }

                    }
                    if (!Level1ArmorFullyDamagedSwitched)
                    {

                        if (level1ArmorHealth <= level1ArmorMaxHealth / 3)
                        {
                            Level1ArmorFullyDamagedSwitched = true;
                            loadCone(3);
                        }

                    }
                    if (level1ArmorHealth <= 0)
                    {
                        level1ArmorIsDrop = true;
                        loadCone(0);
                    }


                }
                break;
            case 2:
                if (!level1ArmorIsDrop)
                {
                    if (!Level1ArmorHalfDamagedSwitched)
                    {

                        if (level1ArmorHealth <= level1ArmorMaxHealth / 3 * 2)
                        {
                            Level1ArmorHalfDamagedSwitched = true;
                            loadBucket(2);
                        }

                    }
                    if (!Level1ArmorFullyDamagedSwitched)
                    {

                        if (level1ArmorHealth <= level1ArmorMaxHealth / 3)
                        {
                            Level1ArmorFullyDamagedSwitched = true;
                            loadBucket(3);
                        }

                    }

                    if (level1ArmorHealth <= 0)
                    {
                        level1ArmorIsDrop = true;
                        loadBucket(0);
                    }
                }
                break;
            default: break;
        }
        if (IsIronDoorZombie)
        {
            loadDoorStatus();
        }

        if (Health <= MaxHealth / 2)//断手
        {
            dropArm();
        }
        if (Health <= MaxHealth / 3)//断头  
        {
            //隐藏头
            hideHead();
        }
        if (Health <= 0 && alive == true)
        {
            die();
        }
        LoadHealthText();


    }

    public virtual void die()
    {
        if (!alive) return;

        alive = false;

        SwitchPoisonedState(false);
        SwitchFrozenState(false);
        SwitchFuriousState(false);

        // ? 动画控制器安全检查
        if (myAnimator != null)
        {
            if (HasParameter(myAnimator, "Walk"))
                myAnimator.SetBool("Walk", false);
            else
                Debug.LogWarning("Animator 缺少参数: Walk");

            if (HasParameter(myAnimator, "Die"))
                myAnimator.SetBool("Die", true);
            else
                Debug.LogWarning("Animator 缺少参数: Die");
        }
        else
        {
            Debug.LogError("myAnimator 未被赋值！");
        }

        // ? 血量处理
        level1ArmorHealth = 0;
        level2ArmorHealth = 0;
        Health = 0;
        loadArmorStatus();

        // ? 失效碰撞体前确认存在
        Collider2D col = gameObject.GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;
        else
            Debug.LogWarning("未找到 Collider2D 组件");

        // ? 通知 ZombieManagement（带空引用检查）
        if (GameManagement.instance.zombieManagement != null)    
        {
            ZombieManagement zm = GameManagement.instance.zombieManagement.GetComponent<ZombieManagement>();
            if (zm != null)
                zm.minusZombieNumAll(gameObject);
            else
                Debug.LogWarning("zombieManagement 对象上没有 ZombieManagement 组件");
        }
        else
        {
            Debug.LogWarning("GameManagement.zombieManagement 为 null");
        }

        
    }

    // ?? 工具方法：检查 Animator 是否包含某参数
    private bool HasParameter(Animator anim, string paramName)
    {
        foreach (AnimatorControllerParameter param in anim.parameters)
        {
            if (param.name == paramName) return true;
        }
        return false;
    }


    public virtual void LawnMowerDie()
    {
        Health = 0;
        level1ArmorHealth = 0;
        level2ArmorHealth = 0;
        loadArmorStatus();
        Destroy(gameObject);
        alive = false;
        dying = true;
        //全场僵尸数减一
        GameManagement.instance.zombieManagement.GetComponent<ZombieManagement>().minusZombieNumAll(gameObject);
    }


    //public virtual void lawnMowerDie()
    //{
    //    Debug.Log("111");
    //    if (alive)
    //    {
    //        alive = false;

    //        myAnimator.enabled = false;

    //        float animationDuration = 1f; // 60 frames at 60 FPS
    //        float timeElapsed = 0f;

    //        Vector3 initialPosition = transform.position;
    //        Quaternion initialRotation = transform.rotation;
    //        Vector3 initialScale = transform.localScale;

    //        Vector3 positionDelta23Frames = new Vector3(0.2913f, 0f, 0f);
    //        Vector3 positionDelta60Frames = new Vector3(0.41f, -0.3751f, 0f);

    //        Quaternion rotationDelta23Frames = Quaternion.Euler(0f, 0f, -29.536f);
    //        Quaternion rotationDelta60Frames = Quaternion.Euler(0f, 0f, -90f);

    //        Vector3 scaleDelta23Frames = new Vector3(-0.144f, 0f, 0f); // Relative to initial x scale
    //        Vector3 scaleDelta60Frames = new Vector3(-0.29478f, 0f, 0f);

    //        gameObject.GetComponent<Collider2D>().enabled = false;
    //        if (GameManagement.zombieManagement != null)
    //        {
    //            GameManagement.zombieManagement.GetComponent<ZombieManagement>().minusZombieNumAll(gameObject);
    //        }
    //        void UpdateAnimation()
    //        {
    //            timeElapsed += Time.deltaTime;
    //            float normalizedTime = Mathf.Clamp01(timeElapsed / animationDuration);

    //            if (normalizedTime <= 23f / 60f)
    //            {
    //                // Interpolation between initial state and 23 frames
    //                float t = normalizedTime / (23f / 60f);
    //                transform.position = Vector3.Lerp(initialPosition, initialPosition + positionDelta23Frames, t);
    //                transform.rotation = Quaternion.Slerp(initialRotation, initialRotation * rotationDelta23Frames, t);
    //                transform.localScale = Vector3.Lerp(initialScale, initialScale + Vector3.Scale(scaleDelta23Frames, initialScale), t);
    //            }
    //            else
    //            {
    //                // Interpolation between 23 frames and 60 frames
    //                float t = (normalizedTime - 23f / 60f) / (37f / 60f);
    //                transform.position = Vector3.Lerp(initialPosition + positionDelta23Frames, initialPosition + positionDelta60Frames, t);
    //                transform.rotation = Quaternion.Slerp(initialRotation * rotationDelta23Frames, initialRotation * rotationDelta60Frames, t);
    //                transform.localScale = Vector3.Lerp(initialScale + Vector3.Scale(scaleDelta23Frames, initialScale), initialScale + Vector3.Scale(scaleDelta60Frames, initialScale), t);
    //            }
    //        }

    //        StartCoroutine(AnimationCoroutine());

    //        System.Collections.IEnumerator AnimationCoroutine()
    //        {
    //            while (timeElapsed < animationDuration)
    //            {
    //                UpdateAnimation();
    //                yield return null;

    //            }
    //            Destroy(gameObject,1f);

    //        }
    //    }
    //}

    /// <summary>
    /// 濒死扣血 - 濒死状态下持续扣血的协程
    /// </summary>
    protected IEnumerator DyingHealthDeduction()
    {
        float timer = 0f;
        while (true)
        {
            if (alive)
            {
                beAttacked(MaxHealth / 10, 2, -1);
                yield return new WaitForSeconds(1f);
                timer += 1f;

                if (timer >= 10f && alive)
                {
                    die();
                    ZombieManagement.instance.minusZombieNumAll(gameObject);
                    yield break;
                }
            }
            else
            {
                yield break;
            }
        }
    }

    public void fadeDisappear()
    {
        if(hasFaded)
            return;
        hasFaded = true;
        myAnimator.enabled = false;
        StartCoroutine(FadeBlendColor());
    }
    public void disappear()
    {
        Destroy(gameObject);
    }
    #endregion

    #region 设置图层
    public virtual void setPosRow(int pos)
    {
        //设置所在行
        pos_row = pos;

        //设置顺序图层及显示顺序
        SpriteRenderer[] spriteRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>(true);
        GetComponent<SortingGroup>().sortingLayerName = "Zombie-" + pos_row;
        //GetComponent<SortingGroup>().sortingOrder += orderOffset * 20;
        //orderOffset++;
    }

    public virtual void SetSortingOrder(int 排序)
    {
        SpriteRenderer[] spriteRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>(true);
        GetComponent<SortingGroup>().sortingOrder += 排序 * 50;
        
    }
    #endregion

    #region 材质
    public virtual void TriggerHighlight(int bulletType)
    {
        if(!GameManagement.isPerformance)
        {
            if(_highlightCoroutine == null)
            {
                _highlightCoroutine = StartCoroutine(HighlightCoroutine(bulletType));
            }
            else
            {
                StopCoroutine(_highlightCoroutine);
                _highlightCoroutine = StartCoroutine(HighlightCoroutine(bulletType));
            }
            
        }
        
    }

    public virtual IEnumerator HighlightCoroutine(int bulletType)
    {
        float duration = 0.15f; // 高亮和恢复的持续时间
        float startTime = Time.time;

        // 获取当前透明度
        float currentAlpha = 0f;

        // 透明度增加：从当前透明度到最大值 1
        while (Time.time - startTime < duration)
        {
            float lerpValue = Mathf.Lerp(currentAlpha, 0.2f, (Time.time - startTime) / duration); // 从当前透明度到 1
            foreach (Renderer renderer in allRenderers)
            {
                if (renderer != null && renderer.gameObject.name != "Shadow")
                {
                    Material mat = renderer.material;
                    Color currentColor = mat.GetColor("_Color");
                    currentColor.a = lerpValue;  // 更新透明度
                    mat.SetColor("_Color", currentColor);
                }
            }
            yield return null;
        }
        startTime = Time.time;

        // 透明度减少：从 1 到 0
        while (Time.time - startTime < duration)
        {
            float lerpValue = Mathf.Lerp(0.2f, 0f, (Time.time - startTime) / duration); // 从 1 到 0
            foreach (Renderer renderer in allRenderers)
            {
                if (renderer != null && renderer.gameObject.name != "Shadow")
                {
                    Material mat = renderer.material;
                    Color currentColor = mat.GetColor("_Color");
                    currentColor.a = lerpValue;  // 更新透明度
                    mat.SetColor("_Color", currentColor);
                }
            }
            yield return null;
        }

        // 确保透明度最终为 0
        foreach (Renderer renderer in allRenderers)
        {
            if (renderer != null && renderer.gameObject.name != "Shadow")
            {
                Material mat = renderer.material;
                Color currentColor = mat.GetColor("_Color");
                currentColor.a = 0f;  // 最终透明度为 0
                mat.SetColor("_Color", currentColor);
            }
        }
    }

    private IEnumerator FadeBlendColor()//死亡时逐渐消失
    {
        yield return new WaitForSeconds(0.5f);

        float elapsed = 0f;

        // 在开始前，将所有材质的初始颜色缓存下来
        var initialColors = new Color[allRenderers.Length][];
        for (int i = 0; i < allRenderers.Length; i++)
        {
            var mats = allRenderers[i].materials;
            initialColors[i] = new Color[mats.Length];
            for (int j = 0; j < mats.Length; j++)
            {
                if (mats[j].HasProperty("_BlendColor"))
                    initialColors[i][j] = mats[j].GetColor("_BlendColor");
                else
                    initialColors[i][j] = Color.clear; // 占位
            }
        }

        // 渐变过程
        while (elapsed < 1f)
        {
            float t = elapsed / 1f; // 从 0 到 1
            for (int i = 0; i < allRenderers.Length; i++)
            {
                var mats = allRenderers[i].materials;
                for (int j = 0; j < mats.Length; j++)
                {
                    if (mats[j].HasProperty("_BlendColor"))
                    {
                        Color c = initialColors[i][j];
                        // 把 alpha 从初始 (通常为1) lerp 到 0
                        c.a = Mathf.Lerp(initialColors[i][j].a, 0f, t);
                        mats[j].SetColor("_BlendColor", c);
                    }
                }
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        // 确保最终完全透明
        for (int i = 0; i < allRenderers.Length; i++)
        {
            var mats = allRenderers[i].materials;
            for (int j = 0; j < mats.Length; j++)
            {
                if (mats[j].HasProperty("_BlendColor"))
                {
                    Color c = mats[j].GetColor("_BlendColor");
                    c.a = 0f;
                    mats[j].SetColor("_BlendColor", c);
                }
            }
        }

        disappear();
    }
    #endregion

    #region 碰撞箱
    /// <summary>
    /// 关闭碰撞箱 - 关闭僵尸的碰撞箱
    /// </summary>
    public virtual void CloseCollider()
    {
    }

    /// <summary>
    /// 开启碰撞箱 - 开启僵尸的碰撞箱
    /// </summary>
    public virtual void OpenCollider()
    {
    }
    #endregion

    #region 铁门代码
    public void loadDoorStatus()
    {
        IronDoorArmBrokenControl();
        if (!level2ArmorIsDrop)
        {
            if (!Level2ArmorHalfDamagedSwitched)
            {
                if (level2ArmorHealth <= level2ArmorMaxHealth / 3 * 2)
                {
                    Level2ArmorHalfDamagedSwitched = true;
                    foreach (GameObject gameObject in _ironDoors)
                    {
                        if (gameObject != null)
                        {
                            gameObject.GetComponent<SpriteRenderer>().sprite = DoorSprite[1];
                        }
                    }
                }
            }
            if (!Level2ArmorFullyDamagedSwitched)
            {
                if (level2ArmorHealth <= level2ArmorMaxHealth / 3 * 1)
                {
                    Level2ArmorFullyDamagedSwitched = true;
                    foreach (GameObject gameObject in _ironDoors)
                    {
                        if (gameObject != null)
                        {
                            gameObject.GetComponent<SpriteRenderer>().sprite = DoorSprite[2];
                        }
                    }
                }

            }
            if (level2ArmorHealth <= 0)
            {
                IronDoorWalkWithoutDoorDisplayLogic();
                level2ArmorIsDrop = true;
                myAnimator.SetBool("LostDoor", true);
                if (newZombie)
                {
                    GameObject go = DynamicObjectPoolManager.Instance.GetFromPool(PoolType.ZombiePendantDrop);
                    go.GetComponent<ParticleSystem>().textureSheetAnimation.RemoveSprite(0);
                    go.GetComponent<ParticleSystem>().textureSheetAnimation.AddSprite(_ironDoors[0].GetComponent<SpriteRenderer>().sprite);
                    go.transform.position = _ironDoors[0].transform.position;
                    go.transform.rotation = Quaternion.identity;
                }
            }
        }

        if(Health <= 0)
        {
            IronDoorWalkWithoutDoorDisplayLogic();
        }
    }
    /// <summary>
    /// 铁门行走显示逻辑 - 控制铁门僵尸行走时的显示逻辑
    /// </summary>
    public void IronDoorWalkDisplayLogic()
    {
        SetActiveForObjects(_ironDoorAll, false);
        SetActiveForObjects(_ironDoorZombieWalkDisplay, true);
        IronDoorArmBrokenControl();
    }

    /// <summary>
    /// 铁门啃咬显示逻辑 - 控制铁门僵尸啃咬时的显示逻辑
    /// </summary>
    public void IronDoorBiteDisplayLogic()
    {
        SetActiveForObjects(_ironDoorAll, false);
        SetActiveForObjects(_ironDoorZombieBiteDisplay, true);
        IronDoorArmBrokenControl();
    }

    /// <summary>
    /// 铁门无门行走显示逻辑 - 控制铁门僵尸无门行走时的显示逻辑
    /// </summary>
    public void IronDoorWalkWithoutDoorDisplayLogic()
    {
        SetActiveForObjects(_ironDoorAll, false);
        SetActiveForObjects(_ironDoorZombieWalkWithoutDoorDisplay, true);
        SetActiveForObjects(_ironDoors, false);
        IronDoorArmBrokenControl();
    }

    /// <summary>
    /// 铁门销毁 - 销毁铁门对象
    /// </summary>
    public void DestroyIronDoor()
    {
        SetActiveForObjects(_ironDoors, false);
        foreach (GameObject objects in _ironDoors)
        {
            if (objects != null)
            {
                Destroy(objects);
            }
        }
    }

    /// <summary>
    /// 铁门断臂控制 - 控制铁门僵尸断臂时的显示
    /// </summary>
    public void IronDoorArmBrokenControl()
    {
        if (armIsDrop)
        {
            SetActiveForObjects(_ironDoorArmBrokenForceHide, false);
            SetActiveForObjects(_ironDoorArmBrokenForceShow, true);
        }
    }
    private void SetActiveForObjects(GameObject[] objects, bool isActive)
    {
        foreach (GameObject gameObject in objects)
        {
            if (gameObject != null)
            {
                gameObject.SetActive(isActive);
            }
        }
    }

    /// <summary>
    /// 铁门初始化 - 初始化铁门僵尸的相关对象
    /// </summary>
    public void InitializeIronDoor()
    {
        Transform[] allChildrens = GetComponentsInChildren<Transform>(true);

        if (!newZombie)
        {
            _ironDoorZombieBiteDisplay = GetGameObjectsByNames(allChildrens, new string[]
        {
        "Zombie_outerarm_upper", "Zombie_outerarm_lower", "Zombie_outerarm_hand",
        "Zombie_innerarm_upper", "Zombie_innerarm_lower", "Zombie_innerarm_hand",
        "ScreenDoor_Eating"
        });

            _ironDoorZombieWalkDisplay = GetGameObjectsByNames(allChildrens, new string[]
            {
        "ScreenDoor", "ScreenDoor_outerarm", "Screen_innerhand", "Screen_innerarm"
            });

            _ironDoorZombieWalkWithoutDoorDisplay = GetGameObjectsByNames(allChildrens, new string[]
            {
        "Zombie_outerarm_upper", "Zombie_outerarm_lower", "Zombie_outerarm_hand",
        "Zombie_innerarm_upper", "Zombie_innerarm_lower", "Zombie_innerarm_hand",
        "ScreenDoor"
            });

            _ironDoors = GetGameObjectsByNames(allChildrens, new string[]
            {
        "ScreenDoor", "ScreenDoor_Eating"
            });

            _ironDoorArmBrokenForceHide = GetGameObjectsByNames(allChildrens, new string[]
            {
        "ScreenDoor_outerarm", "ScreenDoor_outerarm", "Screen_innerarm","Screen_innerhand"
            });

            _ironDoorArmBrokenForceShow = GetGameObjectsByNames(allChildrens, new string[]
            {
        "Zombie_outerarm_upper"
            });
        }
        else
        {

            _ironDoorZombieWalkDisplay = GetGameObjectsByNames(allChildrens, new string[]
            {
        "ZOMBIE_SCREENDOOR1", "ZOMBIE_OUTERARM_SCREENDOOR", "ZOMBIE_INNERARM_SCREENDOOR_HAND", "ZOMBIE_INNERARM_SCREENDOOR"
            });

            _ironDoorZombieWalkWithoutDoorDisplay = GetGameObjectsByNames(allChildrens, new string[]//也是铁门僵尸啃咬显示
            {
        "ZOMBIE_OUTERARM_UPPER", "ZOMBIE_OUTERARM_LOWER", "ZOMBIE_OUTERARM_HAND",
        "ZOMBIE_INNERARM_UPPER", "ZOMBIE_INNERARM_LOWER", "ZOMBIE_INNERARM_HAND",
        "ZOMBIE_SCREENDOOR1"
            });

            _ironDoorZombieBiteDisplay = _ironDoorZombieWalkWithoutDoorDisplay;

            _ironDoors = GetGameObjectsByNames(allChildrens, new string[]
            {
        "ZOMBIE_SCREENDOOR1"
            });

            _ironDoorArmBrokenForceHide = GetGameObjectsByNames(allChildrens, new string[]
            {
        "ZOMBIE_OUTERARM_SCREENDOOR", "ZOMBIE_INNERARM_SCREENDOOR","ZOMBIE_INNERARM_SCREENDOOR_HAND"
            });

            _ironDoorArmBrokenForceShow = GetGameObjectsByNames(allChildrens, new string[]
            {
        "ZOMBIE_OUTERARM_UPPER"
            });
        }

    }
    void AddUnique(GameObject[] sourceArray, List<GameObject> list)
    {
        if (sourceArray != null)
        {
            foreach (GameObject item in sourceArray)
            {
                if (!list.Contains(item)) // 如果list不包含这个元素
                {
                    list.Add(item);
                }
            }
        }
    }
    private GameObject[] GetGameObjectsByNames(Transform[] allChildrens, string[] names)
    {
        List<GameObject> foundObjects = new List<GameObject>();
        foreach (var name in names)
        {
            foreach (var child in allChildrens)
            {
                var foundObject = FindChildByNameRecursive(child, name);
                if (foundObject != null)
                {
                    foundObjects.Add(foundObject);
                    break;
                }
            }
        }

        return foundObjects.ToArray();
    }
    public GameObject FindChildByNameRecursive(Transform parent, string name)
    {
        if (parent == null) return null;
        if (parent.name == name)
        {
            return parent.gameObject;
        }
        foreach (Transform child in parent)
        {
            var foundObject = FindChildByNameRecursive(child, name);
            if (foundObject != null)
            {
                return foundObject;
            }
        }

        return null;
    }

    private GameObject FindChildByName(Transform[] allChildrens, string name)
    {
        foreach (var child in allChildrens)
        {
            if (child.name == name)
            {
                return child.gameObject;
            }
        }
        return null;
    }
    #endregion

    #region 血量显示

    /// <summary>
    /// 血量文本 - 生成血量显示文本
    /// </summary>
    private string HealthText()
    {
        string healthDisplay = "";
        if (Health > 0)
        {
            healthDisplay += "本体：" + Health + "/" + MaxHealth + "\n";
        }
        if (level1ArmorHealth > 0)
        {
            healthDisplay += "一类：" + level1ArmorHealth + "/" + level1ArmorMaxHealth + "\n";
        }
        if (level2ArmorHealth > 0)
        {
            healthDisplay += "二类：" + level2ArmorHealth + "/" + level2ArmorMaxHealth + "\n";
        }
        if (debuff.Poison > 0)
        {
            healthDisplay += "毒素：" + debuff.Poison;
        }
        return healthDisplay;
    }

    /// <summary>
    /// 加载血量文本 - 更新血量显示文本
    /// </summary>
    public void LoadHealthText()
    {
        _healthDisplay.text = HealthText();
    }

    /// <summary>
    /// 变更血量显示 - 切换血量显示的可见性
    /// </summary>
    /// <param name="visible">b - 是否显示</param>
    public void ChangeHealthDisplay(bool visible)
    {
        _healthDisplay.gameObject.SetActive(visible);
    }


    #endregion
}

/// <summary>
/// 僵尸debuff - 僵尸的负面效果结构体
/// </summary>
public struct ZombieDebuff
{
    /// <summary>
    /// 减速 - 减速效果的持续时间
    /// </summary>
    public float Deceleration;

    /// <summary>
    /// 中毒 - 中毒层数
    /// </summary>
    public int Poison;

    /// <summary>
    /// 魅惑 - 是否被魅惑
    /// </summary>
    public bool Charmed;

    /// <summary>
    /// 狂暴 - 是否处于狂暴状态
    /// </summary>
    public bool Furious;

    /// <summary>
    /// 冻结 - 是否被冻结
    /// </summary>
    public bool Frozen;
}

/// <summary>
/// 僵尸buff - 僵尸的增益效果结构体
/// </summary>
public struct ZombieBuff
{
    /// <summary>
    /// 隐匿 - 是否处于隐匿状态
    /// </summary>
    public bool Stealth;

    /// <summary>
    /// 坚韧 - 坚韧层数
    /// </summary>
    public int Toughness;

    /// <summary>
    /// 免疫秒杀 - 是否免疫秒杀效果
    /// </summary>
    public bool ImmuneToInstantKill;

    /// <summary>
    /// 免疫冰冻 - 是否免疫冰冻效果
    /// </summary>
    public bool ImmuneToFreeze;

    /// <summary>
    /// 免疫魅惑 - 是否免疫魅惑效果
    /// </summary>
    public bool ImmuneToCharm;
}