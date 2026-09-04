using UnityEngine;

public class HealthUpItem : Item
{
    private const float HealthUp = 20.0f;

    private void Start()
    {
    }

    protected override void Pickup()
    {
        Player player = _player.GetComponent<Player>();
        player.Heal(HealthUp);
    }
}