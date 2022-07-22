using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(loadAsynchronously(sceneIndex));
    }

    IEnumerator loadAsynchronously(int sceneIndex)
    {

        float timer = 0f;
        bool isCounted = true;
        //slider.gameObject.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            //slider.value = progress;
            timer += Time.deltaTime;

            if (progress >= 1 && isCounted)
            {
                
                isCounted = false;
            }
            yield return null;
        }


        //Destroy(gameObject);
        //NextSceneLoad();
    }
}
