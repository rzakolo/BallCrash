using System;
using UnityEngine;
using static UnityEngine.ParticleSystem;

internal class ParticleManager
{
    public ParticleSystem prefab;
    public MinMaxGradient StartColor
    {
        get { return prefab.main.startColor; }
        set
        {
            var main = prefab.main;

            main.startColor = value;
        }
    }

    public ParticleManager()
    {
        prefab = Resources.Load("Death", typeof(ParticleSystem)) as ParticleSystem;
    }
}

