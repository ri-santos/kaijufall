using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    List<CardUI> kaijus;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddKaiju(CardUI kaiju)
    {
        kaijus.Add(kaiju);
    }

    public void RemoveKaiju(CardUI kaiju)
    {
        kaijus.Remove(kaiju);
    }

}
