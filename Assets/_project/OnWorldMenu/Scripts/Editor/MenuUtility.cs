using UnityEditor;

public static class MenuUtility
{
    public static string folder = "OnWorldMenu/";

    [MenuItem("GameObject/On World Menu/Press Button")]
    public static void CreatePressButton(MenuCommand menuCommand)
    {
        CreateUtility.CreatePrefab(folder + "Press Button");
    }

    [MenuItem("GameObject/On World Menu/Hold Button")]
    public static void CreateHoldButton(MenuCommand menuCommand)
    {
        CreateUtility.CreatePrefab(folder + "Hold Button");
    }
}
