using UnityEngine;


[CreateAssetMenu(fileName = "SceneRefEncaps", menuName = "Scriptable Objects/SceneRefEncaps")]
public class SceneRefEncaps : ScriptableObject
{
    public string sceneName;
    public SceneRefEncaps previousSceneInGroup;
    public SceneRefEncaps nextSceneInGroup;
}
