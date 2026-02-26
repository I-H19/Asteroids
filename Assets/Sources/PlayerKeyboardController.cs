using UnityEngine;
using VContainer;

public class PlayerKeyboardController : MonoBehaviour
{
    private DirectionalRotator _playerRotator;
    private InertialMover _playerMover;
    private PlayerMovementSettings _playerSettings;
    private KeyboardMonitor _keyboardMonitor;

    [Inject]
    private void Construct(InertialMover mover, DirectionalRotator rotator, PlayerMovementSettings playerSettings, KeyboardMonitor keyboardMonitor)
    {
        _playerSettings = playerSettings;
        _keyboardMonitor = keyboardMonitor;
        _playerMover = mover;
        _playerRotator = rotator;
    }

    private void Start()
    {
        KeyboardMonitorSubscribe();
    }
    private void Update()
    {
        _playerMover.Move();
        _playerRotator.Rotate();
    }
    private void OnDestroy() => KeyboardMonitorUnsubscribe();

    private void KeyboardMonitorSubscribe()
    {
        _keyboardMonitor.ForwardButtonDown += () => _playerMover.SetMoving(true);
        _keyboardMonitor.ForwardButtonUp += () => _playerMover.SetMoving(false);

        _keyboardMonitor.BackwardButtonDown += () => _playerMover.SetBraking(true);
        _keyboardMonitor.BackwardButtonUp += () => _playerMover.SetBraking(false);

        _keyboardMonitor.LeftButtonDown += () => _playerRotator.SetRotateLeft(true);
        _keyboardMonitor.LeftButtonUp += () => _playerRotator.SetRotateLeft(false);

        _keyboardMonitor.RightButtonDown += () => _playerRotator.SetRotateRight(true);
        _keyboardMonitor.RightButtonUp += () => _playerRotator.SetRotateRight(false);
    }
    private void KeyboardMonitorUnsubscribe()
    {
        _keyboardMonitor.ForwardButtonDown -= () => _playerMover.SetMoving(true);
        _keyboardMonitor.ForwardButtonUp -= () => _playerMover.SetMoving(false);

        _keyboardMonitor.BackwardButtonDown -= () => _playerMover.SetBraking(true);
        _keyboardMonitor.BackwardButtonUp -= () => _playerMover.SetBraking(false);

        _keyboardMonitor.LeftButtonDown -= () => _playerRotator.SetRotateLeft(true);
        _keyboardMonitor.LeftButtonUp -= () => _playerRotator.SetRotateLeft(false);

        _keyboardMonitor.RightButtonDown -= () => _playerRotator.SetRotateRight(true);
        _keyboardMonitor.RightButtonUp -= () => _playerRotator.SetRotateRight(false);
    }
}