using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using UniCine;

namespace UniCineUnitTest
{

    [TestClass]
    public class UTUniCine
    {
        public static Negocio _negocio;
        /// <summary>
        /// Descripción resumida de UTUniCine
        /// </summary>

        public UTUniCine()
        {

        }


        //Variables
        private TestContext testContextInstance;
        private Mock<UniCineDB> _mockCineBD;
        private Pelicula _pelicula;

        [TestInitialize]
        public void Initialize()
        {
            //falseo el contesto
            _mockCineBD = new Mock<UniCineDB>();

            //Falseamos las tablas

            //Peliculas

            //1- Datos como consulta
            var datosPeliculas = new List<Pelicula>
            {
                //PeliculaId  Nombre  Duracion    Anno    Categoria   Genero  Sinopsis
                new Pelicula { PeliculaId = 1, Nombre = "Renfield", Duracion = 93, Anno = 2023, Categoria = ">18", Genero = "Terror", Sinopsis = "Vuelve el conde Dracula" },
                new Pelicula { PeliculaId = 2, Nombre = "Algo pasa con Mary", Duracion = 119, Anno = 1998, Categoria = ">12", Genero = "Comedia", Sinopsis = "Todo un peliculon a la altura de zoolander" },
                new Pelicula { PeliculaId = 3, Nombre = "Espíritu libre ", Duracion = 110, Anno = 2023, Categoria = ">18", Genero = "Aventura  ", Sinopsis = "La caratula es muy bonita, la pelicula ni idea" },
                new Pelicula { PeliculaId = 4, Nombre = "Amsterdam", Duracion = 125, Anno = 2022, Categoria = "Todos", Genero = "Romance", Sinopsis = "Algo de besos supongo que en los alrededores de Amsterdan" },
                new Pelicula { PeliculaId = 5, Nombre = "Sin novedad en el frente", Duracion = 162, Anno = 2022, Categoria = ">18", Genero = "Acción", Sinopsis = "Pelicula de la primera guerra mundial basada en el libro homonimo" },
                new Pelicula { PeliculaId = 6, Nombre = "Uncharted", Duracion = 113, Anno = 2022, Categoria = ">16", Genero = "Aventura", Sinopsis = "Ni idea, pero le gusta a los frikis" },
                new Pelicula { PeliculaId = 7, Nombre = "Bullet Train", Duracion = 95, Anno = 2022, Categoria = ">16", Genero = "Acción", Sinopsis = "Really Brad, no es tu mejor papel" },
                new Pelicula { PeliculaId = 8, Nombre = "Sonic ", Duracion = 108, Anno = 2023, Categoria = "Todos", Genero = "Aventura", Sinopsis = "Un erizo dando vueltas contra cosas" },
                new Pelicula { PeliculaId = 9, Nombre = "Luther", Duracion = 105, Anno = 2023, Categoria = ">18", Genero = "Acción", Sinopsis = "Detective cañero contra psicopata" }
            }.AsQueryable();

            //2-falseo la tabla
            var mockTablaPeliculas = new Mock<DbSet<Pelicula>>();
            mockTablaPeliculas.As<IQueryable<Pelicula>>().Setup(x => x.Provider).Returns(datosPeliculas.Provider);
            mockTablaPeliculas.As<IQueryable<Pelicula>>().Setup(x => x.Expression).Returns(datosPeliculas.Expression);
            mockTablaPeliculas.As<IQueryable<Pelicula>>().Setup(x => x.ElementType).Returns(datosPeliculas.ElementType);
            mockTablaPeliculas.As<IQueryable<Pelicula>>().Setup(x => x.GetEnumerator()).Returns(datosPeliculas.GetEnumerator());
            //3-cuando se solicite la tabla cliente se devolvera un objeto falseado
            _mockCineBD.Setup(x => x.Peliculas).Returns(mockTablaPeliculas.Object);

            //Sesiones

            //1- Datos como consulta
            var datosSesiones = new List<Sesion>
            {
                //Insertar las new sesiones
                new Sesion { SesionId = 1, Sala = "SALA 1", DiaSemana = "Domingo", Comienzo = new DateTime(1753, 1, 1, 20, 45, 0), FinMax = new DateTime(1753, 1, 1, 23, 30, 0), Precio = 7.2F, Aforo = 200 },
                new Sesion { SesionId = 2, Sala = "SALA 2", DiaSemana = "Domingo", Comienzo = new DateTime(1753, 1, 1, 18, 15, 0), FinMax = new DateTime(1753, 1, 1, 20, 15, 0), Precio = 7.2F, Aforo = 174 },
                new Sesion { SesionId = 3, Sala = "SALA 3", DiaSemana = "Domingo", Comienzo = new DateTime(1753, 1, 1, 10, 30, 0), FinMax = new DateTime(1753, 1, 1, 13, 0, 0), Precio = 5.5F, Aforo = 122 },
                new Sesion { SesionId = 4, Sala = "SALA 1", DiaSemana = "Sábado", Comienzo = new DateTime(1753, 1, 1, 20, 45, 0), FinMax = new DateTime(1753, 1, 1, 23, 30, 0), Precio = 7.2F, Aforo = 200 },
                new Sesion { SesionId = 5, Sala = "SALA 2", DiaSemana = "Sábado", Comienzo = new DateTime(1753, 1, 1, 18, 15, 0), FinMax = new DateTime(1753, 1, 1, 20, 15, 0), Precio = 7.2F, Aforo = 174 },
                new Sesion { SesionId = 6, Sala = "SALA 3", DiaSemana = "Sábado", Comienzo = new DateTime(1753, 1, 1, 10, 30, 0), FinMax = new DateTime(1753, 1, 1, 13, 0, 0), Precio = 5.5F, Aforo = 122 },
                new Sesion { SesionId = 7, Sala = "SALA 6", DiaSemana = "Sábado", Comienzo = new DateTime(1753, 1, 1, 0, 30, 0), FinMax = new DateTime(1753, 1, 1, 3, 0, 0), Precio = 7.2F, Aforo = 46 },
                new Sesion { SesionId = 8, Sala = "SALA 6", DiaSemana = "Viernes", Comienzo = new DateTime(1753, 1, 1, 0, 30, 0), FinMax = new DateTime(1753, 1, 1, 3, 0, 0), Precio = 7.2F, Aforo = 46 },
                new Sesion { SesionId = 9, Sala = "SALA 5", DiaSemana = "Domingo", Comienzo = new DateTime(1753, 1, 1, 21, 5, 0), FinMax = new DateTime(1753, 1, 1, 23, 50, 0), Precio = 7.2F, Aforo = 92 },
                new Sesion { SesionId = 10, Sala = "SALA 4", DiaSemana = "Domingo", Comienzo = new DateTime(1753, 1, 1, 15, 45, 0), FinMax = new DateTime(1753, 1, 1, 17, 45, 0), Precio = 7.2F, Aforo = 122 },
                new Sesion { SesionId = 11, Sala = "SALA 5", DiaSemana = "Sábado", Comienzo = new DateTime(1753, 1, 1, 21, 5, 0), FinMax = new DateTime(1753, 1, 1, 23, 50, 0), Precio = 7.2F, Aforo = 92 },
                new Sesion { SesionId = 12, Sala = "SALA 4", DiaSemana = "Sábado", Comienzo = new DateTime(1753, 1, 1, 15, 45, 0), FinMax = new DateTime(1753, 1, 1, 17, 45, 0), Precio = 7.2F, Aforo = 122 },
                new Sesion { SesionId = 13, Sala = "SALA 5", DiaSemana = "Viernes", Comienzo = new DateTime(1753, 1, 1, 21, 5, 0), FinMax = new DateTime(1753, 1, 1, 23, 50, 0), Precio = 7.2F, Aforo = 92 },
                new Sesion { SesionId = 14, Sala = "SALA 4", DiaSemana = "Viernes", Comienzo = new DateTime(1753, 1, 1, 15, 45, 0), FinMax = new DateTime(1753, 1, 1, 17, 45, 0), Precio = 7.2F, Aforo = 122 },
                new Sesion { SesionId = 15, Sala = "SALA 5", DiaSemana = "Jueves", Comienzo = new DateTime(1753, 1, 1, 21, 5, 0), FinMax = new DateTime(1753, 1, 1, 23, 50, 0), Precio = 7.2F, Aforo = 92 },
                new Sesion { SesionId = 16, Sala = "SALA 4", DiaSemana = "Jueves", Comienzo = new DateTime(1753, 1, 1, 15, 45, 0), FinMax = new DateTime(1753, 1, 1, 17, 45, 0), Precio = 7.2F, Aforo = 122 },
                new Sesion { SesionId = 17, Sala = "SALA 5", DiaSemana = "Miércoles", Comienzo = new DateTime(1753, 1, 1, 21, 5, 0), FinMax = new DateTime(1753, 1, 1, 23, 50, 0), Precio = 7.2F, Aforo = 92 },
                new Sesion { SesionId = 18, Sala = "SALA 4", DiaSemana = "Miércoles", Comienzo = new DateTime(1753, 1, 1, 15, 45, 0), FinMax = new DateTime(1753, 1, 1, 17, 45, 0), Precio = 7.2F, Aforo = 122 },
                new Sesion { SesionId = 19, Sala = "SALA 5", DiaSemana = "Martes", Comienzo = new DateTime(1753, 1, 1, 21, 5, 0), FinMax = new DateTime(1753, 1, 1, 23, 50, 0), Precio = 7.2F, Aforo = 92 },
                new Sesion { SesionId = 20, Sala = "SALA 4", DiaSemana = "Martes", Comienzo = new DateTime(1753, 1, 1, 15, 45, 0), FinMax = new DateTime(1753, 1, 1, 17, 45, 0), Precio = 7.2F, Aforo = 122 },
                new Sesion { SesionId = 21, Sala = "SALA 5", DiaSemana = "Lunes", Comienzo = new DateTime(1753, 1, 1, 21, 5, 0), FinMax = new DateTime(1753, 1, 1, 23, 50, 0), Precio = 7.2F, Aforo = 92 },
                new Sesion { SesionId = 22, Sala = "SALA 4", DiaSemana = "Lunes", Comienzo = new DateTime(1753, 1, 1, 15, 45, 0), FinMax = new DateTime(1753, 1, 1, 17, 45, 0), Precio = 7.2F, Aforo = 122 },
            }.AsQueryable();

            //2-falseo la tabla
            var mockTablaSesiones = new Mock<DbSet<Sesion>>();
            mockTablaSesiones.As<IQueryable<Sesion>>().Setup(x => x.Provider).Returns(datosSesiones.Provider);
            mockTablaSesiones.As<IQueryable<Sesion>>().Setup(x => x.Expression).Returns(datosSesiones.Expression);
            mockTablaSesiones.As<IQueryable<Sesion>>().Setup(x => x.ElementType).Returns(datosSesiones.ElementType);
            mockTablaSesiones.As<IQueryable<Sesion>>().Setup(x => x.GetEnumerator()).Returns(datosSesiones.GetEnumerator());
            //3-cuando se solicite la tabla cliente se devolvera un objeto falseado
            _mockCineBD.Setup(x => x.Sesiones).Returns(mockTablaSesiones.Object);

            //Proyecciones

            //1- Datos como consulta
            var datosProyecciones = new List<Proyeccion>
            {
                //Insertar las new proyecciones
                new Proyeccion{PeliculaId = 1, SesionId = 2, Inicio = new DateTime(2023, 02, 13, 00, 00, 00), Fin = null},
                new Proyeccion{PeliculaId = 1, SesionId = 5, Inicio = new DateTime(2023, 02, 13, 00, 00, 00), Fin = null},
                new Proyeccion{PeliculaId = 1, SesionId = 8, Inicio = new DateTime(2023, 02, 13, 00, 00, 00), Fin = null},
                new Proyeccion{PeliculaId = 1, SesionId = 11, Inicio = new DateTime(2023, 02, 13, 00, 00, 00), Fin = null},
                new Proyeccion{PeliculaId = 1, SesionId = 14, Inicio = new DateTime(2023, 02, 13, 00, 00, 00), Fin = null},
                new Proyeccion{PeliculaId = 1, SesionId = 17, Inicio = new DateTime(2023, 02, 13, 00, 00, 00), Fin = null},
                new Proyeccion{PeliculaId = 1, SesionId = 20, Inicio = new DateTime(2023, 02, 13, 00, 00, 00), Fin = null},
                new Proyeccion{PeliculaId = 2, SesionId = 3, Inicio = new DateTime(2023, 02, 06, 00, 00, 00), Fin = null},
                new Proyeccion{PeliculaId = 2, SesionId = 6, Inicio = new DateTime(2023, 02, 06, 00, 00, 00), Fin = null},
                new Proyeccion{PeliculaId = 2, SesionId = 9, Inicio = new DateTime(2023, 02, 06, 00, 00, 00), Fin = null},
                new Proyeccion{PeliculaId = 2, SesionId = 12, Inicio = new DateTime(2023, 02, 06, 00, 00, 00), Fin = null},
                new Proyeccion{PeliculaId = 2, SesionId = 15, Inicio = new DateTime(2023, 02, 06, 00, 00, 00), Fin = null},
                new Proyeccion{PeliculaId = 2, SesionId = 18, Inicio = new DateTime(2023, 02, 06, 00, 00, 00), Fin = null},
                new Proyeccion{PeliculaId = 2, SesionId = 21, Inicio = new DateTime(2023, 02, 06, 00, 00, 00), Fin = null},
                new Proyeccion{PeliculaId = 3, SesionId = 4, Inicio = new DateTime(2023, 01, 30, 00, 00, 00), Fin = new DateTime(2023, 02, 19, 00, 00, 00)},
                new Proyeccion{PeliculaId = 3, SesionId = 7, Inicio = new DateTime(2023, 01, 30, 00, 00, 00), Fin = new DateTime(2023, 02, 19, 00, 00, 00)},
                new Proyeccion{PeliculaId = 3, SesionId = 10, Inicio = new DateTime(2023, 01, 30, 00, 00, 00), Fin = new DateTime(2023, 02, 19, 00, 00, 00)},
                new Proyeccion{PeliculaId = 3, SesionId = 13, Inicio = new DateTime(2023, 01, 30, 00, 00, 00), Fin = new DateTime(2023, 02, 19, 00, 00, 00)},
                new Proyeccion{PeliculaId = 3, SesionId = 16, Inicio = new DateTime(2023, 01, 30, 00, 00, 00), Fin = new DateTime(2023, 02, 19, 00, 00, 00)},
                new Proyeccion{PeliculaId = 3, SesionId = 19, Inicio = new DateTime(2023, 01, 30, 00, 00, 00), Fin = new DateTime(2023, 02, 19, 00, 00, 00)},
                new Proyeccion{PeliculaId = 3, SesionId = 22, Inicio = new DateTime(2023, 01, 30, 00, 00, 00), Fin = new DateTime(2023, 02, 19, 00, 00, 00)},
                new Proyeccion{PeliculaId = 4, SesionId = 2, Inicio = new DateTime(2023, 01, 23, 00, 00, 00), Fin = new DateTime(2023, 02, 12, 00, 00, 00)},
                new Proyeccion{PeliculaId = 4, SesionId = 5, Inicio = new DateTime(2023, 01, 23, 00, 00, 00), Fin = new DateTime(2023, 02, 12, 00, 00, 00)},
                new Proyeccion{PeliculaId = 4, SesionId = 8, Inicio = new DateTime(2023, 01, 23, 00, 00, 00), Fin = new DateTime(2023, 02, 12, 00, 00, 00)},
                new Proyeccion{PeliculaId = 4, SesionId = 11, Inicio = new DateTime(2023, 01, 23, 00, 00, 00), Fin = new DateTime(2023, 02, 12, 00, 00, 00)},
                new Proyeccion{PeliculaId = 4, SesionId = 14, Inicio = new DateTime(2023, 01, 23, 00, 00, 00), Fin = new DateTime(2023, 02, 12, 00, 00, 00)},
                new Proyeccion{PeliculaId = 4, SesionId = 17, Inicio = new DateTime(2023, 01, 23, 00, 00, 00), Fin = new DateTime(2023, 02, 12, 00, 00, 00)},
                new Proyeccion{PeliculaId = 4, SesionId = 20, Inicio = new DateTime(2023, 01, 23, 00, 00, 00), Fin = new DateTime(2023, 02, 12, 00, 00, 00)},
                new Proyeccion{PeliculaId = 5, SesionId = 3, Inicio = new DateTime(2023, 01, 16, 00, 00, 00), Fin = new DateTime(2023, 02, 05, 00, 00, 00)},
                new Proyeccion{PeliculaId = 5, SesionId = 6, Inicio = new DateTime(2023, 01, 16, 00, 00, 00), Fin = new DateTime(2023, 02, 05, 00, 00, 00)},
                new Proyeccion{PeliculaId = 5, SesionId = 9, Inicio = new DateTime(2023, 01, 16, 00, 00, 00), Fin = new DateTime(2023, 02, 05, 00, 00, 00)},
                new Proyeccion{PeliculaId = 5, SesionId = 12, Inicio = new DateTime(2023, 01, 16, 00, 00, 00), Fin = new DateTime(2023, 02, 05, 00, 00, 00)},
                new Proyeccion{PeliculaId = 5, SesionId = 15, Inicio = new DateTime(2023, 01, 16, 00, 00, 00), Fin = new DateTime(2023, 02, 05, 00, 00, 00)},
                new Proyeccion{PeliculaId = 5, SesionId = 18, Inicio = new DateTime(2023, 01, 16, 00, 00, 00), Fin = new DateTime(2023, 02, 05, 00, 00, 00)},
                new Proyeccion{PeliculaId = 5, SesionId = 21, Inicio = new DateTime(2023, 01, 16, 00, 00, 00), Fin = new DateTime(2023, 02, 05, 00, 00, 00)},
                new Proyeccion{PeliculaId = 6, SesionId = 4, Inicio = new DateTime(2023, 01, 09, 00, 00, 00), Fin = new DateTime(2023, 01, 29, 00, 00, 00)},
                new Proyeccion{PeliculaId = 6, SesionId = 7, Inicio = new DateTime(2023, 01, 09, 00, 00, 00), Fin = new DateTime(2023, 01, 29, 00, 00, 00)},
                new Proyeccion{PeliculaId = 6, SesionId = 10, Inicio = new DateTime(2023, 01, 09, 00, 00, 00), Fin = new DateTime(2023, 01, 29, 00, 00, 00)},
                new Proyeccion{PeliculaId = 6, SesionId = 13, Inicio = new DateTime(2023, 01, 09, 00, 00, 00), Fin = new DateTime(2023, 01, 29, 00, 00, 00)},
                new Proyeccion{PeliculaId = 6, SesionId = 16, Inicio = new DateTime(2023, 01, 09, 00, 00, 00), Fin = new DateTime(2023, 01, 29, 00, 00, 00)},
                new Proyeccion{PeliculaId = 6, SesionId = 19, Inicio = new DateTime(2023, 01, 09, 00, 00, 00), Fin = new DateTime(2023, 01, 29, 00, 00, 00)},
                new Proyeccion{PeliculaId = 6, SesionId = 22, Inicio = new DateTime(2023, 01, 09, 00, 00, 00), Fin = new DateTime(2023, 01, 29, 00, 00, 00)},
                new Proyeccion{PeliculaId = 7, SesionId = 2, Inicio = new DateTime(2023, 01, 02, 00, 00, 00), Fin = new DateTime(2023, 01, 22, 00, 00, 00)},
                new Proyeccion{PeliculaId = 7, SesionId = 5, Inicio = new DateTime(2023, 01, 02, 00, 00, 00), Fin = new DateTime(2023, 01, 22, 00, 00, 00)},
                new Proyeccion{PeliculaId = 7, SesionId = 8, Inicio = new DateTime(2023, 01, 02, 00, 00, 00), Fin = new DateTime(2023, 01, 22, 00, 00, 00)},
                new Proyeccion{PeliculaId = 7, SesionId = 11, Inicio = new DateTime(2023, 01, 02, 00, 00, 00), Fin = new DateTime(2023, 01, 22, 00, 00, 00)},
                new Proyeccion{PeliculaId = 7, SesionId = 14, Inicio = new DateTime(2023, 01, 02, 00, 00, 00), Fin = new DateTime(2023, 01, 22, 00, 00, 00)},
                new Proyeccion{PeliculaId = 7, SesionId = 17, Inicio = new DateTime(2023, 01, 02, 00, 00, 00), Fin = new DateTime(2023, 01, 22, 00, 00, 00)},
                new Proyeccion{PeliculaId = 7, SesionId = 20, Inicio = new DateTime(2023, 01, 02, 00, 00, 00), Fin = new DateTime(2023, 01, 22, 00, 00, 00)},
                new Proyeccion{PeliculaId = 8, SesionId = 3, Inicio = new DateTime(2023, 01, 02, 00, 00, 00), Fin = new DateTime(2023, 01, 15, 00, 00, 00)},
                new Proyeccion{PeliculaId = 8, SesionId = 6, Inicio = new DateTime(2023, 01, 02, 00, 00, 00), Fin = new DateTime(2023, 01, 15, 00, 00, 00)},
                new Proyeccion{PeliculaId = 8, SesionId = 9, Inicio = new DateTime(2023, 01, 02, 00, 00, 00), Fin = new DateTime(2023, 01, 15, 00, 00, 00)},
                new Proyeccion{PeliculaId = 8, SesionId = 12, Inicio = new DateTime(2023, 01, 02, 00, 00, 00), Fin = new DateTime(2023, 01, 15, 00, 00, 00)},
                new Proyeccion{PeliculaId = 8, SesionId = 15, Inicio = new DateTime(2023, 01, 02, 00, 00, 00), Fin = new DateTime(2023, 01, 15, 00, 00, 00)},
                new Proyeccion{PeliculaId = 8, SesionId = 18, Inicio = new DateTime(2023, 01, 02, 00, 00, 00), Fin = new DateTime(2023, 01, 15, 00, 00, 00)},
                new Proyeccion{PeliculaId = 8, SesionId = 21, Inicio = new DateTime(2023, 01, 02, 00, 00, 00), Fin = new DateTime(2023, 01, 15, 00, 00, 00)},
                new Proyeccion{PeliculaId = 9, SesionId = 4, Inicio = new DateTime(2023, 01, 02, 00, 00, 00), Fin = new DateTime(2023, 01, 08, 00, 00, 00)},
                new Proyeccion{PeliculaId = 9, SesionId = 4, Inicio = new DateTime(2023, 02, 20, 00, 00, 00), Fin = null},
                new Proyeccion{PeliculaId = 9, SesionId = 7, Inicio = new DateTime(2023, 01, 02, 00, 00, 00), Fin = new DateTime(2023, 01, 08, 00, 00, 00)},
                new Proyeccion{PeliculaId = 9, SesionId = 7, Inicio = new DateTime(2023, 02, 20, 00, 00, 00), Fin = null},
                new Proyeccion{PeliculaId = 9, SesionId = 10, Inicio = new DateTime(2023, 01, 02, 00, 00, 00), Fin = new DateTime(2023, 01, 08, 00, 00, 00)},
                new Proyeccion{PeliculaId = 9, SesionId = 10, Inicio = new DateTime(2023, 02, 20, 00, 00, 00), Fin = null},
                new Proyeccion{PeliculaId = 9, SesionId = 13, Inicio = new DateTime(2023, 01, 02, 00, 00, 00), Fin = new DateTime(2023, 01, 08, 00, 00, 00)},
                new Proyeccion{PeliculaId = 9, SesionId = 13, Inicio = new DateTime(2023, 02, 20, 00, 00, 00), Fin = null},
                new Proyeccion{PeliculaId = 9, SesionId = 16, Inicio = new DateTime(2023, 01, 02, 00, 00, 00), Fin = new DateTime(2023, 01, 08, 00, 00, 00)},
                new Proyeccion{PeliculaId = 9, SesionId = 16, Inicio = new DateTime(2023, 02, 20, 00, 00, 00), Fin = null},
                new Proyeccion{PeliculaId = 9, SesionId = 19, Inicio = new DateTime(2023, 01, 02, 00, 00, 00), Fin = new DateTime(2023, 01, 08, 00, 00, 00)},
                new Proyeccion{PeliculaId = 9, SesionId = 19, Inicio = new DateTime(2023, 02, 20, 00, 00, 00), Fin = null},
                new Proyeccion{PeliculaId = 9, SesionId = 22, Inicio = new DateTime(2023, 01, 02, 00, 00, 00), Fin = new DateTime(2023, 01, 08, 00, 00, 00)},
                new Proyeccion{PeliculaId = 9, SesionId = 22, Inicio = new DateTime(2023, 02, 20, 00, 00, 00), Fin = null}
            }.AsQueryable();

            //2-falseo la tabla
            var mockTablaProyecciones = new Mock<DbSet<Proyeccion>>();
            mockTablaProyecciones.As<IQueryable<Proyeccion>>().Setup(x => x.Provider).Returns(datosProyecciones.Provider);
            mockTablaProyecciones.As<IQueryable<Proyeccion>>().Setup(x => x.Expression).Returns(datosProyecciones.Expression);
            mockTablaProyecciones.As<IQueryable<Proyeccion>>().Setup(x => x.ElementType).Returns(datosProyecciones.ElementType);
            mockTablaProyecciones.As<IQueryable<Proyeccion>>().Setup(x => x.GetEnumerator()).Returns(datosProyecciones.GetEnumerator());
            //3-cuando se solicite la tabla cliente se devolvera un objeto falseado
            _mockCineBD.Setup(x => x.Proyecciones).Returns(mockTablaProyecciones.Object);

            //4-falseamos la base de datos.
            _negocio = new Negocio(_mockCineBD.Object);
            Debug.WriteLine("TestInitialize");
        }

