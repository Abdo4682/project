using System;
namespace Project
{
    class Quiz 
    {
        public string topic;
        public string difficultyLevel;
        public int quizID;
        public void getQuizID() { Console.WriteLine($"Quiz ID : {quizID}"); }
        public void getTopic() { Console.WriteLine($"Quiz topic : {topic}"); }
        public void getDifficultyLevel() { Console.WriteLine($"Quiz difficulty level : {difficultyLevel}"); }

    }///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    class MultipleChoiceQuiz : Quiz 
    {
        
         
        public void addQuestion() 
        {
            Console.WriteLine("Adding question to quiz ");
            Console.Write("Enter quiz ID : ");
            int.TryParse(Console.ReadLine(), out quizID);   
            Console.Write("Enter Question : ");
            string q1 = Console.ReadLine();
            Console.Write("Enter answer choices (seperated by commas , ) : ");
            string choices = Console.ReadLine();
        }
        // override getDifficultyLevel 

    }///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    class EasyQuiz : Quiz 
    {
        public void addQuestion() 
        {

        }
        // override getDifficultyLevel

    }///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    class Student
    {
        public string name;
        public int studentID;
        public double grade;
        public void getStudentID() 
        {

        }
        public void getName() 
        {
            
        }
        public void getGrade() 
        {

        }

    }///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    class QuizManager 
    {
        public void createQuiz() 
        {

        }
        public void addQuestionToQuiz() 
        {
          
        }
        public void gradeQuiz() 
        {

        }
        public void viewQuizzes() 
        {

        }
        public void viewStudentGrades() 
        {

        }

    }///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    class Program
    {
        public static Quiz quiz = new Quiz();
        public static MultipleChoiceQuiz mcq = new MultipleChoiceQuiz();
        public static EasyQuiz Easyquiz = new EasyQuiz();
        public static Student student = new Student();
        public static QuizManager manager = new QuizManager();
        public static void createQuiz()
        {
            
        }
        public static void addQuestionToQuiz()
        {
            mcq.addQuestion();
        }
        public static void gradeQuiz()
        {

        }
        public static void viewQuizzes()
        {

        }
        public static void viewStudentGrades()
        {

        }
        static void Main(string[] args)
        {
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
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": createQuiz() ; break;
                    case "2": addQuestionToQuiz() ; break;
                    case "3": gradeQuiz() ; break;
                    case "4": viewQuizzes() ; break;
                    case "5": viewStudentGrades(); break;
                    case "6": return;
                    default:  Console.WriteLine("Invalid choice. Please try again."); break;
                }
                Console.WriteLine("\n");
            }
        }

    }
}
