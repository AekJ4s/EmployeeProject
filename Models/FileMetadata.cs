using System.ComponentModel.DataAnnotations;

namespace myFirstProject.Models

{
    public class FileMetadata
    {

    }

    [MetadataType(typeof(FileMetadata))]

    public partial class File{
        public static File Create(EmployeeContext db, File file)
        {
            file.CreateDate = DateTime.Now;
            file.UpdateDate = DateTime.Now;
            file.IsDelete = false;
            db.Files.Add(file);
            db.SaveChanges();

            return file;
        }
    }
}