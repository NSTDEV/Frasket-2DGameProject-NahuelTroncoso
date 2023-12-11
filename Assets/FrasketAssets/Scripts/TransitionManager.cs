using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public GameObject transition;

    public void ShowTransition(float duration)
    {
        if (transition != null)
        {
            transition.SetActive(true);
            Invoke("HideTransition", duration);
        }
    }

    private void HideTransition()
    {
        if (transition != null)
        {
            transition.SetActive(false);
        }
    }
}