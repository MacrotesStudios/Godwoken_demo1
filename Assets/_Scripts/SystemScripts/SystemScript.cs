using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemScript : MonoBehaviour
{
    //Setting up the system so that it will retain over multiple scenes.
    #region Singleton and Awake function
    public static SystemScript instance;

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

        DontDestroyOnLoad(this.gameObject);
    }
    #endregion
}
