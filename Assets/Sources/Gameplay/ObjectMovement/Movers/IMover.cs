using UnityEngine;

public interface IMover
{
    public void Init(IMoverSettings settings, Rigidbody2D rigidBody);
    public void Move();
    public void SetMoving(bool value);
    public void SetBraking(bool value);
    public void SetEnabled(bool enabled);
}
