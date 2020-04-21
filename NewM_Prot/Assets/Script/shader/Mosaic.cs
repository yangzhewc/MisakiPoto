using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mosaic : CustomImageEffect
{
    #region Fields

    [SerializeField]
    [Range(1, 100)]
    private float m_Size;

    #endregion

    #region Properties

    public override string ShaderName
    {
        get { return "Custom/Mosaic"; }
    }

    #endregion

    #region Methods

    protected override void UpdateMaterial()
    {
        Material.SetFloat("_Size", m_Size);
    }

    #endregion

    public void SetSize(float a)
    {
        m_Size = a;
    }
    public float GetSize()
    {
        return m_Size;
    }
    public void AddSize(float a)
    {
        m_Size += a;
    }
}