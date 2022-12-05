using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Godwoken;
using UnityEngine.UI;
using ERC721Example.Contracts.ERC721Example.ContractDefinition;
using TMPro;

public class GameplayUI : MonoBehaviour
{
    public MetaMaskController metamaskController;

    public Button openChestButton;
    public Button mintKeyButton;
    public Image keyImage;
    public TextMeshProUGUI errorText;

    private int currentKeys = 0;

    public void OpenChestButtonFunction()
    {
        openChestButton.interactable = false;
        metamaskController.GetNumberOfExampleNFTs(ERC721ExampleDeployment.ADDRESS, (int noOfKeys) =>
        {
            if (noOfKeys == 0)
            {
                errorText.text = "You have No key";
                ChestManager.instance.OnClosedChestCommand?.Invoke();
                openChestButton.interactable = true;
                return;
            }
            else
            {
                errorText.text = "You have " + noOfKeys.ToString() + " keys now";
                ChestManager.instance.OnOpenChestCommand?.Invoke();
                openChestButton.gameObject.SetActive(false);
                currentKeys = noOfKeys;
            }
        });
    }

    public void MintKeyButtonFunction()
    {
        mintKeyButton.interactable = false;
        StartCoroutine(MintKey());
    }

    private IEnumerator MintKey()
    {
        //Create Transaction
        var transaction = metamaskController.UnityToGodwokenTransaction();
        if (transaction == null)
        {
            errorText.text = "Transaction is null";
            yield break;
        }

        //Fill transaction details
        var mintFunction = new SafeMintFunction()
        {
            To = metamaskController.GetAddress(),
            Uri = "https://my-json-server.typicode.com/aj-Baba-yaga/DummyNFT/tokens"
        };

        //sign and send transaction
        yield return transaction.SignAndSendTransaction(mintFunction, ERC721ExampleDeployment.ADDRESS);

        errorText.text = "Transation is signed and sent. ERC721 Address: " + ERC721ExampleDeployment.ADDRESS.ToString();
        mintKeyButton.interactable = true;

        //Showing Image
        yield return new WaitForSeconds(3f);
        ShowNFTImage();
    }

    public void ShowNFTImage()
    {
        errorText.text = "Inside show NFT";
        metamaskController.GetExampleNFTs(ERC721ExampleDeployment.ADDRESS, (List<Sprite> sprites) =>
        {         
            keyImage.sprite = sprites[sprites.Count - 1];
            //mintKeyButton.interactable = true;
        });
    }
}
