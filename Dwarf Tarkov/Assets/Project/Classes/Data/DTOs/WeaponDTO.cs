using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponDTO
{
    public WeaponDTO(WeaponData data)
    {
        IsAutoFire = data.IsAutoFire;
        RateOfFire = data.RateOfFire;
        AmmoSubtypes = ConvertTypesToDTOs(data.AmmoSubtypes);
        IsPrimary = data.IsPrimary;
        MagCapacity = data.MagCapacity;
        ReloadTime = data.ReloadTime;
        BaseSpreadAngle = data.BaseSpreadAngle;
        AmountOfBullets = data.AmountOfBullets;
        FiringEventName = data.firingEventName;
        AmmoEmptyFiringEventName = data.ammoEmptyFiringEventName;
        ReloadEventName = data.reloadEventName;
        FiringEventRadius = data.firingEventRadius;
        AmmoEmptyEventRadius = data.ammoEmptyEventRadius;
        ReloadEventRadius = data.reloadEventRadius;
    }

    public bool IsAutoFire;
    public float RateOfFire;
    public List<AmmoSubtypeDTO> AmmoSubtypes;
    public bool IsPrimary;
    public int MagCapacity;
    public float ReloadTime;
    public float BaseSpreadAngle;
    public int AmountOfBullets;
    public string FiringEventName;
    public string AmmoEmptyFiringEventName;
    public string ReloadEventName;
    public float FiringEventRadius;
    public float AmmoEmptyEventRadius;
    public float ReloadEventRadius;

    private List<AmmoSubtypeDTO> ConvertTypesToDTOs(List<AmmoSubtype> types)
    {
        List<AmmoSubtypeDTO> dtos = new List<AmmoSubtypeDTO>(); 
        foreach (AmmoSubtype type in types)
        {
            dtos.Add(new AmmoSubtypeDTO(type));
        }
        return dtos;
    }
}
