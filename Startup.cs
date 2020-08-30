using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using web_identity_csharp_base.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using web_identity_csharp_base.Services;
using System.Text;

namespace web_identity_csharp_base
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _env;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // verifica o ambiente atual se é desenvolvimento ou produção
            if (_env.IsDevelopment())
            {
                Console.WriteLine(_env.EnvironmentName);
            }
            else if (_env.IsStaging())
            {
                Console.WriteLine(_env.EnvironmentName);
            }
            else
            {
                Console.WriteLine("Not dev or staging");
            }

            // busca a string de conexão no arquivo appsettings.json na seção connectionStrings
            var connString = Configuration["ConnectionStrings:database"];

            // inicia o banco de dados no Postgres, se necessário altere 
            services.AddDbContext<ApplicationDBContext>(o => o.UseNpgsql(connString));

            // inicia o servico de identidade no entity framework, troque as classes de usuário e perfil se necessário
            // se necessário altere o banco de dados, pode ficar no mesmo banco da aplicação ou em outro separado
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDBContext>();

            // configura as opções padrões do Identy, força de senha, etc
            services.Configure<IdentityOptions>(options => {
                // configura password com dígitos, tamanho mínimo 8, e sem necessidade de alfanuméricos
                // options.Password.RequireDigit = true;
                // options.Password.RequiredLength = 8;
                // options.Password.RequireNonAlphanumeric = false;

                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 2;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                // configura se o usuário errar a senha 4 vezes, bloqueia por 5 minutos
                options.Lockout.MaxFailedAccessAttempts = 4;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);

                // // configura para que seja obrigatório o email de confirmação ao registrar
                // options.SignIn.RequireConfirmedEmail = true;

            });

            // Carrega toda a seção de configuração SMTP na classe Smtp options e injeta
            //services.Configure<SmtpOptions>(Configuration.GetSection("Smtp"));

            // configura o cookie de login e duração do cookie e 
            // indica as páginas de login e acesso negado, usado em MVC
            services.ConfigureApplicationCookie(options => {
                options.LoginPath = "/Authentication/SignIn";
                options.AccessDeniedPath = "/Authentication/AccessDenied";
                // especifica o tempo para o token expirar
                options.ExpireTimeSpan = TimeSpan.FromHours(8);
            });

            // utiliza policy
            // services.AddAuthorization(options => {
            //     options.AddPolicy("MemberSales", p => {
            //         p.RequireClaim("Department", "Sales").RequireRole("Member");
            //     });
            // });

            // le as configurações do token no appsettings
            var tokenKey = Configuration["Tokens:Key"];
            var tokenAudience = Configuration["Tokens:Audience"];
            var tokenIssuer = Configuration["Tokens:Issuer"];

            // adiciona o serviço de autenticação na aplicação através de JWT Token
            services.AddAuthentication().AddJwtBearer(options => {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters{
                    ValidIssuer = tokenIssuer,
                    ValidAudience = tokenAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey))
                };

            });

            // // habilita a interface de envio de email com smtpemailsender na forma de singleton para toda aplicação
            // services.AddSingleton<IEmailSender, SmtpEmailSender>();

            //adiciona os controlers
            services.AddControllers();

            // adiciona o CORS, quando API e frontend não possuem o mesmo endereço
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }else{
                // Habilita o protocolo HTTPS
                app.UseHttpsRedirection();
            }

            // permite o acesso do CORS de qualquer origem
            app.UseCors(option => {
                option.AllowAnyOrigin();
                option.AllowAnyHeader();
                option.AllowAnyMethod();
            }); 

            app.UseRouting();

            // indica que esta usando autenticação
            app.UseAuthentication();

            // indica que esta usando autorização
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
