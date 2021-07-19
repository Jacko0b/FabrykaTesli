using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject menuIngame;

    public void openMenuIngame()
    {
        if (Time.timeScale == 1)
        {
            //game pause
            Time.timeScale = 0;
            menuIngame.SetActive(true);
        }
        else
        {
            //unpause
            Time.timeScale = 1;
            menuIngame.SetActive(false);
        }
    }

}
