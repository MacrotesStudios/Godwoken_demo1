using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Godwoken;
using ERC721Example.Contracts.ERC721Example.ContractDefinition;

public class MetamaskConnectionManager : MonoBehaviour
{
    #region Singleton and Awake function
    public static MetamaskConnectionManager instance;

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

    public MetaMaskController metamaskController;

    #region Events
    public Action OnMetamaskLoginSuccess;
    public Action OnMetamaskLoginFailure;
    public Action OnContractActivation;
    #endregion

    // Start is called before the first frame update
    void Start()
    {

    }

    #region Handling Metamask Login
    public void ConnectWallet()
    {
        metamaskController.Initialize(OnMetamaskInitialized);
    }

    public void OnMetamaskInitialized(string address)
    {
        OnMetamaskLoginSuccess?.Invoke();
        SetupContractAfterLogin();


    }

    public void LoginFailed()
    {
        OnMetamaskLoginFailure?.Invoke();
    }
    #endregion

    #region ContractSetup
    private void SetupContractAfterLogin()
    {
        metamaskController.SetupContract(ERC721ExampleDeployment.HASH, OnContractSetupComplete);
        OnContractActivation?.Invoke();
    }

    private void OnContractSetupComplete(string contractAddress)
    {
        ERC721ExampleDeployment.ADDRESS = contractAddress;
    }
    #endregion
}
