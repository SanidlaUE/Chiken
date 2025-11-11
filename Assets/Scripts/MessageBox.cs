using System.Collections;
using TMPro;
using UnityEngine;

public class MessageBox : MonoBehaviour
{
    public TMP_Text dialogText;
        public GameObject body;
        public float showingTime;
    
        public void ShowMessage(string message)
        {
            dialogText.text = message;
            StartCoroutine(ShowMessageRoutine());
        }
    
        private IEnumerator ShowMessageRoutine()
        {
            body.SetActive(true);
            yield return new WaitForSeconds(showingTime);
            body.SetActive(false);
        }
}
