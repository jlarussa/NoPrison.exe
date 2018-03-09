using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

  [SerializeField]
  private string sceneName = string.Empty;

  [SerializeField]
  private GameObject loadingImage = null;

  void Start()
  {
    if (loadingImage != null)
    {
      loadingImage.SetActive(false);
    }
  }

  public virtual void LoadLevel()
  {
    if (loadingImage != null)
    {
      loadingImage.SetActive(true);
    }
    if (sceneName != string.Empty)
    {
      SceneManager.LoadScene(sceneName);
    }
  }
}
