using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public static class CreateUtility
{
    public static void CreatePrefab(string path)
    {
        GameObject newObject = PrefabUtility.InstantiatePrefab(Resources.Load(path)) as GameObject;
        Place(newObject);
    }

    public static void CreateObject(string name, params Type[] types)
    {
        GameObject newObject = ObjectFactory.CreateGameObject(name, types);
        Place(newObject);
    }

    private static void Place(GameObject newObject)
    {
        SceneView sceneView = SceneView.lastActiveSceneView;
        newObject.transform.position = sceneView ? sceneView.pivot : Vector3.zero;

        StageUtility.PlaceGameObjectInCurrentStage(newObject);
        GameObjectUtility.EnsureUniqueNameForSibling(newObject);

        Undo.RegisterCreatedObjectUndo(newObject, $"Create game object: {newObject.name}");
        Selection.activeGameObject = newObject;

        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    }
}
