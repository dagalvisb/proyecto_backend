using Usuarios.server.Controllers;
using Usuarios.server.Models;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Class Data = new Class();

            int result = Data.suma(5, 3, 1);
            Assert.Equal(8, result);
        }

        [Fact]
        public void Test2()
        {
            Class Data = new Class();

            int result = Data.suma(5, 3, 2);
            Assert.Equal(2, result);
        }

        [Fact]
        public void Test3()
        {
            Class Data = new Class();

            int result = Data.suma(5, 3, 3);
            Assert.Equal(15, result);
        }

        [Fact]
        public void Test4()
        {
            Class Data = new Class();

            int result = Data.suma(5, 3, 4);
            Assert.Equal(0, result);
        }

        [Fact]
        public void testUsuarios()
        {
            UsuariosController usuariosController = new UsuariosController(null, null);

            string result = usuariosController.GetMateriasUnicas();
            
        }
    }
}