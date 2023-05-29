using APIGrupoFycas.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data;

namespace APIGrupoFycas.Controllers
{
    [EnableCors("RulesCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class APIsController : ControllerBase
    {
        private readonly string cadenaSql;

        public APIsController(IConfiguration config)
        {
            cadenaSql = config.GetConnectionString("CadenaSQL");
        }
        //GET's
        [HttpGet]
        [Route("Lista_Clientes")]
        public ActionResult<Cliente[]> ListaClientes()
        {
            List<Cliente> lista = new List<Cliente>();

            try
            {
                using (var conexion = new MySqlConnection(cadenaSql))
                {
                    conexion.Open();
                    var cmd = new MySqlCommand("sp_lista_clientes", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new Cliente()
                            {
                                IdCliente = Convert.ToInt32(rd["ID_CLIENTE"]),
                                Nombre = rd["NOMBRE"].ToString(),
                                Apellido = rd["APELLIDO"].ToString(),
                                Cedula = rd["CEDULA"].ToString(),
                                Telefono = rd["TELEFONO"].ToString()
                            });
                        }
                    }
                }

                return StatusCode(StatusCodes.Status200OK, lista.ToArray());
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
            }
        }

        [HttpGet]
        [Route("Lista_Cliente_Factura")]
        public IActionResult ListaClienteFactura()
        {
            List<Factura> listaFactura = new List<Factura>();
            List<Cliente> listaCliente = new List<Cliente>();
            //List<List<string>> listaPrincipal = new List<List<string>>();
            try
            {
                using (var conexion = new MySqlConnection(cadenaSql))
                {
                    conexion.Open();
                    var cmd = new MySqlCommand("sp_lista_clientes_facturas", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            listaCliente.Add(new Cliente()
                            {
                                IdCliente = Convert.ToInt32(rd["ID_CLIENTE"]),
                                Nombre = rd["NOMBRE"].ToString(),
                                Apellido = rd["APELLIDO"].ToString(),
                                Cedula = rd["CEDULA"].ToString(),
                                Telefono = rd["TELEFONO"].ToString()
                            });
                            listaFactura.Add(new Factura()
                            {
                                IdFactura = Convert.ToInt32(rd["ID_FACTURA"]),
                                MontoSolicitado = Convert.ToDecimal(rd["MONTO_SOLICITADO"]),
                                Tasa = Convert.ToDecimal(rd["TASA"]),
                                Cuotas = Convert.ToDecimal(rd["CUOTAS"]),
                                CuotasMensuales = Convert.ToDecimal(rd["CUOTAS_MENSUALES"]),
                                Capital = Convert.ToDecimal(rd["CAPITAL"]),
                                Interes = Convert.ToDecimal(rd["INTERES"]),
                                PagoNuevo = Convert.ToDecimal(rd["PAGO_NUEVO"]),
                                PagoRealizado = Convert.ToDecimal(rd["PAGO_REALIZADO"]),
                                Fecha = (DateTime?)rd["FECHA"],
                                ClienteId = Convert.ToInt32(rd["CLIENTE_ID"])
                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Lista Clientes y Facturas", response = listaCliente.ToArray(), responseFactura = listaFactura.ToArray() });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = listaCliente, listaFactura });
            }
        }

        [HttpGet]
        [Route("Lista_Factura")]
        public ActionResult<Factura[]> ListaFactura()
        {
            List<Factura> facturasLista = new List<Factura>();

            try
            {
                using (var conexion = new MySqlConnection(cadenaSql))
                {
                    conexion.Open();
                    var cmd = new MySqlCommand("sp_lista_facturas", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            facturasLista.Add(new Factura()
                            {
                                IdFactura = Convert.ToInt32(rd["ID_FACTURA"]),
                                MontoSolicitado = Convert.ToDecimal(rd["MONTO_SOLICITADO"]),
                                Tasa = Convert.ToDecimal(rd["TASA"]),
                                Cuotas = Convert.ToDecimal(rd["CUOTAS"]),
                                CuotasMensuales = Convert.ToDecimal(rd["CUOTAS_MENSUALES"]),
                                Capital = Convert.ToDecimal(rd["CAPITAL"]),
                                Interes = Convert.ToDecimal(rd["INTERES"]),
                                PagoNuevo = Convert.ToDecimal(rd["PAGO_NUEVO"]),
                                PagoRealizado = Convert.ToDecimal(rd["PAGO_REALIZADO"]),
                                Fecha = (DateTime?)rd["FECHA"],
                                ClienteId = Convert.ToInt32(rd["CLIENTE_ID"])
                            });
                        }
                    }
                }

                return StatusCode(StatusCodes.Status200OK, facturasLista.ToArray());
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
            }
        }

        [HttpGet]
        [Route("Lista_Historial")]
        public ActionResult<Factura[]> ListaHistorial()
        {
            List<Historialfactura> historialCliente = new List<Historialfactura>();

            try
            {
                using (var conexion = new MySqlConnection(cadenaSql))
                {
                    conexion.Open();
                    var cmd = new MySqlCommand("sp_lista_historialFactura", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            historialCliente.Add(new Historialfactura()
                            {
                                IdHistorialfactura = Convert.ToInt32(rd["ID_HISTORIALFACTURA"]),
                                ClienteId = Convert.ToInt32(rd["CLIENTE_ID"]),
                                Nombre = rd["NOMBRE"].ToString(),
                                Apellido = rd["APELLIDO"].ToString(),
                                Cedula = rd["CEDULA"].ToString(),
                                Telefono = rd["TELEFONO"].ToString(),
                                FacturaId = Convert.ToInt32(rd["FACTURA_ID"]),
                                MontoSolicitado = Convert.ToDecimal(rd["MONTO_SOLICITADO"]),
                                Tasa = Convert.ToDecimal(rd["TASA"]),
                                Cuotas = Convert.ToDecimal(rd["CUOTAS"]),
                                CuotasMensuales = Convert.ToDecimal(rd["CUOTAS_MENSUALES"]),
                                Capital = Convert.ToDecimal(rd["CAPITAL"]),
                                Interes = Convert.ToDecimal(rd["INTERES"]),
                                PagoNuevo = Convert.ToDecimal(rd["PAGO_NUEVO"]),
                                PagoRealizado = Convert.ToDecimal(rd["PAGO_REALIZADO"]),
                                Fecha = (DateTime?)rd["FECHA"],
                            });
                        }
                    }
                }

                return StatusCode(StatusCodes.Status200OK, historialCliente.ToArray());
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
            }
        }

        /*Detalles*/
        [HttpGet]
        [Route("ObtenerCliente/{cedula}")]
        public IActionResult ObtenerCliente(string cedula)
        {
            List<Cliente> lista = new List<Cliente>();
            List<Cliente> detalle = new List<Cliente>();
            try
            {
                using (var conexion = new MySqlConnection(cadenaSql))
                {
                    conexion.Open();
                    var cmd = new MySqlCommand("sp_lista_clientes", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new Cliente()
                            {
                                IdCliente = Convert.ToInt32(rd["ID_CLIENTE"]),
                                Nombre = rd["NOMBRE"].ToString(),
                                Apellido = rd["APELLIDO"].ToString(),
                                Cedula = rd["CEDULA"].ToString(),
                                Telefono = rd["TELEFONO"].ToString()
                            });
                        }
                    }
                }
                detalle = lista.Where(item => item.Cedula == cedula).ToList();

                return StatusCode(StatusCodes.Status200OK, detalle.ToArray());
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = detalle });
            }
        }

        [HttpGet]
        [Route("ObtenerFactura/{idfactura:int}")]
        public IActionResult ObtenerFactura(int idfactura)
        {
            List<Factura> listaFactura = new List<Factura>();
            List<Factura> factura = new List<Factura>();
            try
            {
                using (var conexion = new MySqlConnection(cadenaSql))
                {
                    conexion.Open();
                    var cmd = new MySqlCommand("sp_lista_facturas", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            listaFactura.Add(new Factura()
                            {
                                IdFactura = Convert.ToInt32(rd["ID_FACTURA"]),
                                MontoSolicitado = Convert.ToDecimal(rd["MONTO_SOLICITADO"]),
                                Tasa = Convert.ToDecimal(rd["TASA"]),
                                Cuotas = Convert.ToDecimal(rd["CUOTAS"]),
                                CuotasMensuales = Convert.ToDecimal(rd["CUOTAS_MENSUALES"]),
                                Capital = Convert.ToDecimal(rd["CAPITAL"]),
                                Interes = Convert.ToDecimal(rd["INTERES"]),
                                PagoNuevo = Convert.ToDecimal(rd["PAGO_NUEVO"]),
                                PagoRealizado = Convert.ToDecimal(rd["PAGO_REALIZADO"]),
                                Fecha = (DateTime?)rd["FECHA"],
                                ClienteId = Convert.ToInt32(rd["CLIENTE_ID"])
                            });
                        }
                    }
                }
                factura = listaFactura.Where(item => item.IdFactura == idfactura).ToList();

                return StatusCode(StatusCodes.Status200OK, factura.ToArray());
            }

            catch (Exception error)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = factura });
            }
        }

        [HttpGet]
        [Route("ObtenerClienteFactura/{cedula}")]
        public IActionResult ObtenerClienteFactura(string cedula)
        {
            List<ClienteFactura> lista = new List<ClienteFactura>();
            List<ClienteFactura> clienteFactura = new List<ClienteFactura>();

            try
            {
                using (var conexion = new MySqlConnection(cadenaSql))
                {
                    conexion.Open();
                    var cmd = new MySqlCommand("sp_lista_clientes_facturas", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new ClienteFactura()
                            {
                                IdCliente = Convert.ToInt32(rd["ID_CLIENTE"]),
                                Nombre = rd["NOMBRE"].ToString(),
                                Apellido = rd["APELLIDO"].ToString(),
                                Cedula = rd["CEDULA"].ToString(),
                                Telefono = rd["TELEFONO"].ToString(),
                                IdFactura = Convert.ToInt32(rd["ID_FACTURA"]),
                                MontoSolicitado = Convert.ToDecimal(rd["MONTO_SOLICITADO"]),
                                Tasa = Convert.ToDecimal(rd["TASA"]),
                                Cuotas = Convert.ToDecimal(rd["CUOTAS"]),
                                CuotasMensuales = Convert.ToDecimal(rd["CUOTAS_MENSUALES"]),
                                Capital = Convert.ToDecimal(rd["CAPITAL"]),
                                Interes = Convert.ToDecimal(rd["INTERES"]),
                                PagoNuevo = Convert.ToDecimal(rd["PAGO_NUEVO"]),
                                PagoRealizado = Convert.ToDecimal(rd["PAGO_REALIZADO"]),
                                Fecha = (DateTime?)rd["FECHA"]
                            });
                        }
                    }
                }
                clienteFactura = lista.Where(item => item.Cedula == cedula).ToList();

                return StatusCode(StatusCodes.Status200OK, clienteFactura.ToArray());
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = clienteFactura });
            }
        }

        [HttpGet]
        [Route("ObtenerHistorialCliente/{cedula}")]
        public IActionResult ObtenerHistorialCliente(string cedula)
        {
            List<Historialfactura> lista = new List<Historialfactura>();
            List<Historialfactura> clienteFactura = new List<Historialfactura>();

            try
            {
                using (var conexion = new MySqlConnection(cadenaSql))
                {
                    conexion.Open();
                    var cmd = new MySqlCommand("sp_lista_historialFactura", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new Historialfactura()
                            {
                                IdHistorialfactura = Convert.ToInt32(rd["ID_HISTORIALFACTURA"]),
                                ClienteId = Convert.ToInt32(rd["CLIENTE_ID"]),
                                Nombre = rd["NOMBRE"].ToString(),
                                Apellido = rd["APELLIDO"].ToString(),
                                Cedula = rd["CEDULA"].ToString(),
                                Telefono = rd["TELEFONO"].ToString(),
                                FacturaId = Convert.ToInt32(rd["FACTURA_ID"]),
                                MontoSolicitado = Convert.ToDecimal(rd["MONTO_SOLICITADO"]),
                                Tasa = Convert.ToDecimal(rd["TASA"]),
                                Cuotas = Convert.ToDecimal(rd["CUOTAS"]),
                                CuotasMensuales = Convert.ToDecimal(rd["CUOTAS_MENSUALES"]),
                                Capital = Convert.ToDecimal(rd["CAPITAL"]),
                                Interes = Convert.ToDecimal(rd["INTERES"]),
                                PagoNuevo = Convert.ToDecimal(rd["PAGO_NUEVO"]),
                                PagoRealizado = Convert.ToDecimal(rd["PAGO_REALIZADO"]),
                                Fecha = (DateTime?)rd["FECHA"]
                            });
                        }
                    }
                }
                clienteFactura = lista.Where(item => item.Cedula == cedula).ToList();

                return StatusCode(StatusCodes.Status200OK, clienteFactura.ToArray());
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = clienteFactura });
            }
        }

        /*Guardando*/
        [HttpPost]
        [Route("GuardarCliente")]
        public IActionResult GuardarCliente([FromBody] Cliente objeto)
        {
            try
            {
                using (var conexion = new MySqlConnection(cadenaSql))
                {
                    conexion.Open();
                    var cmd = new MySqlCommand("sp_guardar_cliente", conexion);
                    cmd.Parameters.AddWithValue("nombre", objeto.Nombre);
                    cmd.Parameters.AddWithValue("apellido", objeto.Apellido);
                    cmd.Parameters.AddWithValue("cedula", objeto.Cedula);
                    cmd.Parameters.AddWithValue("telefono", objeto.Telefono);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Datos Registrados" });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        [HttpPost]
        [Route("GuardarFactura")]
        public IActionResult GuardarFactura([FromBody] Factura objeto)
        {
            try
            {
                using (var conexion = new MySqlConnection(cadenaSql))
                {
                    conexion.Open();
                    var cmd = new MySqlCommand("sp_guardar_factura", conexion);
                    cmd.Parameters.AddWithValue("montosolicitado", objeto.MontoSolicitado);
                    cmd.Parameters.AddWithValue("tasa", objeto.Tasa);
                    cmd.Parameters.AddWithValue("cuotas", objeto.Cuotas);
                    cmd.Parameters.AddWithValue("cuotasmensuales", objeto.CuotasMensuales);
                    cmd.Parameters.AddWithValue("capital", objeto.Capital);
                    cmd.Parameters.AddWithValue("interes", objeto.Interes);
                    cmd.Parameters.AddWithValue("pagonuevo", objeto.PagoNuevo);
                    cmd.Parameters.AddWithValue("pagorealizado", objeto.PagoRealizado);
                    cmd.Parameters.AddWithValue("fecha", objeto.Fecha);
                    cmd.Parameters.AddWithValue("clienteid", objeto.ClienteId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Datos Registrados" });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        [HttpPost]
        [Route("GuardarHistorial")]
        public IActionResult GuardarHistorial([FromBody] Historialfactura objeto)
        {
            try
            {
                using (var conexion = new MySqlConnection(cadenaSql))
                {
                    conexion.Open();
                    var cmd = new MySqlCommand("sp_guardar_historialfactura", conexion);
                    cmd.Parameters.AddWithValue("clienteid", objeto.ClienteId);
                    cmd.Parameters.AddWithValue("nombre", objeto.Nombre);
                    cmd.Parameters.AddWithValue("apellido", objeto.Apellido);
                    cmd.Parameters.AddWithValue("cedula", objeto.Cedula);
                    cmd.Parameters.AddWithValue("telefono", objeto.Telefono);
                    cmd.Parameters.AddWithValue("facturaid", objeto.FacturaId);
                    cmd.Parameters.AddWithValue("montosolicitado", objeto.MontoSolicitado);
                    cmd.Parameters.AddWithValue("tasa", objeto.Tasa);
                    cmd.Parameters.AddWithValue("cuotas", objeto.Cuotas);
                    cmd.Parameters.AddWithValue("cuotasmensuales", objeto.CuotasMensuales);
                    cmd.Parameters.AddWithValue("capital", objeto.Capital);
                    cmd.Parameters.AddWithValue("interes", objeto.Interes);
                    cmd.Parameters.AddWithValue("pagonuevo", objeto.PagoNuevo);
                    cmd.Parameters.AddWithValue("pagorealizado", objeto.PagoRealizado);
                    cmd.Parameters.AddWithValue("fecha", objeto.Fecha);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Datos Registrados" });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
        //Editar
        [HttpPut]
        [Route("EditarCliente")]
        public IActionResult Editar([FromBody] Cliente objeto)
        {
            try
            {
                using (var conexion = new MySqlConnection(cadenaSql))
                {
                    conexion.Open();
                    var cmd = new MySqlCommand("sp_editar_cliente", conexion);
                    cmd.Parameters.AddWithValue("idcliente", objeto.IdCliente == 0 ? DBNull.Value : objeto.IdCliente);
                    cmd.Parameters.AddWithValue("nombre", objeto.Nombre is null ? DBNull.Value : objeto.Nombre);
                    cmd.Parameters.AddWithValue("apellido", objeto.Apellido is null ? DBNull.Value : objeto.Apellido);
                    cmd.Parameters.AddWithValue("cedula", objeto.Cedula is null ? DBNull.Value : objeto.Cedula);
                    cmd.Parameters.AddWithValue("telefono", objeto.Telefono is null ? DBNull.Value : objeto.Telefono);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Datos Editados" });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        [HttpPut]
        [Route("EditarFactura")]
        public IActionResult EditarFactura([FromBody] Factura objeto)
        {
            try
            {
                using (var conexion = new MySqlConnection(cadenaSql))
                {
                    conexion.Open();
                    var cmd = new MySqlCommand("sp_editar_factura", conexion);
                    cmd.Parameters.AddWithValue("idfactura", objeto.IdFactura == 0 ? DBNull.Value : objeto.IdFactura);
                    cmd.Parameters.AddWithValue("montosolicitado", objeto.MontoSolicitado is null ? DBNull.Value : objeto.MontoSolicitado);
                    cmd.Parameters.AddWithValue("tasa", objeto.Tasa is null ? DBNull.Value : objeto.Tasa);
                    cmd.Parameters.AddWithValue("cuotas", objeto.Cuotas is null ? DBNull.Value : objeto.Cuotas);
                    cmd.Parameters.AddWithValue("cuotasmensuales", objeto.CuotasMensuales is null ? DBNull.Value : objeto.CuotasMensuales);
                    cmd.Parameters.AddWithValue("capital", objeto.Capital is null ? DBNull.Value : objeto.Capital);
                    cmd.Parameters.AddWithValue("interes", objeto.Interes is null ? DBNull.Value : objeto.Interes);
                    cmd.Parameters.AddWithValue("pagonuevo", objeto.PagoNuevo is null ? DBNull.Value : objeto.PagoNuevo);
                    cmd.Parameters.AddWithValue("pagorealizado", objeto.PagoRealizado is null ? DBNull.Value : objeto.PagoRealizado);
                    cmd.Parameters.AddWithValue("fecha", objeto.Fecha is null ? DBNull.Value : objeto.Fecha);
                    cmd.Parameters.AddWithValue("clienteid", objeto.ClienteId is null ? DBNull.Value : objeto.ClienteId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Datos Editados" });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        /*Eliminar*/
        [HttpDelete]
        [Route("Eliminar Cliente/{idCliente:int}")]
        public IActionResult Eliminar (int idCliente)
        {
            try
            {
                using (var conexion = new MySqlConnection(cadenaSql))
                {
                    conexion.Open();
                    var cmd = new MySqlCommand("sp_eliminar_clientes", conexion);
                    cmd.Parameters.AddWithValue("idCliente", idCliente);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Cliente eliminado" });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
    }
}

