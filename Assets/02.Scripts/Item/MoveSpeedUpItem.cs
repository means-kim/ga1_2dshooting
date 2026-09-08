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

        Debug.Log($"플레이어 이동 속도: {player.SppedStep}");
        player.MoveSpeedUp(MoveSpeedItem);
    }
}