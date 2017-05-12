using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

//遍历各个场景
//截取相关路径
//遍历场景中每一个功能文件夹
//改变物体的Assetbundle名称

public class BuildAssetbundle
{
    [MenuItem("Tools/BuildAssetBundle")]
    public static void BuildeAssetBundle()
    {
        string outPath = Application.dataPath + "/ABs";
        if (!Directory.Exists(outPath))
        {
            Directory.CreateDirectory(outPath);
        }
        BuildPipeline.BuildAssetBundles(outPath, 0, EditorUserBuildSettings.activeBuildTarget);
        AssetDatabase.Refresh();     
    }

    [MenuItem("Tools/MarkAssetBundle")]
    public static void MarkAssetBundle()
    {
        //移除未引用BundleName
        AssetDatabase.RemoveUnusedAssetBundleNames();

        string path = Application.dataPath + "/Prefabs";
        DirectoryInfo info = new DirectoryInfo(path);
        FileSystemInfo[] fileInfo = info.GetFileSystemInfos();
        for (int i = 0; i < fileInfo.Length; i++)
        {
            FileSystemInfo tempInfo = fileInfo[i];
            if (tempInfo is DirectoryInfo)
            {
                string tempName = Path.Combine(path, tempInfo.Name);
                ScenesOverview(tempName);
            }
        }
    }

    /// <summary>
    /// 对整个场景遍历
    /// </summary>
    /// <param name="scenePath"></param>
    public static void ScenesOverview(string scenePath)
    {
        Debug.Log(scenePath);
        //string txtName = "Record.txt";
        //string tempPath = scenePath + txtName;
        //FileStream fs = new FileStream(tempPath, FileMode.Create);
        //StreamWriter sw = new StreamWriter(fs);

        //Dictionary<string, string> redDic = new Dictionary<string, string>();

        
        //sw.Close();
        //fs.Close();
        //ChanagerHead(scenePath, null);
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 截取相对路径
    /// </summary>
    /// <param name="fullPath"></param>
    /// <param name="fileItem"></param>
    private static void ChanagerHead(string fullPath,Dictionary<string,string> fileItem)
    {
        int tempCount = fullPath.IndexOf("Assets");
        int tempLength = fullPath.Length;

        string replacePath = fullPath.Substring(tempCount, tempLength - tempCount);
        DirectoryInfo info = new DirectoryInfo(replacePath);
        if (info != null)
        {
            ListFiles(info,replacePath,fileItem);
        }
        else
        {
            Debug.Log("this path is not");
        }
    }

    /// <summary>
    /// 遍历功能文件夹
    /// </summary>
    /// <param name="info"></param>
    /// <param name="replacePath"></param>
    /// <param name="fileItem"></param>
    private static void ListFiles(FileSystemInfo info, string replacePath, Dictionary<string, string> fileItem)
    {
        if (!info.Exists)
        {
             Debug.Log("this path is not");
             return;
        }
        DirectoryInfo dir = info as DirectoryInfo;
        FileSystemInfo[] files = dir.GetFileSystemInfos();
        for (int i = 0; i < files.Length; i++)
        {
            FileInfo file = files[i] as FileInfo;
            if (file != null)
            {


            }
            else
            {
                ListFiles(files[i],replacePath,fileItem);
            }
        }
    }

    private static string FixedWindowsPath(string path)
    {
        path = path.Replace("/", "\\");
        return path;
    }
    private static string GetBundlePath(FileInfo file,string replacePath)
    {
        string pathName = file.FullName;
        pathName = FixedWindowsPath(pathName);
        int assetCount = pathName.IndexOf(replacePath);
        assetCount += replacePath.Length + 1;

        int nameCount = pathName.LastIndexOf(file.Name);
        int tempCount = replacePath.LastIndexOf("/");

        string sceneName = replacePath.Substring(tempCount + 1, replacePath.Length - tempCount - 1);
        int tempLength = nameCount - assetCount;
        if (tempCount > 0)
        {
            string subStr = pathName.Substring(assetCount, pathName.Length - assetCount);
            string[] result = subStr.Split("/".ToCharArray());
            return sceneName + "/" + result[0];
        }
        else
        {
            return sceneName;
        }
        //return "";
    }

    private static void ChanagerMark(FileInfo tempFile, string replacePath, Dictionary<string, string> fileItem)
    {
        if (tempFile.Extension==".meta")
        {
            return;
        }
        string markStr = GetBundlePath(tempFile, replacePath);
      
    }
    
}
