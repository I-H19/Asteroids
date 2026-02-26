using UnityEngine;

public interface IMover
{
    public void Move();
    public void SetMoving(bool value);
    public void SetBraking(bool value);
}
