using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ResourcesSpawner : GenericSpawner<Resource>
{
    [SerializeField] private float _delaySpawn;
    [SerializeField] private float _radiusSpawn = 20f;
    [SerializeField] private int _maxResourceCount = 7;

    private WaitForSeconds _waitForSeconds;
    private Vector2 _randomPositionOnCircle;
    
    private void Start()
    {
        _waitForSeconds = new WaitForSeconds(_delaySpawn);
        StartCoroutine(Spawn());
    }

    private Vector2 GetRandomPointOnCircle()
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);
        float distance = Random.Range(0f, _radiusSpawn);

        float x = Mathf.Cos(angle) * distance;
        float y = Mathf.Sin(angle) * distance;

        return new Vector2(x, y);
    }

    private Vector3 GetRandomPointInCircle3D(Vector3 center)
    {
        Vector2 point2D = GetRandomPointOnCircle();
        return new Vector3(center.x + point2D.x, center.y, center.z + point2D.y);
    }

    protected override void Release(Resource resource)
    {
        base.Release(resource);
        resource.Collected -= Release;
    }

    private IEnumerator Spawn()
    {
        while (enabled)
        {
            if (_maxResourceCount > CountObjects())
            {
                Resource resource = TakeObject();
                resource.Collected += Release;
                
                resource.transform.position = GetRandomPointInCircle3D(transform.position);
            }

            yield return _waitForSeconds;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _radiusSpawn);
    }
}