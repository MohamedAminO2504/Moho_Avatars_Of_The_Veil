using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Tenue", menuName = "Card/Tenue")]
public class Tenue : ScriptableObject
{
    public string nom;
    public Card personnage;
    public Sprite preview;
    public GameObject m3d;
    public Caracteristique caracteristique;
    public Spell competence;
    public Sprite portrait;

}
