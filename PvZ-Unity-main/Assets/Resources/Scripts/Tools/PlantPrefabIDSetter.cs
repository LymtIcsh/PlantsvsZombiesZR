#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class PlantPrefabIDSetter
{
    [MenuItem("Tools/Set Plant IDs on Prefabs")]
    public static void SetPlantIDs()
    {
        string[] guids = AssetDatabase.FindAssets("t:Prefab", new[] { "Assets/Resources/Prefabs/Plants" });

        int successCount = 0;
        int failCount = 0;

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            Plant plantData = prefab.GetComponent<Plant>();

            if (plantData == null)
            {
                plantData = prefab.AddComponent<Plant>();
            }

            string cleanName = prefab.name;
            PlantStruct plantStruct = PlantStructManager.GetPlantStructByName(cleanName);

            if (plantStruct.id == 0)
            {
                Debug.LogWarning($"❌ 未找到名为 [{cleanName}] 的 PlantStruct（路径：{path}）");
                failCount++;
                continue;
            }

            plantData.ID = plantStruct.id;

            // 标记为已修改并保存
            EditorUtility.SetDirty(prefab);
            successCount++;
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"✅ Plant ID 设置完成！成功：{successCount}，失败：{failCount}");
    }
}
#endif
