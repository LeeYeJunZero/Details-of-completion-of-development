소스코드 내용





















<html>
    <head>
    </head>
        <body>
public class Logic_Test : MonoBehaviour
{

    public Text creditVal, abilityVal, abilityCostVal, employeeVal, employeeCostVal; //외부에서 txt창과 연결

    float credit; //코인 또는 크레딧 

    //Ability 관련 변수 

    float ability, abilityCost;


    //Employee 관련 변수

    float employee, employeeCost, employeeSpeed;


    // Start is called before the first frame update

    void Start()

    {

        credit = 100; // 시작시 크레딧을 0으로 초기화, 100 대입

        ability = 0; // 기본적으로는 0의값을 가짐

        abilityCost = 100; //credit을 100을 모아야 능력을 추가 가능

        employee = 0; //일꾼 0명

        employeeCost = 500; //일꾼 고용비용

        employeeSpeed = 0.01f; // 스피드 제한

    }



    

    // Update is called once per frame

    void Update()

    {

        EmployeeDoJob(); // 함수를 생성

        View_Status(); // 계속적으로 상태를 화면에 보여주기 위해 직접 정의한 함수

    }



    void View_Status() // 상태창

    {

        creditVal.text = ((int)credit).ToString(); .

        abilityVal.text = ability.ToString(); // 능력이 얼마 인가

        abilityCostVal.text = abilityCost.ToString(); // 능력 구매에 필요한 비용 


        employeeVal.text = employee.ToString(); //고용인수 

        employeeCostVal.text = employeeCost.ToString(); //고용비용


    }



    public void Cheat_Btn()

    {

        credit = credit + 100000; //Cheat 버튼 생성 후 연결

    }





    public void Click_Btn()

    {

        //credit++;

        credit = credit + 1 + ability;  //능력까지 같이 credit에 더해줌 현재는 능력이 0이니 1만 증가.

        //버튼 을 누르면 credit 이 증가

        Debug.Log("버튼 클릭 시 돈증가");

    }





    public void Ability_Btn()

    {

      
        if(credit-abilityCost>=0)

        {

            

            credit = credit - abilityCost; //위에서는 실제로 뺀값을 credit에 넣은게 아니기때문에 여기서 credit값을 실제로 뺀값으로 저장해줘야함



            abilityCost = abilityCost * 2; //비용이 계속적으로 2배씩 증가하기로 한다.(플레이타임 증가+ 점진적 난이도 향상)



            ability = ability + 5; // 능력의 갯수



        }

    }



    public void Employee_Btn() 

    {

        if (credit - employeeCost>=0) 
        {



            credit = credit - employeeCost;

            employeeCost = employeeCost * 2; 

            employee = employee + 1;

        }

    }



    void EmployeeDoJob()

    {

        credit = credit + employee * employeeSpeed;

    }



}



<hr>
//vr게임 영역 문을 바라보면 문이 열리는 기능 구성

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Script : MonoBehaviour
{

    public void GvrOn(){
      
      
        StartCoroutine("OpenDoor");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator OpenDoor(){  

            for(int i= 0; i<=90; i++){
               
                transform.eulerAngles = new Vector3(0, i, 0);  
                yield return new WaitForSeconds(0.01f);
              
            }

    }

}
<hr>
종스크롤 탄막 게임

-플레이어 이동
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = curPos + nextPos;
    }
}

