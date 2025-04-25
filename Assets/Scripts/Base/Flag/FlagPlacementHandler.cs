using UnityEngine;

public class FlagPlacementHandler : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Flag _flagPrefab;
    [SerializeField] private PlayerInput _playerInput;

    private Base _currentBase;
    private bool _isPlacingFlag = false;

    private void OnEnable()
    {
        _playerInput.LeftButtonClicked += TryPlaceFlag;
    }

    private void OnDisable()
    {
        _playerInput.LeftButtonClicked -= TryPlaceFlag;
    }

    public void PlaceFlag(Base targetBase)
    {
        _currentBase = targetBase;
        _isPlacingFlag = true;
    }

    private void TryPlaceFlag()
    {
        if (_isPlacingFlag == false)
            return;

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
            }
        }
    }
}