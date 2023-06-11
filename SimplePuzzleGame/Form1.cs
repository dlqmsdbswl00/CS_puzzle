using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SimplePuzzleGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int movesNumber = 0, labelIndex = 0;  //이동 횟수와 버튼 숫자를 초기화

        //숫자 버튼 셔플 메소드
        private void shuffleButtons()       
        {
            List<int> labelList = new List<int>();  //숫자를 추적해 중복이 되지 않도록 하는 리스트

            Random r = new Random();
            foreach (Button btn in this.pnl.Controls)   //panel 컨트롤 내의 모든 버튼(btn)에 대해
            {
                while (labelList.Contains(labelIndex))  //labelList가 lableIndex를 포함하고 있다면
                    labelIndex = r.Next(16);            //0~15 사이의 무작위값 생성

                if (labelIndex == 0)                    //labelIndex가 0이라면
                {
                    btn.Text = "";                      //btn text를 공백으로 하고 색 변경
                    btn.BackColor = Color.Green;
                }
                else
                {
                    btn.Text = (labelIndex).ToString();   //btn text를 Index값으로 수정하고 색 변경
                    btn.BackColor = Color.White;
                }
                labelList.Add(labelIndex);              //lableList에 lableIndex를 추가한다 (중복 방지)
            }
            movesNumber = 0;
            lblNoOfMoves.Text = "이동 횟수 : " + movesNumber;
        }


        //버튼 이동 메소드
        private void swapLabel(Object sender, EventArgs e)  
        {
            Button btn = (Button)sender;

            if (btn.Text == "")         //빈 버튼이라면 아무 동작 수행 X
                return;

            Button nullBtn = null;     //빈 버튼 변수(nullBtn)를 null로 초기화
            foreach (Button bt in this.pnl.Controls)    // panel의 버튼 bt에 대해서
            {
                if (bt.Text == "")       //bt가 비어있다면
                {
                    nullBtn = bt;       //nullBtn bt할당하고 반복문 종료
                    break;
                }
            }

            //선택한 버튼(btn)과 빈 버튼(nullBtn)의 위치를 비교해 이동 가능한 경우 동작 수행
            if (btn.TabIndex == (nullBtn.TabIndex - 1) ||   //선택한 버튼이 빈 버튼의 왼쪽
                btn.TabIndex == (nullBtn.TabIndex - 4) ||   //선택한 버튼이 빈 버튼의 위쪽
                btn.TabIndex == (nullBtn.TabIndex + 1) ||   //선택한 버튼이 빈 버튼의 오른쪽
                btn.TabIndex == (nullBtn.TabIndex + 4))     //선택한 버튼이 빈 버튼의 아래쪽
            {
                nullBtn.BackColor = Color.White;            //빈 버튼의 배경색을 흰색으로
                btn.BackColor = Color.Green;                //선택 버튼의 배경색을 초록색으로
                nullBtn.Text = btn.Text;                    //빈 버튼의 텍스트를 선택 버튼의 텍스트로
                btn.Text = "";                              //선택 버튼의 텍스트를 공백으로
                movesNumber++;                              //이동 횟수 +1
                lblNoOfMoves.Text = "이동 횟수 : " + movesNumber;   //이동 횟수 레이블 출력
            }
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            shuffleButtons();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            shuffleButtons();
        }
    }
}