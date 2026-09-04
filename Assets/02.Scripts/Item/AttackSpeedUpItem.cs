using UnityEngine;

public class AttackSpeedUpItem : Item
{
    private const float CooldownMinus = 0.1f;

    private void Start()
    {
    }

    protected override void Pickup()
    {
        PlayerFire player = _player.GetComponent<PlayerFire>();
        player.FireCooldown -= CooldownMinus;
    }
}