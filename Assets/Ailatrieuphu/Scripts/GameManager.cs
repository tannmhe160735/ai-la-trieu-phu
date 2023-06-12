using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Section2
{
    [Serializable]
    public class QuestionData
    {
        public string question;
        public string answerA;
        public string answerB;
        public string answerC;
        public string answerD;
        public string correctAnswer;
    }

    public enum GameState
    {
        Home,
        GamePlay,
        GameOver,
        Victory
    }

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_TxtQuestion;
        [SerializeField] private TextMeshProUGUI m_TxtAnswerA;
        [SerializeField] private TextMeshProUGUI m_TxtAnswerB;
        [SerializeField] private TextMeshProUGUI m_TxtAnswerC;
        [SerializeField] private TextMeshProUGUI m_TxtAnswerD;
        [SerializeField] private Image m_ImgTxtAnswerA;
        [SerializeField] private Image m_ImgTxtAnswerB;
        [SerializeField] private Image m_ImgTxtAnswerC;
        [SerializeField] private Image m_ImgTxtAnswerD;
        [SerializeField] private Sprite m_ButtonGreen;
        [SerializeField] private Sprite m_ButtonYellow;
        [SerializeField] private Sprite m_ButtonBlack;
        [SerializeField] private AudioSource m_AudioSource;
        [SerializeField] private AudioClip m_MusicMainTheme;
        [SerializeField] private AudioClip m_SfWrongAnswer;
        [SerializeField] private AudioClip m_SfCorrectAnswer;
        [SerializeField] private AudioClip m_SfVictory;
        [SerializeField] private AudioClip m_OpenTheme;


        [SerializeField] private GameObject m_HomePanel, m_GamePanel, m_GameoverPanel, m_VictoryPanel;

        //[SerializeField] private QuestionData[] m_QuestionData;
        [SerializeField] private QuestionScriptTableData[] m_QuestionData;

        private int m_QuestionsIndex;
        private GameState m_gameState;
        // Start is called before the first frame update
        void Start()
        {
            m_AudioSource.Stop();
            m_AudioSource.PlayOneShot(m_OpenTheme);
            SetGameState(GameState.Home);
            m_QuestionsIndex = 0;
            InitQuestion(0);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void btnAnswer_Pressed(string pSelectedAnswer)
        {
            bool traLoiDung = false;
            if (m_QuestionData[m_QuestionsIndex].correctAnswer == pSelectedAnswer)
            {
                if (m_QuestionsIndex >= m_QuestionData.Length - 1)
                {
                    traLoiDung = true;
                    Invoke("Lastquestion", 5f);
                    Debug.Log("Câu trả lời chính xác");
                }
                else
                {
                    traLoiDung = true;
                    m_AudioSource.PlayOneShot(m_SfCorrectAnswer);
                    Debug.Log("Câu trả lời chính xác");
                }
            }
            else
            {
                traLoiDung = false;
                Debug.Log("Câu trả lời sai");
                Invoke("GameOver", 3f);
            }

            switch (pSelectedAnswer)
            {
                case "a":
                    m_ImgTxtAnswerA.sprite = traLoiDung ? m_ButtonGreen : m_ButtonYellow;
                    break;
                case "b":
                    m_ImgTxtAnswerB.sprite = traLoiDung ? m_ButtonGreen : m_ButtonYellow;
                    break;
                case "c":
                    m_ImgTxtAnswerC.sprite = traLoiDung ? m_ButtonGreen : m_ButtonYellow;
                    break;
                case "d":
                    m_ImgTxtAnswerD.sprite = traLoiDung ? m_ButtonGreen : m_ButtonYellow;
                    break;
            }
            if (traLoiDung)
            {
                Invoke("NextQuestion", 2f);
            }
        }

        private void NextQuestion()
        {
            m_QuestionsIndex++;
            InitQuestion(m_QuestionsIndex);
        }

        private void Lastquestion()
        {
            m_AudioSource.PlayOneShot(m_SfCorrectAnswer);
            Invoke("Victory", 3f);
            Debug.Log("Bạn đã chiến thắng");
            return;
        }

        private void GameOver()
        {
            SetGameState(GameState.GameOver);
            m_AudioSource.Stop();
            m_AudioSource.PlayOneShot(m_SfWrongAnswer);
        }

        private void Victory()
        {
            m_AudioSource.Stop();
            m_AudioSource.PlayOneShot(m_SfVictory);
            SetGameState(GameState.Victory);
            m_QuestionsIndex = 0;
            InitQuestion(0);
        }

        private void InitQuestion(int pIndex)
        {
            if (pIndex < 0 || pIndex >= m_QuestionData.Length)
                return;

            m_ImgTxtAnswerA.sprite = m_ButtonBlack;
            m_ImgTxtAnswerB.sprite = m_ButtonBlack;
            m_ImgTxtAnswerC.sprite = m_ButtonBlack;
            m_ImgTxtAnswerD.sprite = m_ButtonBlack;
            m_TxtQuestion.text = m_QuestionData[pIndex].question;
            m_TxtAnswerA.text = "A:" + m_QuestionData[pIndex].answerA;
            m_TxtAnswerB.text = "B:" + m_QuestionData[pIndex].answerB;
            m_TxtAnswerC.text = "C:" + m_QuestionData[pIndex].answerC;
            m_TxtAnswerD.text = "D:" + m_QuestionData[pIndex].answerD;
        }

        public void SetGameState(GameState state)
        {
            m_gameState = state;
            m_HomePanel.SetActive(m_gameState == GameState.Home);
            m_GamePanel.SetActive(m_gameState == GameState.GamePlay);
            m_GameoverPanel.SetActive(m_gameState == GameState.GameOver);
            m_VictoryPanel.SetActive(m_gameState == GameState.Victory);
        }

        public void btn_Play_Pressed()
        {
            m_AudioSource.Stop();
            SetGameState(GameState.GamePlay);
            m_AudioSource.clip = m_MusicMainTheme;
            m_AudioSource.Play();
        }

        public void btn_Home_Pressed()
        {
            Invoke("Start", 2f);
        }
    }
}
