﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public int ActiveScene { get; private set; }
    public List<GameObject> GameplayPersistentObjects { get; set; }

    private static LevelManager instance;
    public static LevelManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LevelManager>();
            }
            return instance;
        }
    }

    void Awake()
    {
        GameplayPersistentObjects = new List<GameObject>();
        ActiveScene = SceneManager.GetActiveScene().buildIndex;

        GameplayPersistentObjects.Add(gameObject);
    }

    public void LoadLevel(int scene)
    {
        /*if(scene == 0 || scene == 2)
        {
            foreach (GameObject go in GameplayPersistentObjects)
            {
                if (go != null) Destroy(go);
            }
        }
        else if (scene > 1 && scene > ActiveScene)
        {
            if (!GameplayPersistentObjects.Contains(gameObject))
            {
                GameplayPersistentObjects.Add(gameObject);
            }
            foreach (GameObject go in GameplayPersistentObjects)
            {
                DontDestroyOnLoad(go);
            }
        }
        */

        if(scene <= ActiveScene)
        {
            for (int i = 1; i < GameplayPersistentObjects.Count; i++)
            {
                GameObject go = GameplayPersistentObjects[i];
                if (go != null) Destroy(go);
            }
            GameplayPersistentObjects.RemoveRange(1, GameplayPersistentObjects.Count - 1);
        }

        foreach (GameObject go in GameplayPersistentObjects)
        {
            DontDestroyOnLoad(go);
        }

        ActiveScene = scene;
        SceneManager.LoadScene(scene);
    }
}
