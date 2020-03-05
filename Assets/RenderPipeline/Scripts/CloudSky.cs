using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

[VolumeComponentMenu("Sky/Cloud Sky")]
[SkyUniqueID(NEW_SKY_UNIQUE_ID)]
public class CloudSky : SkySettings
{
    const int NEW_SKY_UNIQUE_ID = 20382390;
    
    [Tooltip("Specify the cubemap HDRP uses to render the sky.")]
    public CubemapParameter hdriSky = new CubemapParameter(null);

    public override Type GetSkyRendererType()
    {
        return typeof(CloudSkyRenderer);
    }

    public override int GetHashCode()
    {
        int hash = base.GetHashCode();
        unchecked
        {
            hash = hdriSky.value != null ? hash * 23 + hdriSky.GetHashCode() : hash;
        }
        return hash;
    }
}