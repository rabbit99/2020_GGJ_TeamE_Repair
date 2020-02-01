﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<FixableTrigger> FixableObjs;
    public bool StartPlay = false;
    public float MosterHp = 60f;

    private bool first = true;

    public void OnEnable()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (StartPlay)
        {

        }
    }
    public void Play()
    {
        Debug.Log("Play");
        StartPlay = true;
    }
    IEnumerator GameplayLoop()
    {
        float t = 5;
        if (!first)
            t = Random.Range(5f, 8f);
        yield return new WaitForSeconds(t);

        List<FixableTrigger> GoodItems = new List<FixableTrigger>();
        foreach(var item in FixableObjs)
        {
            if (!item.NeedRepair)
                GoodItems.Add(item);
        }
        int r = Random.Range(0, GoodItems.Count);
        GoodItems[r].Broken();


        yield break;
    }
    public void GameOver()
    {
        Debug.Log("GameOver");
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(GameManager))]
public class GameManagerInspector:Editor
{
    GameManager gm;
    private void OnEnable()
    {
        gm = (GameManager)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Start"))
        {
            gm.Play();
        }
    }
}
#endif
