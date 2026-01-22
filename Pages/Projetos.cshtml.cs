using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class ProjetosModel : PageModel
{
    private const int PageSize = 6;

    public class Project
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string DemoUrl { get; set; }
        public string RepoUrl { get; set; }
        public List<string> Tech { get; set; }
    }

    private List<Project> AllProjects = new List<Project>
    {
        //new Project { Title = "API de Pagamentos", Description = "API RESTful em .NET 10 com autenticação e testes.", Image = "/imagens/projects/project1.jpg", DemoUrl = "#", RepoUrl = "#", Tech = new List<string>{" .NET","EF Core","xUnit"} },
        //new Project { Title = "Dashboard Admin", Description = "SPA com React, gráficos e otimizações de performance.", Image = "/imagens/projects/project2.jpg", DemoUrl = "#", RepoUrl = "#", Tech = new List<string>{"React",".NET","SignalR"} },
        //new Project { Title = "Microserviço Billing", Description = "Microserviço com Kafka e observabilidade.", Image = "/imagens/projects/project3.jpg", DemoUrl = "#", RepoUrl = "#", Tech = new List<string>{"Microservices","Kafka",".NET"} },
        new Project { Title = "Site para Studio de Beleza", Description = "Desenvolvido com ASP.NET Core Razor Pages; responsivo, performático e acessível.", Image = "/imagens/studiothamy.png", DemoUrl = "#", RepoUrl = "https://github.com/Davi-reis/Portifolio", Tech = new List<string>{"ASP.NET Core","Razor Pages","C#","HTML","CSS","Bootstrap","JavaScript"} },
        //new Project { Title = "Integração OAuth", Description = "Fluxo OAuth com provedores externos.", Image = "/imagens/projects/project5.jpg", DemoUrl = "#", RepoUrl = "#", Tech = new List<string>{"OAuth",".NET","OpenID"} },
        //new Project { Title = "App Mobile (PWA)", Description = "PWA com caching e notificações.", Image = "/imagens/projects/project6.jpg", DemoUrl = "#", RepoUrl = "#", Tech = new List<string>{"PWA","React","Service Worker"} },
        //new Project { Title = "Automação CI/CD", Description = "Pipelines com testes e deploy automatizado.", Image = "/imagens/projects/project7.jpg", DemoUrl = "#", RepoUrl = "#", Tech = new List<string>{"CI/CD","Azure DevOps","Docker"} },
        //new Project { Title = "Search Engine", Description = "Busca avançada com ElasticSearch.", Image = "/imagens/projects/project8.jpg", DemoUrl = "#", RepoUrl = "#", Tech = new List<string>{"ElasticSearch",".NET","Docker"} }
    };

    public List<Project> PagedProjects { get; set; } = new List<Project>();
    public List<string> Technologies { get; set; } = new List<string>();

    [BindProperty(SupportsGet = true)]
    public string SelectedTech { get; set; }

    [BindProperty(SupportsGet = true)]
    public int Page { get; set; } = 1;

    public int TotalPages { get; set; }

    public void OnGet()
    {
        Technologies = AllProjects.SelectMany(p => p.Tech).Select(t => t.Trim()).Distinct(StringComparer.OrdinalIgnoreCase).OrderBy(t => t).ToList();

        var filtered = string.IsNullOrWhiteSpace(SelectedTech)
            ? AllProjects
            : AllProjects.Where(p => p.Tech.Any(t => string.Equals(t.Trim(), SelectedTech.Trim(), StringComparison.OrdinalIgnoreCase))).ToList();

        var count = filtered.Count;
        TotalPages = Math.Max(1, (int)Math.Ceiling(count / (double)PageSize));
        if (Page < 1) Page = 1;
        if (Page > TotalPages) Page = TotalPages;

        PagedProjects = filtered.Skip((Page - 1) * PageSize).Take(PageSize).ToList();
    }
}