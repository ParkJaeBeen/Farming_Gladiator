using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkScript : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("drink stateExit");
        Character.instance.Move.IsPotion = false;
        Character.instance.inventory.UsePotionOrArrow(Character.instance.inventory.CurrentPotion());
        Character.instance.inventory.DeActivatePotion();
        battleFieldCanvasScript.instance.ChangeCurrentPotionImg(3);
        if (Character.instance.Move.SwordAndShield)
            Character.instance.WearEquip.ChangeActiveWeapon("SwordAndShield");
        else if (Character.instance.Move.Bow)
            Character.instance.WearEquip.ChangeActiveWeapon("Bow");

        Character.instance.Move.Heal();
        Character.instance.Move.IsDrinking = false;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
