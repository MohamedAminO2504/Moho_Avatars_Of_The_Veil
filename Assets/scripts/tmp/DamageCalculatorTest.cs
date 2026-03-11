using UnityEngine;
using System.Collections.Generic;

public class DamageCalculatorTest : MonoBehaviour
{

    private Perso Amin = new Perso();
    private Perso Hitomi = new Perso();
    private Perso Emy = new Perso();
    private Perso Epouvantail = new Perso();
    private Perso Zombie = new Perso();

    void Start()
    {
        InitPerso();

        DamageCalculator dc = new DamageCalculator();

        List<Perso> persos = new List<Perso>() { Amin, Hitomi, Emy };
        List<Perso> ennemys = new List<Perso>() { Epouvantail, Zombie };
        List<Perso> all = new List<Perso>(persos);
        all.AddRange(ennemys);
        bool isTurnPerso = true;

        for (int i = 0; i < 10; i++)
        {
            List<Perso> personnages = isTurnPerso ? persos : ennemys;
            List<Perso> autres = isTurnPerso ? ennemys : persos;

            int index = Random.Range(0, personnages.Count);
            int index2 = Random.Range(0, autres.Count);

            Perso perso = personnages[index];
            Perso autre = autres[index2];

            isTurnPerso = !isTurnPerso;

            dc.CalculDamage(perso, autre, Random.Range(10, 25));
        }

         for (int i = 0; i < 10; i++)
        {
            int index = Random.Range(0, all.Count);
            int index2 = Random.Range(0, all.Count);

            Perso perso = all[index];
            Perso autre = all[index2];

            dc.CalculDamage(perso, autre, Random.Range(10, 25));
        }
    }

    public void InitPerso()
    {
        Amin.nom = "Amin";
        Amin.stats = new Stats
        {
            hp = 120,
            hpMax = 120,
            atk = 18,
            def = 12,
            vit = 14,
            mag = 10,
            esp = 11
        };

        Hitomi.nom = "Hitomi";
        Hitomi.stats = new Stats
        {
            hp = 95,
            hpMax = 95,
            atk = 12,
            def = 10,
            vit = 16,
            mag = 18,
            esp = 15
        };

        Emy.nom = "Emy";
        Emy.stats = new Stats
        {
            hp = 100,
            hpMax = 100,
            atk = 14,
            def = 11,
            vit = 17,
            mag = 14,
            esp = 12
        };

        Epouvantail.nom = "Epouvantail";
        Epouvantail.stats = new Stats
        {
            hp = 130,
            hpMax = 130,
            atk = 16,
            def = 14,
            vit = 8,
            mag = 6,
            esp = 10
        };

        Zombie.nom = "Zombie";
        Zombie.stats = new Stats
        {
            hp = 90,
            hpMax = 90,
            atk = 11,
            def = 9,
            vit = 12,
            mag = 13,
            esp = 11
        };
    }

}

public enum TypePerso{
    PERSONNAGE,BESTIAIRE
}

public class Perso {
    public string nom;
    public Stats stats;
    public TypePerso type;
}

[System.Serializable]
public struct Stats{
    public int hp;
    public int hpMax;
    public int atk;
    public int def;
    public int vit;
    public int mag;
    public int esp;
}