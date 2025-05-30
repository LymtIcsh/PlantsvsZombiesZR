using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogLevel : MonoBehaviour
{
    public CrazyDave crazyDave;   //������Ľű����
    public SpeechBubble peaSpeechBubble;    //�㶹�Ի���
    public SpeechBubble daveSpeechBubble;   //������Ի���
    public int LevelNumber;

    int count = 1;  //�Ի���������ǰ�ǵڼ����Ի�
                    // Start is called before the first frame update
                    //void Update()
                    //{

    //    switch(LevelNumber)
    //    {
    //        case 1:LevelDialog1(); break;
    //        case 2: LevelDialog2(); break;
    //        case 3: LevelDialog3(); break;
    //        case 4: LevelDialog4(); break;
    //        case 5: LevelDialog5(); break;
    //        case 6: LevelDialog6(); break;
    //        case 7: LevelDialog7(); break;
    //        case 8: LevelDialog8(); break;
    //        case 9: LevelDialog9(); break;
    //        case 10: LevelDialog10(); break;
    //        case 12: LevelDialog12(); break;
    //        case 13: LevelDialog13(); break;
    //        case 51: LevelDialog51(); break;
    //        case 52: LevelDialog52(); break;
    //        case 53: LevelDialog53(); break;
    //        case 54: LevelDialog54(); break;
    //        case 55: LevelDialog55(); break;
    //        case 56: LevelDialog56(); break;
    //        case 57: LevelDialog57(); break;
    //        case 58: LevelDialog58(); break;
    //        case 59: LevelDialog59(); break;
    //        case 241: LevelDialog241(); break;
    //        case 242: LevelDialog242(); break;
    //        case 243: LevelDialog243(); break;
    //        case 244: LevelDialog244(); break;
    //        case 245: LevelDialog245(); break;
    //        case 246: LevelDialog246(); break;
    //        case 247: LevelDialog247(); break;
    //        case 253: LevelDialog253(); break;
    //        default: 
    //            GameManagement.instance.GetComponent<GameManagement>().awakeAll();
    //            count++; 
    //            gameObject.SetActive(false); 
    //            break;
    //    }
    //}
    void Update()
    {
        string methodName = "LevelDialog" + LevelNumber;
        var method = GetType().GetMethod(methodName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

        if (method != null)
        {
            method.Invoke(this, null);
        }
        else
        {
            GameManagement.instance.GetComponent<GameManagement>().awakeAll();
            count++;
            gameObject.SetActive(false);
        }
    }



    public void activeTofalse()
    {
        
        gameObject.SetActive(false);
    }

    public void LevelDialog1()
    {
        //�����������������һ�¼�
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    crazyDave.talk("��ӭ������ȻȺϵ��ð��ģʽ");
                    count++;
                    break;
                case 2:
                    crazyDave.talk("�ⲿ�ֵĹؿ����Ϊ����ԭ�����ݣ����и���ȵĻ��ƽ�ѧ");
                    count++;
                    break;
                case 3:
                    crazyDave.talk("���������ð汾��ɫ�������滷��ģʽ");
                    count++;
                    break;
                case 4:
                    crazyDave.talk("ð��ģʽ��������ֲ��");
                    count++;
                    break;
                case 5:
                    crazyDave.smallTalk("��Ϸ��죡");
                    count++;
                    break;
                case 6:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();count++;
                    Invoke("activeTofalse", 1.5f);
                    break;
                default:
                    break;
            }
        }
    }

    public void LevelDialog2()
    {
        //�����������������һ�¼�
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    crazyDave.talk("���տ�����Ҫ�������ȡ��Ԫ��");
                    count++;
                    break;
                case 2:
                    crazyDave.talk("���������ý�������Ѷȡ�" + "\n" + "��Ĭ���Ѷ�2���Ƽ��������Ѷ�2-3��");
                    count++;
                    break;
                case 3:
                    crazyDave.smallTalk("�����ܵ��Ѷ����õ�Ӱ�졣");
                    count++;
                    break;
                case 4:
                    crazyDave.talk("�Ѷ�1-�Ѷ�2ʱ��ÿ����ͨ�����ֵ50������㡣" + "\n" + "�Ѷ�3-�Ѷ�5ʱ��ÿ����ͨ�����ֵ25������㡣");
                    count++;
                    break;
                case 5:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();count++;
                    Invoke("activeTofalse", 1.5f);
                    break;
                default:
                    break;
            }
        }
    }

    public void LevelDialog3()
    {
        //�����������������һ�¼�
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.talk("��ȻȺϵ��Ľ�ʬӵ�м���Ч��" + "\n" + "������� ������>ѡ�� �Ҳ�˵����");
                    count++;
                    break;
                case 2:
                    crazyDave.talk("���ڽ�ʬ���壬����Ч����������Ρ�" + "\n" + "������� ������>ѡ�� �Ҳ�˵����");
                    count++;
                    break;
                case 3:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();count++;
                    Invoke("activeTofalse", 1.5f);
                    break;
                default:
                    break;
            }
        }
    }
    public void LevelDialog4()
    {
        //�����������������һ�¼�
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.talk("�ھ��ڿ��������ף���ݼ�2���ƶ�ֲ�");
                    count++;
                    break;
                case 2:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();count++;
                    Invoke("activeTofalse", 1.5f);
                    break;
                default:
                    break;
            }
        }
    }
    public void LevelDialog5()
    {
        //�����������������һ�¼�
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    crazyDave.talk("ÿ�󲨽�ʬ�����ڳ��Ͻ�ʬ�����ˢ�¡�");
                    count++;
                    break;
                case 2:
                    crazyDave.gameObject.SetActive(true);
                    crazyDave.talk("��׮��ֲ�����ǿ���������з��ӵ���������ĵз��ӵ���ת��Ϊ�ҷ��ӵ���������۳�ֲ������ֵ");
                    count++;
                    break;
                case 3:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();count++;
                    Invoke("activeTofalse", 1.5f);
                    break;
                default:
                    break;
            }
        }
    }
    public void LevelDialog6()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    crazyDave.talk("����ȻȺϵ���У�ֲ����Ը��ݻ������б��֡�");
                    count++;
                    break;
                case 2:
                    crazyDave.talk("��������һ��ԭʼ��׮����ʾ");
                    count++;
                    break;
                case 3:
                    PlantGrid allObjects = FindFirstObjectByType<PlantGrid>(); // �������� PlantGrid ���󣬰����Ǽ����
                    allObjects.plantByGod("Wood");
                    crazyDave.talk("��������һ��ԭʼ��׮");
                    count++;
                    break;
                case 4:
                    PlantGrid Objects = FindFirstObjectByType<PlantGrid>();
                    if(Objects.nowPlant != null)
                    {
                        PlantManagement.RemovePlant(Objects.nowPlant);
                        Destroy(Objects.nowPlant);
                    }
                    Objects.plantByGod("TorchWood");
                    
                    crazyDave.talk("������˻����׮��" + "\n" + "����ǻ������ֻ��ơ�");
                    count++;
                    break;
                case 5:
                    crazyDave.talk("ԭʼ��׮�ڰ���-�ݵػ�����ֲ���Զ����ֳ��˻����׮");
                    count++;
                    break;
                case 6:
                    crazyDave.talk("ԭʼ��׮Ϊ����ֲ������׮Ϊ����ֲ�");
                    count++;
                    break;
                case 7:
                    crazyDave.talk("����ֲ���ڲ�ͬ����ӵ�в�ͬ�ı���ֲ�");
                    count++;
                    break;
                case 8:
                    crazyDave.talk("ͨ��������������ñ���ֲ�ﲻ�����Ķ��������");
                    count++;
                    break;
                case 9:
                    crazyDave.talk("������Ĳݵ�ֲ�ﶼ�ǻ���ֲ�ԭʼ��׮��Ϊ����");
                    count++;
                   
                    break;
                case 10:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();count++;
                    Invoke("activeTofalse", 1.5f);
                    break;
                default:
                    break;
            }
        }
    }
    public void LevelDialog7()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    peaSpeechBubble.showDialog("Ϊʲô�ҿ����˼���");                   
                    count++;
                    break;
                case 2:                
                    crazyDave.talk("�����ƶ�ʱ�и�������ֲ��");
                    count++;
                    break;
                case 3:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();count++;
                    Invoke("activeTofalse", 1.5f);
                    break;
                default:
                    break;
            }
        }
    }
    public void LevelDialog8()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    crazyDave.talk("�ղŵ���ɫ��ʬ�С��ָ�硱");
                    count++;
                    break;
                case 2:
                    crazyDave.talk("�������ͷű�������ֲ����������ɴ���˺���");
                    count++;
                    break;
                case 3:
                    crazyDave.talk("�ش̿������������ָ��ı�����");
                    count++;
                    break;
                case 4:
                    crazyDave.smallTalk("һ��������");
                    count++;
                    break;
                case 5:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();count++;
                    Invoke("activeTofalse", 1.5f);
                    break;
                default:
                    break;
            }
        }
    }

    public void LevelDialog9()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    crazyDave.talk("������ʬ�յ��������ҧʱ�����ͷŴ����ļ�");
                    count++;
                    break;
                case 2:
                    crazyDave.talk("���Ѫ���ϴ�ʱ���ܵ����������ͷż�");
                    count++;
                    break;
                case 3:
                    crazyDave.talk("�����ʵ������ʱ����Ҫ������");
                    count++;
                    break;
                case 4:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll(); count++;
                    Invoke("activeTofalse", 1.5f);
                    break;
                default:
                    break;
            }
        }
    }

    public void LevelDialog10()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    crazyDave.talk("�������᡿�ո���Ȩ������ӫ���˿����ʳ���������硣");
                    count++;
                    break;
                case 2:
                    crazyDave.talk("�ݵغ�ҹֲ��ʹ󲿷ֲݵذ���ֲ��һ����ͬ���ڻ���ֲ��");
                    count++;
                    break;
                case 3:
                    crazyDave.talk("��ҹ����������ֲ�ͬ������");
                    count++;
                    break;
                case 4:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll(); count++;
                    Invoke("activeTofalse", 1.5f);
                    break;
                default:
                    break;
            }
        }
    }

    public void LevelDialog12()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    crazyDave.talk("���տ���ʬ�ڳ�ʱ�᲻�Ͽ۳��������");
                    count++;
                    break;
                case 2:
                    crazyDave.talk("������Ա���Ϊ����");
                    count++;
                    break;
                case 3:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll(); count++;
                    Invoke("activeTofalse", 1.5f);
                    break;
                default:
                    break;
            }
        }
    }

    public void LevelDialog13()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    crazyDave.talk("���繽���Դ�͸���˵Ķ�����ߣ��������ŵ�");
                    count++;
                    break;
                case 2:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll(); count++;
                    Invoke("activeTofalse", 1.5f);
                    break;
                default:
                    break;
            }
        }
    }

    public void LevelDialog14()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    crazyDave.talk("���컨�������ɴ󲿷ֵĽ�ʬ");
                    count++;
                    break;
                case 2:
                    crazyDave.gameObject.SetActive(true);
                    crazyDave.talk("��������Ͱ�����Ż������ʬʱ�����Գ����ô��컨��������");
                    count++;
                    break;
                case 3:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll(); count++;
                    Invoke("activeTofalse", 1.5f);
                    break;
                default:
                    break;
            }
        }
    }

    public void LevelDialog15()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    crazyDave.talk("�Ȼ󹽿���ʹһֻ��ʬΪ����ս��");
                    count++;
                    break;
                case 2:
                    crazyDave.gameObject.SetActive(true);
                    crazyDave.talk("���Գ����Ȼ�����ʬ�����������Ȼ����������Ȼ�����");
                    count++;
                    break;
                case 3:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll(); count++;
                    Invoke("activeTofalse", 1.5f);
                    break;
                default:
                    break;
            }
        }
    }

    public void LevelDialog16()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    crazyDave.talk("���𹽿��Ի���һ��Ƭ��ʬ");
                    count++;
                    break;
                case 2:
                    crazyDave.gameObject.SetActive(true);
                    crazyDave.talk("�����ڱ���ֲ������´���180��ĿӶ�");
                    count++;
                    break;
                case 3:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll(); count++;
                    Invoke("activeTofalse", 1.5f);
                    break;
                default:
                    break;
            }
        }
    }

    public void LevelDialog51()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    peaSpeechBubble.showDialog("��ӭ����ɭ�ֻ�����");
                    count++;
                    break;
                case 2:
                    crazyDave.talk("��ͬ�����Ļ�����ɫ����Ȼ��ĺ����淨");
                    count++;
                    break;
                case 3:
                    crazyDave.talk("�󲿷ֻ���ֲ���ڲ�ͬ������ֲ���Ի�ñ���ֲ��");
                    count++;
                    break;
                case 4:
                    crazyDave.talk("��ɭ�ֻ�����ֲԭʼ��׮�����Ի��ɭ����׮");
                    count++;
                    break;
                case 5:
                    crazyDave.talk("ɭ����׮���ӵ�ǿ��Ϊɭ���ӵ���ÿ��ǿ���۳�����һ��Ѫ��");
                    count++;
                    break;
                case 6:
                    crazyDave.talk("��ɭ����׮ǿ�������㶹�ӵ�ʱ��������������");
                    count++;
                    break;
                case 7:
                    crazyDave.talk("ɭ���ӵ�����Ϊ�з�ʩ�Ӷ���");
                    count++;
                    break;
                case 8:
                    crazyDave.talk("��ɭ�ֻ����£��ҷ��͵з����ж����Ի���ɭ��ֵ����Ⱦɭ��ֵ");
                    count++;
                    break;
                case 9:
                    crazyDave.talk("������Ⱦ��ɭ��ֵ�ﵽһ���޶Ⱥ󣬻ᴥ��ɭ������Ч��");
                    count++;
                    break;

                case 10:
                    crazyDave.talk("�ҷ�����ɭ��ֵ������Ϊ�з�ȫ�帽�Ӵ�������");
                    count++;
                    break;
                case 11:
                    crazyDave.talk("�з�������Ⱦɭ��ֵ������ʹ�з�ȫ������״̬");
                    count++;
                    break;
                case 12:
                    crazyDave.talk("�ѶȲ�������ʱ��ÿ�δ���Ч����ʹ�з�������ֵ���������ֵ����Ϊ����");
                    count++;
                    break;
                case 13:
                    crazyDave.talk("�з��Ŀ�״̬����ͨ�������Թ������");
                    count++;
                    break;
                case 14:
                    crazyDave.talk("���ܣ���Ⱦ��ɭ��ֵ���ٶ��ܵ��Ѷ����õ�Ӱ��");
                    count++;
                    break;
                case 15:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();count++;
                    Invoke("activeTofalse", 1.5f);
                    break;
                default:
                    break;
            }
        }
    }

    public void LevelDialog52()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    crazyDave.talk("ɭ�����տ��и�ǿ������������������������");
                    count++;
                    break;
                case 2:
                    crazyDave.talk("ɭ�����տ��ڲ��������ͬʱ���Ƹ���������ֲ��");
                    count++;
                    break;
                case 3:
                    crazyDave.talk("����С��1000ʱ�������⣬�뾶1����ÿ��һ��ɭ�����տ��������һ������");
                    count++;
                    break;
                case 4:
                    crazyDave.talk("�������1000ʱ�������⣬�����ֲ������ֵ���������������������������");
                    count++;
                    break;
                case 5:
                    crazyDave.talk("ɭ��ֲ��ĸ�����Ϊ�����Ի���ɭ��ֵ��");
                    count++;
                    break;
                case 6:
                    crazyDave.talk("����ɭ�����տ��������⡢ɭ����׮ǿ���ӵ���ɭ���ӵ����е���...");
                    count++;
                    break;
                case 7:
                    crazyDave.talk("���е�ɭ�ֽ�ʬ�ڳ���һ��ʱ����и���������");
                    count++;
                    break;
                case 8:
                    crazyDave.talk("��ɭ�ֽ�ʬ���Ѷ�4-5�Զ�����ʱ�����������⸺��Ч����");
                    count++;
                    break;
                case 9:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();count++;
                    Invoke("activeTofalse", 1.5f);
                    break;
                default:
                    break;
            }
        }
    }
    public void LevelDialog53()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    crazyDave.talk("ɭ�ּ���ܵ�����ʱ���������������");
                    count++;
                    break;
                case 2:
                    crazyDave.talk("ɭ�ּ���ڱ���ʳʱ��ÿ��Ϊ��ʬ����һ�㶾��");
                    count++;
                    break;
                case 3:
                    crazyDave.talk("ÿ�㶾��ÿ�������һ���˺����ڱ������������");
                    count++;
                    break;
                case 4:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();count++;
                    Invoke("activeTofalse", 1.5f);
                    break;
                default:
                    break;
            }
        }
    }

    public void LevelDialog54()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    crazyDave.talk("ɭ�������Ž�ʬ���������ܵ����������������Ⱦɭ��ֵ");
                    count++;
                    break;
                case 2:
                    crazyDave.smallTalk("ͨ���ش̽��д�͸������");
                    count++;
                    break;
                case 3:
                    crazyDave.talk("ɭ�ֵش�ÿ�ι������������з����ж��أ���ɴ���˺�");
                    count++;
                    break;
                case 4:
                    crazyDave.talk("�����˺����ݵз��������ֵ�İٷֱȺͶ��ز�������");
                    count++;
                    break;
                case 5:
                    crazyDave.talk("����ɭ�ֵش��Լ����ܸ����ж�Ч������Ҫ���ʹ��");
                    count++;
                    break;
                case 6:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll(); count++;
                    Invoke("activeTofalse", 1.5f);
                    break;
                default:
                    break;
            }
        }
    }

    public void LevelDialog55()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    count++;
                    break;
                case 2:
                    crazyDave.talk("��ɭ�ֻ����У�ÿ��һ��ʱ���ڳ����������ɭ�ֹ�ľ�ԣ���������");
                    count++;
                    break;
                case 3:
                    crazyDave.talk("��ľ�����ߴ󲿷�ֱ���ӵ��˺���ֻ�ɱ�ɭ���㶹����");
                    count++;
                    break;
                case 4:
                    crazyDave.talk("�����ľ���еĽ�ʬӵ�С����䡹Ч�������ᱻ�����ֲ�������ʹ�����ӵ�������");
                    count++;
                    break;
                case 5:
                    crazyDave.talk("��ľ��ÿ����һ��ʱ����������һ����ʬ");
                    count++;
                    break;
                case 6:
                    crazyDave.talk("��ľ�������󣬴��������Ⱦɭ��ֵ");
                    count++;
                    break;
                case 7:
                    crazyDave.talk("ɭ�ֳŸ�����ʬ�ڽ����ľ��ʱ�����̽����״̬");
                    count++;
                    break;
                case 8:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();
                    count++;
                    Invoke("activeTofalse", 1.5f);
                    break;

                default:
                    break;

            }
        }
    }
    public void LevelDialog56()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    count++;
                    break;
                case 2:
                    crazyDave.talk("ɭ�����佩ʬ���Լ�����Ϊ�˹�ľ��");
                    count++;
                    break;
                case 3:
                    crazyDave.talk("ɭ�����佩ʬ������С����䡹Ч��");
                    count++;
                    break;
                case 4:
                    crazyDave.talk("����ÿ��һ��ʱ���������λ���ٻ�һ����ʬ");
                    count++;
                    break;
                case 5:
                    crazyDave.talk("��ľ�����ֿ������ӡ����䡹Ч����������");
                    count++;
                    break;
                case 6:
                    crazyDave.talk("����ÿ�ι������Ը��ݵз����ز�����ɴ���˺�");
                    count++;
                    break;
                case 7:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();
                    count++;
                    Invoke("activeTofalse", 1.5f);
                    break;

                default:
                    break;

            }
        }
    }

    public void LevelDialog57()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    count++;
                    break;
                case 2:
                    crazyDave.talk("����С�繽��С�繽��ɭ�ֱ���");
                    count++;
                    break;
                case 3:
                    crazyDave.talk("ӫ��Ģ�������⹽��ɭ�ֱ���");
                    count++;
                    break;
                case 4:
                    crazyDave.talk("ӫ��Ģ������Χ���Լ�ͨ�����ս�����ϵ");
                    count++;
                    break;
                case 5:
                    crazyDave.talk("����������ʱ����������Χӫ��Ģ����������߲���");
                    count++;
                    break;
                case 6:
                    crazyDave.talk("����С�繽��ӫ��Ģ���������������Ⱦ����Ϊ�з����Ӵ�������");
                    count++;
                    break;
                case 7:
                    crazyDave.talk("������ں�볡������֣����ô��С����䡹Ч��");
                    count++;
                    break;
                case 8:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();
                    count++;
                    Invoke("activeTofalse", 1.5f);
                    break;

                default:
                    break;

            }
        }
    }

    public void LevelDialog58()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    count++;
                    break;
                case 2:
                    crazyDave.talk("���̴��繽�Ǵ��繽��ɭ�ֱ���");
                    count++;
                    break;
                case 3:
                    crazyDave.talk("���̴��繽�Ǽ���ǿ����ֲ��");
                    count++;
                    break;
                case 4:
                    crazyDave.talk("���繽��ֲ����Թ��������С����䡹Ч���ĵ�λ");
                    count++;
                    break;
                case 5:
                    crazyDave.talk("���̴��繽����Ϊ�з����Ӷ���");
                    count++;
                    break;
                case 6:
                    crazyDave.talk("�������������Ⱦ����Ϊ�з����Ӵ������ز�����");
                    count++;
                    break;
                case 7:
                    crazyDave.talk("�ݴ��������ڲ�Ѫ������ɭ�ֲݴԣ����ڲݴԺ�ָ�����ֵ");
                    count++;
                    break;
                case 8:
                    crazyDave.talk("ɭ�ֲݴԱ�����ʱ����۴�����Ⱦɭ��ֵ");
                    count++;
                    break;
                case 9:
                    crazyDave.talk("ɭ�ֲݴ������з���λ");
                    count++;
                    break;

                case 10:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();
                    count++;
                    Invoke("activeTofalse", 1.5f);
                    break;

                default:
                    break;

            }
        }
    }

    public void LevelDialog59()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    count++;
                    break;
                case 2:
                    crazyDave.talk("����Ⱦ�����Ĳ�����ֲ������ֲʱ��ֲ�ﲻ�����");
                    count++;
                    break;
                case 3:
                    crazyDave.talk("��Ⱦģʽ�µ�ֲ����������ֵ���޳����ܵ��˺�");
                    count++;
                    break;
                case 4:
                    crazyDave.talk("ľ�Ǽ������ȫ��Ⱦ��ʬ����ɭ�ֶ�ӵ�����㡸���͡�");
                    count++;
                    break;
                case 5:
                    crazyDave.talk("���ܵ��з�������ÿ����Ϳ�ת��Ϊ50�㻤��");
                    count++;
                    break;
                case 6:
                    crazyDave.talk("ľ�Ǽ���ܵ���������˸�������ľ���ӵ�");
                    count++;
                    break;
                case 7:
                    crazyDave.talk("ÿ���ӵ���������˺�������һ�㶾��");
                    count++;
                    break;
                case 8:
                    crazyDave.talk("ľ�Ǽ���ܵ����������������");
                    count++;
                    break;
                case 9:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();
                    count++;
                    Invoke("activeTofalse", 1.5f);
                    break;

                default:
                    break;

            }
        }
    }

    public void LevelDialog241()
    {
        //�����������������һ�¼�
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    crazyDave.talk("�����п��Բ������ֲ�");
                    count++;
                    break;
                case 2:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll(); count++;
                    Invoke("activeTofalse", 1.5f);
                    break;
                default:
                    break;
            }
        }
    }

    //public void LevelDialog242()
    //{
    //    //�����������������һ�¼�
    //    if (Input.GetKeyDown(KeyCode.Mouse0))
    //    {
    //        switch (count)
    //        {
    //            case 1:
    //                crazyDave.gameObject.SetActive(true);
    //                crazyDave.talk("��ӭ������ѩ�ؿ�ǰհ��");
    //                count++;
    //                break;
    //            case 2:
    //                crazyDave.talk("�˹ؿ�Ϊ��ѩ�ؿ��Ļ���ֲ��Cost��");
    //                count++;
    //                break;
    //            case 3:
    //                crazyDave.leave();
    //                GameManagement.instance.GetComponent<GameManagement>().awakeAll(); count++;
    //                Invoke("activeTofalse", 1.5f);
    //                break;
    //            default:
    //                break;
    //        }
    //    }
    //}

    public void LevelDialog243()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    count++;
                    break;
                case 2:
                    crazyDave.talk("��ɽ����ģʽ������û��CD");
                    count++;
                    break;
                case 3:
                    crazyDave.talk("��ɽ����ģʽ����ֲ���Ͽ�ֲ��ʱ��ͬʱ��������ֲ");
                    count++;
                    break;
                case 4:
                    crazyDave.talk("˫����ү�ڷ�ŭ��ᷢ�䴩͸���ӵ���ÿ��һ��ʱ�䷢��ȼ�����еĻ����ӵ�");
                    count++;
                    break;
                case 5:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();
                    count++;
                    Invoke("activeTofalse", 1.5f);
                    break;

                default:
                    break;

            }
        }
    }

    public void LevelDialog244()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    count++;
                    break;
                case 2:
                    crazyDave.talk("������׮������ģʽǰ��������ð�պͻ���ģʽ���˽�����ɭ����׮");
                    count++;
                    break;
                case 3:
                    crazyDave.talk("��Ȼ������׮�ܵ��з���ǿ���ӵ������󣬻ᷴ���ӵ���Ӧ��ǿ���ӵ�");
                    count++;
                    break;
                case 4:
                    crazyDave.talk("��׮������ģʽ�£���׮�����ӵ�����۳���������ֵ");
                    count++;
                    break;
                case 5:
                    crazyDave.talk("���WASD�����ⰴ�����ƶ���׮");
                    count++;
                    break;
                case 6:
                    crazyDave.talk("�������Ҽ���Q����������̿��л���׮");
                    count++;
                    break;
                case 7:
                    crazyDave.talk("<color=red>��ע�⣬��ģʽ�¾��������׮�������ӵ����������ӵ��˺�</color>");
                    count++;
                    break;
                case 8:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();
                    count++;
                    Invoke("activeTofalse", 1.5f);
                    break;

                default:
                    break;

            }
        }
    }

    public void LevelDialog245()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    count++;
                    break;
                case 2:
                    crazyDave.talk("�����㶹��ʬ����ɱ�ɭ����׮�����Ļ����㶹");
                    count++;
                    break;
                case 3:
                    crazyDave.talk("ɭ����׮���������㶹ʱ������������");
                    count++;
                    break;
                case 4:
                    crazyDave.talk("����ﵽ1000�������1000�����ͷż���");
                    count++;
                    break;
                case 5:
                    crazyDave.talk("��������������м���E�������ⰴ�������ͷż���");
                    count++;
                    break;
                case 6:
                    crazyDave.talk("�����׮�����ͷ�һ�еĻ��棬ɭ����׮���Դ����������ɭ��ף��");
                    count++;
                    break;
                case 7:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();
                    count++;
                    Invoke("activeTofalse", 1.5f);
                    break;

                default:
                    break;

            }
        }
    }

    public void LevelDialog246()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    count++;
                    break;
                case 2:
                    crazyDave.talk("˫�����ֶ�ү�ڱ�ֽ����ǰ���ṥ��");
                    count++;
                    break;
                case 3:
                    crazyDave.talk("����ʹ��������ʬ���ӵ����ܹ�����");
                    count++;
                    break;
                case 4:
                    crazyDave.talk("˫�����ֶ�ү��ֽ������䳬�������ӵ����ɱ�ɭ����׮����");
                    count++;
                    break;
                case 5:
                    crazyDave.talk("�����������Է������һЩ���׵Ķ�����");
                    count++;
                    break;
                case 6:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();
                    count++;
                    Invoke("activeTofalse", 1.5f);
                    break;

                default:
                    break;

            }
        }
    }

    public void LevelDialog247()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    count++;
                    break;
                case 2:
                    crazyDave.talk("��ģʽΪ��׮��������ģʽ�����ܿ����ϴ�");
                    count++;
                    break;
                case 3:
                    crazyDave.talk("����ģʽ�³�ʼӵ�д�������");
                    count++;
                    break;
                case 4:
                    crazyDave.talk("����ģʽֻ����ֻ�ǹ���ֽ�ʬ");
                    count++;
                    break;
                case 5:
                    crazyDave.talk("��ǹ���ֽ�ʬ��ͷ������һ�����");
                    count++;
                    break;
                case 6:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();
                    count++;
                    Invoke("activeTofalse", 1.5f);
                    break;

                default:
                    break;

            }
        }
    }

    public void LevelDialog253()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    count++;
                    break;
                case 2:
                    crazyDave.talk("ը��ը��ը��");
                    count++;
                    break;
                case 3:
                    crazyDave.talk("�����ըģʽ�������׿������ޱ�ը");
                    count++;
                    break;
                case 4:
                    crazyDave.talk("ʹ��WASD�������ⰴ���ƶ�������");
                    count++;
                    break;
                case 5:
                    crazyDave.talk("��ʬ���ٶȽ������Ѷ���������");
                    count++;
                    break;
                case 6:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();
                    count++;
                    Invoke("activeTofalse", 1.5f);
                    break;

                default:
                    break;

            }
        }
    }

    public void LevelDialog201()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    count++;
                    break;
                case 2:
                    crazyDave.talk("��ӭ������Ȼ����޽Ӱ�������ؿ���");
                    count++;
                    break;
                case 3:
                    crazyDave.talk("ͨ������ؿ����㽫���������������������ǹ����");
                    count++;
                    break;
                case 4:
                    crazyDave.talk("������ǹ���ַ��������ڻ����㶹���ӵ�������������������ҽ�");
                    count++;
                    break;
                case 5:
                    crazyDave.talk("�����ؿ��ڲ������ݶȳ��֣��һ���ִ󲿷ֽ�ʬ");
                    count++;
                    break;
                case 6:
                    crazyDave.talk("�����Ҹ���ѡ���Ŀ���������ս����");
                    count++;
                    break;
                case 7:
                    crazyDave.smallTalk("���Ͱɣ�");
                    count++;
                    break;
                case 8:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();
                    count++;
                    Invoke("activeTofalse", 1.5f);
                    break;

                default:
                    break;

            }
        }
    }

    public void LevelDialog202()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (count)
            {
                case 1:
                    crazyDave.gameObject.SetActive(true);
                    count++;
                    break;
                case 2:
                    crazyDave.talk("��ӭ������Ȼ�����ӽ���������ؿ���");
                    count++;
                    break;
                case 3:
                    crazyDave.talk("�ؿ��ڻ�������˽�ʬ");
                    count++;
                    break;
                case 4:
                    crazyDave.talk("���˽�ʬ�ڲ���ҧʱ���С����䡹Ч���������ӡ����䡹Ч����ֲ��Ը�����");
                    count++;
                    break;
                case 5:
                    crazyDave.talk("�����ؿ��ڲ������ݶȳ��֣��һ���ִ󲿷ֽ�ʬ");
                    count++;
                    break;
                case 6:
                    crazyDave.smallTalk("ս����");
                    count++;
                    break;
                case 7:
                    crazyDave.smallTalk("���Ͱɣ�");
                    count++;
                    break;
                case 8:
                    crazyDave.leave();
                    GameManagement.instance.GetComponent<GameManagement>().awakeAll();
                    count++;
                    Invoke("activeTofalse", 1.5f);
                    break;

                default:
                    break;

            }
        }
    }
}

