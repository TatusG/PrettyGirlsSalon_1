// See https://aka.ms/new-console-template for more information

using AccesoDatosSalon.Models;
using AccesoDatosSalon.Opetarions;

//ClientDAO opCliente = new ClientDAO();

//Insertar Cliente
//opCliente.insertar("40256870", "Carla Zambrano", "965201457", "carla_zam@gmail.com");

//ActualizarCliente
//opCliente.actualizar(3, "20568794", "Sofía Guevara", "965124785", "sofia@gmail.com", new DateOnly(2025,5,15));

//Eliminar Cliente
//opCliente.eliminar(5);

////Seleccionar todos los clientes
//var clientes = opCliente.seleccionarTodos();
//foreach (var cliente in clientes)
//{
//    Console.WriteLine($"Nombre: {cliente.FullName}");
//    Console.WriteLine($"Email: {cliente.Email}");
//    Console.WriteLine($"Fecha de Registro: {cliente.RegistrationDate} \n------");
//}

//Console.WriteLine("\n---------------------------------");

////Seleccionar clientes por ID
//var clienteSeleccionado = opCliente.seleccionarCliente(2);
//if (clienteSeleccionado != null)
//{
//    Console.WriteLine("El cliente con Id = 2 es " + clienteSeleccionado.FullName);
//}
//else
//{
//    Console.WriteLine("Cliente no existe");
//}

//Console.WriteLine("\n---------------------------------");

////Lista de clientes por servicio

//var clienteServ = opCliente.seleccionarClienteServicio();
//foreach (ClienteServicios clienteServicio in clienteServ)
//{
//    Console.WriteLine(clienteServicio.NombreCliente + "-------> " + clienteServicio.NombreServicio);
//}

//Console.WriteLine("Grupo 3: \n Entidad Cliente --> Tatiana Grados");

//StylistDAO opEstilista = new StylistDAO();

////Insertar Estilista
////opEstilista.insertar("carmen", "4321", "Maricarmen Zevallos", "Maquilladora", "mcarmenz@gmail.com", true);

////Actualizar Estilista
////opEstilista.actualizar("tatus", "789", "Tatiana Grados", "Maquilladora", "tgvertiz@gmail.com", true);

////Ver Lista de Estilistas
//var estilistas = opEstilista.seleccionarEstilistas();

//foreach (var estilista in estilistas)
//{
//    Console.WriteLine(estilista.FullName);
//}

//Console.WriteLine("*************************");

////Buscar Estilista
//var estilistaSeleccionado = opEstilista.seleccionarEstilista("laura");

//if (estilistaSeleccionado != null)
//{
//    Console.WriteLine("La especialida del estilista laura es: " + estilistaSeleccionado.Specialty);
//}
//else
//{
//    Console.WriteLine("El usuario no existe");
//}

//Console.WriteLine("*************************");

//var citaPend = opEstilista.seleccionarCitasPendientes("laura", "pending");

//Console.WriteLine("\nCITAS PENDIENTES DE LAURA");
//Console.WriteLine("------------------------------------------------------------");

//foreach (CitasPendientes citasPendientes in citaPend)
//{
//    Console.WriteLine(
//        $"• Cliente: {citasPendientes.NombreCliente}\n" +
//        $"  Servicio: {citasPendientes.Servicio}\n" +
//        $"  Estilista: {citasPendientes.Estilista}\n" +
//        $"  Fecha: {citasPendientes.FechaHora:dd/MM/yyyy HH:mm}\n" +
//        $"  Teléfono: {citasPendientes.TelefonoCliente}\n" +
//        "------------------------------------------------------------");
//}

//Console.WriteLine("*************************");


//ServiceDAO opServicios = new ServiceDAO();

////pServicios.insertar("Laceado Brasilero", 260, 200, "Elimina el frizz", true);

////opServicios.actualizar(6,"Alisado Japonés", 240, 200, "Utiliza productos químicos", true);

////opServicios.eliminar(7);

//var servicios = opServicios.todosLosServicios();

//foreach (var service in servicios)
//{
//    Console.WriteLine(service.ServiceName);
//}

//Console.WriteLine("*************************");

//var servicioSeleccionado = opServicios.seleccionarServicio(3);

//if (servicioSeleccionado != null)
//{
//    Console.WriteLine("El servicio número 3 es: " + servicioSeleccionado.ServiceName);
//}
//else
//{
//    Console.WriteLine("El servicio  no existe");
//}

//Console.WriteLine("*************************");

//var servicioXEst = opServicios.SeleccionarServiciosPorEstilista();
//foreach (ServiciosEstilista service in servicioXEst)
//{
//    Console.WriteLine(service.NombreEstilista + "-------------> " + service.Especialidad + "------------->" + service.Especialidad);
//}







