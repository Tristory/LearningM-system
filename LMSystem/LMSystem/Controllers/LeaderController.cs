using LMSystem.Models;
using LMSystem.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Security.Claims;

namespace LMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Leader")]
    public class LeaderController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IFileMService _fileMService;
        private readonly ISubjectMService _subjectMService;
        private readonly IMessageMService _messageMService;
        private readonly ISettingMService _settingMService;

        public LeaderController
        (
            IAuthService authService,
            IFileMService fileMService, 
            ISubjectMService subjectMService, 
            IMessageMService messageMService, 
            ISettingMService settingMService
        ) 
        { 
            _authService = authService;
            _fileMService = fileMService;
            _subjectMService = subjectMService;
            _messageMService = messageMService;
            _settingMService = settingMService;
        }
        
        // 1. Subject Management
        // Show the list of Subject
        [HttpGet("Get all subject")]
        public List<Subject> GetSubjects()
        {
            return _subjectMService.GetSubjects();
        }

        // Show the list of subject material
        [HttpGet("Get materials by subject Id")]
        public List<Material> GetSubjectMaterials(int subjectId)
        {
            return _fileMService.GetSubjectMaterials(subjectId);
        }

        // Approve material
        [HttpPost("Approve material")]
        public string ApproveMaterial(int materialId)
        {
            return _fileMService.ApproveMaterial(materialId);
        }

        // Destroy material
        [HttpPost("Destroy material")]
        public string DestroyMaterial(int materialId)
        {
            return _fileMService.DestroyMaterial(materialId);
        }

        // View material detail
        [HttpPost("View material detail")]
        public Material GetDetailMaterial(int materialId)
        {
            return _fileMService.GetDetailMaterial(materialId);
        }

        // Get material by status
        [HttpGet("Get material by status")]
        public List<Material> ByStatusGetMaterial(string status)
        {
            return _fileMService.GetStatusMaterials(status);
        }

        // Get subject detail
        [HttpGet("Get detail of subject")]
        public Subject GetDetailSubject(int subjectId)
        {
            return _subjectMService.GetDetailSubject(subjectId);
        }

        // Filter the subject
        [HttpGet("Get subject with filter")]
        public List<Subject> GetFilteredSubject(string filterBy, string filterValue)
        {

            switch(filterBy)
            {
                case "Name":
                    return _subjectMService.GetFilterNameSubjects(filterValue);
                case "Status":
                    return _subjectMService.GetFilterStatusSubjects(filterValue);
                case "Teacher":
                    return _subjectMService.GetFilterTeacherSubjects(filterValue);
                default:
                    break;
            }

            return null;
        }

        // Search for material
        [HttpGet("Search material")]
        public List<Material> SearchMaterial(string searchS)
        {
            return _fileMService.GetSearchedMaterials(searchS);
        }

        // Get material for download
        [HttpGet("Download material")]
        public async Task<IActionResult> DownloadMaterial(int materialId)
        {
            var fileInfor = await _fileMService.DownloadMaterial(materialId);

            return File(fileInfor.Bytes, fileInfor.ContentType, fileInfor.FilePath);
        }

        // 2. File Management
        // View all file
        [HttpGet("Get files")]
        public List<Models.File>  GetFiles()
        {
            return _fileMService.GetFiles();
        }

        // Upload file
        [HttpPost("Upload file")]
        public async Task<IActionResult> UploadFile(IFormFile formFile, FileFrame fileFrame)
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;

            if (userName == null)
                return Content("No user name!");

            var user = await _authService.GetApplicationUserByNameAsync(userName);

            if (user == null)
                return Content("No user like this!");

            var file = new Models.File
            {
                Name = fileFrame.Name,
                Type = fileFrame.Type,
                LastChangedD = DateTime.Now,
                OwnerId = user.Id
            };

            var result = await _fileMService.CreateFile(formFile, file, file.Type);

            return Content(result);
        }

        // Download file
        [HttpGet("Download file")]
        public async Task<IActionResult> DownloadFile(int fileId)
        {
            var file = _fileMService.GetFile(fileId);

            var fileInfor = await _fileMService.DownloadFile(file.Id, file.Type);

            return File(fileInfor.Bytes, fileInfor.ContentType, fileInfor.FilePath);
        }

        // Search file
        [HttpGet("Search file")]
        public List<Models.File> SearchFiles(string searchS)
        {
            return _fileMService.GetSearchFiles(searchS);
        }

        // Get file by type
        [HttpGet("Get file of type")]
        public List<Models.File> GetTypeFiles(string type)
        {
            return _fileMService.GetTypeFiles(type);
        }

        // View file before download
        [HttpGet("Show file")]
        public IFormFile ShowFile(int fileId)
        {
            var file = _fileMService.GetFile(fileId);

            return _fileMService.ShowFile(file);
        }

        // Change file's name
        [HttpPatch("Change file's name")]
        public IActionResult ChangeFileName(int fileId, string newName)
        {
            var file = _fileMService.GetFile(fileId);

            var result =  _fileMService.ChangeFileName(file, newName);

            return Content(result);
        }

        // Delete file
        [HttpDelete("Delete file")]
        public IActionResult DeleteFile(int fileId)
        {
            var file = _fileMService.GetFile(fileId);

            var result = _fileMService.DeleteFile(file);

            return Content(result);
        }

        // 3. Exam Management
        // Show the list of Exam
        [HttpGet("Get all exams")]
        public List<Exam> GetExams()
        {
            return _fileMService.GetExams();
        }

        // Filter exams
        [HttpGet("Filter exams")]
        public List<Exam> GetFilteredExams(string filterBy, string filterValue)
        {

            switch(filterBy)
            {
                case "Subject":
                    var intValue = int.Parse(filterValue);
                    return _fileMService.GetSubjectExams(intValue);
                case "Teacher":
                    return _fileMService.GetTeacherSubjectExams(filterValue);
                case "Status":
                    return _fileMService.GetStatusExams(filterValue);
                default:
                    break;
            }

            return null;
        }

        // Search exams
        [HttpGet("Search exams")]
        public List<Exam> GetSearchedExams(string searchS)
        {
            return _fileMService.GetSearchExams(searchS);
        }

        // Approve the exams
        [HttpPatch("Approve exam")]
        public IActionResult ApproveExam(int examId)
        {
            var result = _fileMService.ApproveExam(examId);

            return Content(result);
        }

        // Destroy the exams
        [HttpPatch("Destroy exam")]
        public IActionResult DestroyExam(int examId)
        {
            var result = _fileMService.DestroyExam(examId);

            return Content(result);
        }

        // Download exam
        [HttpGet("Dowload exam")]
        public async Task<IActionResult> DownloadExam(int examId)
        {
            var fileInfor = await _fileMService.DownloadExam(examId);

            return File(fileInfor.Bytes, fileInfor.ContentType, fileInfor.FilePath);
        }

        // Get detail exam
        [HttpGet("Detail exam")]
        public Exam GetDetailExam(int examId)
        {
            return _fileMService.GetDetailExam(examId);
        }

        // 4. Notification Management
        // Get all user notification
        [HttpGet("Get user notify")]
        public List<Notification> GetUserNotify(string userId)
        {
            return _messageMService.GetAllUserNotify(userId);
        }

        // Search for notification
        [HttpGet("Search notify")]
        public List<Notification> SearchNotify(string searchS)
        {
            return _messageMService.GetSearchNotify(searchS);
        }

        // Delete notification
        [HttpPatch("Delete notify")]
        public string SetNotifyToDeletable(int notifyId)
        {
            var notify = _messageMService.GetNotification(notifyId);

            return _messageMService.SetDeletable(notify);
        }

        // Set check and uncheck
        [HttpPatch("Change check")]
        public string ChangeNotifyCheck(int notifyId)
        {
            var notify = _messageMService.GetNotification(notifyId);

            return _messageMService.SetChecked(notify);
        }

        // Refresh notification can be achieved by recall the GetUserNotify

        // Access user setting
        [HttpGet("Get user setting")]
        public Setting GetUserSetting(string userId)
        {
            return _settingMService.GetUserSetting(userId);
        }

        // Update user setting
        [HttpPatch("Update user setting")]
        public string UpdateSetting(int settingId, SettingFrame settingFrame)
        {
            var setting = _settingMService.GetSetting(settingId);

            setting.Description = settingFrame.Description;

            return _settingMService.UpdateSetting(setting);
        }

        // Change the notification setting
        [HttpPatch("Change user notify setting")]
        public string ChangenNotifySetting(int settingId)
        {
            var setting = _settingMService.GetSetting(settingId);

            return _settingMService.NotitfySetting(setting);
        }

        // 5. Setting Management
        // Get User Setting is already showed above

        // Save is already showed above

        // Cancel is achievable by reload the get user setting instead of Update

        // Get all available role
        [HttpGet("Get roles")]
        public async Task<List<IdentityRole>> GetRoles()
        {
            return await _authService.GetRoles();
        }

        // Add more role, used when user sure that they want to add new role
        [HttpPost("Add role")]
        public async Task<string> CreateRole(string roleName)
        {
            if (await _authService.CreateRole(roleName))
                return "Create successed!";

            return "Not success!";
        }

        // To cancel the Create role, we can reload the page.

        // Delete role
        [HttpDelete("Delete role")]
        public async Task<string> DeleteRole(string roleName)
        {
            if (await _authService.DeleteRole(roleName))
                return "Delete successed!";

            return "Not success!";
        }

        // Get all user
        [HttpGet("Get users")]
        public async Task<List<ApplicationUser>> GetUsers()
        {
            return await _authService.GetApplicationUsers();
        }

        // Search for user
        [HttpGet("Search users")]
        public async Task<List<ApplicationUser>> SearchedUsers(string searchS)
        {
            return await _authService.GetUserByNameOrId(searchS);
        }

        // Get Users base on role
        [HttpGet("Get user by role")]
        public async Task<List<ApplicationUser>> GetUsersByRole(string roleName)
        {
            return await _authService.GetUsersByRole(roleName);
        }

        // Add user
        [HttpPost("Add user")]
        public async Task<string> MakeNewUser(LoginUser user)
        {
            if (await _authService.RegisterUser(user))
                return "Create successed!";

            return "Not success!";
        }

        // Edit user role
        [HttpPost("Give user role")]
        public async Task<string> GiveUserRole(string userName, string roleName)
        {
            return await _authService.GiveUserRole(userName, roleName);
        }

        [HttpPost("Delete user role")]
        public async Task<string> DeleteUserRole(string userName, string roleName)
        {
            return await _authService.DeleteUserRole(userName, roleName);
        }

        // Remove user
        [HttpDelete("Delete user")]
        public async Task<string> DeleteUser(string userId)
        {
            if (await _authService.DeleteUser(userId))
                return "Delete successed!";

            return "Not success!";
        }

        // 6. Send help
        // Show all request
        [HttpGet("Show requests")]
        public List<Request> GetRequests()
        {
            return _messageMService.GetRequests();
        }

        // Make a request
        [HttpPost("Make request")]
        public async Task<string> CreateRequest(RequestFrame requestFrame)
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;

            if (userName == null)
                return "No user name!";

            var user = await _authService.GetApplicationUserByNameAsync(userName);

            if (user == null)
                return "No user like this!";

            var request = new Request
            {
                Header = requestFrame.Header,
                Content = requestFrame.Content,
                SenderId = user.Id
            };

            return _messageMService.CreateRequest(request);
        }

        // 7. Account Management
        // Alot of User information is inside of setting table, so in this project get account info will be the same as get user setting

        // Change or Upload portrait picture
        [HttpPost("Change portrait")]
        public async Task<string> ChangePortrait(int settingId, IFormFile portrait)
        {
            var setting = _settingMService.GetSetting(settingId);

            return await _settingMService.ChangePotraitSetting(setting, portrait);
        }

        // Delete portrait picture
        [HttpDelete("Delete portrait")]
        public string DeletePortrait(int settingId)
        {
            var setting = _settingMService.GetSetting(settingId);

            return _settingMService.DeleteSetting(setting);
        }

        // Change and save password
        [HttpPatch("Change password")]
        public async Task<string> ChangePassword(string userId, string oldPassword, string newPassword)
        {
            var user = await _authService.GetApplicationUserByIdAsync(userId);

            if (user == null)
                return "No user!";

            var result = await _authService.ChangePassword(user, oldPassword, newPassword);

            return result;
        }

        // If we want to cancel, we can alway reload the page.*/
    }
}
