using LMSystem.Data;
using LMSystem.Models;
using LMSystem.Repository;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace LMSystem.Services
{
    public class SubjectMService : ISubjectMService
    {
        private readonly ApplicationDbContext _context;

        public SubjectMService(ApplicationDbContext context)
        {
            _context = context;
        }

        // For Subject Handling
        public List<Subject> GetSubjects()
        {
            return _context.Subjects
                .Include(e => e.ApplicationUser)
                .OrderByDescending(e => e.SendingD).ToList();
        }

        public List<Subject> GetFilterNameSubjects(string name)
        {
            return _context.Subjects
                .Include(e => e.ApplicationUser)
                .Where(e => e.Name == name).ToList();
        }

        public List<Subject> GetFilterTeacherSubjects(string teacherId)
        {
            return _context.Subjects
                .Include(e => e.ApplicationUser)
                .Where(e => e.TeacherId == teacherId).ToList();
        }

        public List<Subject> GetFilterStatusSubjects(string status)
        {
            return _context.Subjects
                .Include(e => e.ApplicationUser)
                .Where(e => e.Status == status).ToList();
        }

        public List<Subject> GetSearchSubjects(string searchS)
        {
            return _context.Subjects
                .Include(e => e.ApplicationUser)
                .Where(e => e.Name.Contains(searchS)).ToList();
        }

        public Subject GetSubject(int id) => _context.Subjects.Find(id);

        public Subject GetDetailSubject(int id)
        {
            return _context.Subjects
                .Include(e => e.ApplicationUser)
                .FirstOrDefault(e => e.Id == id);
        }

        public string CreateSubject(Subject subject)
        {
            _context.Subjects.Add(subject);
            _context.SaveChanges();

            return "Create success!";
        }

        public string UpdateSubject(Subject subject)
        {
            _context.Subjects.Update(subject);
            _context.SaveChanges();

            return "Update success!";
        }

        public string DeleteSubject(Subject subject)
        {
            _context.Subjects.Remove(subject);
            _context.SaveChanges();

            return "Delete success!";
        }

        // For StudentSubject Handling
        public List<StudentSubject> GetStudentSSs(string studentId)
        {
            return _context.StudentSubjects
                .Include(e => e.ApplicationUser)
                .Include(e => e.Subject)
                .Where(e => e.StudentId == studentId).ToList();
        }

        public List<StudentSubject> GetSubjectSSs(int subjectId)
        {
            return _context.StudentSubjects
                .Include(e => e.ApplicationUser)
                .Include(e => e.Subject)
                .Where(e => e.SubjectId == subjectId).ToList();
        }

        public List<StudentSubject> GetMarkedSSs(string studentId)
        {
            return _context.StudentSubjects
                .Include(e => e.ApplicationUser)
                .Include(e => e.Subject)
                .Where(e => e.StudentId == studentId && e.IsMarked).ToList();
        }

        public List<StudentSubject> GetOrderByNameSSs(string studentId)
        {
            return _context.StudentSubjects
                .Include(e => e.ApplicationUser)
                .Include(e => e.Subject)
                .Where(e => e.StudentId == studentId)
                .OrderByDescending(e => e.Subject.Name).ToList();
        }

        public List<StudentSubject> GetOrderByDateSSs(string studentId)
        {
            return _context.StudentSubjects
                .Include(e => e.ApplicationUser)
                .Include(e => e.Subject)
                .Where(e => e.StudentId == studentId)
                .OrderByDescending(e => e.AccessD).ToList();
        }

        public StudentSubject GetdetailSS(string studentId, int subjectId)
        {
            return _context.StudentSubjects
                .Include(e => e.ApplicationUser)
                .Include(e => e.Subject)
                .FirstOrDefault(e => e.SubjectId == subjectId && e.StudentId == studentId);
        }

        public string CreateStudentSubject(StudentSubject studentSub)
        {
            _context.StudentSubjects.Add(studentSub);
            _context.SaveChanges();

            return "Create success!";
        }

        public string UpdateStudentSubject(StudentSubject studentSub)
        {
            _context.StudentSubjects.Update(studentSub);
            _context.SaveChanges();

            return "Update success!";
        }

        public string DeleteStudentSubject(StudentSubject studentSub)
        {
            _context.StudentSubjects.Remove(studentSub);
            _context.SaveChanges();

            return "Delete success!";
        }

        public string MarkManager(StudentSubject studentSub)
        {
            studentSub.IsMarked = !studentSub.IsMarked;

            return UpdateStudentSubject(studentSub);
        }

        // For Topic Handling
        public List<Topic> GetSubjectTopics(int subjectId)
        {
            return _context.Topics
                .Include(e => e.Subject)
                .Where(e => e.SubjectId == subjectId).ToList();
        }

        public List<Topic> GetSearchTopics(string searchS)
        {
            return _context.Topics
                .Include(e => e.Subject)
                .Where(e => e.Name.Contains(searchS) || e.Description.Contains(searchS)).ToList();
        }

        public Topic GetTopic(int id) => _context.Topics.Find(id);

        public string CreateTopic(Topic topic)
        {
            _context.Topics.Add(topic);
            _context.SaveChanges();

            return "Create success!";
        }
        public string UpdateTopic(Topic topic)
        {
            _context.Topics.Update(topic);
            _context.SaveChanges();

            return "Update success!";
        }
        public string DeleteTopic(Topic topic)
        {
            _context.Topics.Remove(topic);
            _context.SaveChanges();

            return "Delete success!";
        }

        // For Question Handling
        public List<Question> GetTopicQuestions(int topicId)
        {
            return _context.Questions
                .Include(e => e.Topic)
                .Include(e => e.ApplicationUser)
                .Where(e => e.TopicId == topicId).ToList();
        }

        public List<Question> GetSearchQuestions(string searchS)
        {
            return _context.Questions
                .Include(e => e.Topic)
                .Include(e => e.ApplicationUser)
                .Where(e => e.Name.Contains(searchS) || e.Content.Contains(searchS)).ToList();
        }

        public string CreateQuestion(Question question)
        {
            _context.Questions.Add(question);
            _context.SaveChanges();

            return "Create success!";
        }
        public string UpdateQuestion(Question question)
        {
            _context.Questions.Update(question);
            _context.SaveChanges();

            return "Update success!";
        }
        public string DeleteQuestion(Question question)
        {
            _context.Questions.Remove(question);
            _context.SaveChanges();

            return "Delete success!";
        }

        // For Answer Handling
        public List<Answer> GetQuestionAnswers(int questionId)
        {
            return _context.Answers
                .Include(e => e.Question)
                .Include(e => e.ApplicationUser)
                .Where(e => e.QuestionId == questionId).ToList();
        }

        public List<Answer> GetSearchAnswers(string searchS)
        {
            return _context.Answers
                .Include(e => e.Question)
                .Include(e => e.ApplicationUser)
                .Where(e => e.Content.Contains(searchS)).ToList();
        }

        public string CreateAnswer(Answer answer)
        {
            _context.Answers.Add(answer);
            _context.SaveChanges();

            return "Create success!";
        }
        public string UpdateAnswer(Answer answer)
        {
            _context.Answers.Update(answer);
            _context.SaveChanges();

            return "Update success!";
        }
        public string DeleteAnswer(Answer answer)
        {
            _context.Answers.Remove(answer);
            _context.SaveChanges();

            return "Delete success!";
        }
    }
}
