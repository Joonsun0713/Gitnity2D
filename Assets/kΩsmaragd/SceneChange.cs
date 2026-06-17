using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    // Start is called before the first frame update

    public string SC;       // SceneChange - 씬 이동용 string
    public void Load()
    {
        string sceneName = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log(sceneName);

        if (sceneName == "Stage_2" || sceneName == "Stage_3" || sceneName == "Stage_4" || sceneName == "Try_Button")
            SC = "testSenes";   // 특정 스테이지로 가는 버튼일 경우 testScene 으로 가도록 설정
                                // 추가 수정으로 새로운 스테이지를 만들 때마다, else if 를 추가하여 각각 새로운 스테이지를 이동하게끔 수정
        else if (sceneName == "Stage_1")
            SC = "Stage1";

        else if (sceneName == "Start_Button" || sceneName == "Back_Button" || sceneName == "Stage1")
            SC = "StageSelect"; // 스테이지 밖으로 벗아날 때, 또는 타이틀 화면에서 시작 버튼을 눌렀을 때, 스테이지 선택창으로 이동

        SceneManager.LoadScene(SC);     // 여기서 이동시킬 씬의 이름을 적으면 된다.
    }

    public void Title_Go()
    {
        SceneManager.LoadScene("Main_Title");
    }

    public void GameEnd()   // End 버튼 전용으로 게임을 종료할 때 사용.
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터에서 테스트 중일 때
#else
            Application.Quit(); // 실제 빌드된 게임에서 종료할 때
#endif
    }
}
