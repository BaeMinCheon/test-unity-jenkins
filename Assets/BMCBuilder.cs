using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

class BMCBuilder
{
#if UNITY_EDITOR

    static string[] SCENES = FindEnabledEditorScenes();

    static string APP_NAME = "YourProject";
    static string TARGET_DIR = "Build";

    public static void Build()
    {
        string target_dir = APP_NAME + ".apk";

        if (Directory.Exists(TARGET_DIR) == false)
        {
            Directory.CreateDirectory(TARGET_DIR);
        }

        GenericBuild(SCENES, TARGET_DIR + "/" + target_dir, BuildTarget.Android, BuildOptions.None);
    }

    private static string[] FindEnabledEditorScenes()
    {
        List<string> EditorScenes = new List<string>();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (!scene.enabled) continue;
            EditorScenes.Add(scene.path);
        }
        return EditorScenes.ToArray();
    }

    private static void GenericBuild(string[] scenes, string target_dir, BuildTarget build_target, BuildOptions build_options)
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, build_target);
        string res = BuildPipeline.BuildPlayer(scenes, target_dir, build_target, build_options).ToString();
        if (res.Length > 0)
        {
            throw new Exception("BuildPlayer failure : " + res);
        }
    }

#endif
}