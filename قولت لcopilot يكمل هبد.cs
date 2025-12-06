// ...existing code...
using System;
using System.Linq;
using System.Collections.Generic;
قولت لcopilot يكمل هبد.cs
namespace Project
{
    class Program
    {
        // CHANGED: added simple Question type to replace two separate lists
        class Question
        {
            public string Text;
            public string[] Choices;        // for MCQ
            public int CorrectIndex;       // for MCQ (-1 if N/A)
            public string[] Keywords;      // for Essay
        }

        class Quiz
        {
            // CHANGED: replaced original List<string> questions and List<string[]> answers
            public List<Question> questions = new List<Question>();

            public string topic;
            public int difficultyLevel;
            public int quizID;
            public int type;

            public int getQuizID() { return quizID; }
            public string getTopic() { return topic; }
            public int getDifficultyLevel() { return difficultyLevel; }
            public int getType() { return type; }

            public void setID()
            {
                Console.Write("Enter quiz ID : ");
                int.TryParse(Console.ReadLine() ?? string.Empty, out quizID); // CHANGED: null-safe read
            }
            public void setTopic()
            {
                Console.Write("Enter quiz topic : ");
                topic = Console.ReadLine() ?? string.Empty; // CHANGED: null-safe read
            }
            public void setdifficluty()
            {
                Console.Write("Enter quiz difficulty level <1 - 10> : ");
                int.TryParse(Console.ReadLine() ?? string.Empty, out difficultyLevel); // CHANGED: null-safe read
                while (difficultyLevel < 1 || difficultyLevel > 10)
                {
                    Console.Write("difficulty level must be between <1-10> try again : ");
                    int.TryParse(Console.ReadLine() ?? string.Empty, out difficultyLevel); // CHANGED
                }
            }
            public void settype()
            {
                Console.WriteLine("Choose quiz type :- \n1. Multiple Choice\n2. Essay");
                Console.Write("Enter your answer : ");
                int.TryParse(Console.ReadLine() ?? string.Empty, out type); // CHANGED
                while (type != 1 && type != 2)
                {
                    Console.Write("Wrong type try choose again (1 or 2): ");
                    int.TryParse(Console.ReadLine() ?? string.Empty, out type); // CHANGED
                }
            }
        } // end Quiz

        class MultipleChoiceQuiz : Quiz
        {
            // CHANGED: addQuestion now adds a Question to this.questions list
            public void addQuestion()
            {
                Console.Write("Enter Question : ");
                string question = Console.ReadLine() ?? string.Empty; // CHANGED

                Console.Write("Enter answer choices (separated by commas): ");
                string choicesInput = Console.ReadLine() ?? string.Empty; // CHANGED
                string[] choices = choicesInput
                                   .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                   .Select(s => s.Trim())
                                   .ToArray();

                int correct = -1;
                if (choices.Length > 0)
                {
                    Console.WriteLine("Choices:");
                    for (int i = 0; i < choices.Length; i++)
                        Console.WriteLine($"{i + 1}. {choices[i]}");

                    Console.Write("Enter index (1-based) of correct choice: ");
                    int.TryParse(Console.ReadLine() ?? string.Empty, out correct);
                    while (correct < 1 || correct > choices.Length)
                    {
                        Console.Write("Invalid index. Enter index of correct choice: ");
                        int.TryParse(Console.ReadLine() ?? string.Empty, out correct);
                    }
                    correct = correct - 1;
                }

                questions.Add(new Question
                {
                    Text = question,
                    Choices = choices,
                    CorrectIndex = correct,
                    Keywords = Array.Empty<string>()
                });
            }
        } // end MultipleChoiceQuiz

        class EssayQuiz : Quiz
        {
            // CHANGED: addQuestion now adds a Question to this.questions list
            public void addQuestion()
            {
                Console.Write("Enter Question : ");
                string question = Console.ReadLine() ?? string.Empty; // CHANGED

                Console.Write("Enter the keywords for the answer (separated by commas): ");
                string input = Console.ReadLine() ?? string.Empty; // CHANGED
                string[] keywords = input
                                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                    .Select(s => s.Trim())
                                    .ToArray();

                questions.Add(new Question
                {
                    Text = question,
                    Choices = Array.Empty<string>(),
                    CorrectIndex = -1,
                    Keywords = keywords
                });
            }
        } // end EssayQuiz

        class Student
        {
            public string name;
            public int studentID;
            public double grade;

            public void setStudentID()
            {
                Console.Write("Enter the student's ID : ");
                int.TryParse(Console.ReadLine() ?? string.Empty, out studentID); // CHANGED
            }
            public void setName()
            {
                Console.Write("Enter the student's name : ");
                name = Console.ReadLine() ?? string.Empty; // CHANGED
            }
            // NOTE: student.getGrade was incomplete in your original file.
            // Keep Student minimal; grading is handled by QuizManager (CHANGED).
            public int getStudentID() { return studentID; }
            public string getName() { return name; }
        } // end Student

        // keep existing static collections (minimal change)
        static List<MultipleChoiceQuiz> McqQuizzes = new List<MultipleChoiceQuiz>();
        static List<EssayQuiz> EssayQuizzes = new List<EssayQuiz>();
        static Quiz quiz = new Quiz();
        static Student student = new Student();

        class QuizManager
        {
            // createQuiz: minimal change, uses your existing quiz object for type selection
            public void createQuiz()
            {
                quiz.settype();
                int t = quiz.getType();
                if (t == 1)
                {
                    MultipleChoiceQuiz mcq = new MultipleChoiceQuiz();
                    mcq.setID();          // CHANGED: null-safe inside setID
                    mcq.setTopic();
                    mcq.setdifficluty();
                    mcq.type = 1;
                    McqQuizzes.Add(mcq);
                    Console.WriteLine("Multiple choice quiz created.");
                }
                else
                {
                    EssayQuiz eq = new EssayQuiz();
                    eq.setID();
                    eq.setTopic();
                    eq.setdifficluty();
                    eq.type = 2;
                    EssayQuizzes.Add(eq);
                    Console.WriteLine("Essay quiz created.");
                }
            }

