using UnityEngine;

public class AttackSpeedUpItem : Item
{
    private const float CooldownMinus = 0.1f;

    protected override void Pickup()
    {
        PlayerFire player = _player.GetComponent<PlayerFire>();

        if (player == null)
        {
            return;
        }

        Debug.Log($"쿨타임 : {player.CooldownFire}");
        player.Cooldown(CooldownMinus);
    }
}