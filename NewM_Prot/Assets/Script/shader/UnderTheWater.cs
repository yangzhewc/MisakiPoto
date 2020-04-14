using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class UnderTheWater: CustomImageEffect
{
    [SerializeField]
    [Range(0, 1)]
    public float red;
    [SerializeField]
    [Range(0, 1)]
    public float green;
    [SerializeField]
    [Range(0, 1)]
    public float blue;
    [SerializeField]
    [Range(0, 300)]
    public float speed;
    [SerializeField]
    [Range(0, 1)]
    public float power;
    [SerializeField]
    [Range(100, 1000)]
    public float density;

    public override string ShaderName
    {
        get { return "Custom/UnderTheWater"; }
    }

    protected override void UpdateMaterial()
    {
        Material.SetFloat("_R", red);
        Material.SetFloat("_G", green);
        Material.SetFloat("_B", blue);
        Material.SetFloat("_Speed", speed);
        Material.SetFloat("_Power", power);
        Material.SetFloat("_Density", density);
    }
}