using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.server.Data;
using Usuarios.server.Models;

namespace TestProject1
{
    public abstract class TestBase : IDisposable
    {
        protected ApplicationDbContext context { get; private set; }

        protected ServiceProvider serviceProvider { get; private set; }
        public TestBase()
        {
            // Código de configuración común para las pruebas
            var service = new ServiceCollection();

            service.AddDbContext<ApplicationDbContext>(static options =>
            {
                options.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
                options.EnableSensitiveDataLogging();
            });

            serviceProvider = service.BuildServiceProvider();
            context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            context.Database.EnsureDeleted();
        }

        protected async Task SeedTestDatabase() 
        {

            var Usuarios = new List<Usuario>
            {
                new Usuario
                {
                    nombre = "Daniel Galvis",
                    correo = "vdagalvis@gmail.com",
                    lugarNacimiento = "Manizales",
                    dni = "1053814829",
                    direccion = "Vereda la Aurora",
                    cp = "17002",
                    ciudad = "Manizales",
                    movil = "3193949304",
                    firma = "Daniel",
                    tipo_usuario = "Estudiante",
                    bloque1 = "",
                    bloque2 = ""
                },

                new Usuario
                {
                    nombre = "Ana Perez",
                    correo = "anaperez@gmail.com",
                    lugarNacimiento = "Bogota",
                    dni = "1098765432",
                    direccion = "Calle 123 #45-67",
                    cp = "11001",
                    ciudad = "Bogota",
                    movil = "3001234567",
                    firma = "Ana",
                    tipo_usuario = "Profesor",
                    bloque1 = "",
                    bloque2 = ""
                }
            };

            var materias = new List<Materia>
            {
               new Materia
               {
                   codigo = "MAT101",
                   materia = "Matematicas Basicas",
                   semestre = 1
               },

               new Materia
               {
                    codigo = "FIS101",
                    materia = "Fisica Basica",
                    semestre = 1
               },

               new Materia
               {
                    codigo = "QUI101",
                    materia = "Quimica Basica",
                    semestre = 1
               },

               new Materia
               {
                    codigo = "HIS101",
                    materia = "Historia Universal",
                    semestre = 1
               },

               new Materia
               {
                    codigo = "BIO101",
                    materia = "Biologia General",
                    semestre = 1
               }
            };

            context.Usuarios.AddRange(Usuarios);
            context.Materias.AddRange(materias);
            await context.SaveChangesAsync();
        }

        protected void SetupValidModelState<T>(T model) where T : class
        {
            // Configuración común para modelos válidos
        }

        protected void SetupInvalidModelState<T>(T model) where T : class
        {
            // Configuración común para modelos inválidos
        }
        public void Dispose()
        {
            // Código de limpieza común para las pruebas
            context?.Dispose();
            serviceProvider?.Dispose();
        }
    }
}
