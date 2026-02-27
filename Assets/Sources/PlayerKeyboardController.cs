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

    private void Start() => KeyboardMonitorSubscribe();
    private void Update()
    {
        _playerMover.Move();
        _playerRotator.Rotate();
    }
    private void OnDestroy() => KeyboardMonitorUnsubscribe();
    private void KeyboardMonitorSubscribe()
    {
        _keyboardMonitor.ForwardButtonDown += HandleForwardDown;
        _keyboardMonitor.ForwardButtonUp += HandleForwardUp;

        _keyboardMonitor.BackwardButtonDown += HandleBackwardDown;
        _keyboardMonitor.BackwardButtonUp += HandleBackwardUp;

        _keyboardMonitor.LeftButtonDown += HandleLeftDown;
        _keyboardMonitor.LeftButtonUp += HandleLeftUp;

        _keyboardMonitor.RightButtonDown += HandleRightDown;
        _keyboardMonitor.RightButtonUp += HandleRightUp;
    }
    private void KeyboardMonitorUnsubscribe()
    {
        _keyboardMonitor.ForwardButtonDown -= HandleForwardDown;
        _keyboardMonitor.ForwardButtonUp -= HandleForwardUp;

        _keyboardMonitor.BackwardButtonDown -= HandleBackwardDown;
        _keyboardMonitor.BackwardButtonUp -= HandleBackwardUp;

        _keyboardMonitor.LeftButtonDown -= HandleLeftDown;
        _keyboardMonitor.LeftButtonUp -= HandleLeftUp;

        _keyboardMonitor.RightButtonDown -= HandleRightDown;
        _keyboardMonitor.RightButtonUp -= HandleRightUp;
    }

    private void HandleForwardDown() => _playerMover.SetMoving(true);
    private void HandleForwardUp() => _playerMover.SetMoving(false);

    private void HandleBackwardDown() => _playerMover.SetBraking(true);
    private void HandleBackwardUp() => _playerMover.SetBraking(false);

    private void HandleLeftDown() => _playerRotator.SetRotateLeft(true);
    private void HandleLeftUp() => _playerRotator.SetRotateLeft(false);

    private void HandleRightDown() => _playerRotator.SetRotateRight(true);
    private void HandleRightUp() => _playerRotator.SetRotateRight(false);
}