using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 内存层
/// </summary>
public class IABResource
{   
    //内存中的AssetBundle
    public AssetBundle assetBundle;

    public UnityEngine.Object LoadResource(string path)
    {
        return assetBundle.LoadAsset(path);
    }

    public UnityEngine.Object[] LoadResources(string path)
    {
        return assetBundle.LoadAssetWithSubAssets(path);
    }
}

/// <summary>
/// 从硬盘中加载内存
/// </summary>
public class LoadAssetBundle
{
    Dictionary<string, IABResource> abList = new Dictionary<string, IABResource>();

    public string abName;

    public void SettingABName(string name)
    {
        this.abName = name;
    }

    public IEnumerator ABLoad(string path)
    {
        WWW tempLoad = new WWW(path);
        while (!tempLoad.isDone)
        {
            yield return tempLoad;
        }
        //tempLoad.assetBundle;
        IABResource ab = new IABResource();
        ab.assetBundle = tempLoad.assetBundle;
        abList.Add(abName, ab);
    }

    public Object LoadSingle(string abName,string res)
    {
        if (abList.ContainsKey(abName))
        {
            IABResource temp = abList[abName];
            return temp.LoadResource(res);
        }
        return null;
    }

    public Object[] LoadMuch(string abName,string res)
    {
        if (abList.ContainsKey(abName))
        {
             IABResource temp = abList[abName];
            return temp.LoadResources(res);
        }
        return null;
    }
}


/// <summary>
/// 建造者模式(每一个环节提供不同功能，分工明确)
/// </summary>
public class BuilderPattern : MonoBehaviour {


	void Start () {

        LoadAssetBundle ab = new LoadAssetBundle();
        ab.SettingABName("test.assetbundle");
        StartCoroutine(ab.ABLoad("//Path/test.assetbundle"));
	}
	
	
	void Update () {
        
	}
}
