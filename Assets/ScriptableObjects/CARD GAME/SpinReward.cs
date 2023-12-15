using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/FreeSpin")]
public class SpinReward : ScriptableObject
{
    public List<float> _weightedValueList;
}