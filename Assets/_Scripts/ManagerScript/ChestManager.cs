using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChestManager : MonoBehaviour
{
    #region Awake and Singleton
    public static ChestManager instance;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    #endregion

    public Action OnOpenChestCommand;
    public Action OnClosedChestCommand;
}
