using LMSystem.Models;

namespace LMSystem.Repository
{
    public interface IFileMService
    {
        string ApproveExam(int examId);
        string ApproveMaterial(int materialId);
        string ChangeFileName(Models.File file, string newName);
        // string CreateExam(Exam exam, IFormFile uploadFile, Models.File file);
        Task<string> CreateFile(IFormFile uploadFile, Models.File file, string folderType);
        //string CreateMaterial(Material material, IFormFile uploadFile, Models.File file);
        string DeleteExam(Exam exam);
        string DeleteFile(Models.File file);
        string DeleteMaterial(Material material);
        string DestroyMaterial(int materialId);
        Task<FileInfor> DownloadFile(int id, string folderType);
        List<Material> GetCheckedMaterials();
        Exam GetDetailExam(int id);
        Material GetDetailMaterial(int id);
        Exam GetExam(int id);
        List<Exam> GetExams();
        Models.File GetFile(int id);
        Exam GetFileExam(int fileId);
        Material GetFileMaterial(int fileId);
        Material GetMaterial(int id);
        List<Material> GetMaterials();
        List<Exam> GetSubNameExams(string searchS);
        List<Material> GetSearchedMaterials(string searchS);
        List<Exam> GetSearchExams(string searchS);
        List<Material> GetSearchMaterials(string searchS);
        List<Exam> GetSubjectExams(int subjectId);
        List<Material> GetSubjectMaterials(int subjectId);
        List<Exam> GetStatusExams(string status);
        List<Material> GetStatusMaterials(string status);
        List<Exam> GetTeacherSubjectExams(string teacherId);
        List<Material> GetTeacherSubjectMaterials(string teacherId);
        List<Models.File> GetTypeFiles(string type);
        List<Material> GetUncheckedMaterials();
        List<Models.File> GetUserFiles(string userId);
        IFormFile ShowFile(Models.File file);
        string UpdateExam(Exam exam);
        string UpdateFile(Models.File file);
        string UpdateMaterial(Material material);
        Task<FileInfor> DownloadMaterial(int id);
        List<Models.File> GetFiles();
        List<Models.File> GetSearchFiles(string searchS);
        string DestroyExam(int examId);
        Task<FileInfor> DownloadExam(int id);
        List<Material> GetApprovedSubjectMaterials(int subjectId);
        Task<string> CreateExam(Exam exam, IFormFile uploadFile, Models.File file);
        Task<string> CreateMaterial(Material material, IFormFile uploadFile, Models.File file);
        Task<string> UpdateMaterialFile(IFormFile uploadFile, Material material);
    }
}