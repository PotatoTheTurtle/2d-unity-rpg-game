using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicManager : MonoBehaviour
{

    public Slider magicSlider;
    public Inventory playerInventory;
    void Start()
    {
        magicSlider.maxValue = playerInventory.maxMagic;
        magicSlider.value = playerInventory.currentMagic;
    }

    public void AddMagic()
    {

        if(playerInventory.currentMagic < playerInventory.maxMagic)
        {
            magicSlider.value += 1;
            playerInventory.currentMagic += 1;
        }

        /*magicSlider.value = playerInventory.currentMagic;
        if (magicSlider.value > magicSlider.maxValue)
        {
            magicSlider.value = magicSlider.maxValue;
            playerInventory.currentMagic = playerInventory.maxMagic;
        }*/
    }

    public void DecreaseMagic()
    {
        if(playerInventory.currentMagic > 0)
        {
            magicSlider.value -= 1;
            playerInventory.currentMagic -= 1;
        }
        else if (playerInventory.currentMagic <= 0)
        {
            magicSlider.value = 0;
            playerInventory.currentMagic = 0;
        }
    }

}
