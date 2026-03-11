using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CreateAssetMenu(fileName = "Data", menuName = "Data")]
public class Data : ScriptableObject
{
    public Card firstCard;
    public List<Card> cards;
    public List<string> variables;
    public List<Tenue> tenuesFound;
    public List<Card> equipes;


    public string GetAllVariable(){
        string res = "";
        foreach(string v in variables){
            res += " "+v;
        }
        return res;
    }
}
