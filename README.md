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

<img src=img/fig-pasta-models.png>

Criar as classes categoria e produto
 
 <img src=img/fig-02.png>
 
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

<img src=img/03.png>
 
Restriçoes de rota
 
<img src=img/04.png>

<img src=img/05.png>
 
ModelBinding
[BindRequired]: torna obrigatório o parâmetro passado
[BindNever]: informa Model Binder para não vincular a informação ao parâmetro
 
<img src=img/06.png>
 
 
Data Annotations
Principais data annotation
 
<img src=img/07.png> 

<img src=img/08.png>

<img src=img/09.png>

<img src=img/10.png>
 
Validação Personalizada por modelo

<img src=img/11.png>


Duas Abordagem

<img src=img/12.png>

<img src=img/13.png>
 
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
 
<img src=img/14.png>
 
<img src=img/15.png>
 
<img src=img/16.png>

<img src=img/17.png>
 

Implementar filter de log
Microsoft.Extensions.Logging

<img src=img/18.png>
 
Aplicando o serviço

<img src=img/19.png>

Tratamento de erros global com Middleware
 
<img src=img/20.png>

<img src=img/21.png>

<img src=img/22.png>


Log de dados e Logging

<img src=img/23.png>

<img src=img/24.png>


Niveis de Log

<img src=img/25.png>

<img src=img/26.png>
 
<img src=img/27.png> 

Log Customizado
Padrão Repositorio
Padrao Unit of Work

<img src=img/28.png>
 
<img src=img/29.png>

<img src=img/30.png>

Implementar DTO – Data Transfer Object e Automapper
nuget
•	AutoMapper
•	AutoMapper.Extensions.Microsoft.DependencyInjection
            

<img src=img/31.png>

<img src=img/32.png>

<img src=img/33.png>

<img src=img/34.png>

<img src=img/35.png>

<img src=img/36.png>

<img src=img/37.png>

<img src=img/38.png>

<img src=img/39.png>

<img src=img/40.png>

<img src=img/41.png>

<img src=img/42.png>

Terceira forma
Microsoft.AspNetCore.Mvc.Api.Analyzers
 
<img src=img/43.png>

Instalar pacote nuget
Swashbuckle.AspNetCore.Swagger

<img src=img/44.png>

<img src=img/45.png>

Segurança
 
<img src=img/46.png>

<img src=img/47.png>

<img src=img/48.png>

<img src=img/49.png>

<img src=img/50.png> 

Instalar

<img src=img/51.png>

Microsoft.AspNetCore.Identity.EntityFrameworkCore
 
<img src=img/52.png>

<img src=img/53.png>
 
<img src=img/54.png>

System.IdentityModel.Tokens.JWT
Microsoft.AspNetCore.Authentication.JwtBearer

<img src=img/55.png>

<img src=img/56.png>

<img src=img/57.png>

<img src=img/58.png>
 
Aplicando CORS

<img src=img/59.png>

<img src=img/60.png>

<img src=img/61.png>

<img src=img/62.png>

<img src=img/63.png>

<img src=img/64.png>

<img src=img/65.png>

<img src=img/66.png>
   
<img src=img/67.png> 

Versionamento
Microsoft.AspNetCore.Mvc.Versioning

<img src=img/68.png>

<img src=img/69.png>

<img src=img/70.png>

Documentação Swagger

Swashbuckle.AspNetCore
Install-Package Swashbuckle.AspNetCore -Version 5.0.0-rc5
https://github.com/domaindrivendev/Swashbuckle.AspNetCore/blob/master/README.md
Microsoft.OpenApi

<img src=img/71.png>

<img src=img/72.png>
 
<img src=img/73.png> 

Enriquecendo documentação com comentários xml

<img src=img/74.png>

Habilitar documento xml

<img src=img/75.png>


Removendo os warning de xml comente

<img src=img/76.png>

 
Testes
Instalar tbem pacote nuget
FluentAssertions



<img src=img/77.png>

<img src=img/78.png>

<img src=img/79.png> 

Paginaçao

<img src=img/80.png>

<img src=img/81.png>

Padrao OData
Nuget: Microsoft.AspNetCore.OData
  
<img src=img/82.png>

<img src=img/83.png>

<img src=img/84.png>

<img src=img/85.png>

<img src=img/86.png>
