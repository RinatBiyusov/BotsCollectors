using UnityEngine.Pool;
using UnityEngine;

public class GenericSpawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _maxPoolCapacity;

    private ObjectPool<T> _pool;
    
    private void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (T) =>
            {
                T.gameObject.SetActive(true);
                ActionOnGet(T);
            },
            actionOnRelease: (ground) => ground.gameObject.SetActive(false),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _maxPoolCapacity
        );
    }

    protected void GetObject() =>
        _pool.Get();

    protected T TakeObject() =>
        _pool.Get();

    protected virtual void Release(T t) =>
        _pool.Release(t);

    protected virtual void ActionOnGet(T t)
    {
    }

    protected int CountObjects() =>
        _pool.CountActive;

    private void OnDestroy()
    {
        _pool.Clear();
    }
}