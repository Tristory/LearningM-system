using LMSystem.Data;
using LMSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LMSystem.Services
{
    public class FileMService
    {
        private readonly ApplicationDbContext _context;

        public FileMService(ApplicationDbContext context)
        {
            _context = context;
        }

        // For File Handling
        public List<Models.File> GetUserFiles(string userId)
        {
            return _context.Files
                .Include(e => e.ApplicationUser)
                .Where(e => e.OwnerId == userId).ToList();
        }

        public string CreateFile(Models.File file) 
        {
            _context.Files.Add(file);
            _context.SaveChanges();

            return "Create success!";
        }
        public string UpdateFile(Models.File file) 
        {
            _context.Files.Update(file);
            _context.SaveChanges();

            return "Update success!";
        }
        public string DeleteFile(Models.File file) 
        {
            _context.Files.Remove(file);
            _context.SaveChanges();

            return "Delete success!";
        }

        // For Material Handling
        public Material GetFileMaterial(int fileId)
        {
            return _context.Materials
                .Include(e => e.File)
                .FirstOrDefault(e => e.FileId == fileId);
        }

        public string CreateMaterial(Material material) 
        {
            _context.Materials.Add(material);
            _context.SaveChanges();

            return "Create success!";
        }
        public string UpdateMaterial(Material material) 
        {
            _context.Materials.Update(material);
            _context.SaveChanges();

            return "Update success!";
        }
        public string DeleteMaterial(Material material) 
        {
            _context.Materials.Remove(material);
            _context.SaveChanges();

            return "Delete success!";
        }

        // For Exam Handling
        public Exam GetFileExam(int fileId)
        {
            return _context.Exams
                .Include(e => e.File)
                .FirstOrDefault(e => e.FileId == fileId);
        }

        public string CreateExam(Exam exam) 
        {
            _context.Exams.Add(exam);
            _context.SaveChanges();

            return "Create success!";
        }
        public string UpdateExam(Exam exam) 
        { 
            _context.Exams.Update(exam);
            _context.SaveChanges();

            return "Update success!";
        }
        public string DeleteExam(Exam exam) 
        { 
            _context.Exams.Remove(exam);
            _context.SaveChanges();

            return "Delete success!";
        }
    }
}
