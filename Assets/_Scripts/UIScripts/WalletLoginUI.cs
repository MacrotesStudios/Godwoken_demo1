using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalletLoginUI : MonoBehaviour
{
    public Transform[] loginStatus;

    public Transform contractActivateText;

    public void ActivateStatus(int index)
    {
        if (index >= loginStatus.Length)
        {
            Debug.Log("Invalid loginstatus index");
            return;
        }

        DeactivateAllStatus();
        loginStatus[index].gameObject.SetActive(true);
    }

    public void DeactivateAllStatus()
    {
        foreach (Transform t in loginStatus)
        {
            t.gameObject.SetActive(false);
        }
    }

    public void Start()
    {
        MetamaskConnectionManager.instance.OnMetamaskLoginSuccess += LoginSuccess;
        MetamaskConnectionManager.instance.OnMetamaskLoginFailure += LoginFailed;
        MetamaskConnectionManager.instance.OnContractActivation += ContractActivated;

        contractActivateText.gameObject.SetActive(false);

    }

    private void ContractActivated()
    {
        contractActivateText.gameObject.SetActive(true);
    }

    public void LoginSuccess()
    {
        ActivateStatus(1);
        StartCoroutine(GameStart());
    }

    public void LoginFailed()
    {
        ActivateStatus(2);
    }

    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(2f);
        UIManager.instance.ActivateUIScreen(1);
    }

    public void OnDisable()
    {
        MetamaskConnectionManager.instance.OnMetamaskLoginSuccess -= LoginSuccess;
        MetamaskConnectionManager.instance.OnMetamaskLoginFailure -= LoginFailed;
        MetamaskConnectionManager.instance.OnContractActivation -= ContractActivated;
    }
    #region ButtonFunctions
    public void LoginButtonFunction()
    {
        ActivateStatus(0);
        MetamaskConnectionManager.instance.ConnectWallet();
    }
    #endregion
}
