using LMSystem.Data;
using LMSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LMSystem.Services
{
    public class ClassMService
    {
        private readonly ApplicationDbContext _context;

        public ClassMService(ApplicationDbContext context) 
        { 
            _context = context;
        }

        // For Class Handling
        public List<Class> GetSubjectClasses(int subjectId)
        {
            return _context.Classes
                .Include(e => e.Subject)
                .Where(e => e.SubjectId == subjectId).ToList();
        }

        public string CreateClass(Class pclass)
        {
            _context.Classes.Add(pclass);
            _context.SaveChanges();

            return "Create success!";
        }

        public string UpdateClass(Class pclass)
        {
            _context.Classes.Update(pclass);
            _context.SaveChanges();

            return "Update success!";
        }

        public string DeleteClass(Class pclass)
        {
            _context.Classes.Remove(pclass); 
            _context.SaveChanges();

            return "Delete success!";
        }


        // For ClassMaterial Handling
        public List<ClassMaterial> GetClassMaterials(int classId)
        {
            return _context.ClassMaterials
                .Include(e => e.Class)
                .Include(e => e.Material)
                .Where(e => e.ClassId == classId).ToList();
        }
        
        public string CreateClassMaterial(ClassMaterial classMaterial)
        {
            _context.ClassMaterials.Add(classMaterial);
            _context.SaveChanges();

            return "Create success!";
        }

        public string UpdateClassMaterial(ClassMaterial classMaterial)
        {
            _context.ClassMaterials.Update(classMaterial);
            _context.SaveChanges();

            return "Update success!";
        }

        public string DeleteClassMaterial(ClassMaterial classMaterial)
        {
            _context.ClassMaterials.Remove(classMaterial);
            _context.SaveChanges();

            return "Delete success!";
        }

        // For Lecture Handling
        public List<Lecture> GetClassLecture(int classId)
        {
            return _context.Lectures
                .Include(e => e.Class)
                .Where(e => e.ClassId == classId).ToList();
        }

        public string CreateLecture(Lecture lecture)
        {
            _context.Lectures.Add(lecture);
            _context.SaveChanges();

            return "Create success!";
        }

        public string UpdateLecture(Lecture lecture)
        {
            _context.Lectures.Update(lecture);
            _context.SaveChanges();

            return "Update success!";
        }

        public string DeleteLecture(Lecture lecture)
        {
            _context.Lectures.Remove(lecture);
            _context.SaveChanges();

            return "Delete success!";
        }

    }
}
