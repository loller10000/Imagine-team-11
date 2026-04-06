using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

/// <summary>
/// This script reads from AtmospherePresets
/// And applies the values to the scene.
/// </summary>

public class AtmosphereSystem : MonoBehaviour
{
    public Material RuntimeSkybox => runtimeSkybox;
    // public Material RuntimeClouds => runtimeClouds;

    [Header("References")]
    [SerializeField] private Light sunLight;
    [SerializeField] private Material skyboxMaterial;
    //[SerializeField] private Material cloudMaterial;
    // [SerializeField] private MeshRenderer cloudsRenderer;
    //[SerializeField] private Volume volumeA;
    // [SerializeField] private Volume volumeB;
    
    [Header("Preset")]
    [SerializeField] private AtmospherePreset currentPreset;
    [SerializeField] private AtmospherePreset[] presets;
    
    private Material runtimeSkybox;
    private Material runtimeClouds;

    private Volume activeVolume;
    private Volume targetVolume;
    
    private AtmospherePreset fromPreset;
    private AtmospherePreset toPreset;
    
    private float blendTime;
    private float blendDuration = 2f;
    private bool isBlending;
    
    #region Unity Lifecycle
    private void Awake()
    {
        // Automate getting Directional Light in scene.

        // Clone materials for runtime use
        runtimeSkybox = new Material(skyboxMaterial);
        // runtimeClouds = new Material(cloudMaterial);
        
        // Apply runtime skybox & cache active
        RenderSettings.skybox = runtimeSkybox;
        runtimeSkybox = RenderSettings.skybox;
        // cloudsRenderer.sharedMaterial = runtimeClouds;
        
        // Init volumes
        // activeVolume = volumeA;
        // targetVolume = volumeB;

        // activeVolume.weight = 0f;
        // targetVolume.weight = 0f;

        // activeVolume.profile = null;
        // targetVolume.profile = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            TransitionTo(presets[0], 2f);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            TransitionTo(presets[1], 2f);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            TransitionTo(presets[2], 2f);;
        
        if (Input.GetKeyDown(KeyCode.Alpha0)) // Default - Everything = 0
            TransitionTo(presets[3], 2f);

        if (!isBlending) return;

        blendTime += Time.deltaTime;
        float rawT = Mathf.Clamp01(blendTime / blendDuration);
        float t = rawT * rawT * (3f - 2f * rawT); // SmoothStep

        ApplyBlended(fromPreset, toPreset, t);

        if (t >= 1f)
        {
            currentPreset = toPreset;
            isBlending = false;
        }
    }
    #endregion
    
    #region Public API
    // Other systems call these functions
    public void ApplyPreset(AtmospherePreset preset)
    {
        if (!preset)
            return;
        
        ApplySky(preset);
        ApplySun(preset);
        ApplyClouds(preset);
        ApplyFog(preset);
    }
    
    public void TransitionTo(AtmospherePreset nextPreset, float duration)
    {
        if (!currentPreset)
        {
            currentPreset = nextPreset;
            ApplyPreset(nextPreset);

            //activeVolume.profile = nextPreset.volumeProfile;
            //activeVolume.weight = 1f;
            //targetVolume.weight = 0f;
            
            Debug.Log($"Atmosphere: Set initial preset to {nextPreset.name}");
            return;
        }
        
        if (duration <= 0f)
        {
            //activeVolume.profile = nextPreset.volumeProfile;
            activeVolume.weight = 1f;
            targetVolume.weight = 0f;

            currentPreset = nextPreset;
            return;
        }
        
        fromPreset = currentPreset;
        toPreset = nextPreset;

        //activeVolume.profile = currentPreset.volumeProfile;
        //targetVolume.profile = nextPreset.volumeProfile;

        blendTime = 0f;
        blendDuration = duration;
        isBlending = true;
        
        // Add caller info to identify which system called it
        Debug.Log($"Atmosphere Transition: {currentPreset?.name} -> {nextPreset.name} ({duration}s)");
    }
    
    public void NextPreset(float duration)
    {
        if (presets == null || presets.Length == 0) return;

        int idx = Array.IndexOf(presets, currentPreset);
        if (idx < 0) idx = 0;
        idx = (idx + 1) % presets.Length;
        TransitionTo(presets[idx], duration);
    }

