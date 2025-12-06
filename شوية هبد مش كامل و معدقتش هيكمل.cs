using System;
using System.Linq;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Data.Common;

namespace Project
{

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    class Program
    {

        class Quiz 
        {
            List<string> questions = new List<string>();
            List<string[]> answers = new List<string[]>();
            public string topic = string.Empty;
            public int difficultyLevel;
            public int quizID;
            public int type;
            public int getQuizID() { return quizID; }
            public string getTopic() { return topic; }
            public int getDifficultyLevel() { return difficultyLevel; }
            public int getType() { return type; }
            public void setID() 
            { 
                Console.WriteLine($"Enter quiz ID : "); 
                int.TryParse(Console.ReadLine(), out quizID);
            }
            public void setTopic()
            {
                Console.WriteLine($"Enter quiz topic : "); 
                topic = Console.ReadLine() ?? String.Empty;      /**/ 

            }
            public void setdifficluty()
            {
                Console.WriteLine($"Enter quiz difficulty level <1 - 10> : "); 
                int.TryParse(Console.ReadLine(), out difficultyLevel); 
                while (difficultyLevel < 1 || difficultyLevel > 10)
                {
                    Console.WriteLine("difficulty level must be between <1- 10> try again : ");
                    int.TryParse(Console.ReadLine(), out difficultyLevel);                 
                }           
            }
            public void settype()
            {
                Console.WriteLine("Choose quiz type :- \n1. Multiple Choice\n2.Essay\nEnter your answer : ");
                int.TryParse(Console.ReadLine(), out type);
                while(type != 1 && type != 2)
                {
                    Console.WriteLine("Wrong type try choose again (1 or 2): ");
                    int.TryParse(Console.ReadLine(), out type);
                }           
            }

        }///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        class MultipleChoiceQuiz : Quiz 
        {    
            public void addQuestion() 
            {
                Console.WriteLine("Adding question to quiz ");
                Console.Write("Enter quiz ID : ");
                int.TryParse(Console.ReadLine(), out quizID);   
                Console.Write("Enter Question : ");
                string question = Console.ReadLine() ?? string.Empty;   /**/
                Console.Write("Enter answer choices (seperated by commas <,> ) : ");
                string choices = Console.ReadLine() ?? string.Empty;    /**/
            }
            // override getDifficultyLevel 

        }///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        class EssayQuiz : Quiz 
        {
            public void addQuestion() 
            {
                Console.WriteLine("Adding question to quiz ");
                Console.Write("Enter quiz ID : ");
                int.TryParse(Console.ReadLine(), out quizID);
                Console.Write("Enter Question : ");
                string question = Console.ReadLine() ?? string.Empty;   /**/
                Console.Write("Enter the keywords for the answer (seperated by commas <,> ) : ");
                string input = Console.ReadLine() ?? string.Empty;      /**/
                string [] keywords = input.Split(',').Select(k => k.Trim()).ToArray();
            }
            // override getDifficultyLevel

        }///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        class Student
        {
            public string name = string.Empty;
            public int studentID;
            public double grade;
            public void setStudentID() 
            {
                Console.WriteLine("Enter the student's ID : ");
                int.TryParse(Console.ReadLine(), out studentID);
            }
            public void setName() 
            {
                Console.WriteLine("Enter the student's name : ");
                name = Console.ReadLine() ?? string.Empty;      /**/
            }
            public void getGrade(int quizID, int quizType) 
            {
                if (quizType == 1) // mcq
                {
                    Console.WriteLine("Enter student's answer : ");
                    string answer ;
                    foreach (var q in McqQuizzes)
                    {
                        int quizid = q.getQuizID();
                        if (quizid == quizID)
                        {
                            
                        }
                    }
                }
                else if(quizType == 2) // essay
                {
                    Console.WriteLine("Enter student's answer : ");
                           
                }
            }
            public int getStudentID() { return studentID; }
            public string getName() { return name; }

        }///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////









            static List<MultipleChoiceQuiz> McqQuizzes = new List<MultipleChoiceQuiz>();
            static List<EssayQuiz> EssayQuizzes = new List<EssayQuiz>();
            static Quiz quiz = new Quiz();
            MultipleChoiceQuiz mcq = new MultipleChoiceQuiz();
            EssayQuiz essayquiz = new EssayQuiz();
            Student student = new Student();

        class QuizManager 
        {
            public void createQuiz() 
            {
                quiz.settype();
                int type = quiz.getType();
                if (type == 1)
                {
                    MultipleChoiceQuiz mcq = new MultipleChoiceQuiz();
                    mcq.setID();
                    mcq.setTopic();
                    mcq.setdifficluty();
                    McqQuizzes.Add(mcq);
                }
                else if (type == 2)
                {
                    EssayQuiz essayquiz = new EssayQuiz();
                    essayquiz.setID();
                    essayquiz.setTopic();
                    essayquiz.setdifficluty();
                    EssayQuizzes.Add(essayquiz);
                }
            }
            public void addQuestionToQuiz() 
            {
                quiz.settype();
                quiz.setID();
                int type = quiz.getType();
                int id = quiz.getQuizID();
                if (type == 1)
                {
                    foreach (var q in McqQuizzes)
                    {
                        int quizid = q.getQuizID();
                        if (quizid == id)
                        {
                            q.addQuestion();
                            Console.WriteLine("Successfully added the question");
                            return ;
                        }
                    }    
                    Console.WriteLine("ID not found");
                }
                else if (type == 2)
                {
                    foreach (var q in EssayQuizzes)
                    {
                        int quizid = q.getQuizID();
                        if (quizid == id)
                        {
                            q.addQuestion();
                            Console.WriteLine("Successfully added the question");
                            return ;
                        }
                    }
                    Console.WriteLine("ID not found");
                }
            }
            public void gradeQuiz() 
            {
                quiz.settype();
                quiz.setID();
                int type = quiz.getType();
                int id = quiz.getQuizID();
                student.setName();
                student.setStudentID();


            }
            public void viewQuizzes() 
            {

            }
            public void viewStudentGrades() 
            {

            }









            static void Main(string[] args)
            {
                QuizManager manager = new QuizManager();
                while (true)
                {
                    Console.WriteLine("Quiz Management System");
                    Console.WriteLine("1. Create Quiz");
                    Console.WriteLine("2. Add Question to Quiz");
                    Console.WriteLine("3. Grade Quiz");
                    Console.WriteLine("4. View Quizzes");
                    Console.WriteLine("5. View Student Grades");
                    Console.WriteLine("6. Exit");
                    Console.Write("Enter your choice: ");
                    string choice = Console.ReadLine() ?? string.Empty;     /**/
                    switch (choice)
                    {
                        case "1": manager.createQuiz() ; break;
                        case "2": manager.addQuestionToQuiz() ; break;
                        case "3": manager.gradeQuiz() ; break;
                        case "4": manager.viewQuizzes() ; break;
                        case "5": manager.viewStudentGrades(); break;
                        case "6": return;
                        default:  Console.WriteLine("Invalid choice. Please try again."); break;
                    }
                    Console.WriteLine("\n");
                }
            }
        }
    }
}
