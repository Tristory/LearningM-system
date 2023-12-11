using LMSystem.Data;
using LMSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LMSystem.Services
{
    public class SubjectMService
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

        // For Topic Handling
        public List<Topic> GetSubjectTopics(int subjectId)
        {
            return _context.Topics
                .Include(e => e.Subject)
                .Where(e => e.SubjectId == subjectId).ToList();
        }
        
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