<hr>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // GameManager 객체
    public GameManager gameManager;

    // 오디오 클립들
    public AudioClip audioJump;
    public AudioClip audioAttack;
    public AudioClip audioDamaged;
    public AudioClip audioItem;
    public AudioClip audioDie;
    public AudioClip audioFinish;

    // 이동 최대 속도
    public float maxSpeed;

    // 점프 힘
    public float jumpPower;

    // Rigidbody2D, SpriteRenderer, Animator, BoxCollider2D, AudioSource 컴포넌트 변수
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;
    BoxCollider2D boxCollider;
    AudioSource audioSource;

    // 초기화
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // 고정 주기적으로 호출되는 업데이트
    private void FixedUpdate()
    {
        // 좌우 이동
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h * 3, ForceMode2D.Impulse);

        // 최대 이동 속도 제한
        if (rigid.velocity.x > maxSpeed) // 오른쪽
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < maxSpeed * (-1)) // 왼쪽
        {
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }

        // 아래 방향 충돌 체크
        if (rigid.velocity.y < 0)
        {
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platforms"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                {
                    animator.SetBool("isJump", false);
                }
            }
        }
    }

    // 소리 재생
    void PlaySound(string action)
    {
        switch (action)
        {
            case "JUMP":
                audioSource.clip = audioJump;
                break;
            case "ATTACK":
                audioSource.clip = audioAttack;
                break;
            case "DAMAGED":
                audioSource.clip = audioDamaged;
                break;
            case "ITEM":
                audioSource.clip = audioItem;
                break;
            case "DIE":
                audioSource.clip = audioDie;
                break;
            case "FINISH":
                audioSource.clip = audioFinish;
                break;
        }
        audioSource.Play();
    }

    // 업데이트
    void Update()
    {
        // 점프
        if (Input.GetButtonDown("Jump") && !animator.GetBool("isJump"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            animator.SetBool("isJump", true);
            PlaySound("JUMP");
        }

        // 이동 중지
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        // 방향 스프라이트 변경
        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }

        // 애니메이션 설정
        if (Mathf.Abs(rigid.velocity.x) < 0.4)
        {
            animator.SetBool("isWalk", false);
        }
        else
        {
            animator.SetBool("isWalk", true);
        }
    }

    // 충돌 발생 시 호출되는 함수
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // 공격
            if(rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y)
            {
                OnAttack(collision.transform);
            }
            else
            {
                OnDamaged(collision.transform.position);
            }
        }
    }

    // 공격 처리
    void OnAttack(Transform enemy)
    {
        PlaySound("ATTACK");
        gameManager.stagePoint += 100;

        // 반작용 힘
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        // 적 사망 처리
        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
        enemyMove.OnDamaged();
    }

    // 피해 처리
    void OnDamaged(Vector2 vec)
    {
        PlaySound("DAMAGED");
        // 체력 감소
        gameManager.HealthDown();

        // 레이어 변경
        gameObject.layer = 9;

        // 스프라이트 색상 변경
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        int dirc = transform.position.x - vec.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1)*7, ForceMode2D.Impulse);

        // 애니메이션 설정
        animator.SetTrigger("doDamaged");
        Invoke("OffDamaged", 3);
    }

    // 피해 무효화
    void OffDamaged()
    {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    // 트리거 충돌 시 호출되는 함수
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            PlaySound("ITEM");
            // 점수 획득
            bool isBronze = collision.gameObject.name.Contains("Bronze");
            bool isSilver = collision.gameObject.name.Contains("Silver");
            bool isGold = collision.gameObject.name.Contains("Gold");

            if (isBronze)
                gameManager.stagePoint += 50;
            else if(isSilver)
                gameManager.stagePoint += 100;
            else if (isGold)
                gameManager.stagePoint += 300;

            gameManager.stagePoint += 100;

            // 아이템 비활성화
            collision.gameObject.SetActive(false);
        }
        else if(collision.gameObject.tag == "Finish")
        {
            PlaySound("FINISH");
            gameManager.NextStage();
        }
    }

    // 죽음 처리
    public void OnDie()
    {
        PlaySound("DIE");
        // 스프라이트 투명도 조절
        spriteRenderer.color = new Color(1, 1, 1, 0.3f);

        // 스프라이트 Y축 반전
        spriteRenderer.flipY = true;


-- 적의 이동 과 동작 관리 스크립트
// EnemyMove 클래스는 적 캐릭터의 이동 및 동작을 관리하는 스크립트입니다.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    // Rigidbody2D, Animator, SpriteRenderer, CapsuleCollider2D 컴포넌트 변수
    Rigidbody2D rigid;
    Animator animator;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D capsuleCollider;

    // 다음 이동 방향을 나타내는 변수
    public int nextMove;

    // 초기화
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        
        // 일정 시간마다 Think 함수 호출
        Invoke("Think", 5);
    }

    // 고정 주기적으로 호출되는 업데이트
    void FixedUpdate()
    {
        // 이동
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        // 플랫폼 체크
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.3f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platforms"));
        if (rayHit.collider == null)
        {
            // 플랫폼이 없으면 방향 전환
            Debug.Log("그러다 골로가용ㅎㅎ");
            Turn();
        }
    }

    // 생각하는 동작
    void Think()
    {
        // 다음 이동 방향 설정
        nextMove = Random.Range(-1, 2);

        // 스프라이트 애니메이션 설정
        animator.SetInteger("WalkSpeed", nextMove);

        // 스프라이트 반전
        if (nextMove != 0)
        {
            spriteRenderer.flipX = nextMove == 1;
        }

        // 다음 생각 시간 설정
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }

    // 방향 전환
    void Turn()
    {
        nextMove *= -1;
        spriteRenderer.flipX = nextMove == 1;
        CancelInvoke();
        Invoke("Think", 5);
    }

    // 피해 처리
    public void OnDamaged()
    {
        // 스프라이트 투명도 조절
        spriteRenderer.color = new Color(1, 1, 1, 0.3f);

        // 스프라이트 Y축 반전
        spriteRenderer.flipY = true;

        // 콜라이더 비활성화
        capsuleCollider.enabled = false;

        // 죽음 효과 점프
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        // 일정 시간 후 비활성화
        Invoke("Deactive", 5);
    }

    // 비활성화
    void Deactive()
    {
        gameObject.SetActive(false);
    }
}
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

</body>
</html>
