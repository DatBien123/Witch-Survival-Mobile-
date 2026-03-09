using UnityEngine;

public class AudioInstance : MonoBehaviour, IObjectPool<AudioInstance>
{
    public int poolID { get; set; }
    public ObjectPooler<AudioInstance> pool { get; set; }

    AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void Play(AudioClip clip, float volume)
    {
        source.clip = clip;
        source.volume = volume;
        source.Play();

        StartCoroutine(pool.Free(this, clip.length));
    }
}