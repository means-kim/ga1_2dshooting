using UnityEngine;

public class MoveSpeedUpItem : Item
{
    private const float MoveSpeedItem = 1.0f;

    protected override void Pickup()
    {
        PlayerMove player = _player.GetComponent<PlayerMove>();

        if (player == null)
        {
            return;
        }

        player.Speed += MoveSpeedItem;
    }
}