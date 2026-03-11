using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Scriptable Objects/Spell")]
public class Spell : ScriptableObject
{
    public GameObject m3d;

    public int power = 10;

    public TypeCible cible;

    public Vector3 decalage;
}

public enum TypeCible{
    ENNEMIE, ALLIE, SELF
}