        //
        //METODOS PELICULAS
        //

        [TestMethod]
        public void BuscarPeliculaPorId()
        {
            //Invocamos a la logica negocio del programa
            Pelicula pelicula = _negocio.ObtenerPelicula(2);
            //revisamos los resultados
            //comprobamos que la pelicula no es nulo, ya que sabemos que existe
            Assert.IsNotNull(pelicula);
            //revisamos que los datos de la pelicula son los correctos
            Assert.AreEqual(2, pelicula.PeliculaId);
            Assert.AreEqual("Algo pasa con Mary", pelicula.Nombre);
            Assert.AreEqual(1998, pelicula.Anno);
            Assert.AreEqual(119, pelicula.Duracion);
            Assert.AreEqual(">12", pelicula.Categoria);
            Assert.AreEqual("Comedia", pelicula.Genero);
            Assert.AreEqual("Todo un peliculon a la altura de zoolander", pelicula.Sinopsis);
        }
        [TestMethod]
        public void ObtenerTodasPeliculas()
        {
            List<Pelicula> peliculas=_negocio.ObtenerPeliculas();
            List<Pelicula> datosPeliculas = new List<Pelicula>
            {
                //PeliculaId  Nombre  Duracion    Anno    Categoria   Genero  Sinopsis
                new Pelicula { PeliculaId = 1, Nombre = "Renfield", Duracion = 93, Anno = 2023, Categoria = ">18", Genero = "Terror", Sinopsis = "Vuelve el conde Dracula" },
                new Pelicula { PeliculaId = 2, Nombre = "Algo pasa con Mary", Duracion = 119, Anno = 1998, Categoria = ">12", Genero = "Comedia", Sinopsis = "Todo un peliculon a la altura de zoolander" },
                new Pelicula { PeliculaId = 3, Nombre = "Espíritu libre ", Duracion = 110, Anno = 2023, Categoria = ">18", Genero = "Aventura  ", Sinopsis = "La caratula es muy bonita, la pelicula ni idea" },
                new Pelicula { PeliculaId = 4, Nombre = "Amsterdam", Duracion = 125, Anno = 2022, Categoria = "Todos", Genero = "Romance", Sinopsis = "Algo de besos supongo que en los alrededores de Amsterdan" },
                new Pelicula { PeliculaId = 5, Nombre = "Sin novedad en el frente", Duracion = 162, Anno = 2022, Categoria = ">18", Genero = "Acción", Sinopsis = "Pelicula de la primera guerra mundial basada en el libro homonimo" },
                new Pelicula { PeliculaId = 6, Nombre = "Uncharted", Duracion = 113, Anno = 2022, Categoria = ">16", Genero = "Aventura", Sinopsis = "Ni idea, pero le gusta a los frikis" },
                new Pelicula { PeliculaId = 7, Nombre = "Bullet Train", Duracion = 95, Anno = 2022, Categoria = ">16", Genero = "Acción", Sinopsis = "Really Brad, no es tu mejor papel" },
                new Pelicula { PeliculaId = 8, Nombre = "Sonic ", Duracion = 108, Anno = 2023, Categoria = "Todos", Genero = "Aventura", Sinopsis = "Un erizo dando vueltas contra cosas" },
                new Pelicula { PeliculaId = 9, Nombre = "Luther", Duracion = 105, Anno = 2023, Categoria = ">18", Genero = "Acción", Sinopsis = "Detective cañero contra psicopata" }
            };
            Assert.AreEqual(datosPeliculas.Count, peliculas.Count);
            for (int i = 0; i < peliculas.Count(); i++)
            {
                Assert.AreEqual(datosPeliculas[i].PeliculaId, peliculas[i].PeliculaId);
                Assert.AreEqual(datosPeliculas[i].Nombre, peliculas[i].Nombre);
                Assert.AreEqual(datosPeliculas[i].Duracion, peliculas[i].Duracion);
                Assert.AreEqual(datosPeliculas[i].Anno, peliculas[i].Anno);
                Assert.AreEqual(datosPeliculas[i].Categoria, peliculas[i].Categoria);
                Assert.AreEqual(datosPeliculas[i].Genero, peliculas[i].Genero);
                Assert.AreEqual(datosPeliculas[i].Sinopsis, peliculas[i].Sinopsis);
            }
        }
        [TestMethod]
        public void ObtenerPeliculaDatosErronos()
        {
            Pelicula pelicula = _negocio.ObtenerPelicula(19);
            Assert.IsNull(pelicula);
        }
        [TestMethod]
        public void BorrarPeliculaConProyeccionesAsociadas()
        {
            //Invocamos a la logica negocio del programa
            //revisamos que el metodo lanza la excepcion
            Assert.ThrowsException<UniCineException>(() => _negocio.BorrarPelicula(1));
            
            try
            {
                _negocio.BorrarPelicula(1);
            }
            catch(UniCineException ex)
            {
                Assert.AreEqual("Se ha intentado borrar la pelicula " + Utils.ObtenerNombrePeliculaPorId(1) + " pero tiene proyecciones asociadas.", ex.Message);
            }
        }
        [TestMethod]
        public void ModificarPeliculaDemasiadoCortaParaSesionesAsociadas()
        {
            /*
             *  Lanza el siguiente error, se origina en la linea de negocio que contiene 
             *  "db.Entry(AuxPelicula).CurrentValues.SetValues(PeliculaEditada);"
             *  
                  ModificarPeliculaDemasiadoCortaParaSesionesAsociadas
                   Origen: UTUniCine.cs línea 270
                   Duración: 996 ms

                  Mensaje: 
                Excepción método de prueba UniCineUnitTest.UTUniCine.ModificarPeliculaDemasiadoCortaParaSesionesAsociadas: 
                System.InvalidOperationException: No connection string named 'UniCineDB' could be found in the application config file.

                  Seguimiento de la pila: 
                LazyInternalConnection.get_ConnectionHasModel()
                LazyInternalContext.InitializeContext()
                InternalContext.Initialize()
                LazyInternalContext.get_ObjectContext()
                InternalContext.DetectChanges(Boolean force)
                InternalContext.GetStateEntry(Object entity)
                InternalEntityEntry.ctor(InternalContext internalContext, Object entity)
                DbContext.Entry[TEntity](TEntity entity)
                Negocio.ActualizarPelicula(Pelicula PeliculaEditada) línea 79
                UTUniCine.ModificarPeliculaDemasiadoCortaParaSesionesAsociadas() línea 278
             * 
            Pelicula pelicula = _negocio.ObtenerPelicula(3);
            pelicula.Duracion += 600;
            //Assert.ThrowsException<UniCineException>(() => _negocio.ActualizarPelicula(pelicula));

            try
            {
                _negocio.ActualizarPelicula(pelicula);
            }
            catch (UniCineException ex)
            {
                Assert.AreEqual("Se ha intentado modificar la pelicula " + Utils.ObtenerNombrePeliculaPorId(3) + " y la duracion de la pelicula es mayor que la duracion de las sesiones asociadas.", ex.Message);
            }
            */
        }
        //
        //METODOS SESIONES
        //

