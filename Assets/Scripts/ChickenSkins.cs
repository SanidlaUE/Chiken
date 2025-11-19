using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChickenSkins : MonoBehaviour
{
    public Animator animator;
    public RuntimeAnimatorController animation1;
    public RuntimeAnimatorController animation2;
    public RuntimeAnimatorController animation3;
    public RuntimeAnimatorController animation4;
    public RuntimeAnimatorController animation5;
    public RuntimeAnimatorController animation6;
    public RuntimeAnimatorController animation7;

    public void OnEnable()
    {
        switch (PlayerPrefs.GetInt("skinNum", 0))
        {
            case 0:
            {
                animator.runtimeAnimatorController = animation1;
                break;
            }
            case 1:
            {
                animator.runtimeAnimatorController = animation2;
                break;
            }
            case 2:
            {
                animator.runtimeAnimatorController = animation3;
                break;
            }
            case 3:
            {
                animator.runtimeAnimatorController = animation4;
                break;
            }
            case 4:
            {
                animator.runtimeAnimatorController = animation5;
                break;
            }
            case 5:
            {
                animator.runtimeAnimatorController = animation6;
                break;
            }
            case 6:
            {
                animator.runtimeAnimatorController = animation7;
                break;
            }
        }
        
    }
}
