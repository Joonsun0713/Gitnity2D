using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    // Start is called before the first frame update

    public string SC;       // SceneChange - 씬 이동용 string
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Load()
    {
        SceneManager.LoadScene(SC);     // 여기서 이동시킬 씬의 이름을 적으면 된다.
    }

    public void GameEnd()
    {
        return;
    }
}
