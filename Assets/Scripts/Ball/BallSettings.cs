using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSettings : MonoBehaviour
{
    public float BallDestroyDelay { get; private set; } = 0.0f;
    public float Speed { get; private set; }
    public int RewardPoint { get; private set; }
    public int Damage { get; private set; }
    public int Health { get; set; }
    public Color Color { get; private set; }

    private float progressingDifficult = 1;

    void Awake()
    {
        Health = Random.Range(1, 3);
        Speed = 1 * progressingDifficult;
        Color = Random.ColorHSV();
        RewardPoint = Random.Range(1, 10);
        Damage = Random.Range(1, 10);
        gameObject.GetComponent<Renderer>().material.color = Color;
    }
    public void IncreaseDifficult(float increaseValue, float defaultDifficult = 1)
    {
        if (increaseValue > 0)
            Speed = defaultDifficult + increaseValue;
    }
}
