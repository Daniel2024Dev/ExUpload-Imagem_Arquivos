using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Ex_data_base_com_imagem_e_arquivos.Models;

namespace Ex_data_base_com_imagem_e_arquivos.Controllers;
public class MultipleUploadController : Controller
{
    public IActionResult MultipleUpload()
    {
        MultipleUploadModel model = new MultipleUploadModel();
        return View(model);
    }

    [HttpPost]
    public IActionResult MultipleUpload(MultipleUploadModel model)
    {
        if (ModelState.IsValid)
        {
            model.IsResponse = true;
            if (model.Files.Count > 0)
            {
                /*
                    Recebe Arquivo por parâmetro de List em MultipleUploadModel
                    ((( MultipleUploadModel model))) >>> MultipleUploadModel
                */
                foreach (var file in model.Files)
                {
                    //diretório dos arquivos
                    string CaminhoImagens = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/ImagesMain");

                    

                    //criar pasta se não existir
                    if (!Directory.Exists(CaminhoImagens))
                        Directory.CreateDirectory(CaminhoImagens);

                    //Adiciona um novo nome das imagens para evitar nomes iguais 
                    //NovoNomeParaImagens recebe também file.FileName >>> Nome das imagens
                    string NovoNomeParaImagens = Guid.NewGuid().ToString() + "_" + file.FileName;

                    //Concatena CaminhoImagens + NovoNomeParaImagens por parâmetro dentro de SalvarImagens  
                    string SalvarImagens = Path.Combine(CaminhoImagens, NovoNomeParaImagens);

                    using (var stream = new FileStream(SalvarImagens, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                //Retorno para MultipleUpload por parâmetro
                model.IsSuccess = true;
                model.Message = "Files upload successfully";
            }
            else
            {   
                //Retorno para MultipleUpload por parâmetro
                model.IsSuccess = false;
                model.Message = "Please select files";
            }
        } //Retorno para MultipleUpload em View
        return View("MultipleUpload", model);
    }
}