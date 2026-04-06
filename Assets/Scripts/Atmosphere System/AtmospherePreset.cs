using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// This script reads from AtmospherePresets
/// And applies the values to the scene.
/// </summary>

[CreateAssetMenu(fileName = "AtmospherePreset", menuName = "Weather/Atmosphere Preset")]
public class AtmospherePreset : ScriptableObject
{
    [Header("Sun")] 
    //public Color sunColor;    // Make HDR color property eventually
    public float sunIntensity;
    public float sunSize;
    //[Range(0, 1)] public float sunElevation; // 0 = horizon, 1 = zenith

    [Header("Sky")] 
    public Color zenithColor;
    public Color horizonColor;
    public Color nadirColor;

    [Header("Clouds Skybox")]
    [Range(0, 1)] public float cloudScatter;
    [Range(0, 1)] public float cloudDensity;
    public Vector2 panSpeed;
    
    // [Header("Clouds Worldspace")]
    // public float cloudPower;
    // public Vector2 cloudSpeed;

    [Header("Fog")] 
    public Color fogColor;
    public float fogDensity;

    // [Header("Post Processing")] 
    // public VolumeProfile volumeProfile; // New method for PostFX
}
