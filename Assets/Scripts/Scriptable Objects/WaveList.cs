using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class WaveList : ScriptableObject
{
    [SerializeField]
    public Wave[] _wavesList;

    [Serializable]
    public class Wave
    {
        public SubWave[] subWaves;
    }

    [Serializable]
    public class SubWave
    {
        public int id;
        public int count;
    }
}
