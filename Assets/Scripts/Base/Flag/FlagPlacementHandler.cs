using UnityEngine;

public class FlagPlacementHandler : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Flag _flagPrefab;

    private Base _currentBase;
    private bool _isPlacingFlag = false;

    private void Update()
    {
        if (_isPlacingFlag == false)
            return;

        if (Input.GetMouseButtonDown(1))
            CancelPlacement();

        if (Input.GetMouseButtonDown(0))
            TryPlaceFlag();
    }

    public void StartPlacingFlag(Base targetBase)
    {
        _currentBase = targetBase;
        _isPlacingFlag = true;
    }

    private void TryPlaceFlag()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.TryGetComponent(out Ground ground))
            {
                Vector3 position = hitInfo.point;

                if (_currentBase.CurrentFlag == null)
                {
                    if (_currentBase.CanBuildBase())
                    {
                        Flag newFlag = Instantiate(_flagPrefab, position, Quaternion.identity);
                        _currentBase.SetFlag(newFlag);
                        
                        _isPlacingFlag = false;
                        _currentBase.SendWorkerToCreateNewBase();
                    }
                }
                else
                    _currentBase.CurrentFlag.MoveFlag(position);
            }
        }
    }

    private void CancelPlacement() =>
        _isPlacingFlag = false;
}