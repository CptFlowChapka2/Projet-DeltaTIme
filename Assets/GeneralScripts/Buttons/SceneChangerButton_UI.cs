using System;
using UnityEditor;
using UnityEngine;

public class SceneChangerButton_UI : MonoBehaviour
{
    public SceneRefEncaps targetScene;
    public SceneLoaderEncaps SceneLoaderEncaps;
    public ButtonAction action;
    private PauseHandler _pauseHandler;
    private SoundManager _soundManager;

    private void Start()
    {
        _pauseHandler = FindAnyObjectByType<PauseHandler>();
        _soundManager = FindAnyObjectByType<SoundManager>();
    }


    public void OnButtonClic()
    {
        _soundManager.clic.Play();
        switch (action)
        {
            case ButtonAction.Next:
                SceneLoaderEncaps.SceneLoader.LoadNextSceneInGroup();
                break;
            case ButtonAction.Previous:
                SceneLoaderEncaps.SceneLoader.LoadPreviousSceneInGroup();
                break;
            case ButtonAction.LoadPrecise:
                SceneLoaderEncaps.SceneLoader.LoadScene(targetScene);
                break;
            case ButtonAction.Pause:
                _pauseHandler.PauseCalled.Invoke(PauseState.noInfo);
                break;
            case ButtonAction.Reload:
                SceneLoaderEncaps.SceneLoader.ReloadScene();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    
}

public enum ButtonAction
{
    Next=0,
    Previous=1,
    LoadPrecise=2,
    Pause=3,
    Reload=4
    
}
#if UNITY_EDITOR 


[CustomEditor(typeof(SceneChangerButton_UI))]
public class SceneChangerButton_UI_Inspector : Editor
{
    
    
    SerializedProperty m_TargetScene;
    SerializedProperty m_SceneLoader;
    SerializedProperty m_ActionMode;

    private void OnEnable()
    {
        m_ActionMode = serializedObject.FindProperty("action");
        m_SceneLoader=serializedObject.FindProperty("SceneLoaderEncaps");
        m_TargetScene=serializedObject.FindProperty("targetScene");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(m_SceneLoader);
        EditorGUILayout.PropertyField(m_ActionMode);
        if(m_ActionMode.enumValueIndex==2)
            EditorGUILayout.PropertyField(m_TargetScene);
        serializedObject.ApplyModifiedProperties();
    }
}
#endif
