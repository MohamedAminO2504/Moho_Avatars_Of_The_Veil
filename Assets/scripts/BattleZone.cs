using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleZone : MonoBehaviour
{
    public List<Transform> persoPositions;

    public List<Transform> enemyPositions;

    public Animator animator;

    public void Eleve(){
        animator.SetTrigger("Eleve");

    }

    public void Down(){
            animator.SetTrigger("Down");

    }
}
