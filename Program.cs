using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VendaDeSementes.Models;
using VendaDeSementes.Data;

var builder = WebApplication.CreateBuilder(args);

// Configuração do Entity Framework para os contextos de banco de dados
builder.Services.AddDbContext<Contexto>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("conexao")));

builder.Services.AddDbContext<DBContexto>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("conexao")));

// Configuração de identidade com suporte para papéis (roles)
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true; // Exige conta confirmada para login
})
    .AddRoles<IdentityRole>() // Adiciona suporte para papéis
    .AddEntityFrameworkStores<DBContexto>(); // Usa DBContexto como repositório de identidade

// Adiciona suporte para controladores e visualizações
builder.Services.AddControllersWithViews();

// Adiciona suporte para páginas Razor (necessário para a área de identidade)
builder.Services.AddRazorPages();

var app = builder.Build();

// Configuração do pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Página de erro customizada
    app.UseHsts(); // Força o uso de HTTPS com HSTS
}

app.UseHttpsRedirection(); // Redireciona automaticamente para HTTPS
app.UseStaticFiles(); // Suporte para arquivos estáticos (CSS, JS, imagens)

app.UseRouting(); // Configuração de rotas

// Middleware de autenticação e autorização
app.UseAuthentication(); // Necessário para autenticação
app.UseAuthorization(); // Necessário para autorização

// Mapeamento de rotas para controladores
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Mapeamento de rotas para páginas Razor (incluindo a área de identidade)
app.MapRazorPages();

// Inicializa o aplicativo
app.Run();
