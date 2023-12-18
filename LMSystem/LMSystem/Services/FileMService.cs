using LMSystem.Data;
using LMSystem.Models;
using LMSystem.Repository;
using Microsoft.EntityFrameworkCore;

namespace LMSystem.Services
{
    public class FileMService : IFileMService
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileHandlerService _fileHandlerService;

        public FileMService(ApplicationDbContext context, IFileHandlerService fileHandlerService)
        {
            _context = context;
            _fileHandlerService = fileHandlerService;
        }

        // For File Handling
        public List<Models.File> GetFiles()
        {
            return _context.Files
                .Include(e => e.ApplicationUser).ToList();
        }

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

        public List<Models.File> GetSearchFiles(string searchS)
        {
            return  _context.Files
                .Include(e => e.ApplicationUser)
                .Where(e => e.Name.Contains(searchS)).ToList();
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

        public async Task<string> ReuploadFile(IFormFile uploadFile, Models.File file, string folderType)
        {
            await _fileHandlerService.UploadFile(uploadFile, file.Name, folderType);

            return "Reupload success!";
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

        public List<Material> GetApprovedSubjectMaterials(int subjectId)
        {
            return _context.Materials
                .Include(e => e.Subject)
                .Include(e => e.File)
                .Where(e => e.SubjectId == subjectId && e.IsApproved).ToList();
        }

        public List<Material> GetSearchMaterials(string searchS)
        {
            return _context.Materials
                .Include(e => e.Subject)
                .Include(e => e.File)
                .Where(e => e.File.Name.Contains(searchS)).ToList();
        }

        public List<Material> GetStatusMaterials(string status)
        {
            return _context.Materials
                .Include(e => e.Subject)
                .Include(e => e.File)
                .Where(e => e.status == status).ToList();
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

        public async Task<string> CreateMaterial(Material material, IFormFile uploadFile, Models.File file)
        {
            var result = await CreateFile(uploadFile, file, "Materials");

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

        public async Task<string> UpdateMaterialFile(IFormFile uploadFile, Material material)
        {
            var file = GetFile(material.FileId);

            return await ReuploadFile(uploadFile, file, "Materials");

        }

        public string DeleteMaterial(Material material)
        {
            var file = GetFile(material.FileId);

            _context.Materials.Remove(material);
            //_context.SaveChanges();

            return DeleteFile(file);
        }

        public string ApproveMaterial(int materialId)
        {
            var material = GetMaterial(materialId);

            material.status = "Checked";
            material.IsApproved = !material.IsApproved;

            return UpdateMaterial(material);
        }

        public string DestroyMaterial(int materialId)
        {
            var material = GetMaterial(materialId);

            material.IsDeleted = !material.IsDeleted;

            UpdateMaterial(material);

            return "Destroy material!";
        }

        public async Task<FileInfor> DownloadMaterial(int id)
        {
            var material = GetMaterial(id);

            return await DownloadFile(material.FileId, "Materials");
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

        public List<Exam> GetStatusExams(string status)
        {
            return _context.Exams
                .Include(e => e.Subject)
                .Include(e => e.File)
                .Where(e => e.status == status).ToList();
        }

        public List<Exam> GetTeacherSubjectExams(string teacherId)
        {
            return _context.Exams
                .Include(e => e.Subject)
                .Include(e => e.File)
                .Where(e => e.Subject.TeacherId == teacherId).ToList();
        }

        public List<Exam> GetSubNameExams(string subName)
        {
            return _context.Exams
                .Include(e => e.Subject)
                .Include(e => e.File)
                .Where(e => e.Subject.Name.Contains(subName)).ToList();
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
                .FirstOrDefault(e => e.Id == id);
        }

        public async Task<string> CreateExam(Exam exam, IFormFile uploadFile, Models.File file)
        {
            var result = await CreateFile(uploadFile, file, "Exams");

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
            var file = GetFile(exam.FileId);

            _context.Exams.Remove(exam);
            //_context.SaveChanges();

            return DeleteFile(file);
        }

        public string ApproveExam(int examId)
        {
            var exam = GetExam(examId);

            exam.status = "Checked";
            exam.IsApproved = !exam.IsApproved;

            return UpdateExam(exam);
        }

        public string DestroyExam(int examId)
        {
            var exam = GetExam(examId);

            exam.IsDeleted = !exam.IsDeleted;

            UpdateExam(exam);

            return "Destroy Exam!";
        }

        public async Task<FileInfor> DownloadExam(int id)
        {
            var exam = GetExam(id);

            return await DownloadFile(exam.FileId, "Exams");
        }
    }
}
