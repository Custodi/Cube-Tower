using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class MetaWaveList : ScriptableObject
{
    [SerializeField]
    public MetaWave[] _metaWaveCollection;

    [Serializable]
    public class MetaWave
    {
        [Tooltip("Offer timer, after that a new wave is released")]
        public float offerDuration;
        [Tooltip("Delay time after wave releasing to show up next offer")]
        public float nextOfferTime;
        public float offerReward; // Reward for early releasing new wave
    }
}
