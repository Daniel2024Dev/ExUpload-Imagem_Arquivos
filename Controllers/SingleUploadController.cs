using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Ex_data_base_com_imagem_e_arquivos.Models;

namespace Ex_data_base_com_imagem_e_arquivos.Controllers;

public class SingleUploadController : Controller
{
    public IActionResult SingleUpload()
    {
        //Passando os objetos de SingleUploadModel para View por parâmetro
        SingleUploadModel model = new SingleUploadModel();
        return View(model);
    }

    // Recebe (SingleUploadModel imagem) por Parâmetro
    [HttpPost]
    public IActionResult SingleUpload(SingleUploadModel imagem)
    {
        if (ModelState.IsValid)
        {

            imagem.IsResponse = true;

            //diretório das imagens
            string CaminhoImagens = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/ImagesMain");

            //Adiciona um novo nome das imagens para evitar nomes iguais  
            string NovoNomeParaImagens = Guid.NewGuid().ToString();

            //criar pasta se não existir
            if (!Directory.Exists(CaminhoImagens))
                Directory.CreateDirectory(CaminhoImagens);

            /*
            objetos vindo de SingleFileModel 
                File    FileName 
            */
            //obter extensão de arquivo
            FileInfo fileInfo = new FileInfo(imagem.File.FileName);

            //FileName >>> informações da imagem
            string NomeImagens = imagem.FileName + "_" + NovoNomeParaImagens + fileInfo.Extension;

            //Concatena CaminhoImagens + NomeImagens por parâmetro dentro de SalvarImagens  
            string SalvarImagens = Path.Combine(CaminhoImagens, NomeImagens);

            //Função que faz a copia dos arquivos e evita erros
            using (var stream = new FileStream(SalvarImagens, FileMode.Create))
            {
                imagem.File.CopyTo(stream);
            }

            //Retorno para SingleUpload por parâmetro
            imagem.IsSuccess = true;
            imagem.Message = "File upload successfully";
        }//Retorno para SingleUpload em View
        return View("SingleUpload", imagem);
    }



}