    public void PrevPreset(float duration)
    {
        if (presets == null || presets.Length == 0) return;

        int idx = Array.IndexOf(presets, currentPreset);
        if (idx < 0) idx = 0;
        idx = (idx - 1 + presets.Length) % presets.Length;
        TransitionTo(presets[idx], duration);
    }
    
    #endregion
    
    #region Application (Single Preset)
    // Application
    private void ApplySky(AtmospherePreset p)
    {
        runtimeSkybox.SetColor("_SkyColor", p.zenithColor);
        runtimeSkybox.SetColor("_HorizonColor", p.horizonColor);
        runtimeSkybox.SetColor("_GroundColor", p.nadirColor);
    }
    
    private void ApplySun(AtmospherePreset p)
    {
        sunLight.intensity = p.sunIntensity;

        runtimeSkybox.SetFloat("_SunSize", p.sunSize);
        runtimeSkybox.SetFloat("_SunIntensity", p.sunIntensity);
    }
    
    private void ApplyClouds(AtmospherePreset p)
    {
        runtimeSkybox.SetFloat("_CloudScatter", p.cloudScatter);
        runtimeSkybox.SetFloat("_CloudDensity", p.cloudDensity);
        runtimeSkybox.SetVector("_CloudSpeed", p.panSpeed);

        // Optional: world-space clouds
        //runtimeClouds.SetFloat("_CloudsPower", p.cloudPower);
        //runtimeClouds.SetVector("_CloudSpeed", p.cloudSpeed);
    }
    
    private void ApplyFog(AtmospherePreset p)
    {
        RenderSettings.fogColor = p.fogColor;
        RenderSettings.fogDensity = p.fogDensity;
    }
    #endregion
    
    #region Blending
    // Blending
    private void ApplyBlended(AtmospherePreset a, AtmospherePreset b, float t)
    {
        // ## SKY COLORS
        runtimeSkybox.SetColor("_SkyColor",Color.Lerp(a.zenithColor, b.zenithColor, t));
        runtimeSkybox.SetColor("_HorizonColor",Color.Lerp(a.horizonColor, b.horizonColor, t));
        runtimeSkybox.SetColor("_GroundColor",Color.Lerp(a.nadirColor, b.nadirColor, t));

        // ## SUN
        float sunSize = Mathf.Lerp(a.sunSize, b.sunSize, t);
        float sunIntensity = Mathf.Lerp(a.sunIntensity, b.sunIntensity, t);

        runtimeSkybox.SetFloat("_SunSize", sunSize);
        runtimeSkybox.SetFloat("_SunIntensity", sunIntensity);

        sunLight.intensity = sunIntensity;

        // ## SKYBOX CLOUDS
        runtimeSkybox.SetFloat("_CloudDensity",Mathf.Lerp(a.cloudDensity, b.cloudDensity, t));
        runtimeSkybox.SetFloat("_CloudScatter",Mathf.Lerp(a.cloudScatter, b.cloudScatter, t));
        runtimeSkybox.SetVector("_CloudSpeed",Vector2.Lerp(a.panSpeed, b.panSpeed, t));
        
        // ## WORLD-SPACE CLOUDS
        //runtimeClouds.SetFloat("_CloudsPower", Mathf.Lerp(a.cloudPower, b.cloudPower, t));
        //runtimeClouds.SetVector("_CloudSpeed", Vector2.Lerp(a.cloudSpeed, b.cloudSpeed, t));

        // ## FOG
        RenderSettings.fogColor = Color.Lerp(a.fogColor, b.fogColor, t);
        RenderSettings.fogDensity = Mathf.Lerp(a.fogDensity, b.fogDensity, t);

        // ## POST PROCESS (DUAL VOLUME BLEND)
        // activeVolume.weight = 1f - t;
        // targetVolume.weight = t;
        
        // if (t >= 1f) {
        //     // Finalize
        //     activeVolume.weight = 0f;
        //     targetVolume.weight = 1f;
        //
        //     // Swap references (ping-pong)
        //     (activeVolume, targetVolume) = (targetVolume, activeVolume);
        //
        //     // Cleanup
        //     targetVolume.profile = null;
        //     targetVolume.weight = 0f;
        // }
    }
    #endregion
    
}