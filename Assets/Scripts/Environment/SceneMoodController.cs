using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public enum SceneMoodType
{
    Normal,
    Combat,
    Danger
}

[System.Serializable]
public class SceneMoodSetting
{
    public SceneMoodType moodType;
    public Color lightColor = Color.white;  // Directional Light의 색.
    public float lightIntensity = 1.0f; // Directional Light의 세기.
    public float postExposure = 0.0f;   // 화면 전체 밝기 보정.
    public float contrast = 0.0f;   // 명암 대비.
    public Color colorFilter = Color.white; // 화면 전체 색감.
}

public class SceneMoodController : MonoBehaviour
{
    [SerializeField] private Light directionalLight;
    [SerializeField] private Volume globalVolume;
    [SerializeField] private SceneMoodType defaultMood = SceneMoodType.Normal;

    [SerializeField] private SceneMoodSetting[] moodSettings;

    [SerializeField] private float blendSpeed = 4.0f;   // 값이 목표 분위기로 바뀌는 속도.
    private ColorAdjustments colorAdjustments;
    private SceneMoodSetting currentSetting;

    private void Awake()
    {
        CacheVolumeOverrides();
        ApplyMood(defaultMood, true);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentSetting == null)
        {
            return;
        }

        float t = Time.deltaTime * blendSpeed;

        UpdateDirectionalLight(t);
        UpdateColorAdjustments(t);
    }

    void ApplyMoodImmediately(SceneMoodSetting setting)
    {
        if(directionalLight != null)
        {
            directionalLight.color = setting.lightColor;
            directionalLight.intensity = setting.lightIntensity;
        }
    }

    void UpdateColorAdjustments(float t)
    {
        if(colorAdjustments == null)
        {
            return;
        }

        colorAdjustments.postExposure.value = Mathf.Lerp(colorAdjustments.postExposure.value, currentSetting.postExposure, t);

        colorAdjustments.contrast.value = Mathf.Lerp(colorAdjustments.contrast.value,
            currentSetting.contrast, t);

        colorAdjustments.colorFilter.value = Color.Lerp(colorAdjustments.colorFilter.value, currentSetting.colorFilter, t);
    }

    void UpdateDirectionalLight(float t)
    {
        if(directionalLight == null)
        {
            return;
        }

        directionalLight.color = Color.Lerp(directionalLight.color, currentSetting.lightColor, t);

        directionalLight.intensity = Mathf.Lerp(directionalLight.intensity, currentSetting.lightIntensity, t);
    }

    SceneMoodSetting FindSetting(SceneMoodType moodType)
    {
        foreach(SceneMoodSetting setting in moodSettings)
        {
            if(setting != null && setting.moodType == moodType)
            {
                return setting;
            }
        }

        return null;
    }

    void CacheVolumeOverrides()
    {
        if(globalVolume == null || globalVolume.profile == null)
        {
            return;
        }

        globalVolume.profile.TryGet(out colorAdjustments);
    }

    public void ApplyMood(SceneMoodType moodType, bool immediate = false)
    {
        SceneMoodSetting setting = FindSetting(moodType);

        if(setting != null)
        {
            currentSetting = setting;

            if(immediate == true)
            {
                ApplyMoodImmediately(setting);
            }
        }
    }
}
