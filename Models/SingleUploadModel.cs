using System.ComponentModel.DataAnnotations;

namespace Ex_data_base_com_imagem_e_arquivos.Models;
public class SingleUploadModel : ReponseModel
{
    [Required(ErrorMessage = "Please enter file name")]
    public string? FileName { get; set; }
    [Required(ErrorMessage = "Please select file")]
    public IFormFile File{ get; set; }
}