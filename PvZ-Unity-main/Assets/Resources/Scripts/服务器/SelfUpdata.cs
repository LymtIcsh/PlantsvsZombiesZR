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
    [Header("服务器更新")]
    public string url = "https://wwwe.lanzouq.com/s/QXUpdate"; // 更新链接
    public string makerUrl = "https://space.bilibili.com/1956298381";
    [Header("UI 元素")]
    public GameObject updatePanel;       // 更新面板
    public Text updateText;              // 更新中显示文本
    public Button cancelButton;          // 取消检测按钮
    public Button reloadButton;    
    [FormerlySerializedAs("更新")] [Header("更新 btn ")]// 重新加载按钮
    public Button updateBtn;
    private Thread getUrlThread;         // 获取网页的线程
    private string urlContent;           // 网页内容
    private string urlVersion;           // 服务器版本号
    private string urlAnnouncement;      // 服务器公告
    private bool isConnecting = false;   // 是否正在连接服务器
    private bool isUrlContentReady = false; // 网页内容是否准备好
    private float connectionTimer = 0f;  // 连接计时器
    [Header("是否显示过")]
    public static bool isShowOver = false;

    private void Start()
    {
        // 初始化 UI
        updatePanel.SetActive(false);
        
        cancelButton.onClick.AddListener(OnCancelButtonClicked);
        reloadButton.onClick.AddListener(OnReloadButtonClicked);
        updateBtn.onClick.AddListener(OnUpdateButtonClicked);
        if(!isShowOver)
        {
            // 开始检测更新
            StartUpdateCheck();
            isShowOver = true;
        }
        

        
    }

    private void Update()
    {
        // 更新连接状态文本
        if (isConnecting)
        {
            connectionTimer += Time.deltaTime;
            if (connectionTimer >= 0.3f)
            {
                if (updateText.text == "正在连接到服务器.")
                {
                    updateText.text = "正在连接到服务器..";
                }
                else if (updateText.text == "正在连接到服务器..")
                {
                    updateText.text = "正在连接到服务器...";
                }
                else if (updateText.text == "正在连接到服务器...")
                {
                    updateText.text = "正在连接到服务器.";
                }
                connectionTimer = 0f;
            }
        }

        // 检查网页内容是否准备好
        if (isUrlContentReady)
        {
            GetHtmlStr();
            isUrlContentReady = false;
        }
    }

    // 开始检测更新
    private void StartUpdateCheck()
    {
        updatePanel.SetActive(true);
        StaticThingsManagement.IsSecondaryPanelOpen = true;
        updateText.text = "正在连接到服务器.";
        isConnecting = true;

        getUrlThread = new Thread(UrlThread);
        getUrlThread.Start();
    }

    // 获取网页内容的线程
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
                isUrlContentReady = true; // 标记内容已准备好
                isConnecting = false;
            }
        }
        finally
        {
            
        }
    }

    // 解析网页内容
    private void GetHtmlStr()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            updateText.text = "无网络连接，请检查网络设置。";
            return;
        }

        if (string.IsNullOrEmpty(urlContent))
        {
            updateText.text = "连接超时，请重试。";
            return;
        }

        try
        {
              
            //string[] sArray = urlContent.Split(new char[] { 'π', 'π' });
            string[] sArray = urlContent.Split("π");
            if (sArray.Length > 1)
            {
                //string[] arrat2 = sArray[1].Split(new char[] { '$', '$' });
                string[] arrat2 = sArray[1].Split("$");
                if (arrat2.Length > 2)
                {
                    urlVersion = arrat2[1];          // 服务器版本号
                    urlAnnouncement = arrat2[2];     // 服务器公告

                    // 比较版本号
                    int comparisonResult = CompareVersions(urlVersion, Application.version);
                    if (comparisonResult > 0)
                    {
                        updateText.text = $"发现新版本: {urlVersion}";
                    }
                    else if (comparisonResult == 0)
                    {
                        updateText.text = $"当前已是最新版本: {urlVersion}\n公告: {urlAnnouncement}";
                    }
                    else
                    {
                        updateText.text = "您的版本比服务器版本更新。";
                    }
                }
                else
                {
                    updateText.text = "服务器返回的数据格式不正确。";
                }
            }
            else
            {
                updateText.text = "服务器返回的数据格式不正确。";
            }
        }
        catch (Exception e)
        {
            updateText.text = "解析版本信息失败: " + e.Message;
        }
    }

    // 比较版本号
    private int CompareVersions(string versionA, string versionB)
    {
        // 将版本号拆分为部分
        string[] partsA = versionA.Split('.');
        string[] partsB = versionB.Split('.');

        // 确保两个版本号的部分数量一致（最多 4 部分）
        int maxLength = Math.Max(partsA.Length, partsB.Length);
        for (int i = 0; i < maxLength; i++)
        {
            // 获取当前部分的值（如果部分不存在，则默认为 0）
            int partA = (i < partsA.Length) ? int.Parse(partsA[i]) : 0;
            int partB = (i < partsB.Length) ? int.Parse(partsB[i]) : 0;

            // 比较部分
            if (partA > partB)
            {
                return 1; // 版本 A 大于版本 B
            }
            else if (partA < partB)
            {
                return -1; // 版本 A 小于版本 B
            }
        }

        return 0; // 版本 A 等于版本 B
    }

    // 取消按钮点击事件
    private void OnCancelButtonClicked()
    {
        updatePanel.SetActive(false);
        StaticThingsManagement.IsSecondaryPanelOpen = false;
        if (getUrlThread != null && getUrlThread.IsAlive)
        {
            getUrlThread.Abort();
        }
    }

    // 重新加载按钮点击事件
    private void OnReloadButtonClicked()
    {
        StartUpdateCheck();
    }

    // 更新按钮点击事件
    private void OnUpdateButtonClicked()
    {
        Application.OpenURL(makerUrl);
        updateText.text = "正在跳转到更新页面...";
    }
}