using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // 총 점수, 스테이지 점수, 현재 스테이지 인덱스, 플레이어, 스테이지 게임오브젝트 배열 변수
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;
    public PlayerMove player;
    public GameObject[] Stages;

    // UI 요소
    public Image[] UIHealth;
    public Text UIPoint;
    public Text UIStage;
    public GameObject RestartBtn;

    // 업데이트 함수
    private void Update()
    {
        // UI 갱신
        UIPoint.text = (totalPoint + stagePoint).ToString();
    }

    // 다음 스테이지 진행
    public void NextStage()
    {
        // 스테이지 변경
        if(stageIndex < Stages.Length - 1)
        {
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
            PlayerReposition();

            UIStage.text = "STAGE " + (stageIndex + 1).ToString();
        }
        else
        {
            // 게임 클리어
            Time.timeScale = 0;
            // 결과 UI 표시
            Debug.Log("Game Clear");
            // 재시작 버튼 UI 설정
            Text btnText = RestartBtn.GetComponentInChildren<Text>();
            btnText.text = "Game Clear";
            RestartBtn.SetActive(true);
        }

        // 점수 계산
        totalPoint += stagePoint;
        stagePoint = 0;
    }

    // 플레이어 체력 감소
    public void HealthDown()
    {
        if(health > 1)
        {
            health--;
            UIHealth[health].color = new Color(1, 0, 0, 0.4f);
        }
        else
        {
            UIHealth[0].color = new Color(1, 0, 0, 0.4f);
            // 플레이어 죽음 처리
            player.OnDie();
            // 결과 UI 표시
            Debug.Log("죽었습니다.");
            // 재시작 버튼 UI 설정
            RestartBtn.SetActive(true);
        }
    }

    // 플레이어와 충돌 시 처리
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // 플레이어 위치 재설정
            if(health > 1)
            {
                PlayerReposition();
            }
            // 체력 감소
            HealthDown();
        }
    }

    // 플레이어 위치 재설정
    void PlayerReposition()
    {
        player.transform.position = new Vector3(0, 0, 0);
        player.VelocityZero();
    }

    // 게임 재시작
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
