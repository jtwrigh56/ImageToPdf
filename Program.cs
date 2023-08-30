using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("------------------------------Document Creation Started------------------------------");
        ImageToPdf();
    }

    static void ImageToPdf()
    {
        Document document = new Document();

        var list = ImageNames();

        using (var stream = new FileStream("Test.pdf", FileMode.Create, FileAccess.Write, FileShare.None))
        {
            PdfWriter.GetInstance(document, stream);
            document.Open();
            foreach (var item in list)
            {
                using (var imageStream = new FileStream(@$"ADD YOUR PATH HERE\Images\{item}", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {

                    var image = Image.GetInstance(imageStream);
                    var scalePercent = (((document.PageSize.Width / image.Width) * 100) - 4);
                    image.ScalePercent(scalePercent);
                    Console.WriteLine($"{item} has been added to the document!");
                    document.Add(image);
                }
            }
            document.Close();

            Console.WriteLine("Document created Successfully");
        }
    }

    static List<string> ImageNames()
    {
        List<string> list = new List<string>();
        string folder = @"ADD YOUR PATH HERE\Images\";
        Console.WriteLine("------------------------Gathering Images-------------------------");
        var files = Directory.GetFiles(folder, "*.jpeg");
        foreach (var file in files)
        {
            string fileName = Path.GetFileName(file);
            var fileInfo = new FileInfo(file);
            long fileSize = fileInfo.Length;
            Console.WriteLine($"{fileName} / {fileSize}");
            list.Add(fileName);
        }
        return list;
    }
}

