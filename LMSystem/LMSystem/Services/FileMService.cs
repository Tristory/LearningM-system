using LMSystem.Data;
using LMSystem.Models;
using LMSystem.Repository;
using Microsoft.EntityFrameworkCore;

namespace LMSystem.Services
{
    public class FileMService
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileHandlerService _fileHandlerService;

        public FileMService(ApplicationDbContext context, IFileHandlerService fileHandlerService)
        {
            _context = context;
            _fileHandlerService = fileHandlerService;
        }

        // For File Handling
        public List<Models.File> GetUserFiles(string userId)
        {
            return _context.Files
                .Include(e => e.ApplicationUser)
                .Where(e => e.OwnerId == userId).ToList();
        }

        public List<Models.File> GetTypeFiles(string type)
        {
            return _context.Files
                .Include(e => e.ApplicationUser)
                .Where(e => e.Type == type).ToList();
        }

        public Models.File GetFile(int id) => _context.Files.Find(id);

        public async Task<string> CreateFile(IFormFile uploadFile, Models.File file, string folderType)
        {
            var path = await _fileHandlerService.UploadFile(uploadFile, file.Name, folderType);

            file.FilePath = path;

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
            _fileHandlerService.DeleteFile(file.Name, file.Type);

            _context.Files.Remove(file);
            _context.SaveChanges();

            return "Delete success!";
        }

        public IFormFile ShowFile(Models.File file)
        {
            return _fileHandlerService.ShowFile(file.Name, file.Type);
        }

        public string ChangeFileName(Models.File file, string newName)
        {
            _fileHandlerService.ChangeFileName(file.Name, newName, file.Type);

            file.Name = newName;

            return UpdateFile(file);
        }

        public async Task<FileInfor> DownloadFile(int id, string folderType)
        {
            var file = GetFile(id);

            var fileInfor = await _fileHandlerService.DownloadFile(file.Name, folderType);

            return fileInfor;
        }

        // For Material Handling
        public List<Material> GetSubjectMaterials(int subjectId)
        {
            return _context.Materials
                .Include(e => e.Subject)
                .Include(e => e.File)
                .Where(e => e.SubjectId == subjectId).ToList();
        }

        public List<Material> GetSearchMaterials(string searchS)
        {
            return _context.Materials
                .Include(e => e.Subject)
                .Include(e => e.File)
                .Where(e => e.File.Name.Contains(searchS)).ToList();
        }

        public List<Material> GetSubjectStatusMaterials(string status)
        {
            return _context.Materials
                .Include(e => e.Subject)
                .Include(e => e.File)
                .Where(e => e.Subject.Status == status).ToList();
        }

        public List<Material> GetTeacherSubjectMaterials(string teacherId)
        {
            return _context.Materials
                .Include(e => e.Subject)
                .Include(e => e.File)
                .Where(e => e.Subject.ApplicationUser.Id == teacherId).ToList();
        }

        public List<Material> GetCheckedMaterials()
        {
            return _context.Materials
                .Include(e => e.Subject)
                .Include(e => e.File)
                .Where(e => e.status == "Checked").ToList();
        }

        public List<Material> GetUncheckedMaterials()
        {
            return _context.Materials
                .Include(e => e.Subject)
                .Include(e => e.File)
                .Where(e => e.status != "Checked").ToList();
        }

        public List<Material> GetSearchedMaterials(string searchS)
        {
            return _context.Materials
                .Include(e => e.Subject)
                .Include(e => e.File)
                .Where(e => e.File.Name.Contains(searchS)).ToList();
        }

        public List<Material> GetMaterials() => _context.Materials.ToList();

        public Material GetMaterial(int id) => _context.Materials.Find(id);

        public Material GetDetailMaterial(int id)
        {
            return _context.Materials
                .Include(e => e.Subject)
                .Include(e => e.File)
                .FirstOrDefault(e => e.Id == id);
        }
        
        public Material GetFileMaterial(int fileId)
        {
            return _context.Materials
                .Include(e => e.File)
                .FirstOrDefault(e => e.FileId == fileId);
        }

        public string CreateMaterial(Material material, IFormFile uploadFile, Models.File file)
        {
            var result = CreateFile(uploadFile, file, "Materials");

            material.FileId = file.Id;

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

        public string AppproveMaterial(int materialId)
        {
            var material = GetMaterial(materialId);

            material.status = "Checked";
            material.IsApproved = !material.IsApproved;                      

            return UpdateMaterial(material);
        }
        
        public string DestroyMaterial(Material material)
        {
            material.IsDeleted = !material.IsDeleted;

            return "Destroy material!";
        }

        // For Exam Handling
        public List<Exam> GetSubjectExams(int subjectId)
        {
            return _context.Exams
                .Include(e => e.Subject)
                .Include(e => e.File)
                .Where(e => e.SubjectId == subjectId).ToList();
        }

        public List<Exam> GetSearchExams(string searchS)
        {
            return _context.Exams
                .Include(e => e.Subject)
                .Include(e => e.File)
                .Where(e => e.File.Name.Contains(searchS)).ToList();
        }

        public List<Exam> GetSubjectStatusExams(string status)
        {
            return _context.Exams
                .Include(e => e.Subject)
                .Include(e => e.File)
                .Where(e => e.Subject.Status == status).ToList();
        }

        public List<Exam> GetTeacherSubjectExams(string teacherId)
        {
            return _context.Exams
                .Include(e => e.Subject)
                .Include(e => e.File)
                .Where(e => e.Subject.ApplicationUser.Id == teacherId).ToList();
        }

        public List<Exam> GetSearchedExams(string searchS)
        {
            return _context.Exams
                .Include(e => e.Subject)
                .Include(e => e.File)
                .Where(e => e.File.Name.Contains(searchS)).ToList();
        }

        public List<Exam> GetExams() => _context.Exams.ToList();

        public Exam GetFileExam(int fileId)
        {
            return _context.Exams
                .Include(e => e.File)
                .FirstOrDefault(e => e.FileId == fileId);
        }

        public Exam GetExam(int id) => _context.Exams.Find(id);

        public Exam GetDetailExam(int id)
        {
            return _context.Exams
                .Include(e => e.Subject)
                .Include(e => e.File)
                .FirstOrDefault(e => e.Id  == id);
        }

        public string CreateExam(Exam exam, IFormFile uploadFile, Models.File file) 
        {
            var result = CreateFile(uploadFile, file, "Exams");

            exam.FileId = file.Id;

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

        public string AppproveExam(int examId)
        {
            var exam = GetExam(examId);

            exam.status = "Checked";
            exam.IsApproved = !exam.IsApproved;

            return UpdateExam(exam);
        }

        public string DestroyExam(Exam exam)
        {
            exam.IsDeleted = !exam.IsDeleted;

            return "Destroy Exam!";
        }
    }
}
