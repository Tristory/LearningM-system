using LMSystem.Models;

namespace LMSystem.Repository
{
    public interface IClassMService
    {
        string CreateClass(Class pclass);
        string CreateClassMaterial(ClassMaterial classMaterial);
        string CreateLecture(Lecture lecture);
        string DeleteClass(Class pclass);
        string DeleteClassMaterial(ClassMaterial classMaterial);
        string DeleteLecture(Lecture lecture);
        Class GetClass(int id);
        List<Lecture> GetClassLecture(int classId);
        List<ClassMaterial> GetClassMaterials(int classId);
        Lecture GetDetailLecture(int id);
        List<Class> GetSubjectClasses(int subjectId);
        List<Lecture> GetSubjectLectures(int subjectId);
        string UpdateClass(Class pclass);
        string UpdateClassMaterial(ClassMaterial classMaterial);
        string UpdateLecture(Lecture lecture);
    }
}