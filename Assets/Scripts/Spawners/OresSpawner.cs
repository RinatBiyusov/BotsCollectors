using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class OresSpawner : GenericSpawner<Ore>
{
    [SerializeField] private float _delaySpawn;
    [SerializeField] private float _radiusSpawn = 20f;
    [SerializeField] private int _maxResourceCount = 7;

    private readonly float _fullCircleRadians = Mathf.PI * 2f;

    private WaitForSeconds _waitForSeconds;
    private Vector2 _randomPositionOnCircle;

    private void Start()
    {
        _waitForSeconds = new WaitForSeconds(_delaySpawn);
        StartCoroutine(Spawn());
    }
    
    protected override void Release(Ore ore)
    {
        base.Release(ore);
        ore.Collected -= Release;
    }

    private Vector2 GetRandomPointOnCircle()
    {
        float angle = Random.Range(0f, _fullCircleRadians);
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

    private IEnumerator Spawn()
    {
        while (enabled)
        {
            if (_maxResourceCount > CountObjects())
            {
                Ore ore = TakeObject();
                ore.Collected += Release;

                ore.transform.position = GetRandomPointInCircle3D(transform.position);
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