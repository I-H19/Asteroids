using TMPro;
using UnityEngine;

public class TEMP_DEBUG_SCRIPT : MonoBehaviour
{
    public TextMeshProUGUI DebugText;
    public GameObject Player;
    private Rigidbody2D _playerRigidBody;

    private void Start()
    {
        _playerRigidBody = Player.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        DebugText.text = $"RigidBody pos: {_playerRigidBody.position} \n" +
            $"Transform pos: {_playerRigidBody.gameObject.transform.position}\n" +
            $"Screen W/H: {Camera.main.pixelWidth}:{Camera.main.pixelHeight} \n";

    }
}
