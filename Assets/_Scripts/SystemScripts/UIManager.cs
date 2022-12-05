using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Singleton and Awake function
    public static UIManager instance;
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

    public List<Transform> uiScreens = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ActivateUIScreen(int index)
    {
        if (index >= uiScreens.Count)
        {
            Debug.Log("Invalid UI index");
            return;
        }
        DeactivateAllScreens();
        uiScreens[index].gameObject.SetActive(true);
    }

    public void DeactivateAllScreens()
    {
        foreach (Transform t in uiScreens)
        {
            t.gameObject.SetActive(false);
        }
    }
}
