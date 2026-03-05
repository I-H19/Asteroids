using System;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public int Score { get; private set; } = 0;
    public Action Changed;
    public void Increment()
    {
        Score++;
        Changed?.Invoke();
    }
    public void ResetScore()
    {
        Score = 0;
        Changed?.Invoke();
    }
}
