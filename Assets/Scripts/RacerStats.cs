/*
 * Marlow Greenan
 * Created: 11/20/2026
 * Last Updated: 11/20/2026 by Marlow Greenan
 * 
 * Holds stats for racers.
 */
using NaughtyAttributes;
using UnityEngine;

[System.Serializable]
public class RacerStats
{
    [SerializeField, MinValue(0)]
    private float _speed = 1;

    [SerializeField, MinValue(0)]
    private float _acceleration = 1;

    [SerializeField, MinValue(0)]
    private float _stamina = 1;

    [SerializeField, MinValue(0)]
    private float _balance = 1;

    [SerializeField, MinValue(0)]
    private float _discipline = 1;

    [SerializeField, MinValue(0)]
    private float _charisma = 1;
}
