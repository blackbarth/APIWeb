APIWeb
WebAPI

Referência EFCore e Provedor Banco de dados Pomelo
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.Tools – Add-Migration, Update-Database
Pomelo.EntityFrameworkCore.MySql – provedor para MySql
Microsoft.EntityFrameworkCore.SqlServer – Provedor SqlServer
Microsoft.AspNetCore.Mvc.NewtonsoftJson
Microsoft.Extensions.Logging


Criar Pasta Models
<img src=”img/fig-pasta-models.png”>

Criar as classes categoria e produto
 
Classe: Categoria
    public class Categoria
    {
        public Categoria()
        {
            Produtos = new Collection<Produto>();
        }

        [Key]
        public int CategoriaId { get; set; }
        public string Nome { get; set; }
        public string ImagemUrl { get; set; }
        public ICollection<Produto> Produtos { get; set; }
    }

Classe: Produto
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public string ImagemUrl { get; set; }
        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }
        public Categoria Categoria { get; set; }
        public int CategoriaId { get; set; }
    }

Package Manager Console
PM> Add-Migration
PM> Update-database
 
Restriçoes de rota
 
 

ModelBinding
[BindRequired]: torna obrigatório o parâmetro passado
[BindNever]: informa Model Binder para não vincular a informação ao parâmetro
 

Data Annotations
Principais data annotation
 
 

 
 
Validação Personalizada por modelo
 
Duas Abordagem
 
 
    public class PrimeiraLetraMaiusculaAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            //logica de validação customizada

            var primeiraLetra = value.ToString()[0].ToString();
            if (primeiraLetra != primeiraLetra.ToUpper())
            {
                return new ValidationResult("A Primeira letra do nome do produto deve ser maiúscula");
            }
            return ValidationResult.Success;

        }
    }
2 – Abordagem Implementar IValidatableObject 
public class Produto : IValidatableObject
    {
        [Key]
        public int ProdutoId { get; set; }
        [Required(ErrorMessage = "Preenchimento Obrigatório.")]
        [MaxLength(80)]
        [PrimeiraLetraMaiuscula]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Preenchimento Obrigatório.")]
        [MaxLength(250)]
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        [MaxLength(300)]
        public string ImagemUrl { get; set; }
        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }
        public Categoria Categoria { get; set; }
        public int CategoriaId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(this.Nome))
            {
                var primeiraletra = this.Nome[0].ToString();
                if (primeiraletra != primeiraletra.ToUpper())
                {
                    yield return new
                        ValidationResult("A Primeira letra do produto deve ser maiuscula",
                        new[]{
                            nameof(this.Nome)
                            });
                }

                if (this.Estoque <= 0)
                {
                    yield return new
          ValidationResult("Estoque tem que ser maior que Zero",
          new[]{
                            nameof(this.Estoque)
              });
                }
            }
        }
    }

Fundamentos Middleware
Filtros:
 

 
 
 
Implementar filter de log
Microsoft.Extensions.Logging
 
Aplicando o serviço
 

Tratamento de erros global com Middleware
 
 
 

Log de dados e Logging
 
 
Niveis de Log
 
 
 
Log Customizado
Padrão Repositorio
Padrao Unit of Work
 
 

 
Implementar DTO – Data Transfer Object e Automapper
nuget
•	AutoMapper
•	AutoMapper.Extensions.Microsoft.DependencyInjection
            

Terceira forma
Microsoft.AspNetCore.Mvc.Api.Analyzers
 

Instalar pacote nuget
Swashbuckle.AspNetCore.Swagger

  
Segurança
 
 
 
 
 
Instalar
 
Microsoft.AspNetCore.Identity.EntityFrameworkCore
 
 

 

System.IdentityModel.Tokens.JWT
Microsoft.AspNetCore.Authentication.JwtBearer

 
 
 
 

Aplicando CORS
 


  
 
 
   
 
Versionamento
Microsoft.AspNetCore.Mvc.Versioning




  

 


Documentação Swagger

Swashbuckle.AspNetCore
Install-Package Swashbuckle.AspNetCore -Version 5.0.0-rc5
https://github.com/domaindrivendev/Swashbuckle.AspNetCore/blob/master/README.md
Microsoft.OpenApi

 
 
 
Enriquecendo documentação com comentários xml
 
Habilitar documento xml
 

Removendo os warning de xml comente

 
Testes
Instalar tbem pacote nuget
FluentAssertions
  
 
Paginaçao
  

Padrao OData
Nuget: Microsoft.AspNetCore.OData
  
   
