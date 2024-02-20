using System.ComponentModel.DataAnnotations;

namespace Ex_data_base_com_imagem_e_arquivos.Models;

public class MultipleUploadModel : ReponseModel
{
   [Required(ErrorMessage = "Please select files")]
    public List<IFormFile> Files { get; set; }
}