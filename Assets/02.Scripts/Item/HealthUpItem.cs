using UnityEngine;

public class HealthUpItem : Item
{
    private const float HealthUp = 20.0f;

    protected override void Pickup()
    {
        Player player = _player.GetComponent<Player>();

        if (player == null)
        {
            return;
        }

        Debug.Log($"플레이어 체력 : {player.Health}");
        player.Heal(HealthUp);
    }
}