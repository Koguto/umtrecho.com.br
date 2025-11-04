using Autenticacao.servicos;
using Autenticacao.Settings;
using Cliente;
using Clientes;
using Clientes.Servico.Cadastro;
using DAL;
using DAL.Modelos;
using Emails;
using MercadoPago;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Pagamentos;
using Produtos;
using System;
using System.Text;
using Tokens;
using Tokens.Servicos;

namespace Site
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Método para adicionar serviços ao container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(options =>
               {
                   options.Cookie.HttpOnly = false;
                   options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                   options.Cookie.SameSite = SameSiteMode.Strict;
                   options.ExpireTimeSpan = TimeSpan.FromHours(1); // Tempo de expiração do cookie
                   options.LoginPath = "/Conta/Login"; // Caminho de login
                   options.LogoutPath = "/Conta/Logout";

               });


            // Adicionando controllers com Views
            services.AddControllersWithViews();
            services.AddDbContext<LojasContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("Conexao")));


            // Carregar configurações do appsettings.json
            services.Configure<MercadoPagoSettings>(Configuration.GetSection("MercadoPago"));
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.Configure<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
            string chaveSecreta = Configuration["AppSettings:TokenSecret"];


            // Registrar serviços com escopo específico
            services.AddSingleton<MercadoPagoAuthService>();
            services.AddSingleton<ProdutoRepositorio>();
            services.AddTransient<EmailService>();
            services.AddTransient<PerfilService>();
            services.AddTransient<ICadastroService, CadastroService>();
            services.AddTransient<ICadastroRepository, CadastroRepository>();
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IToken, Token>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IPagamentoOpcaoServico, PagamentoOpcaoServico>();
            services.AddHttpClient<MercadoPagoService>();


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
#if DEBUG
                    ValidIssuer = "https://localhost:44378", // Altere para o seu Issuer
                    ValidAudience = "https://localhost:44378", // Altere para sua Audience
#else
                     ValidIssuer = "https://mofya.com.br", // Altere para o seu Issuer
                    ValidAudience = "https://mofya.com.br", // Altere para sua Audience
#endif
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecreta)) // Altere para sua chave secreta
                };
            });

            services.AddAuthorization();

        }

        // Método para configurar o pipeline de requisição HTTP
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // Mapeando Razor Pages
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages(); // ESSENCIAL para Razor Pages
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });



        }
    }
}
