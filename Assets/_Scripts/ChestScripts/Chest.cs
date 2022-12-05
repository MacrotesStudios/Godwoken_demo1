using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Transform chestBody;
    public Animator chestAnim;
    public Transform explosionParticles;
    public Light chestLight;
    public Color openChestLightColor;
    public Color normalChestLightColor;

    public bool isOpen;

    // Start is called before the first frame update
    void Start()
    {
        if (chestAnim == null)
        {
            chestAnim = chestBody.GetComponent<Animator>();
        }
        explosionParticles.gameObject.SetActive(false);

        ChestManager.instance.OnOpenChestCommand += OpenChest;
        ChestManager.instance.OnClosedChestCommand += ClosedChest;
    }

    [ContextMenu("OpenChest")]
    public void OpenChest()
    {
        if (isOpen)
        {
            return;
        }

        isOpen = true;
        chestAnim.Play("Open");
        explosionParticles.gameObject.SetActive(true);
        //chestLight.color = openChestLightColor;
        //chestAnim.ResetTrigger("open");
    }

    [ContextMenu("NoOpenChest")]
    public void ClosedChest()
    {
        //chestLight.color = normalChestLightColor;
        chestAnim.Play("Closed");
        //chestAnim.ResetTrigger("closed");
    }

    public void OnDisable()
    {
        ChestManager.instance.OnOpenChestCommand -= OpenChest;
        ChestManager.instance.OnClosedChestCommand -= ClosedChest;
    }


}
