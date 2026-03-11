using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Card", menuName = "Card/Card")]
public class Card : ScriptableObject
{
    public string nom;
    public int numero;
    public int ordre;
    public Sprite image;
    public Sprite model;
    public GameObject m3d;
    public GameObject donjon;
    public Portrait portrait;
    public TypeCard type;
    public CardVisualiseAR cardAR;

    public Caracteristique cara;

    public Spells spells;

    public List<Model3D> models3d;

    public Card where;

    public bool active = true;
    public bool isPending = false;

    public Tenue currentTenue;

    public List<Tenue> tenues;
    public Model3D getModel3d(TypeModel3d type){
            foreach(Model3D m in models3d){
                if(m.type == type){
                    return m;
                }
            }
            return null;
        }
    public void ChangeM3d(TypeModel3d type){
        foreach(Model3D m in models3d){
            if(m.type == type){
                this.m3d = m.model;
                return;
            }
        }
    }

    public string DisplayCara(){
        return this.name + " : "
        + this.cara.hp + " hp "
        + this.cara.atk + " atk "
        + this.cara.def + " def "
        + this.cara.vit + " vit";
    }
}

public enum TypeCard{
    PERSONNAGE,LIEU,PNJ,BESTIAIRE,INFO,DONJON, SPECIAL
}

[System.Serializable]
public struct Caracteristique{
    public int hp;
    public int hpMax;
    public int atk;
    public int def;
    public int vit;
    public int mag;
    public int esp;
    public string name;
}

[System.Serializable]
public class Spells{
    public Spell spell1;
    public Spell spell2;
    public Spell spell3;

}

[System.Serializable]
public class Model3D{
    public TypeModel3d type;
    public GameObject model;
    public Sprite image;
    public Sprite portrait;
}

public enum TypeModel3d{
    COMBAT,UNIFORME,CIVIL
}

[System.Serializable]
public class Portrait{
    public Sprite basic;
    public Sprite head_talk;
    public Sprite head_battle;
}