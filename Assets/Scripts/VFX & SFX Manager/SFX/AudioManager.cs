using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] int poolCount = 20;
    [SerializeField] AudioInstance audioPrefab;

    ObjectPooler<AudioInstance> pooler;

    private void Awake()
    {
        Instance = this;

        pooler = new ObjectPooler<AudioInstance>();
        pooler.Initialize(this, poolCount, audioPrefab, transform);
    }

    public void PlaySFX(AudioClip clip, float volume)
    {
        AudioInstance audio = pooler.GetNew();
        audio.Play(clip, volume);
    }
}