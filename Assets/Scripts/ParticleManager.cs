using System;
using UnityEngine;

internal class ParticleManager
{
    public ParticleSystem prefab;
    public ParticleManager()
    {
        prefab = Resources.Load("BubbleExplosion", typeof(ParticleSystem)) as ParticleSystem;
    }
}

