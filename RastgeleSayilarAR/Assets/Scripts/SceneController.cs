using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void GoToSayfa1()
    {
        SceneManager.LoadScene("Sayfa1");
    }

    public void GoToSayfa2()
    {
        SceneManager.LoadScene("Sayfa2");
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