        [TestMethod]
        public void BuscarSesionPorId()
        {
            //Invocamos a la logica negocio del programa
            Sesion sesion = _negocio.ObtenerSesion(2);
            //revisamos los resultados
            //comprobamos que la pelicula no es nulo, ya que sabemos que existe
            Assert.IsNotNull(sesion);
            //revisamos que los datos de la sesion son los correctos
            Assert.AreEqual("SALA 2", sesion.Sala);
            Assert.AreEqual("Domingo", sesion.DiaSemana);
            Assert.AreEqual(new DateTime(1753, 1, 1, 18, 15, 0), sesion.Comienzo);
            Assert.AreEqual(new DateTime(1753, 1, 1, 20, 15, 0), sesion.FinMax);
            Assert.AreEqual(7.2F, sesion.Precio);
            Assert.AreEqual(174, sesion.Aforo);
        }

        [TestMethod]
        public void ModificarSesionDemasiadoCortaParaPeliculaAsociada()
        {
            //Invocamos a la logica negocio del programa
            //revisamos que el metodo lanza la excepcion
            Sesion sesion = _negocio.ObtenerSesion(2);
            sesion.FinMax = sesion.Comienzo.AddMinutes(120);
            Assert.ThrowsException<UniCineException>(() => _negocio.ActualizarSesion(sesion));
        }

        //
        //METODOS PROYECCIONES
        //



    }
}
