using UnityEngine;

[System.Serializable]
public class VFXPoolEntry
{
    public int index;
    public VFXInstance prefab;
    public int poolCount = 20;

    [HideInInspector]
    public ObjectPooler<VFXInstance> pool;
}
public class VFXInstance : MonoBehaviour, IObjectPool<VFXInstance>
{
    public int poolID { get; set; }
    public ObjectPooler<VFXInstance> pool { get; set; }

    public float defaultLifeTime = 1f;

    public void Play(Vector3 position, float lifeTime)
    {
        transform.position = position;

        if (lifeTime <= 0)
            lifeTime = defaultLifeTime;

        //StartCoroutine(pool.Free(this, lifeTime));
    }
}