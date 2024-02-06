using MyBox;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [Scene]
    public string m_SceneName;
    public void OnBtnClick() => SceneManager.LoadScene(m_SceneName);
}

