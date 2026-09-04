using UnityEngine;

public class MoveSpeedUpItem : Item
{
    private const float MoveSpeedItem = 1.0f;

    private void Start()
    {
    }

    protected override void Pickup()
    {
        PlayerMove player = _player.GetComponent<PlayerMove>();
        player.Speed += MoveSpeedItem;
    }
}