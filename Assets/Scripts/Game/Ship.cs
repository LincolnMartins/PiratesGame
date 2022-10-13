using UnityEngine;

[CreateAssetMenu]
public class Ship : ScriptableObject
{
    public float ramDamage;
    public float shootDamage;
    public float maxHealth;
    public string shipType;
    public Sprite[] shipStates;
}
