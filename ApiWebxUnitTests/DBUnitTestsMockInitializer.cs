using APIWEB.Context;
using APIWEB.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiWebxUnitTests
{
    public class DBUnitTestsMockInitializer
    {
        public DBUnitTestsMockInitializer()
        {

        }

        public void Seed(APPDBContext context)
        {
            context.Categorias.Add(new Categoria { CategoriaId = 1, Nome = "Categoria 1", ImagemUrl = @"http://www.teste.com.br/imagem1.jpg" });
            context.Categorias.Add(new Categoria { CategoriaId = 2, Nome = "Categoria 2", ImagemUrl = @"http://www.teste.com.br/imagem2.jpg" });
            context.Categorias.Add(new Categoria { CategoriaId = 3, Nome = "Categoria 3", ImagemUrl = @"http://www.teste.com.br/imagem3.jpg" });
            context.Categorias.Add(new Categoria { CategoriaId = 4, Nome = "Categoria 4", ImagemUrl = @"http://www.teste.com.br/imagem4.jpg" });
            context.Categorias.Add(new Categoria { CategoriaId = 5, Nome = "Categoria 5", ImagemUrl = @"http://www.teste.com.br/imagem5.jpg" });
            context.Categorias.Add(new Categoria { CategoriaId = 6, Nome = "Categoria 6", ImagemUrl = @"http://www.teste.com.br/imagem6.jpg" });
            context.Categorias.Add(new Categoria { CategoriaId = 7, Nome = "Categoria 7", ImagemUrl = @"http://www.teste.com.br/imagem7.jpg" });
            context.Categorias.Add(new Categoria { CategoriaId = 8, Nome = "Categoria 8", ImagemUrl = @"http://www.teste.com.br/imagem8.jpg" });
            context.Categorias.Add(new Categoria { CategoriaId = 9, Nome = "Categoria 9", ImagemUrl = @"http://www.teste.com.br/imagem9.jpg" });
            context.Categorias.Add(new Categoria { CategoriaId = 10, Nome = "Categoria 10", ImagemUrl = @"http://www.teste.com.br/imagem10.jpg" });
        }
    }
}
