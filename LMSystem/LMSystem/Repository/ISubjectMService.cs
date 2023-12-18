using LMSystem.Models;

namespace LMSystem.Repository
{
    public interface ISubjectMService
    {
        string CreateAnswer(Answer answer);
        string CreateQuestion(Question question);
        string CreateStudentSubject(StudentSubject studentSub);
        string CreateSubject(Subject subject);
        string CreateTopic(Topic topic);
        string DeleteAnswer(Answer answer);
        string DeleteQuestion(Question question);
        string DeleteStudentSubject(StudentSubject studentSub);
        string DeleteSubject(Subject subject);
        string DeleteTopic(Topic topic);
        StudentSubject GetdetailSS(string studentId, int subjectId);
        Subject GetDetailSubject(int id);
        List<Subject> GetFilterNameSubjects(string name);
        List<Subject> GetFilterStatusSubjects(string status);
        List<Subject> GetFilterTeacherSubjects(string teacherId);
        List<StudentSubject> GetMarkedSSs(string studentId);
        List<StudentSubject> GetOrderByDateSSs(string studentId);
        List<StudentSubject> GetOrderByNameSSs(string studentId);
        List<Answer> GetQuestionAnswers(int questionId);
        List<Answer> GetSearchAnswers(string searchS);
        List<Question> GetSearchQuestions(string searchS);
        List<Subject> GetSearchSubjects(string searchS);
        List<Topic> GetSearchTopics(string searchS);
        List<StudentSubject> GetStudentSSs(string studentId);
        Subject GetSubject(int id);
        List<Subject> GetSubjects();
        List<StudentSubject> GetSubjectSSs(int subjectId);
        List<Topic> GetSubjectTopics(int subjectId);
        Topic GetTopic(int id);
        List<Question> GetTopicQuestions(int topicId);
        string MarkManager(StudentSubject studentSub);
        string UpdateAnswer(Answer answer);
        string UpdateQuestion(Question question);
        string UpdateStudentSubject(StudentSubject studentSub);
        string UpdateSubject(Subject subject);
        string UpdateTopic(Topic topic);
    }
}