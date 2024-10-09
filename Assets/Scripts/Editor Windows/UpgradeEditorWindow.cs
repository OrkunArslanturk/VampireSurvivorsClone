using UnityEditor;
using UnityEngine;

public class UpgradeEditorWindow : EditorWindow
{
    private string upgradeName;
    private UpgradeData.UpgradeType upgradeType;
    private float upgradeValue;

    [MenuItem("Custom Tools/Upgrade Creator")]
    public static void ShowWindow()
    {
        GetWindow<UpgradeEditorWindow>("Upgrade Creator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Create New Upgrade", EditorStyles.boldLabel);

        upgradeName = EditorGUILayout.TextField("Upgrade Name", upgradeName);
        upgradeType = (UpgradeData.UpgradeType)EditorGUILayout.EnumPopup("Upgrade Type", upgradeType);
        upgradeValue = EditorGUILayout.FloatField("Upgrade Value", upgradeValue);

        if (GUILayout.Button("Create Upgrade"))
        {
            CreateUpgrade();
        }
    }

    private void CreateUpgrade()
    {
        UpgradeData newUpgrade = ScriptableObject.CreateInstance<UpgradeData>();
        newUpgrade.upgradeName = upgradeName;
        newUpgrade.upgradeType = upgradeType;
        newUpgrade.upgradeValue = upgradeValue;

        AssetDatabase.CreateAsset(newUpgrade, $"Assets/Upgrades/{upgradeName}.asset");
        AssetDatabase.SaveAssets();

        Debug.Log($"{upgradeName} created successfully!");
    }
}
