using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newwave", menuName = "ScriptableObjects/Subwave", order = 2)]
public class Subwave : ScriptableObject
{
    public List<GameObject> balls;
    [SerializeField]Subwave nextWave;

    public Subwave GetNextwave => nextWave;
}
