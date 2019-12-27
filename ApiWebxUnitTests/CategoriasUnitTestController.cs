using APIWEB.Context;
using APIWEB.Controllers;
using APIWEB.DTOs;
using APIWEB.DTOs.Mappings;
using APIWEB.Repository;
using AutoMapper;
using AutoMapper.Configuration;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Xunit;

namespace ApiWebxUnitTests
{

    public class CategoriasUnitTestController
    {

        private readonly IUnitOfWork repository;
        private readonly IMapper mapper;
        public static DbContextOptions<APPDBContext> DbContextOptions { get; }
        public static string ConnetionString = @"Server=localhost;Database=APPDBAPI;Uid=sa;Pwd=W#k54*%#";

        static CategoriasUnitTestController()
        {
            DbContextOptions = new DbContextOptionsBuilder<APPDBContext>()
            .UseSqlServer(ConnetionString)
            .Options;

        }

        public CategoriasUnitTestController()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            mapper = config.CreateMapper();

            var context = new APPDBContext(DbContextOptions);

            //se base estivesse vazia utilizaria classe criada 
            //DBUnitTestsMockInitializer db = new DBUnitTestsMockInitializer();
            //db.Seed(context);

            //instancia do repositorio

            repository = new UnitOfWork(context);

        }

        //testes unitarios
        [Fact]
        public void GetCategorias_Return_OkResult()
        {
            //Arrange
            var controller = new CategoriasController(repository, null, null, mapper);

            //Act
            var data = controller.Get();

            //Assert
            Assert.IsType<List<CategoriaDTO>>(data.Value);
        }

        //Teste GET BadRequest
        [Fact]
        public void GetCategorias_Return_BadRequestResult()
        {
            //Arrange
            var controller = new CategoriasController(repository, null, null, mapper);

            //Act
            var data = controller.Get();

            //Assert
            Assert.IsType<BadRequestResult>(data.Result);
        }



        //Teste GET Retorna uma lista de objetos Categoria
        [Fact]
        public void GetCategorias_Return_MathResult()
        {
            //Arrange
            var controller = new CategoriasController(repository, null, null, mapper);

            //Act
            var data = controller.Get();

            //Assert
            Assert.IsType<List<CategoriaDTO>>(data.Value);

            var cat = data.Value.Should().BeAssignableTo<List<CategoriaDTO>>().Subject;

            Assert.Equal("Bebidas", cat[0].Nome);
            Assert.Equal("https://http2.mlstatic.com/banco-de-imagens-produtos-distribuidora-de-bebidas-D_NQ_NP_961761-MLB31173345214_062019-O.webp", cat[0].ImagemUrl);

            Assert.Equal("Sobremesas", cat[2].Nome);
            Assert.Equal("https://receitatodahora.com.br/wp-content/uploads/2017/08/sobremesa-para-o-dia-dos-pais.jpg", cat[2].ImagemUrl);


        }

        //Get por id retorn objeto Categoria
        [Fact]
        public void GetCategoriasById_Return_OkResult()
        {
            //Arrange
            var controller = new CategoriasController(repository, null, null, mapper);
            var categoriaId = 3;

            //Act
            var data = controller.Get(categoriaId);
            //Console.WriteLine(data);

            //Assert
            Assert.IsType<CategoriaDTO>(data.Value);
        }

        //Get por id retorno BadRequest()
        [Fact]
        public void GetCategoriasById_Return_NotFoundResult()
        {
            //Arrange
            var controller = new CategoriasController(repository, null, null, mapper);
            var categoriaId = 1;

            //Act
            var data = controller.Get(categoriaId);
            //Console.WriteLine(data);

            //Assert
            Assert.IsType<NotFoundResult>(data.Result);
        }


        //Post createdresult
        [Fact]
        public void Post_Categoria_AddValidData_Return_CreatedResult()
        {
            //Arrange
            var controller = new CategoriasController(repository, null, null, mapper);

            var cat = new CategoriaDTO()
            {
                Nome = "Teste Unitario 1",
                ImagemUrl = "imagem.jpg"
            };

            //Act
            var data = controller.Post(cat);

            //Assert
            Assert.IsType<CreatedAtRouteResult>(data.Result);
        }


        //PUT - altera objeto categoria
        [Fact]
        public void Put_Categorias_Update_ValidData_Return_OkResult()
        {
            //Arrange
            var controller = new CategoriasController(repository, null, null, mapper);
            var categoriaId = 9;

            //Act
            var existingPost = controller.Get(categoriaId);
            var resut = existingPost.Value.Should().BeAssignableTo<CategoriaDTO>().Subject;

            var catDto = new CategoriaDTO();
            catDto.CategoriaId = categoriaId;
            catDto.Nome = "Categoria Atualizada = Teste 1";
            catDto.ImagemUrl = resut.ImagemUrl;

            var updateData = controller.Put(categoriaId, catDto);
            //Console.WriteLine(data);

            //Assert
            Assert.IsType<OkResult>(updateData);
        }

        //Delete - exclui um objeto cateria
        [Fact]
        public void Delete_Categoria_Return_OkResult()
        {
            //Arrange
            var controller = new CategoriasController(repository, null, null, mapper);
            var categoriaId = 8;

            //Act
            var data = controller.Delete(categoriaId);

            Assert.IsType<CategoriaDTO>(data.Value);
        }



    }
}
