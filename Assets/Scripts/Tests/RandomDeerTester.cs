using NaughtyAttributes;
using UnityEngine;

public class RandomDeerTester : MonoBehaviour
{
    [SerializeField] private DeerData randomDeer;

    [Button]
    private void RandomizeDeer()
    {
        randomDeer = DeerData.NewRandomDeer();
    }
}
