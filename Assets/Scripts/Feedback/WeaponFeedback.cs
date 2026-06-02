using UnityEngine;

/// <summary>
/// 무기 발사와 명중에 필요한 시각/청각 피드백을 담당하는 역할.
/// </summary>
public class WeaponFeedback : MonoBehaviour
{
    [SerializeField] private GameObject muzzleFlashEffect;
    [SerializeField] private ParticleSystem[] particles;

    [SerializeField] private GameObject defaultHitEffectPrefab;
    [SerializeField] private GameObject enemyHitEffectPrefab;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip fireClip;

    [SerializeField] private AudioClip defaultHitClip;
    [SerializeField] private AudioClip enemyHitClip;

    [SerializeField] private float hitEffectLifeTime = 2.0f;

    private void Awake()
    {
        if(audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if(muzzleFlashEffect != null)
        {
            particles = GetComponentsInChildren<ParticleSystem>();

            muzzleFlashEffect.SetActive(false);
        }
    }

    public void PlayFireFeedback()
    {
        if(muzzleFlashEffect != null)
        {
            muzzleFlashEffect.SetActive(true);
            if (particles != null)
            {
                for(int i=0; i<particles.Length; ++i)
                {
                    particles[i].Stop();
                    particles[i].Play();
                }
            }
        }

        PlayClip(fireClip);

        Invoke("HideMuzzleFlashEffect", 0.2f);
    }

    void HideMuzzleFlashEffect()
    {
        if(muzzleFlashEffect != null)
        {
            muzzleFlashEffect.SetActive(false);
        }
    }

    public void PlayHitFeedback(RaycastHit hitInfo, bool hitEnemy)
    {
        GameObject hitEffectPrefab = GetHitEffectPrefab(hitEnemy);
        AudioClip hitClip = GetHitClip(hitEnemy);

        SpawnHitEffect(hitInfo, hitEffectPrefab);
        PlayClip(hitClip);
    }

    GameObject GetHitEffectPrefab(bool hitEnemy)
    {
        if(hitEnemy == true && enemyHitEffectPrefab != null)
        {
            return enemyHitEffectPrefab;
        }

        return defaultHitEffectPrefab;
    }

    AudioClip GetHitClip(bool hitEnemy)
    {
        if(hitEnemy == true && enemyHitClip != null)
        {
            return enemyHitClip;
        }

        return defaultHitClip;
    }

    void SpawnHitEffect(RaycastHit hitInfo, GameObject hitEffectPrefab)
    {
        if(hitEffectPrefab == null)
        {
            return;
        }

        Vector3 effectPosition = hitInfo.point + hitInfo.normal * 0.02f;
        Quaternion effectRotation = Quaternion.LookRotation(hitInfo.normal);

        GameObject createdEffect = Instantiate(hitEffectPrefab, effectPosition, effectRotation);

        ParticleSystem[] particles = createdEffect.GetComponentsInChildren<ParticleSystem>();
        if(particles != null)
        {
            for(int i=0; i<particles.Length; ++i)
            {
                particles[i].Play();
            }
        }

        Destroy(createdEffect, hitEffectLifeTime);
    }

    void PlayClip(AudioClip clip)
    {
        if(audioSource == null)
        {
            return;
        }

        if(clip == null)
        {
            return;
        }

        audioSource.PlayOneShot(clip);
    }
}
