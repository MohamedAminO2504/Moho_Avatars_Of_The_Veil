using UnityEngine;

public class DamageCalculator
{

    public int CalculDamage(Perso lanceur, Perso cible, int puissance){

        Debug.Log(lanceur.nom+" lance sur "+cible.nom+" un sort de puiisance "+puissance);
        return 0;
    }
}
