using UnityEngine;
using UnityEngine.SceneManagement;


public class TitleScreen : MonoBehaviour
{
    public void OnClickBtn()
    {
        TitleManager.Instance.NextStage();
    }
}
