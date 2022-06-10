using UnityEngine;

public class BallSettings
{
    public float BallDestroyDelay { get; private set; } = 0.0f;
    public float Speed { get; private set; }
    public int RewardPoint { get; private set; }
    public int Damage { get; private set; }
    public int Health { get; set; }
    public Color Color { get; private set; }
    public BallSettings()
    {
        Health = Random.Range(1, 3);
        Speed = 1;
        Color = Random.ColorHSV();
        RewardPoint = Random.Range(1, 10);
        Damage = Random.Range(1, 10);
        //renderer.material.color = Color;
        IncreaseDifficult(Time.timeSinceLevelLoad / 50.0f);
    }
    public void IncreaseDifficult(float increaseValue, float defaultDifficult = 1)
    {
        if (increaseValue > 0)
            Speed = defaultDifficult + increaseValue;
    }
}
