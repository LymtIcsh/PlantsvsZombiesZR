using UnityEngine;
using System.IO;
using System.Net;
using System;
using System.Threading;
using UnityEngine.UI;
using System.Text;
using System.Collections;
using UnityEngine.Serialization;

public class SelfUpdata : MonoBehaviour
{
    [Header("����������")]
    public string url = "https://wwwe.lanzouq.com/s/QXUpdate"; // ��������
    public string makerUrl = "https://space.bilibili.com/1956298381";
    [Header("UI Ԫ��")]
    public GameObject updatePanel;       // �������
    public Text updateText;              // ��������ʾ�ı�
    public Button cancelButton;          // ȡ����ⰴť
    public Button reloadButton;    
    [FormerlySerializedAs("����")] [Header("���� btn ")]// ���¼��ذ�ť
    public Button updateBtn;
    private Thread getUrlThread;         // ��ȡ��ҳ���߳�
    private string urlContent;           // ��ҳ����
    private string urlVersion;           // �������汾��
    private string urlAnnouncement;      // ����������
    private bool isConnecting = false;   // �Ƿ��������ӷ�����
    private bool isUrlContentReady = false; // ��ҳ�����Ƿ�׼����
    private float connectionTimer = 0f;  // ���Ӽ�ʱ��
    [Header("�Ƿ���ʾ��")]
    public static bool isShowOver = false;

    private void Start()
    {
        // ��ʼ�� UI
        updatePanel.SetActive(false);
        
        cancelButton.onClick.AddListener(OnCancelButtonClicked);
        reloadButton.onClick.AddListener(OnReloadButtonClicked);
        updateBtn.onClick.AddListener(OnUpdateButtonClicked);
        if(!isShowOver)
        {
            // ��ʼ������
            StartUpdateCheck();
            isShowOver = true;
        }
        

        
    }

    private void Update()
    {
        // ��������״̬�ı�
        if (isConnecting)
        {
            connectionTimer += Time.deltaTime;
            if (connectionTimer >= 0.3f)
            {
                if (updateText.text == "�������ӵ�������.")
                {
                    updateText.text = "�������ӵ�������..";
                }
                else if (updateText.text == "�������ӵ�������..")
                {
                    updateText.text = "�������ӵ�������...";
                }
                else if (updateText.text == "�������ӵ�������...")
                {
                    updateText.text = "�������ӵ�������.";
                }
                connectionTimer = 0f;
            }
        }

        // �����ҳ�����Ƿ�׼����
        if (isUrlContentReady)
        {
            GetHtmlStr();
            isUrlContentReady = false;
        }
    }

    // ��ʼ������
    private void StartUpdateCheck()
    {
        updatePanel.SetActive(true);
        StaticThingsManagement.IsSecondaryPanelOpen = true;
        updateText.text = "�������ӵ�������.";
        isConnecting = true;

        getUrlThread = new Thread(UrlThread);
        getUrlThread.Start();
    }

    // ��ȡ��ҳ���ݵ��߳�
    private void UrlThread()
    {
        try
        {
            if (!string.IsNullOrEmpty(url))
            {
                WebRequest request = WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream, Encoding.Default);
                urlContent = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();
                isUrlContentReady = true; // ���������׼����
                isConnecting = false;
            }
        }
        finally
        {
            
        }
    }

    // ������ҳ����
    private void GetHtmlStr()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            updateText.text = "���������ӣ������������á�";
            return;
        }

        if (string.IsNullOrEmpty(urlContent))
        {
            updateText.text = "���ӳ�ʱ�������ԡ�";
            return;
        }

        try
        {
              
            //string[] sArray = urlContent.Split(new char[] { '��', '��' });
            string[] sArray = urlContent.Split("��");
            if (sArray.Length > 1)
            {
                //string[] arrat2 = sArray[1].Split(new char[] { '$', '$' });
                string[] arrat2 = sArray[1].Split("$");
                if (arrat2.Length > 2)
                {
                    urlVersion = arrat2[1];          // �������汾��
                    urlAnnouncement = arrat2[2];     // ����������

                    // �Ƚϰ汾��
                    int comparisonResult = CompareVersions(urlVersion, Application.version);
                    if (comparisonResult > 0)
                    {
                        updateText.text = $"�����°汾: {urlVersion}";
                    }
                    else if (comparisonResult == 0)
                    {
                        updateText.text = $"��ǰ�������°汾: {urlVersion}\n����: {urlAnnouncement}";
                    }
                    else
                    {
                        updateText.text = "���İ汾�ȷ������汾���¡�";
                    }
                }
                else
                {
                    updateText.text = "���������ص����ݸ�ʽ����ȷ��";
                }
            }
            else
            {
                updateText.text = "���������ص����ݸ�ʽ����ȷ��";
            }
        }
        catch (Exception e)
        {
            updateText.text = "�����汾��Ϣʧ��: " + e.Message;
        }
    }

    // �Ƚϰ汾��
    private int CompareVersions(string versionA, string versionB)
    {
        // ���汾�Ų��Ϊ����
        string[] partsA = versionA.Split('.');
        string[] partsB = versionB.Split('.');

        // ȷ�������汾�ŵĲ�������һ�£���� 4 ���֣�
        int maxLength = Math.Max(partsA.Length, partsB.Length);
        for (int i = 0; i < maxLength; i++)
        {
            // ��ȡ��ǰ���ֵ�ֵ��������ֲ����ڣ���Ĭ��Ϊ 0��
            int partA = (i < partsA.Length) ? int.Parse(partsA[i]) : 0;
            int partB = (i < partsB.Length) ? int.Parse(partsB[i]) : 0;

            // �Ƚϲ���
            if (partA > partB)
            {
                return 1; // �汾 A ���ڰ汾 B
            }
            else if (partA < partB)
            {
                return -1; // �汾 A С�ڰ汾 B
            }
        }

        return 0; // �汾 A ���ڰ汾 B
    }

    // ȡ����ť����¼�
    private void OnCancelButtonClicked()
    {
        updatePanel.SetActive(false);
        StaticThingsManagement.IsSecondaryPanelOpen = false;
        if (getUrlThread != null && getUrlThread.IsAlive)
        {
            getUrlThread.Abort();
        }
    }

    // ���¼��ذ�ť����¼�
    private void OnReloadButtonClicked()
    {
        StartUpdateCheck();
    }

    // ���°�ť����¼�
    private void OnUpdateButtonClicked()
    {
        Application.OpenURL(makerUrl);
        updateText.text = "������ת������ҳ��...";
    }
}