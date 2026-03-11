using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.VFX;
using System;

public class Caractere : MonoBehaviour
{
    public Card card;
    public Animator animator;
    public List<GameObject> armes;
    public Transform target;
    public Transform head;
    public bool rotateBody = true;
    public bool useHead = false;
    public CardDisplay cardDisplay;
    private Vector3 runTargetPosition;
    public bool isRunning = false;
    public float runSpeed = 100f;
    public BattelManager battleManager;

    public float stopDistance = 0.05f;

    public UNI_Materialize uni_Materialize;
    public VisualEffect visualEffect;
    private Action onRunComplete;
    public Caracteristique cara;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      if (animator && animator.isHuman)
        {
            head = animator.GetBoneTransform(HumanBodyBones.Head);
        }
    }

    public void ActiveCard(){
        card.active = true;
    }


   void Update()
       {
       // ===== RUN SYSTEM =====
       if (isRunning)
       {
           Vector3 runDirection  = runTargetPosition - transform.position;
           runDirection .y = 0;

           if (runDirection.magnitude <= stopDistance)
           {
               transform.position = new Vector3(runTargetPosition.x, transform.position.y, runTargetPosition.z);

               isRunning = false;

               if (animator != null)
                   animator.SetBool("IsRunning", false);

               // 🔥 Call callback
               onRunComplete?.Invoke();
               onRunComplete = null;

               return;
           }

           // Rotation vers la cible
           Quaternion runRotation  = Quaternion.LookRotation(runDirection);
           transform.rotation = Quaternion.RotateTowards(
               transform.rotation,
               runRotation ,
               500f * Time.deltaTime
           );

           // Déplacement
           transform.position += runDirection.normalized * runSpeed * Time.deltaTime;

           return;
       }

        if (!rotateBody || target == null)
                   return;

               // Direction vers la cible
               Vector3 direction = target.position - transform.position;

               // On ignore la hauteur (sinon il se penche)
               direction.y = 0;

               if (direction.sqrMagnitude < 0.001f)
                   return;

               Quaternion targetRotation = Quaternion.LookRotation(direction);

              float maxTurnSpeed = 150f; // degrés par seconde

              transform.rotation = Quaternion.RotateTowards(
                  transform.rotation,
                  targetRotation,
                  maxTurnSpeed * Time.deltaTime
              );

       }
    [Range(0,1)]
    public float weight = 1f;

    public void RunToAndCallBack(Transform parent, Action callback)
    {
        onRunComplete = callback;
        RunTo(parent, Vector3.zero);
    }
    public void RunTo(Transform parent, Vector3 decalage)
    {
        if(decalage == null){
            decalage = Vector3.zero;
        }
        runTargetPosition = parent.position + decalage;
        isRunning = true;

        if (animator != null)
            animator.SetBool("IsRunning", true);
    }


    void OnAnimatorIK(int layerIndex)
    {
        if (animator && target != null && useHead)
        {
            animator.SetLookAtWeight(weight, 0.3f, 0.6f, 1f, 0.5f);
            animator.SetLookAtPosition(target.position);
        }
    }

    public void Look(Transform t, bool body, bool head){
        rotateBody = body;
        useHead = head;
        target = t;

    }

       public void Look(Caractere t, bool body, bool head){
            rotateBody = body;
            useHead = head;
            this.target = t.head;

        }

       public void ResetLook(){
            this.target = null;
            transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);

       }

    public void IsDead(){
        if(card.type == TypeCard.BESTIAIRE){
            Destroy(this.gameObject);
        }
    }

    public void EnterCombat()
    {
        foreach(GameObject arme in armes){
            arme.SetActive(true);
        }
        animator.SetBool("IsInCombat", true);

    }

    public IEnumerator ExitCombat()
    {
      foreach(GameObject arme in armes){
                arme.SetActive(false);
            }
        this.transform.SetParent(cardDisplay.transform);
        animator.SetBool("IsInCombat", false);
        yield return new WaitForSeconds(0.4f);
        RunToAndCallBack(cardDisplay.transform, () =>
       {
               StartCoroutine(battleManager.TryToEndBattle());

       });
    }

    public void DoAttack()
    {
        animator.SetTrigger("Attack");
    }

    public void DoSkill()
    {
        animator.SetTrigger("Skill");
    }

    public void TakeHit()
    {
        animator.SetTrigger("Hit");
    }

    public void Die()
    {
        animator.SetTrigger("Die");
    }

}
