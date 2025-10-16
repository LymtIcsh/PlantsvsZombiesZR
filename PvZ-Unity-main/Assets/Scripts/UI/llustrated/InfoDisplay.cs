using UnityEngine;
using UnityEngine.UI;

public class InfoDisplay : MonoBehaviour
{
    public Text nameText;  // 显示植物名称
    public GameObject content;
    public Text descriptionText;  // 显示植物描述
    public ScrollRect scrollRect; // 引用ScrollRect，控制滚动视图



    public void ShowPlantInfo(int plantId)
    {
        // 通过 ID 获取植物数据
        PlantStruct plant = PlantStructManager.GetPlantStructById(plantId);

        if (plant.id != 0)
        {
            nameText.text = plant.ChineseName;
            descriptionText.text = plant.CompletedIntroduction;
        }
        else
        {
            nameText.text = "敬请期待";
            descriptionText.text = "该植物将在后续版本出现。";
        }

        // 更新高度
        UpdateContentHeight();

        // 调整滚动视图到顶部
        ResetScrollPosition();
    }

    public void ShowZombieInfo(int plantId)
    {
        // 通过 ID 获取植物数据
        ZombieStruct zombie = ZombieStructManager.GetZombieStructById(plantId);

        if (zombie.id != 0)
        {
            nameText.text = zombie.ChineseName;
            descriptionText.text = zombie.ZombieIntroduction;
        }
        else
        {
            nameText.text = "敬请期待";
            descriptionText.text = "该僵尸将在后续版本出现。";
        }

        // 更新高度
        UpdateContentHeight();

        // 调整滚动视图到顶部
        ResetScrollPosition();
    }

    // ✅ 动态更新 Content 高度，匹配文本高度
    private void UpdateContentHeight()
    {
        // 强制刷新布局，确保 preferredHeight 是最新的
        LayoutRebuilder.ForceRebuildLayoutImmediate(descriptionText.rectTransform);

        float targetHeight = descriptionText.preferredHeight;

        // 设置 content 的高度（只修改 Y 值）
        RectTransform contentRect = content.GetComponent<RectTransform>();
        Vector2 size = contentRect.sizeDelta;
        size.y = targetHeight;
        contentRect.sizeDelta = size;
    }

    // 重置滚动视图的位置到顶部
    private void ResetScrollPosition()
    {
        if (scrollRect != null)
        {
            scrollRect.verticalNormalizedPosition = 1f;
        }
    }
}
