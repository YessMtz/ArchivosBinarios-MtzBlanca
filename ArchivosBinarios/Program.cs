using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ArchivosBinarios
{
    class Program
    {
        class ArchivoBinarioEmpleados
        {
            BinaryWriter bw = null; //flujo de salida -escritura de datos
            BinaryReader br = null; //flujo de entrada - lectura de datos

            //campos de la clase
            string nombre, direccion;
            long Telefono;
            int NumEmp, DiasTrabajados;
            float Salario;

            //metodo que crar el archivo binario
            public void CrearArchvo(string Archivo)
            {
                //variable local del metodo
                char resp;
                try
                {
                    //CREACION DEL FLUJO PRA ESCRIBIR PARA ESCRIBIR DATOS 
                    //FileMode.Create = crea el archivo
                    //FileAccess.Write = da acceso  y crea el archivo
                    bw = new BinaryWriter(new FileStream(Archivo, FileMode.Create, FileAccess.Write));

                    //ciclo do para declarar y capturar las variables que se van a escribir en el archivo
                    do
                    {
                        Console.Clear();
                        Console.Write("numero del empleado: ");
                        NumEmp = Int32.Parse(Console.ReadLine());
                        Console.Write("Nombre del empleado: ");
                        nombre = Console.ReadLine();
                        Console.Write("Direccion del empleado: ");
                        direccion = Console.ReadLine();
                        Console.Write("Telefono del empleado: ");
                        Telefono = Int32.Parse(Console.ReadLine());
                        Console.Write("Dias de trabajo del empleado: ");
                        DiasTrabajados = int.Parse(Console.ReadLine());
                        Console.Write("Salario del empleado: ");
                        Salario = Single.Parse(Console.ReadLine());

                        //escribe los datos al archivo
                        bw.Write(NumEmp);
                        bw.Write(nombre);
                        bw.Write(direccion);
                        bw.Write(Telefono);
                        bw.Write(DiasTrabajados);
                        bw.Write(Salario);

                        //condicional para terminar o repetir el ciclo
                        Console.WriteLine("\n\nDeseas Almacenar otro regristro (s/n)?");
                        resp = Char.Parse(Console.ReadLine());
                    } while ((resp == 'S'));
                }
                catch (IOException mensaje)
                {
                    //mensaje que se despliega si hay un error
                    Console.WriteLine("\nError " + mensaje.Message);
                    Console.WriteLine("\nRuta " + mensaje.Message);
                }
                finally
                {
                    //cierra el flujo de escritura
                    if (bw != null) bw.Close();
                    Console.Write("nPresione <enter> para terminar la Escriturade Datos y regresar al Menu");
                    Console.ReadKey();
                }

            }
            public void mostrarArchivo(string Archivo)
            {
                try
                {
                    //verifica que el archivo exista
                    if (File.Exists(Archivo))
                    {
                        //FILEMODE.OPEN ABRE EL ARCHIVO
                        //FILEACCES.READ LO LEE PARA DESPUES DESPLEGARLO EN PANTALLA

                        br = new BinaryReader(new FileStream(Archivo, FileMode.Open, FileAccess.Read));
                        Console.Read();
                        Console.Clear();
                        do
                        {
                            //lectura de de registros mientras no llegue Endofile
                            NumEmp = br.ReadInt32();
                            nombre = br.ReadString();
                            direccion = br.ReadString();
                            Telefono = br.ReadInt64();
                            DiasTrabajados = br.ReadInt32();
                            Salario = br.ReadSingle();

                               //DESPLIEGUE DE DATOS
                            Console.WriteLine("Numero del Empleado : " + NumEmp +
                            "\nNombre del Empleado : " + nombre +
                            "\nDirección del Empleado : " + direccion +
                            "\nTeléfono del Empleado : " + Telefono +
                            "\nDias Trabajados del Empleado :" + DiasTrabajados +
                            "\nSalario Diario del Empleado :{0:C} " + Salario);
                            Console.WriteLine("SUELDO TOTAL del Empleado : {0:C}", (DiasTrabajados * Salario));
                            Console.WriteLine("\n");
                    } while (true);}   
                    
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("\n\nEL ARCHIVO "+ Archivo + "No Existe en el disco duro.");
                        Console.Write("PRESIONE enter PARA CONTINUAR");
                        Console.ReadKey();

                    }
                                      
                }catch(EndOfStreamException)
                {
                    Console.WriteLine("\n\nFin del Listado de Empleados");
                    Console.Write("\nPresione <enter> para Continuar...");
                    Console.ReadKey();
                }
                finally
                {
                    if (br != null) br.Close(); //cierra flujo
                    Console.Write("\nPresione <enter> para terminar la Lecturade Datos y regresar al Menu.");
                    Console.ReadKey();
                }
            }
        }






        static void Main(string[] args)
        {

            //declaración variables auxiliares
            string Arch = null;
            int opcion;
            //creación del objeto
            ArchivoBinarioEmpleados A1 = new ArchivoBinarioEmpleados();//Menu de Opciones

            do
            {
                Console.Clear();
                Console.WriteLine("\n*** ARCHIVO BINARIO EMPLEADOS***"); Console.WriteLine("1.- Creación de un Archivo.");
                Console.WriteLine("2.- Lectura de un Archivo.");
                Console.WriteLine("3.- Salida del Programa.");
                Console.Write("\nQue opción deseas: ");
                opcion = Int16.Parse(Console.ReadLine());
                switch (opcion)
                {
                    case 1:
                        //bloque de escritura
                        try
                        {
                            //captura nombre archivo
                            Console.Write("\nAlimenta el Nombre del ArchivoaCrear: ");
                            Arch = Console.ReadLine();

                            //verifica si esxiste el archivo
                            char resp = 's';
                            if (File.Exists(Arch))
                            {
                                Console.Write("\nEl Archivo Existe!!, DeseasSobreescribirlo (s/n) ? ");
                                resp = Char.Parse(Console.ReadLine());
                            }
                            if ((resp == 's') || (resp == 'S'))
                                A1.CrearArchvo(Arch);
                        }

                        catch (IOException e)
                        {
                            Console.WriteLine("\nError : " + e.Message);
                            Console.WriteLine("\nRuta : " + e.StackTrace);
                        }
                        break;
                    case 2:
                        try
                        {
                            //captura nombre archivo
                            Console.Write("\nAlimenta el Nombre del Archivoque deseas Leer: "); Arch = Console.ReadLine();
                            A1.mostrarArchivo(Arch);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError : " + e.Message);
                            Console.WriteLine("\nRuta : " + e.StackTrace);
                        }
                        break;

                    case 3:
                        Console.Write("\nPresione <enter> para Salir del Programa.");
                        Console.ReadKey();
                        break;
                    default:
                        Console.Write("\nEsa Opción No Existe!!, Presione<enter> para Continuar...");
                        Console.ReadKey();
                        break;
                }
            } while (opcion != 3);
        }


        
    }
}
