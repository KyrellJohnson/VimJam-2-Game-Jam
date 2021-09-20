using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject popUpBox;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private TMP_Text popUpText;

    public void PopUp(string text)
    {
        popUpBox.SetActive(true);
        popUpText.text = text;
        animator.SetTrigger("pop");
    }

    public void PopUpSetTrigger(string str)
    {
        animator.SetTrigger(str);
    }

}
