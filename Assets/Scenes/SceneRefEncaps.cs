using System.Linq;
using UnityEditor;
using UnityEditor.Build.Profile;

using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SceneRefEncaps", menuName = "Scriptable Objects/SceneRefEncaps")]
public class SceneRefEncaps : ScriptableObject
{
    public string name;
    public SceneRefEncaps previousSceneInGroup;
    public SceneRefEncaps nextSceneInGroup;
}
