개발 완료 내역























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



<br>