            // addQuestionToQuiz: find quiz by ID and call its addQuestion method
            public void addQuestionToQuiz()
            {
                quiz.settype();
                quiz.setID();
                int t = quiz.getType();
                int id = quiz.getQuizID();

                if (t == 1)
                {
                    foreach (var q in McqQuizzes)
                    {
                        int quizid = q.getQuizID();
                        if (quizid == id)
                        {
                            q.addQuestion();
                            Console.WriteLine("Successfully added the question");
                            return;
                        }
                    }
                    Console.WriteLine("ID not found");
                }
                else
                {
                    foreach (var q in EssayQuizzes)
                    {
                        int quizid = q.getQuizID();
                        if (quizid == id)
                        {
                            q.addQuestion();
                            Console.WriteLine("Successfully added the question");
                            return;
                        }
                    }
                    Console.WriteLine("ID not found");
                }
            }

            // gradeQuiz: implemented minimal grading flow for both MCQ and Essay
            public void gradeQuiz()
            {
                quiz.settype();
                quiz.setID();
                int t = quiz.getType();
                int id = quiz.getQuizID();

                // collect student info
                student.setName();
                student.setStudentID();

                if (t == 1)
                {
                    var qz = McqQuizzes.FirstOrDefault(q => q.getQuizID() == id);
                    if (qz == null)
                    {
                        Console.WriteLine("Quiz ID not found.");
                        return;
                    }
                    if (qz.questions.Count == 0)
                    {
                        Console.WriteLine("Quiz has no questions.");
                        return;
                    }

                    int correct = 0;
                    for (int i = 0; i < qz.questions.Count; i++)
                    {
                        var q = qz.questions[i];
                        Console.WriteLine($"Q{i + 1}: {q.Text}");
                        for (int j = 0; j < (q.Choices?.Length ?? 0); j++)
                            Console.WriteLine($"{j + 1}. {q.Choices[j]}");

                        Console.Write("Your answer (index): ");
                        int.TryParse(Console.ReadLine() ?? string.Empty, out int sel); // CHANGED
                        if (sel - 1 == q.CorrectIndex) correct++;
                    }
                    double percent = 100.0 * correct / qz.questions.Count;
                    Console.WriteLine($"Score: {percent:0.##}%");
                    student.grade = percent;
                }
                else // essay
                {
                    var qz = EssayQuizzes.FirstOrDefault(q => q.getQuizID() == id);
                    if (qz == null)
                    {
                        Console.WriteLine("Quiz ID not found.");
                        return;
                    }
                    if (qz.questions.Count == 0)
                    {
                        Console.WriteLine("Quiz has no questions.");
                        return;
                    }

                    double totalScore = 0.0;
                    for (int i = 0; i < qz.questions.Count; i++)
                    {
                        var q = qz.questions[i];
                        Console.WriteLine($"Q{i + 1}: {q.Text}");
                        Console.Write("Student answer: ");
                        string answer = Console.ReadLine() ?? string.Empty; // CHANGED

                        int matches = 0;
                        foreach (var kw in q.Keywords ?? Array.Empty<string>())
                        {
                            if (string.IsNullOrWhiteSpace(kw)) continue;
                            if (answer.IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0) matches++;
                        }
                        double qScore = (q.Keywords == null || q.Keywords.Length == 0) ? 0.0 : (100.0 * matches / q.Keywords.Length);
                        totalScore += qScore;
                        Console.WriteLine($"Question score: {qScore:0.##}%");
                    }
                    double avg = totalScore / qz.questions.Count;
                    Console.WriteLine($"Average essay score: {avg:0.##}%");
                    student.grade = avg;
                }

                // CHANGED: minimal storage of last graded student's grade printed
                Console.WriteLine($"Recorded: {student.getName()} (ID {student.getStudentID()}) => {student.grade:0.##}%");
            }

            // simple listing functions (minimal change)
            public void viewQuizzes()
            {
                Console.WriteLine("Multiple Choice Quizzes:");
                foreach (var q in McqQuizzes)
                {
                    Console.WriteLine($"ID: {q.getQuizID()}, Topic: {q.getTopic()}, Difficulty: {q.getDifficultyLevel()}, Questions: {q.questions.Count}");
                }
                Console.WriteLine("Essay Quizzes:");
                foreach (var q in EssayQuizzes)
                {
                    Console.WriteLine($"ID: {q.getQuizID()}, Topic: {q.getTopic()}, Difficulty: {q.getDifficultyLevel()}, Questions: {q.questions.Count}");
                }
            }

            public void viewStudentGrades()
            {
                // CHANGED: original code had no persistent grade store; show last student only (minimal change)
                Console.WriteLine($"Last graded student (if any): {student.getName()} - Grade: {student.grade:0.##}%");
            }
        } // end QuizManager

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
                string choice = Console.ReadLine() ?? string.Empty; // CHANGED

                switch (choice)
                {
                    case "1": manager.createQuiz(); break;
                    case "2": manager.addQuestionToQuiz(); break;
                    case "3": manager.gradeQuiz(); break;
                    case "4": manager.viewQuizzes(); break;
                    case "5": manager.viewStudentGrades(); break;
                    case "6": return;
                    default: Console.WriteLine("Invalid choice. Please try again."); break;
                }
                Console.WriteLine();
            }
        }
    }
}
